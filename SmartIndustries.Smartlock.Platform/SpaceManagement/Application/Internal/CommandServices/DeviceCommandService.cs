using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Resources.Errors;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Application.Internal.CommandServices;

public class DeviceCommandService(
    IDeviceRepository deviceRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer) : IDeviceCommandService
{
    public async Task<Result<Device>> Handle(ConnectDeviceToSiteCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var device = new Device(command.SiteId, command.Name, command.Mode);
            await deviceRepository.AddAsync(device, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Device>.Success(device);
        }
        catch (ArgumentException exception)
        {
            return Result<Device>.Failure(SpaceManagementError.InvalidData, exception.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<Device>.Failure(SpaceManagementError.OperationCancelled, localizer["SpaceManagementError.OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<Device>.Failure(SpaceManagementError.DatabaseError, localizer["SpaceManagementError.DatabaseError"]);
        }
        catch (Exception)
        {
            return Result<Device>.Failure(SpaceManagementError.InternalServerError, localizer["SpaceManagementError.InternalServerError"]);
        }
    }

    public async Task<Result<Device>> Handle(UpdateDeviceInformationCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var device = await deviceRepository.FindByIdAsync(command.DeviceId, cancellationToken);
            if (device == null)
                return Result<Device>.Failure(SpaceManagementError.DeviceNotFound,
                    localizer["SpaceManagementError.DeviceNotFound"]);

            device.UpdateInformation(command.SiteId, command.Name, command.Mode);
            deviceRepository.Update(device);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Device>.Success(device);
        }
        catch (ArgumentException exception)
        {
            return Result<Device>.Failure(SpaceManagementError.InvalidData, exception.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<Device>.Failure(SpaceManagementError.OperationCancelled, localizer["SpaceManagementError.OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<Device>.Failure(SpaceManagementError.DatabaseError, localizer["SpaceManagementError.DatabaseError"]);
        }
        catch (Exception)
        {
            return Result<Device>.Failure(SpaceManagementError.InternalServerError, localizer["SpaceManagementError.InternalServerError"]);
        }
    }

    public async Task<Result<Device>> Handle(DeleteDeviceCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var device = await deviceRepository.FindByIdAsync(command.DeviceId, cancellationToken);
            if (device == null)
                return Result<Device>.Failure(SpaceManagementError.DeviceNotFound,
                    localizer["SpaceManagementError.DeviceNotFound"]);

            deviceRepository.Remove(device);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Device>.Success(device);
        }
        catch (OperationCanceledException)
        {
            return Result<Device>.Failure(SpaceManagementError.OperationCancelled, localizer["SpaceManagementError.OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<Device>.Failure(SpaceManagementError.DatabaseError, localizer["SpaceManagementError.DatabaseError"]);
        }
        catch (Exception)
        {
            return Result<Device>.Failure(SpaceManagementError.InternalServerError, localizer["SpaceManagementError.InternalServerError"]);
        }
    }
}

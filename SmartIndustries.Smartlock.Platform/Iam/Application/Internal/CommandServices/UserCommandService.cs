using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartIndustries.Smartlock.Platform.Iam.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.Iam.Application.Internal.OutboundServices;
using SmartIndustries.Smartlock.Platform.Iam.Domain.Model;
using SmartIndustries.Smartlock.Platform.Iam.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Iam.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Iam.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Resources.Errors;

namespace SmartIndustries.Smartlock.Platform.Iam.Application.Internal.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer) : IUserCommandService
{
    public async Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsByEmailAsync(command.Email, cancellationToken))
            return Result.Failure(IamError.EmailAlreadyTaken,
                localizer["IamError.EmailAlreadyTaken", command.Email]);

        try
        {
            var password = new Password(command.Password);
            var hashedPassword = hashingService.HashPassword(password.Value);
            var user = new User(command.FirstName, command.LastName, hashedPassword, command.Email);
            await userRepository.AddAsync(user, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (ArgumentException ex)
        {
            return Result.Failure(IamError.InvalidData, ex.Message);
        }
        catch (OperationCanceledException)
        {
            return Result.Failure(IamError.OperationCancelled, localizer["IamError.OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result.Failure(IamError.DatabaseError, localizer["IamError.DatabaseError"]);
        }
        catch (Exception)
        {
            return Result.Failure(IamError.InternalServerError, localizer["IamError.InternalServerError"]);
        }
    }
}
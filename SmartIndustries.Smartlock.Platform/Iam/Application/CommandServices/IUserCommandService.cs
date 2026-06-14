using SmartIndustries.Smartlock.Platform.Iam.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Iam.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;

namespace SmartIndustries.Smartlock.Platform.Iam.Application.CommandServices;

public interface IUserCommandService
{
    Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken);
    
    Task<Result<(User user, string token)>> Handle(SignInCommand command, CancellationToken cancellationToken);
}

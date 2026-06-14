using SmartIndustries.Smartlock.Platform.Iam.Application.QueryServices;
using SmartIndustries.Smartlock.Platform.Iam.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Iam.Domain.Model.queries;
using SmartIndustries.Smartlock.Platform.Iam.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.Iam.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        return await userRepository.FindByIdAsync(query.Id, cancellationToken);
    }
}
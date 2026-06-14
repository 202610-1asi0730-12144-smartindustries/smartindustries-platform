using SmartIndustries.Smartlock.Platform.Iam.Domain.Model.Aggregates;

namespace SmartIndustries.Smartlock.Platform.Iam.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
    
    Task<long?> ValidateToken(string token);
}

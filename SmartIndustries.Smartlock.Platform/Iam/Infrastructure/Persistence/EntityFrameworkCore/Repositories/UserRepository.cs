using Microsoft.EntityFrameworkCore;
using SmartIndustries.Smartlock.Platform.Iam.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Iam.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Configuration;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Repositories;

namespace SmartIndustries.Smartlock.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> FindByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(u => u.Email.Value == email.Value, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Context.Set<User>().AnyAsync(u => u.Email.Value == email, cancellationToken);
    }
}

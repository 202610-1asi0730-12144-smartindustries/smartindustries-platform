using Microsoft.EntityFrameworkCore;
using SmartIndustries.Smartlock.Platform.Access.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using SmartIndustries.Smartlock.Platform.Administration.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using SmartIndustries.Smartlock.Platform.Billing.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using SmartIndustries.Smartlock.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using SmartIndustries.Smartlock.Platform.Report.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Configuration.Extensions;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Interceptors;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

namespace SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Configuration;

/// <summary>
///     Application database context for the Learning Center Platform
/// </summary>
/// <param name="options">
///     The options for the database context
/// </param>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Apply audit timestamp interceptor for all IAuditableEntity implementations
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    /// <summary>
    ///     On creating the database model
    /// </summary>
    /// <remarks>
    ///     This method is used to create the database model for the application.
    /// </remarks>
    /// <param name="builder">
    ///     The model builder for the database context
    /// </param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Access Context
        builder.ApplyAccessConfiguration();

        // Administration Context
        builder.ApplyAdministrationConfiguration();

        // Billing Context
        builder.ApplyBillingConfiguration();

        // IAM Context
        builder.ApplyIamConfiguration();
        
        // Report Context
        builder.ApplyReportConfiguration();
        
        // Space Management Context
        builder.ApplySpaceManagementConfiguration();
        
        // General Naming Convention for the database objects
        builder.UseSnakeCaseNamingConvention();
    }
}

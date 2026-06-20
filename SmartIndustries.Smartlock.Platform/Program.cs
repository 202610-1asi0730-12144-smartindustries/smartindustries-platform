
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi;
using SmartIndustries.Smartlock.Platform.Iam.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.Iam.Application.Acl;
using SmartIndustries.Smartlock.Platform.Iam.Application.Internal.CommandServices;
using SmartIndustries.Smartlock.Platform.Iam.Application.Internal.OutboundServices;
using SmartIndustries.Smartlock.Platform.Iam.Application.Internal.QueryServices;
using SmartIndustries.Smartlock.Platform.Iam.Application.QueryServices;
using SmartIndustries.Smartlock.Platform.Iam.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Iam.Infrastructure.Hashing.BCrypt.Services;
using SmartIndustries.Smartlock.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SmartIndustries.Smartlock.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;
using SmartIndustries.Smartlock.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;
using SmartIndustries.Smartlock.Platform.Iam.Infrastructure.Tokens.Jwt.Services;
using SmartIndustries.Smartlock.Platform.Iam.Interfaces.Acl;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Application.Internal.CommandServices;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Application.Internal.QueryServices;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Application.QueryServices;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SmartIndustries.Smartlock.Platform.Access.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.Access.Application.Internal.CommandServices;
using SmartIndustries.Smartlock.Platform.Access.Application.Internal.QueryServices;
using SmartIndustries.Smartlock.Platform.Access.Application.QueryServices;
using SmartIndustries.Smartlock.Platform.Access.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Access.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Administration.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SmartIndustries.Smartlock.Platform.Administration.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.Administration.Application.Internal.CommandServices;
using SmartIndustries.Smartlock.Platform.Administration.Application.Internal.QueryServices;
using SmartIndustries.Smartlock.Platform.Administration.Application.QueryServices;
using SmartIndustries.Smartlock.Platform.Report.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.Report.Application.Internal.CommandServices;
using SmartIndustries.Smartlock.Platform.Report.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Report.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Mediator.Cortex.Configuration;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Configuration;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;
using SmartIndustries.Smartlock.Platform.Shared.Resources;
using SmartIndustries.Smartlock.Platform.Shared.Resources.Errors;
using ProblemDetailsFactory = SmartIndustries.Smartlock.Platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

// Add ProblemDetails services
builder.Services.AddProblemDetails();

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add Database Connection

// Configure Database Context and route EF logs through the app logger pipeline.
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionStringTemplate))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Explicitly register IStringLocalizer for ErrorMessages and Commons
builder.Services.AddSingleton<IStringLocalizer<ErrorMessages>, StringLocalizer<ErrorMessages>>();
builder.Services
    .AddSingleton<IStringLocalizer<CommonMessages>,
        StringLocalizer<CommonMessages>>(); // Corrected from Common to Commons

// Register the custom ProblemDetailsFactory
builder.Services.AddSingleton<ProblemDetailsFactory>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "SmartIndustries.Smartlock.Platform",
            Version = "v1",
            Description = "SmartLock Platform API",
        });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
        { [new OpenApiSecuritySchemeReference("bearer", document)] = [] });
    options.EnableAnnotations();
});

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add Mediator Injection Configuration
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));

// Add Cortex Mediator for Event Handling
builder.Services.AddCortexMediator(
    [typeof(Program)]);

// IAM Bounded Context
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();

// IAM ACL
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

// SpaceManagement Bounded Context
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IOrganizationCommandService, OrganizationCommandService>();
builder.Services.AddScoped<ISiteRepository, SiteRepository>();
builder.Services.AddScoped<ISiteCommandService, SiteCommandService>();
builder.Services.AddScoped<ISiteQueryService, SiteQueryService>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceCommandService, DeviceCommandService>();
builder.Services.AddScoped<IDeviceQueryService, DeviceQueryService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonCommandService, PersonCommandService>();
builder.Services.AddScoped<IPeopleQueryService, PeopleQueryService>();

// Access Bounded Context
builder.Services.AddScoped<IAccessGroupRepository, AccessGroupRepository>();
builder.Services.AddScoped<IAccessGroupCommandService, AccessGroupCommandService>();
builder.Services.AddScoped<IAccessGroupQueryService, AccessGroupQueryService>();
builder.Services.AddScoped<IPersonAccessRepository, PersonAccessRepository>();
builder.Services.AddScoped<IPersonAccessCommandService, PersonAccessCommandService>();

// Report Bounded Context
builder.Services.AddScoped<IScheduleDayRepository, ScheduleDayRepository>();
builder.Services.AddScoped<IScheduleDayCommandService, ScheduleDayCommandService>();

// Administration Bounded Context
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleCommandService, RoleCommandService>();
builder.Services.AddScoped<IRoleQueryService, RoleQueryService>();
builder.Services.AddScoped<IMembershipRepository, MembershipRepository>();
builder.Services.AddScoped<IMembershipCommandService, MembershipCommandService>();
builder.Services.AddScoped<IMembershipQueryService, MembershipQueryService>();


var app = builder.Build();


// Apply pending migrations on startup (safe to call even when schema is up to date)
// dotnet ef migrations add <name>
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}


// Configure the HTTP request pipeline.
app.UseGlobalExceptionHandler();

var supportedCultures = new[] { "en", "es" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply CORS Policy
app.UseCors("AllowAllPolicy");

// Add Authorization Middleware to Pipeline
app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

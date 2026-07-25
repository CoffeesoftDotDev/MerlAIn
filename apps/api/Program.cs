using MerlAIn.Api.Hubs;
using MerlAIn.Api.Modules;
using MerlAIn.Shared.Identity;
using MerlAIn.Shared.Modules;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Feature modules are discovered once and reused for registration, mapping and the inventory
// endpoint, so the host never keeps a hard-coded list of features.
IReadOnlyList<IFeatureModule> modules = FeatureModuleDiscovery.Discover(typeof(Program).Assembly);
NoOpMcpToolRegistry mcpTools = new();

builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

// Identity is configuration-driven: local accounts always, an external OIDC provider only when
// one is pointed at. MerlAIn hosts no identity provider of its own.
IConfigurationSection authSection = builder.Configuration.GetSection(AuthOptions.SectionName);
builder.Services.Configure<AuthOptions>(authSection);
AuthOptions authOptions = authSection.Get<AuthOptions>() ?? new AuthOptions();

// SignalR gets a Redis backplane in phase 2, which is what allows the API to be replicated.
builder.Services.AddSignalR();

foreach (IFeatureModule module in modules)
{
    module.RegisterServices(builder.Services, builder.Configuration);
    module.RegisterMcpTools(mcpTools);
}

WebApplication app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Liveness probe for compose and, later, for the reverse proxy.
app.MapGet("/health", () => Results.Ok(new { status = "healthy" }))
    .WithName("Health")
    .ExcludeFromDescription();

// Public sign-in capabilities. Served at runtime rather than baked into the frontend bundle, so a
// single web image works against a deployment with SSO and one without. Deliberately exposes no
// secret: the client secret and the signing key never leave the API.
app.MapGet("/api/auth/config", () => Results.Ok(new
{
    LocalAccounts = new
    {
        authOptions.LocalAccounts.Enabled,
        authOptions.LocalAccounts.AllowRegistration,
        authOptions.LocalAccounts.MinimumPasswordLength,
    },
    Oidc = authOptions.Oidc.IsEnabled
        ? new
        {
            Enabled = true,
            authOptions.Oidc.DisplayName,
            authOptions.Oidc.Authority,
            authOptions.Oidc.ClientId,
            authOptions.Oidc.Scopes,
        }
        : null,
}))
    .WithName("GetAuthConfig")
    .WithSummary("Describes which sign-in methods this deployment offers.");

// Inventory of what is compiled into this host. Handy when debugging a module that "did not load".
app.MapGet("/api/modules", () => Results.Ok(modules.Select(module => new
{
    module.Id,
    module.ApiBasePath,
})))
    .WithName("ListModules")
    .WithSummary("Lists the feature modules loaded by this host.");

foreach (IFeatureModule module in modules)
{
    module.MapEndpoints(app);
}

app.MapHub<AgentHub>(AgentHub.Route);

app.Logger.LogInformation(
    "MerlAIn API started with {ModuleCount} feature module(s): {Modules}. " +
    "Local accounts: {LocalAccounts}. External OIDC: {Oidc}.",
    modules.Count,
    string.Join(", ", modules.Select(module => module.Id)),
    authOptions.LocalAccounts.Enabled ? "enabled" : "disabled",
    authOptions.Oidc.IsEnabled ? authOptions.Oidc.Authority : "not configured");

app.Run();

/// <summary>Entry point marker, also used as the assembly anchor for module discovery.</summary>
public partial class Program;

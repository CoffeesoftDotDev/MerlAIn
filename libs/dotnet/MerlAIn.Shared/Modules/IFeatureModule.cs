using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MerlAIn.Shared.Modules;

/// <summary>
/// Contract implemented by every MerlAIn backend feature module.
/// </summary>
/// <remarks>
/// <para>
/// MerlAIn is a <em>modular monolith</em>: modules are class libraries (or folders) compiled
/// into the single <c>api</c> process and discovered at startup. This interface is the only
/// coupling point between the host and a feature, which is what keeps a later extraction into
/// a separate service cheap.
/// </para>
/// <para>
/// Implementations must be public, non-abstract and expose a parameterless constructor:
/// discovery instantiates them before the DI container exists.
/// </para>
/// <example>
/// <code>
/// public sealed class CampaignsModule : IFeatureModule
/// {
///     public string Id => "campaigns";
///     public string ApiBasePath => "/api/campaigns";
/// }
/// </code>
/// </example>
/// </remarks>
public interface IFeatureModule
{
    /// <summary>
    /// Stable identifier, matching the <c>id</c> of the frontend manifest so the two halves of
    /// the feature can be correlated in logs and in the module inventory endpoint.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Route prefix owned by the module, for example <c>/api/campaigns</c>. Nothing outside the
    /// module may map endpoints under this prefix.
    /// </summary>
    string ApiBasePath { get; }

    /// <summary>
    /// Registers the module's own services. Called once at startup, before the container is built.
    /// </summary>
    /// <param name="services">The application service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    void RegisterServices(IServiceCollection services, IConfiguration configuration);

    /// <summary>
    /// Maps the module's HTTP endpoints. Every route must live under <see cref="ApiBasePath"/>.
    /// </summary>
    /// <param name="endpoints">The endpoint route builder of the host.</param>
    void MapEndpoints(IEndpointRouteBuilder endpoints);

    /// <summary>
    /// Registers the MCP tools the module exposes to the agents. Implement as a no-op when the
    /// module has nothing for the agents to call.
    /// </summary>
    /// <param name="tools">The MCP tool registry of the host.</param>
    void RegisterMcpTools(IMcpToolRegistry tools);
}

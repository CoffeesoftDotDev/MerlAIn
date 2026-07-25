using MerlAIn.Shared.Modules;

namespace MerlAIn.Api.Modules.Bestiary;

/// <summary>
/// Monsters and NPC statblocks. Backend half of the <c>bestiary</c> frontend module.
/// </summary>
/// <remarks>
/// Second module on purpose: it proves discovery is not hard-coded to a single feature.
/// </remarks>
public sealed class BestiaryModule : IFeatureModule
{
    /// <inheritdoc />
    public string Id => "bestiary";

    /// <inheritdoc />
    public string ApiBasePath => "/api/bestiary";

    /// <inheritdoc />
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        // Nothing to register yet.
    }

    /// <inheritdoc />
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGroup(ApiBasePath)
            .WithTags("Bestiary")
            .MapGet("/creatures", () => Results.Ok(SampleCreatures))
            .WithName("ListCreatures")
            .WithSummary("Lists the creatures available to the caller.");
    }

    /// <inheritdoc />
    public void RegisterMcpTools(IMcpToolRegistry tools)
    {
        // Phase 5: tools.AddTools<BestiaryTools>() so CharaDesigner can reuse existing statblocks.
    }

    /// <summary>Static stand-in for the future database.</summary>
    private static readonly Creature[] SampleCreatures =
    [
        new("bog-lurker", "Bog Lurker", 3, "Aberration"),
        new("ashen-warden", "Ashen Warden", 7, "Construct"),
    ];

    /// <summary>A creature as shown in the bestiary list.</summary>
    /// <param name="Id">Stable slug identifying the creature.</param>
    /// <param name="Name">Display name.</param>
    /// <param name="ChallengeRating">Challenge rating.</param>
    /// <param name="Kind">Creature type.</param>
    private sealed record Creature(string Id, string Name, int ChallengeRating, string Kind);
}

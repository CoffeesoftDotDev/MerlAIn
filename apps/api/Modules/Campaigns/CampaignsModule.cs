using MerlAIn.Shared.Modules;

namespace MerlAIn.Api.Modules.Campaigns;

/// <summary>
/// Campaigns and scenarios. Backend half of the <c>campaigns</c> frontend module.
/// </summary>
/// <remarks>
/// Scaffold only: the endpoints return a fixed sample so the shape of a module is visible end to
/// end. Persistence, authorisation and MCP tools arrive with their respective phases.
/// </remarks>
public sealed class CampaignsModule : IFeatureModule
{
    /// <inheritdoc />
    public string Id => "campaigns";

    /// <inheritdoc />
    public string ApiBasePath => "/api/campaigns";

    /// <inheritdoc />
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        // Nothing to register yet. Repositories and the campaigns DbContext land in phase 3.
    }

    /// <inheritdoc />
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder group = endpoints
            .MapGroup(ApiBasePath)
            .WithTags("Campaigns");

        group.MapGet("/", () => Results.Ok(SampleCampaigns))
            .WithName("ListCampaigns")
            .WithSummary("Lists the campaigns visible to the caller.");

        group.MapGet("/{id}", (string id) =>
            SampleCampaigns.FirstOrDefault(campaign => campaign.Id == id) is { } match
                ? Results.Ok(match)
                : Results.Problem(statusCode: StatusCodes.Status404NotFound, title: "Campaign not found"))
            .WithName("GetCampaign")
            .WithSummary("Fetches a single campaign.");
    }

    /// <inheritdoc />
    public void RegisterMcpTools(IMcpToolRegistry tools)
    {
        // Phase 5: tools.AddTools<CampaignTools>() so the Scenarist agent can read the campaign bible.
    }

    /// <summary>Static stand-in for the future database.</summary>
    private static readonly CampaignSummary[] SampleCampaigns =
    [
        new("curse-of-the-amber-crown", "Curse of the Amber Crown", 4, 12),
        new("tides-of-the-broken-sea", "Tides of the Broken Sea", 2, 5),
    ];

    /// <summary>A campaign as shown in lists and dashboard widgets.</summary>
    /// <param name="Id">Stable slug identifying the campaign.</param>
    /// <param name="Title">Display title.</param>
    /// <param name="ScenarioCount">Number of scenarios written so far.</param>
    /// <param name="CharacterCount">Number of characters, players and NPCs combined.</param>
    private sealed record CampaignSummary(string Id, string Title, int ScenarioCount, int CharacterCount);
}

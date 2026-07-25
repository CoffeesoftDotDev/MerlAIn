using Microsoft.AspNetCore.SignalR;

namespace MerlAIn.Api.Hubs;

/// <summary>
/// Relays agent progress to the browser.
/// </summary>
/// <remarks>
/// <para>
/// The <c>agents</c> worker publishes progress on a Redis stream; the API consumes it and fans it
/// out through this hub. Clients therefore only ever talk to the API, and the worker stays free of
/// any connection state.
/// </para>
/// <para>
/// Runs are grouped by identifier so several browser tabs can follow the same run, and so a
/// replicated API behind Traefik still delivers every message via the Redis backplane.
/// </para>
/// </remarks>
public sealed class AgentHub : Hub
{
    /// <summary>Name the hub is mapped under.</summary>
    public const string Route = "/hubs/agents";

    /// <summary>
    /// Subscribes the calling connection to the progress of a run.
    /// </summary>
    /// <param name="runId">Identifier of the agent run to follow.</param>
    public Task SubscribeToRun(string runId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(runId);

        return Groups.AddToGroupAsync(Context.ConnectionId, GroupFor(runId), Context.ConnectionAborted);
    }

    /// <summary>
    /// Unsubscribes the calling connection from a run.
    /// </summary>
    /// <param name="runId">Identifier of the agent run to stop following.</param>
    public Task UnsubscribeFromRun(string runId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(runId);

        return Groups.RemoveFromGroupAsync(Context.ConnectionId, GroupFor(runId), Context.ConnectionAborted);
    }

    /// <summary>Builds the SignalR group name for a run.</summary>
    /// <param name="runId">Identifier of the agent run.</param>
    public static string GroupFor(string runId) => $"run:{runId}";
}

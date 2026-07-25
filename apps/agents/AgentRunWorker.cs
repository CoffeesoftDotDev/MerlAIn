using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MerlAIn.Agents;

/// <summary>
/// Consumes agent run requests and executes the corresponding workflow.
/// </summary>
/// <remarks>
/// <para>
/// The worker is deliberately separate from the API: a scenario draft can take minutes and must
/// not hold a request thread or a SignalR connection. The API enqueues a run on a Redis stream,
/// this worker picks it up, runs the Microsoft Agent Framework workflow, calls back into the
/// domain through the API's MCP tools, and publishes progress on another stream that the API
/// relays to the browser.
/// </para>
/// <para>
/// Scaffold only: the loop currently logs a heartbeat so the container has an observable
/// lifecycle. The Redis consumer group and the workflow arrive with phases 4 and 5.
/// </para>
/// </remarks>
/// <param name="logger">Logger for the worker.</param>
public sealed class AgentRunWorker(ILogger<AgentRunWorker> logger) : BackgroundService
{
    /// <summary>How often the placeholder heartbeat is written.</summary>
    private static readonly TimeSpan HeartbeatInterval = TimeSpan.FromSeconds(30);

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Agent worker started. Waiting for runs.");

        using PeriodicTimer timer = new(HeartbeatInterval);

        try
        {
            // PeriodicTimer keeps the loop allocation-free and stops cleanly on shutdown, unlike
            // Task.Delay which throws on cancellation.
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                logger.LogDebug("Agent worker idle. No run queue is wired up yet.");
            }
        }
        catch (OperationCanceledException)
        {
            // Expected on shutdown: swallow so the host can stop without logging a fault.
        }

        logger.LogInformation("Agent worker stopped.");
    }
}

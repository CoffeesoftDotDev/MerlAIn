using MerlAIn.Shared.Modules;

namespace MerlAIn.Api.Modules;

/// <summary>
/// Placeholder MCP registry used until the <c>ModelContextProtocol</c> server is wired up.
/// </summary>
/// <remarks>
/// It records what modules ask for so the module inventory endpoint can already show which
/// features intend to expose tools to the agents.
/// </remarks>
internal sealed class NoOpMcpToolRegistry : IMcpToolRegistry
{
    private readonly List<string> registeredTools = [];

    /// <summary>The tool container type names collected so far.</summary>
    public IReadOnlyList<string> RegisteredTools => registeredTools;

    /// <inheritdoc />
    public void AddTools<TTools>() where TTools : class =>
        registeredTools.Add(typeof(TTools).FullName ?? typeof(TTools).Name);
}

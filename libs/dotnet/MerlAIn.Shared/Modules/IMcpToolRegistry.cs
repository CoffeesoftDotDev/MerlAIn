namespace MerlAIn.Shared.Modules;

/// <summary>
/// Collects the MCP tool classes exposed by feature modules.
/// </summary>
/// <remarks>
/// Abstracting the registry keeps <see cref="IFeatureModule"/> free of a direct dependency on the
/// <c>ModelContextProtocol</c> SDK. The concrete implementation, added with the MCP phase, forwards
/// to <c>IMcpServerBuilder.WithTools&lt;T&gt;()</c>.
/// </remarks>
public interface IMcpToolRegistry
{
    /// <summary>
    /// Registers a class whose <c>[McpServerTool]</c>-annotated methods become agent-callable tools.
    /// </summary>
    /// <typeparam name="TTools">The tool container type.</typeparam>
    void AddTools<TTools>() where TTools : class;
}

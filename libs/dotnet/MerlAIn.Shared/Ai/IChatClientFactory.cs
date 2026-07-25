namespace MerlAIn.Shared.Ai;

/// <summary>
/// Creates chat sessions against the configured inference endpoint.
/// </summary>
/// <remarks>
/// The provider is chosen by <c>LLM_PROVIDER</c>: <c>ollama</c> targets the bundled container,
/// <c>openai-compatible</c> targets any external endpoint speaking the OpenAI wire format. The
/// factory hides that choice so agents never branch on the provider.
/// </remarks>
public interface IChatClientFactory
{
    /// <summary>The resolved provider settings, useful for diagnostics and the health endpoint.</summary>
    LlmOptions Options { get; }

    /// <summary>
    /// Opens a streaming chat session seeded with a system prompt.
    /// </summary>
    /// <param name="systemPrompt">The persona and instructions for the session.</param>
    /// <returns>A session the caller must dispose.</returns>
    IChatSession CreateSession(string systemPrompt);
}

/// <summary>
/// A single streaming conversation with the model.
/// </summary>
public interface IChatSession : IAsyncDisposable
{
    /// <summary>
    /// Sends a message and streams the answer back token by token, so the SignalR hub can relay
    /// progress to the browser while the model is still writing.
    /// </summary>
    /// <param name="message">The user or agent message.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<string> StreamAsync(string message, CancellationToken cancellationToken = default);
}

/// <summary>Inference settings bound from the <c>Llm</c> configuration section.</summary>
/// <param name="Provider">Either <c>ollama</c> or <c>openai-compatible</c>.</param>
/// <param name="Endpoint">Base address of the inference endpoint.</param>
/// <param name="Model">Default model name.</param>
/// <param name="ApiKey">Bearer token, only used by OpenAI-compatible endpoints.</param>
public sealed record LlmOptions(string Provider, Uri Endpoint, string Model, string? ApiKey);

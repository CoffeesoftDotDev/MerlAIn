namespace MerlAIn.Shared.Ai;

/// <summary>
/// Text-to-image generation for equipment cards, portraits and maps.
/// </summary>
/// <remarks>
/// Image generation is optional. When <c>IMAGE_ENDPOINT</c> is empty, <see cref="IsConfigured"/>
/// is <see langword="false"/> and the Artist agent removes itself from the workflow instead of
/// failing the run.
/// </remarks>
public interface IImageGenerator
{
    /// <summary>Whether an image endpoint is configured.</summary>
    bool IsConfigured { get; }

    /// <summary>
    /// Generates an image and stores it, returning the storage key rather than the bytes so large
    /// payloads never cross the agent boundary.
    /// </summary>
    /// <param name="request">The generation parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <exception cref="InvalidOperationException">Thrown when <see cref="IsConfigured"/> is false.</exception>
    Task<ImageResult> GenerateAsync(ImageRequest request, CancellationToken cancellationToken = default);
}

/// <summary>Parameters for a single image generation.</summary>
/// <param name="Prompt">What to draw.</param>
/// <param name="Width">Output width in pixels.</param>
/// <param name="Height">Output height in pixels.</param>
/// <param name="NegativePrompt">What to avoid, when the backend supports it.</param>
public sealed record ImageRequest(string Prompt, int Width = 1024, int Height = 1024, string? NegativePrompt = null);

/// <summary>The stored result of a generation.</summary>
/// <param name="AssetKey">Object storage key of the generated image.</param>
/// <param name="ContentType">MIME type of the stored image.</param>
public sealed record ImageResult(string AssetKey, string ContentType);

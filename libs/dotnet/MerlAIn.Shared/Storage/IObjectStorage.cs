namespace MerlAIn.Shared.Storage;

/// <summary>
/// Object storage abstraction for campaign assets: maps, portraits, equipment card artwork.
/// </summary>
/// <remarks>
/// Backed by any S3-compatible endpoint. MinIO ships in the <c>storage</c> compose profile, but
/// pointing <c>S3_ENDPOINT</c> at an existing bucket is equally supported — that seam is why this
/// interface exists rather than an SDK type leaking into the modules.
/// </remarks>
public interface IObjectStorage
{
    /// <summary>Uploads (or overwrites) an object.</summary>
    /// <param name="key">Storage key, conventionally <c>{scope}/{entityId}/{fileName}</c>.</param>
    /// <param name="content">The content stream. The caller keeps ownership.</param>
    /// <param name="contentType">MIME type stored alongside the object.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task UploadAsync(string key, Stream content, string contentType, CancellationToken cancellationToken = default);

    /// <summary>Opens an object for reading.</summary>
    /// <param name="key">Storage key.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A readable stream owned by the caller.</returns>
    Task<Stream> OpenReadAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Builds a time-limited URL the browser can use directly, so large assets never transit
    /// through the API process.
    /// </summary>
    /// <param name="key">Storage key.</param>
    /// <param name="lifetime">How long the URL stays valid.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<Uri> GetPresignedUrlAsync(string key, TimeSpan lifetime, CancellationToken cancellationToken = default);

    /// <summary>Deletes an object. Succeeds when the object is already absent.</summary>
    /// <param name="key">Storage key.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task DeleteAsync(string key, CancellationToken cancellationToken = default);
}

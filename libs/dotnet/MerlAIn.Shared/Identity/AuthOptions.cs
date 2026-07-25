namespace MerlAIn.Shared.Identity;

/// <summary>
/// Identity settings, bound from the <c>Auth</c> configuration section.
/// </summary>
/// <remarks>
/// MerlAIn does not host an identity provider. It always issues its own session tokens and
/// supports two ways of proving who you are, which may run side by side:
/// local email and password accounts, and an optional external OIDC provider you already run.
/// </remarks>
public sealed class AuthOptions
{
    /// <summary>Configuration section this class binds to.</summary>
    public const string SectionName = "Auth";

    /// <summary>Settings of the session tokens MerlAIn issues itself.</summary>
    public JwtOptions Jwt { get; init; } = new();

    /// <summary>Settings of the built-in email and password accounts.</summary>
    public LocalAccountOptions LocalAccounts { get; init; } = new();

    /// <summary>Settings of the optional external OIDC provider.</summary>
    public OidcOptions Oidc { get; init; } = new();
}

/// <summary>Signing settings for MerlAIn's own session tokens.</summary>
public sealed class JwtOptions
{
    /// <summary>Symmetric signing key. Must be at least 32 bytes.</summary>
    public string SigningKey { get; init; } = string.Empty;

    /// <summary>Issuer stamped on, and required of, MerlAIn session tokens.</summary>
    public string Issuer { get; init; } = "https://merlain.local";

    /// <summary>How long an access token stays valid.</summary>
    public TimeSpan AccessTokenLifetime { get; init; } = TimeSpan.FromMinutes(30);

    /// <summary>How long a refresh token stays valid.</summary>
    public TimeSpan RefreshTokenLifetime { get; init; } = TimeSpan.FromDays(14);
}

/// <summary>Settings of the built-in email and password accounts.</summary>
public sealed class LocalAccountOptions
{
    /// <summary>
    /// Whether email and password sign-in is offered. Turn it off once every Dungeon Master has
    /// moved to the external provider.
    /// </summary>
    public bool Enabled { get; init; } = true;

    /// <summary>Whether visitors may create an account themselves.</summary>
    public bool AllowRegistration { get; init; } = true;

    /// <summary>Minimum password length enforced at registration and on change.</summary>
    public int MinimumPasswordLength { get; init; } = 12;
}

/// <summary>Settings of the optional external OIDC provider.</summary>
public sealed class OidcOptions
{
    /// <summary>
    /// Base URL of the provider, for example <c>https://sso.example.com/application/o/merlain/</c>.
    /// Empty means no external provider: the sign-in page shows local accounts only.
    /// </summary>
    public string Authority { get; init; } = string.Empty;

    /// <summary>
    /// Discovery document URL. Only needed when the browser and the API reach the provider on
    /// different URLs; otherwise it is derived from <see cref="Authority"/>.
    /// </summary>
    public string MetadataAddress { get; init; } = string.Empty;

    /// <summary>Client identifier registered with the provider.</summary>
    public string ClientId { get; init; } = string.Empty;

    /// <summary>Client secret. Leave empty for a public client using PKCE.</summary>
    public string ClientSecret { get; init; } = string.Empty;

    /// <summary>Expected <c>aud</c> claim. Many providers put the client id here.</summary>
    public string Audience { get; init; } = string.Empty;

    /// <summary>Space-separated scopes requested at authorisation time.</summary>
    public string Scopes { get; init; } = "openid profile email";

    /// <summary>Label of the sign-in button, for example "Continue with Authentik".</summary>
    public string DisplayName { get; init; } = "Single sign-on";

    /// <summary>
    /// Whether an external login attaches to an existing local account when the provider reports
    /// the same, verified, email address. Set to <see langword="false"/> when the provider's email
    /// verification cannot be trusted, since a forged claim would otherwise take over an account.
    /// </summary>
    public bool LinkByVerifiedEmail { get; init; } = true;

    /// <summary>
    /// Whether an external provider is configured. Presence of <see cref="Authority"/> is the
    /// single switch, mirroring how the optional image endpoint is handled.
    /// </summary>
    public bool IsEnabled => !string.IsNullOrWhiteSpace(Authority);

    /// <summary>Resolves the discovery document URL.</summary>
    /// <returns>The configured metadata address, or one derived from the authority.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no provider is configured.</exception>
    public string ResolveMetadataAddress()
    {
        if (!IsEnabled)
        {
            throw new InvalidOperationException("No external OIDC provider is configured.");
        }

        return string.IsNullOrWhiteSpace(MetadataAddress)
            ? $"{Authority.TrimEnd('/')}/.well-known/openid-configuration"
            : MetadataAddress;
    }
}

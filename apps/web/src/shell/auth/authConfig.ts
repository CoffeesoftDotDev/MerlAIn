/**
 * Sign-in capabilities of the deployment, served by `GET /api/auth/config`.
 *
 * Read at runtime rather than from build-time environment variables, so the same web image can be
 * deployed against an installation with single sign-on and one without.
 */
export interface AuthConfig {
  localAccounts: {
    enabled: boolean;
    allowRegistration: boolean;
    minimumPasswordLength: number;
  };
  /** `null` when no external provider is configured. */
  oidc: {
    enabled: boolean;
    displayName: string;
    authority: string;
    clientId: string;
    scopes: string;
  } | null;
}

/** Used when the API cannot be reached, so the sign-in page still renders something usable. */
export const fallbackAuthConfig: AuthConfig = {
  localAccounts: { enabled: true, allowRegistration: false, minimumPasswordLength: 12 },
  oidc: null,
};

/**
 * Fetches the sign-in capabilities.
 *
 * @param signal - Abort signal, so a navigation away cancels the request.
 */
export async function fetchAuthConfig(signal?: AbortSignal): Promise<AuthConfig> {
  const response = await fetch('/api/auth/config', {
    headers: { Accept: 'application/json' },
    signal,
  });

  if (!response.ok) {
    throw new Error(`Auth configuration unavailable (HTTP ${response.status})`);
  }

  return (await response.json()) as AuthConfig;
}

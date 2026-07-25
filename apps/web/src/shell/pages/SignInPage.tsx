import { useEffect, useState } from 'react';
import { type AuthConfig, fallbackAuthConfig, fetchAuthConfig } from '../auth/authConfig';

type LoadState =
  | { status: 'loading' }
  | { status: 'ready'; config: AuthConfig; degraded: boolean };

/**
 * Sign-in page.
 *
 * The form it shows is decided by the API, not by the bundle: local accounts when they are enabled,
 * and a single sign-on button only when an external OIDC provider is configured. Phase 2 wires the
 * actual credential exchange; this page currently demonstrates the conditional rendering.
 */
export default function SignInPage() {
  const [state, setState] = useState<LoadState>({ status: 'loading' });

  useEffect(() => {
    const controller = new AbortController();

    fetchAuthConfig(controller.signal)
      .then((config) => setState({ status: 'ready', config, degraded: false }))
      .catch((error: unknown) => {
        if (error instanceof DOMException && error.name === 'AbortError') {
          return;
        }
        // The API being down must not leave the user staring at a spinner.
        setState({ status: 'ready', config: fallbackAuthConfig, degraded: true });
      });

    return () => controller.abort();
  }, []);

  if (state.status === 'loading') {
    return (
      <main className="signin">
        <p className="placeholder" role="status">
          Consulting the grimoire…
        </p>
      </main>
    );
  }

  const { config, degraded } = state;

  return (
    <main className="signin">
      <h1 className="page__title">Sign in to MerlAIn</h1>
      <p className="page__subtitle">Your Dungeon Master assistant awaits.</p>

      {degraded && (
        <p className="signin__notice" role="alert">
          The API is unreachable, so only local sign-in is offered.
        </p>
      )}

      {config.oidc && (
        <>
          <button type="button" className="button button--sso">
            Continue with {config.oidc.displayName}
          </button>

          {config.localAccounts.enabled && (
            <p className="signin__separator">
              <span>or</span>
            </p>
          )}
        </>
      )}

      {config.localAccounts.enabled ? (
        <form className="signin__form">
          <div className="field">
            <label className="field__label" htmlFor="signin-email">
              Email address
            </label>
            <input
              id="signin-email"
              className="field__input"
              type="email"
              name="email"
              autoComplete="username"
              required
            />
          </div>

          <div className="field">
            <label className="field__label" htmlFor="signin-password">
              Password
            </label>
            <input
              id="signin-password"
              className="field__input"
              type="password"
              name="password"
              autoComplete="current-password"
              aria-describedby="signin-password-hint"
              required
            />
            <p id="signin-password-hint" className="field__hint">
              At least {config.localAccounts.minimumPasswordLength} characters.
            </p>
          </div>

          <button type="submit" className="button">
            Sign in
          </button>

          {config.localAccounts.allowRegistration && (
            <p className="signin__footnote">
              No account yet? Registration opens with the identity phase.
            </p>
          )}
        </form>
      ) : (
        <p className="placeholder">
          Local accounts are disabled on this deployment. Use the button above.
        </p>
      )}
    </main>
  );
}

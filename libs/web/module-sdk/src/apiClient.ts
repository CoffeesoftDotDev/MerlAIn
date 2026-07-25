export interface ModuleApiClient {
  get<T>(path?: string, init?: RequestInit): Promise<T>;
  post<T>(path: string, body: unknown, init?: RequestInit): Promise<T>;
}

/** Resolves the bearer token for outgoing calls. Wired to the session store in phase 2. */
export type TokenProvider = () => string | undefined | Promise<string | undefined>;

function joinPath(base: string, path: string): string {
  if (!path) return base;
  return `${base.replace(/\/+$/, '')}/${path.replace(/^\/+/, '')}`;
}

/**
 * Creates an API client bound to a module's `apiBasePath`.
 *
 * Each module builds its own client, so it can only reach its own backend module -
 * the frontend mirror of the backend boundary rules.
 */
export function createModuleApiClient(
  apiBasePath: string,
  getToken?: TokenProvider,
): ModuleApiClient {
  async function request<T>(path: string, init: RequestInit): Promise<T> {
    const headers = new Headers(init.headers);
    headers.set('Accept', 'application/json');

    const token = await getToken?.();
    if (token) {
      headers.set('Authorization', `Bearer ${token}`);
    }

    const response = await fetch(joinPath(apiBasePath, path), { ...init, headers });

    if (!response.ok) {
      throw new Error(`${init.method ?? 'GET'} ${apiBasePath}${path} failed: ${response.status}`);
    }

    return (await response.json()) as T;
  }

  return {
    get: (path = '', init = {}) => request(path, { ...init, method: 'GET' }),
    post: (path, body, init = {}) => {
      const headers = new Headers(init.headers);
      headers.set('Content-Type', 'application/json');
      return request(path, { ...init, method: 'POST', headers, body: JSON.stringify(body) });
    },
  };
}

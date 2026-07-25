import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import { fileURLToPath, URL } from 'node:url';

// On the host the API is published on localhost; inside the dev container it is a
// sibling on the `merlain` network, so the dev container overrides this.
const apiTarget = process.env.VITE_DEV_API_TARGET ?? 'http://localhost:8080';

export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      // The module SDK is consumed straight from source so the shell and the
      // module contract stay in lockstep during development.
      '@merlain/module-sdk': fileURLToPath(
        new URL('../../libs/web/module-sdk/src/index.ts', import.meta.url),
      ),
    },
  },
  server: {
    port: 5173,
    // Bind on all interfaces so the port is reachable when running in a container.
    host: true,
    proxy: {
      // Only used once the backend is running; harmless otherwise.
      '/api': { target: apiTarget, changeOrigin: true },
      '/hubs': { target: apiTarget, changeOrigin: true, ws: true },
    },
  },
});

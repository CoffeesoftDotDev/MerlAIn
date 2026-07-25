import { Route } from 'react-router-dom';
import type { ModuleRegistry } from '@merlain/module-sdk';

/**
 * Turns every registered module's manifest into lazily loaded React Router routes,
 * mounted under the module's `basePath`.
 */
export function buildModuleRoutes(registry: ModuleRegistry) {
  return registry.modules.map((module) => (
    <Route key={module.id} path={module.basePath}>
      {module.routes.map((route) => {
        const Element = route.element;

        return route.index ? (
          <Route key={`${module.id}:index`} index element={<Element />} />
        ) : (
          <Route key={`${module.id}:${route.path}`} path={route.path} element={<Element />} />
        );
      })}
    </Route>
  ));
}

import type { ModuleManifest, ModuleRegistry, ModuleWidget, NavItem } from './types';

const DEFAULT_NAV_ORDER = 1000;

function assertUnique(modules: readonly ModuleManifest[]): void {
  const seenIds = new Set<string>();
  const seenPaths = new Set<string>();

  for (const module of modules) {
    if (seenIds.has(module.id)) {
      throw new Error(`Duplicate module id "${module.id}".`);
    }
    if (seenPaths.has(module.basePath)) {
      throw new Error(`Duplicate module basePath "${module.basePath}" (module "${module.id}").`);
    }
    seenIds.add(module.id);
    seenPaths.add(module.basePath);
  }
}

/**
 * Builds the shell's view over the registered modules.
 *
 * Everything the shell renders - navigation, routes, dashboard widgets - is derived
 * from the manifests, so adding a module never requires touching shell code.
 */
export function createModuleRegistry(modules: readonly ModuleManifest[]): ModuleRegistry {
  assertUnique(modules);

  const sorted = [...modules].sort(
    (a, b) => (a.navOrder ?? DEFAULT_NAV_ORDER) - (b.navOrder ?? DEFAULT_NAV_ORDER),
  );

  const navItems: NavItem[] = sorted
    .filter((module) => !module.hidden)
    .map((module) => ({
      id: module.id,
      title: module.title,
      icon: module.icon,
      path: module.basePath,
    }));

  const widgets: (ModuleWidget & { moduleId: string })[] = sorted.flatMap((module) =>
    (module.widgets ?? []).map((widget) => ({ ...widget, moduleId: module.id })),
  );

  const byId = new Map(sorted.map((module) => [module.id, module]));

  return {
    modules: sorted,
    navItems,
    widgets,
    getById: (id) => byId.get(id),
  };
}

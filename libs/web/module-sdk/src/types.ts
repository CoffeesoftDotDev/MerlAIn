import type { ComponentType, LazyExoticComponent } from 'react';

/** A component loaded on demand, so a module's code is only fetched when used. */
export type LazyComponent = LazyExoticComponent<ComponentType<Record<string, never>>>;

/** A route contributed by a module, mounted under the module's `basePath`. */
export interface ModuleRoute {
  /** Path relative to the module's `basePath`. Use `''` together with `index: true`. */
  path: string;
  /** Renders this route as the module's landing page. */
  index?: boolean;
  /** Label used for in-module sub-navigation and breadcrumbs. */
  title?: string;
  element: LazyComponent;
}

/** A card a module contributes to the dashboard. */
export interface ModuleWidget {
  /** Globally unique, conventionally `<moduleId>.<widgetName>`. */
  id: string;
  title: string;
  component: LazyComponent;
  size?: 'sm' | 'md' | 'lg';
}

/**
 * The contract every MerlAIn frontend module exports.
 *
 * A module is self-contained: its manifest, pages, widgets and API client live in a
 * single folder. Registering it requires no change to shell code beyond one line in
 * the module registry.
 */
export interface ModuleManifest {
  /** Stable identifier. Matches the backend `IFeatureModule.Id`. */
  id: string;
  title: string;
  /** Emoji or icon name shown in the navigation. */
  icon: string;
  /** Frontend route prefix, e.g. `/campaigns`. */
  basePath: string;
  /** Backend route prefix, e.g. `/api/campaigns`. Matches `IFeatureModule.ApiBasePath`. */
  apiBasePath: string;
  /** Short description shown on the dashboard. */
  description?: string;
  /** Permissions required to see this module. Enforced once auth is wired (phase 2). */
  permissions?: string[];
  /** Lower sorts first in the navigation. Defaults to 1000. */
  navOrder?: number;
  /** Hides the module from the navigation while keeping its routes reachable. */
  hidden?: boolean;
  routes: ModuleRoute[];
  widgets?: ModuleWidget[];
}

/** A navigation entry derived from a manifest. */
export interface NavItem {
  id: string;
  title: string;
  icon: string;
  path: string;
}

/** The shell's view over all registered modules. */
export interface ModuleRegistry {
  modules: readonly ModuleManifest[];
  navItems: readonly NavItem[];
  widgets: readonly (ModuleWidget & { moduleId: string })[];
  getById(id: string): ModuleManifest | undefined;
}

/**
 * Identity helper giving a module manifest full type inference and checking.
 *
 * @example
 * export default defineModule({ id: 'campaigns', ... });
 */
export function defineModule(manifest: ModuleManifest): ModuleManifest {
  return manifest;
}

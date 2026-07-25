import { createModuleRegistry } from '@merlain/module-sdk';

import campaigns from '../modules/campaigns';
import bestiary from '../modules/bestiary';

/**
 * The single place where modules are registered.
 *
 * Adding a feature means adding its manifest here - navigation, routes and dashboard
 * widgets are all derived from it. No other shell file needs to change.
 */
export const registry = createModuleRegistry([campaigns, bestiary]);

import { lazy } from 'react';
import { defineModule } from '@merlain/module-sdk';

/**
 * Bestiary module - a second sample proving the shell is fully manifest-driven:
 * nothing about this feature is referenced anywhere outside this folder and the
 * one-line entry in the module registry.
 */
export default defineModule({
  id: 'bestiary',
  title: 'Bestiary',
  icon: '🐉',
  basePath: '/bestiary',
  apiBasePath: '/api/bestiary',
  description: 'Monsters, NPCs and quest tokens.',
  permissions: ['characters:read'],
  navOrder: 20,
  routes: [
    { path: '', index: true, title: 'Creatures', element: lazy(() => import('./pages/BestiaryPage')) },
  ],
  widgets: [
    {
      id: 'bestiary.recent',
      title: 'Recently summoned',
      component: lazy(() => import('./widgets/RecentCreaturesWidget')),
      size: 'sm',
    },
  ],
});

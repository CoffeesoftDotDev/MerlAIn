import { lazy } from 'react';
import { defineModule } from '@merlain/module-sdk';

/**
 * Campaigns module.
 *
 * Everything this feature needs lives in this folder: the manifest, its pages, its
 * dashboard widgets and its API client. The backend counterpart is
 * `apps/api/Modules/Campaigns/CampaignsModule.cs`, which serves the same `apiBasePath`.
 */
export default defineModule({
  id: 'campaigns',
  title: 'Campaigns',
  icon: '📜',
  basePath: '/campaigns',
  apiBasePath: '/api/campaigns',
  description: 'Campaigns and the scenarios they contain.',
  permissions: ['campaigns:read'],
  navOrder: 10,
  routes: [
    { path: '', index: true, title: 'All campaigns', element: lazy(() => import('./pages/CampaignsPage')) },
    { path: 'scenarios', title: 'Scenarios', element: lazy(() => import('./pages/ScenariosPage')) },
  ],
  widgets: [
    {
      id: 'campaigns.summary',
      title: 'Active campaigns',
      component: lazy(() => import('./widgets/CampaignSummaryWidget')),
      size: 'md',
    },
  ],
});

import { createModuleApiClient } from '@merlain/module-sdk';
import manifest from '../index';

export interface CampaignSummary {
  id: string;
  name: string;
  scenarioCount: number;
}

/** Bound to this module's `apiBasePath`, so it can only reach its own backend module. */
const api = createModuleApiClient(manifest.apiBasePath);

export function listCampaigns(): Promise<CampaignSummary[]> {
  return api.get<CampaignSummary[]>();
}

import { createModuleApiClient } from '@merlain/module-sdk';
import manifest from '../index';

export interface CreatureSummary {
  id: string;
  name: string;
  challengeRating: string;
}

const api = createModuleApiClient(manifest.apiBasePath);

export function listCreatures(): Promise<CreatureSummary[]> {
  return api.get<CreatureSummary[]>();
}

# Adding a MerlAIn module (historical)

> **Archived, not a V1 module contract.** Preserved from
> [the former module README at commit 5025421](https://github.com/CoffeesoftDotDev/MerlAIn/blob/5025421320d06162bcde319da9f9b32d1a0e1224/apps/web/src/modules/README.md).
> The referenced shell, SDK, API and build files have been removed from this branch.
> The examples and modular-monolith rules below describe the old scaffold, not an
> approved design for the polyglot Dapr target. Establish the new contracts through
> [issue #4](https://github.com/CoffeesoftDotDev/MerlAIn/issues/4) before implementation.
> See the [implementation readiness guide](../implementation-readiness.md).

A module is **one feature, front to back**: UI, API client, backend endpoints and MCP tools live
in the same folder and are removable in one delete.

## 1. Frontend

```
apps/web/src/modules/<your-module>/
├── index.ts              # the manifest — the only file the shell reads
├── api/client.ts         # typed calls, scoped to the module's apiBasePath
├── pages/                # route targets, lazily loaded
└── widgets/              # optional dashboard cards
```

`index.ts` exports a manifest built with `defineModule`:

```ts
import { lazy } from 'react';
import { defineModule } from '@merlain/module-sdk';

export default defineModule({
  id: 'quests',
  title: 'Quests',
  icon: '🗺️',
  basePath: '/quests',
  apiBasePath: '/api/quests',
  permissions: ['quests:read'],
  navOrder: 30,
  routes: [
    { index: true, path: '', title: 'Quests', element: lazy(() => import('./pages/QuestsPage')) },
  ],
  widgets: [
    { id: 'quests.open', title: 'Open quests', component: lazy(() => import('./widgets/OpenQuestsWidget')) },
  ],
});
```

Register it in [the historical module registry](https://github.com/CoffeesoftDotDev/MerlAIn/blob/5025421320d06162bcde319da9f9b32d1a0e1224/apps/web/src/shell/moduleRegistry.ts):

```ts
import quests from '../modules/quests';

export const registry = createModuleRegistry([campaigns, bestiary, quests]);
```

That is the **only** shell file you touch. Navigation, routing and dashboard placement are derived
from the manifest. `createModuleRegistry` throws at startup if two modules share an `id` or a
`basePath`.

Pages and widgets must be **default exports** — they are loaded with `React.lazy`, which is what
gives each module its own JavaScript chunk.

## 2. Backend

```
apps/api/Modules/<YourModule>/
└── <YourModule>Module.cs   # implements IFeatureModule
```

```csharp
public sealed class QuestsModule : IFeatureModule
{
    public string Id => "quests";                 // matches the frontend manifest id
    public string ApiBasePath => "/api/quests";   // matches apiBasePath

    public void RegisterServices(IServiceCollection services, IConfiguration configuration) { }

    public void MapEndpoints(IEndpointRouteBuilder endpoints) =>
        endpoints.MapGroup(ApiBasePath).MapGet("/", () => Results.Ok(/* … */));

    public void RegisterMcpTools(IMcpToolRegistry tools) => tools.AddTools<QuestTools>();
}
```

No registration step: `FeatureModuleDiscovery` scans the assembly at startup. The class must be
public, non-abstract and have a parameterless constructor. Check `GET /api/modules` to confirm it
was picked up.

## 3. Boundary rules

MerlAIn is a modular monolith. These rules are what keep extracting a module into its own service
cheap, and they are not optional:

1. Each module owns a PostgreSQL schema. No cross-module foreign keys, no cross-module joins.
2. Reference another module's data **by id**, through its interface or MCP tool. Never read its tables.
3. No shared mutable in-process state between modules.
4. Cross-module notifications go over Redis streams, not direct calls.
5. Cross-module synchronous calls go through an interface, so it can become an HTTP or MCP call later.

## 4. Checklist

- [ ] `id` identical on both halves
- [ ] `apiBasePath` identical on both halves, and every route mapped under it
- [ ] Pages and widgets are default exports
- [ ] Manifest registered in `moduleRegistry.ts`
- [ ] `npm run build` passes and produces a chunk per page
- [ ] Module appears in `GET /api/modules`

# Starting MerlAIn V1 from the specifications baseline

This guide separates work the team can start now from decisions and evidence needed
before implementation or activation. It does not approve policies, choose a new stack,
or replace the [core PRD](../.copilot-tracking/squad/members/produit/prd/2026-09-28/merlain-prd-v0.1.md)
and [session-memory addendum](../.copilot-tracking/squad/members/produit/prd/2026-09-28/merlain-session-memory-prd.md).

## What changed in this branch

At the user's request, the branch now contains specifications, documentation and
Copilot/squad material, plus Git metadata and `.gitignore` for repository hygiene.
The cleanup removed 64 implementation/build/runtime files: the React/Vite frontend,
ASP.NET API and worker, shared libraries, SQL and proxy configuration, Dockerfiles,
Compose and devcontainer files, environment example, solution/build files, npm
manifests and lockfile. No application code or replacement runtime was introduced.

Both existing READMEs were retained as historical documents:
[scaffold overview](history/scaffold-readme.md) and
[module authoring](history/module-authoring.md). The root README now describes the
specifications-only state. Existing PRDs, lifecycle companions, squad records,
research, plans, reviews and Copilot instruction files are unchanged.

**Historical evidence is not current architecture.** References to `apps/`, `libs/`,
`infra/`, the old Compose file or the old README in retained research and squad history
describe the pre-cleanup repository at
[commit 5025421](https://github.com/CoffeesoftDotDev/MerlAIn/tree/5025421320d06162bcde319da9f9b32d1a0e1224).
Consult that snapshot rather than recreating the old modular monolith by default.
Its documentation included aspirations beyond the implemented scaffold.

This is a source-tree cleanup only. It does not stop running services, delete Docker
volumes or databases, remove data outside the worktree, or erase Git history.

## Agree the first slice

Recommended milestone: a GM authenticates, creates a campaign, stores a private idea
and publishes a scene. A player sees only authorized material. Data survives restart.
The workflow functions with AI disabled.

This is not a reduction of V1: reusable characters and sheets, branching chronicles,
imports, AI, memory and exports remain in the PRDs and require subsequent delivery work.
The existing nine issues cover a first workflow and two investigations, not the entire V1.

## Work order and ownership

Issue status at this cleanup: all nine issues below were open and unassigned. Role
names are suggested responsibilities, not accepted assignments or delivery dates.

| Work | Can start when | Expected handoff |
| --- | --- | --- |
| [#3 Policies](https://github.com/CoffeesoftDotDev/MerlAIn/issues/3) - product with technical review | Now | Accepted identity, membership and publication policies for the slice; named approvers |
| [#4 Contracts and ownership](https://github.com/CoffeesoftDotDev/MerlAIn/issues/4) - technical lead | Discovery now; freeze after #3 | Service/API/event contracts, Dapr interactions, module dependencies, storage boundaries, directory ownership and acceptance-test mapping |
| [#5 Local foundations](https://github.com/CoffeesoftDotDev/MerlAIn/issues/5) - platform | #3 and #4 resolved; clean baseline shared | New Compose/Dapr foundation, configuration, persistent storage, disabled-AI mode and agreed development/verification commands |
| [#6 Identity](https://github.com/CoffeesoftDotDev/MerlAIn/issues/6) - identity owner | Contracts and foundations available | Local/OIDC access and campaign membership under the agreed policies |
| [#7 Campaign backend](https://github.com/CoffeesoftDotDev/MerlAIn/issues/7) - domain owner | Contracts and foundations available | Persistent campaigns, private notes and controlled scene publication |
| [#8 GM/player UI](https://github.com/CoffeesoftDotDev/MerlAIn/issues/8) - frontend owner | Contracts and foundations available | Accessible manual workflow using agreed contracts |
| [#9 Integration](https://github.com/CoffeesoftDotDev/MerlAIn/issues/9) - integration/quality owner | #5 through #8 delivered | Evidence for the complete workflow, isolation, restart persistence and AI-disabled behavior |
| [#10 Memory contracts](https://github.com/CoffeesoftDotDev/MerlAIn/issues/10) - component owners | Exact repositories and revisions supplied | Verified Whisper, Graphiti, Storyteller and orchestrator interfaces, licenses and failure behavior |
| [#11 Export capabilities](https://github.com/CoffeesoftDotDev/MerlAIn/issues/11) - integration research owner | Documentation research now | Target/version/format evidence and controlled import checks before claiming native compatibility |

After the shared foundation, #6, #7 and #8 can run in parallel in exclusive directories.
Use contract-compatible mocks where needed; end-to-end authentication and publication
cannot be declared done from mocks alone. Assign shared contract/configuration changes
to one owner and coordinate revisions before dependent work proceeds.

### Baseline and tracker reconciliation

The specification PR [#2](https://github.com/CoffeesoftDotDev/MerlAIn/pull/2) was already
merged into `feat/scaffolding`. The repository default branch is `main`; it must not
be assumed to contain that specification baseline. This cleanup needs its own reviewed
change before the team starts from a common clean commit. It is not an update to
the already merged PR.

The existing issue bodies were written against the earlier scaffold and still refer
to its paths. **No tracker writes were made during this cleanup.** Before implementation,
reconcile #4 and dependent issue ownership/path references with the new baseline.
Old paths are historical or candidate future locations, not files to extend today.
Keep one agreed integration base and do not let one team rebuild the scaffold while
another is still removing it.

## Decisions still needed before coding

The core PRD section 11 is the decision register. Its R01-R12 entries are
recommendations, not approvals; completing or merging a document does not adopt them.

| Gate | Decision or evidence needed | Scope |
| --- | --- | --- |
| Product policy (#3; E01/E02) | First-admin bootstrap, account recovery and linking, campaign admission/roles, ownership transfer and publication behavior | Before coding the affected identity and audience rules |
| Architecture/contracts (#4; E10) | Service boundaries and languages, UI stack, versioned contracts, Dapr transports, module enablement/dependencies, storage and ownership | Before teams implement independently; justify Builder/Strategy/Command only where useful |
| Quality agreement (#4/#5/#9) | Map the slice to FR/AC/NFR identifiers; define authorized fixtures, denied-access cases, accessibility checks, restart checks and reproducible build/test commands | Before treating the first integrated workflow as complete |
| Team and integration | Assign the open issues, approvers and shared-file owners; settle the integration base after cleanup | Before parallel implementation |

Do not silently turn the old README's account-linking behavior into a requirement.
The PRD's local Compose deployment, Dapr/polyglot direction, local/OIDC access and
GM/player scope are already established; the clean slate is not permission to reopen them.

### Reconcile retained authoring guidance

The instruction files were preserved as requested, but they are not a coherent stack
decision. In particular:

- [Next.js guidance](../.github/instructions/nextjs.instructions.md) applies broadly to
  TypeScript/JavaScript/CSS, although the removed UI was Vite. Select the V1 UI stack
  in #4, then scope the framework guidance accordingly.
- [C# guidance](../.github/instructions/csharp.instructions.md) targets modern C# while
  [legacy .NET Framework guidance](../.github/instructions/dotnet-framework.instructions.md)
  also matches all C#/project files and requires C# 7.3 and MSBuild. Resolve that overlap
  for the selected services rather than letting competing instructions choose tooling.
- Hosting examples in general guidance do not override the local-only application
  deployment constraint. Optional remote dependencies remain permitted.

Treat this as a contract-readiness concern in #4, not evidence that a particular
language, frontend framework or authentication library has already been selected.

## Work that need not block the manual slice

Memory integration needs repository URLs/revisions, real interfaces and an agreed
episode/session/chronicle mapping (E03/R12). The Graphiti knowledge graph is distinct
from the narrative branch graph. A reported component is not an integration proof.

Native FoundryVTT/Roll20 compatibility needs versions, systems/sheets, supported content
types and import evidence (E04). Downloadable exports remain the product boundary;
direct transfer and synchronization are not implied. The generic fallback R08 still
needs approval.

Before activating the relevant later capabilities, settle source rights and authorized
fixtures (E05), audio consent and retention (E06), measured limits (E07), backup/restore
and disablement policy (E08), and provider/OCR contracts (E09). Image APIs are independent
of the OpenAI-compatible text interface; an Anthropic integration is not in scope.
These are targeted readiness gates, not reasons to delay all manual workflow work.

Once the first slice's contracts are settled, turn the remaining V1 requirements into
additional dependency-aware work packages. Do not label completion of #3 through #11
as completion of the full PRDs.

## Ready to implement means

The team shares the cleaned baseline, has accepted the slice's policies, has frozen
contracts and file ownership, and knows which acceptance checks demonstrate success.
Document unresolved capability-specific decisions explicitly instead of inventing
approvals, compatibility, performance targets or delivery dates.

This guide is AI-assisted readiness guidance, not product approval, an implementation
result or a legal/security certification.

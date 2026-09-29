# MerlAIn

MerlAIn is a planned, administrator-composable assistant for tabletop RPG game masters
and players.

**This branch is specifications-only.** The previous application scaffold, runtime
infrastructure and build tooling have been removed. There is no runnable application,
Docker Compose stack or package installation step here yet.

## Start here

| Document | Purpose |
| --- | --- |
| [Core PRD](.copilot-tracking/squad/members/produit/prd/2026-09-28/merlain-prd-v0.1.md) | Campaigns, characters, permissions, imports, optional AI, exports and acceptance criteria |
| [Session-memory PRD](.copilot-tracking/squad/members/produit/prd/2026-09-28/merlain-session-memory-prd.md) | Audio transcription, Graphiti/GraphRAG, Storyteller summaries and orchestration |
| [Implementation readiness](docs/implementation-readiness.md) | Remaining decisions, team issue dependencies and the new baseline |
| [Squad federation](.copilot-tracking/squad/federation.md) | Product and platform squad responsibilities |

The PRDs are reviewed drafts, **not human-approved delivery commitments**. The core
PRD's sections 11 and 14 distinguish confirmed requirements, proposed policies and
missing evidence. Merging documentation does not approve its recommendations.

## Product direction

- An administrator selects the enabled capabilities. Frontend and server composition
  target polyglot services communicating through Dapr, deployed exclusively through
  local Docker Compose.
- Configured external dependencies are permitted, including remote Authentik/OIDC,
  data services and AI endpoints. Local application deployment does not mean that
  every dependency or data transfer stays on the same machine.
- GMs and players use local username-or-email/password accounts or OIDC, with
  campaign-scoped permissions and audience-aware content.
- Campaigns support narrative direction, private GM ideas, a shared journal, and
  branching/reconverging chronicles representing scenes or events.
- A reusable character library covers custom NPCs, official-character references and
  potential player characters, including characters not yet placed in a story.
  Campaign incarnations retain their source link without sharing mutable state.
- D&D 5e 2014/2024 and The One Ring 2e use edition-adapted sheets, not a promised full
  rules engine. Text and scanned-PDF imports require human review and respect source rights.
- Optional text AI uses OpenAI-compatible endpoints; image generation has its own
  provider contracts. No Anthropic-specific integration is planned at this stage.
  The manual core must work without AI; player-facing AI publication needs GM approval.
- Generated content is exportable through downloadable files/packages and configurable
  target mappings. FoundryVTT and Roll20 are intended targets, not yet verified native
  integrations. Direct transfer and bidirectional synchronization are out of scope.

Builder, Strategy and Command patterns are to be used where relevant and justified,
not imposed on every component. The PRDs remain authoritative over this summary.

## First implementation milestone

Deliver one complete manual workflow: a GM signs in, creates a campaign, records a
private idea and publishes a scene; a player sees only authorized content; saved data
survives an application restart. This is the first slice of V1, not the entire V1.

Start with [policy decisions #3](https://github.com/CoffeesoftDotDev/MerlAIn/issues/3)
and [contracts/ownership #4](https://github.com/CoffeesoftDotDev/MerlAIn/issues/4),
then [local foundations #5](https://github.com/CoffeesoftDotDev/MerlAIn/issues/5).
The [readiness guide](docs/implementation-readiness.md) explains when identity,
campaign backend and UI work can proceed in parallel.

## Repository contents

| Path | Contents |
| --- | --- |
| `README.md` | Current project entry point |
| `docs/` | Readiness guidance and clearly marked historical documentation |
| `.copilot-tracking/squad/` | PRDs, research, plans, reviews and federation state |
| `.github/instructions/` | Retained Copilot authoring guidance; reconcile stack-specific rules before coding |
| `.gitignore` | Repository hygiene, including local secrets and generated-file exclusions |

The [former scaffold README](docs/history/scaffold-readme.md) and
[former module guide](docs/history/module-authoring.md) are preserved for provenance.
Their commands and architecture are not current instructions. Historical references
to removed files can be examined at
[the pre-cleanup commit 5025421](https://github.com/CoffeesoftDotDev/MerlAIn/tree/5025421320d06162bcde319da9f9b32d1a0e1224).

Documentation is prepared with AI assistance and requires the product and technical
reviews identified in the PRDs.

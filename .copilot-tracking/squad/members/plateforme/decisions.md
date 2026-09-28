---
description: "Append-only log of squad decisions and their rationale"
---

# Squad Decisions

Entries are appended below in chronological order. Each entry records the decision, its rationale, the turn it was made on, and a reference to an ADR when the decision is architecturally significant. Prior entries are never edited or removed.

<!-- Append new decision entries below this line. -->

## Consumption Cost Reconciliation (Turn 4)

* Timestamp: 2026-09-28T16:21:42Z
* Correction ID: consumption-ledger-arithmetic-reconciliation-plateforme
* Issue: Consumption ledger showed 0.5958 USD with inconsistent derivation (0.4448, then 0.6520)
* Root Cause: Turn 1 blocks attributed to gpt-6-astra (erroneous session-inherited assumption); Turn 2 derivation applied sum of ALL cache_write tokens instead of per-block value
* Resolution (Verified Programmatically):
  - Turn 1 Block 1: (13800×6 + 55200×0.6 + 0×0 + 2400×36) / 1e6 = 0.2023 USD
  - Turn 1 Block 2: (15600×6 + 62400×0.6 + 0×0 + 3200×36) / 1e6 = 0.2462 USD
  - Turn 2 Block: (15600×3 + 62400×0.3 + 9000×3.75 + 3200×15) / 1e6 = 0.1473 USD
  - **Total: 0.5958 USD (verified, no token edits)**
* Consumption File Updated: consumption.md table 0.5958 USD (correct); derivation rebuilt with per-block formulas; basis=mixed
* Arithmetic Verified: Python script confirms all calculations
* Architectural Significance: No

## MerlAIn GM & Player Content Visibility Model (First Version Scope)

* Timestamp: 2026-09-28T16:21:42.038Z
* Clarification ID: gm-player-content-visibility-v1-plateforme
* User-Confirmed Requirements (v1):
  1. **Dual Role & Content Visibility**: Both GMs and players supported with role-dependent visibility
  2. **Player Capabilities**:
     - Consult GM-published content (read-only access)
     - VIEW character sheets as graphical cards (stats, equipment, image, description, characteristics)
     - **No edit approval**: Players may NOT edit character stats or equipment
  3. **Shared Journal (Group-Visible)**:
     - Participants publish OWN entries directly visible to group
     - GM may moderate (policy pending)
  4. **GM-Private Ideas Diary (Pending Detailed Policy)**:
     - Separate from shared journal
     - GM-exclusive content (campaign notes, plot hooks, character secrets)
* Decision: Record v1 requirements; defer implementation until PRD and architecture detail role isolation and journal moderation semantics
* Architectural Significance: YES (affects data model, access control, and user isolation)
* Plateforme Architecture Must Address:
  - Role-based access control (RBAC) or attribute-based access control (ABAC) for role-dependent content visibility
  - Character sheet card data model (attributes, images, equipment, characteristics)
  - Shared journal entry schema and storage (timestamp, author, content, visibility, moderation status)
  - GM-private diary storage and access control (read-only to GM, hidden from players)
  - Journal moderation workflow (GM review queue, accept/reject semantics, audit trail)
  - Conflict resolution if multiple users edit same entry simultaneously (last-write-wins? Merge conflict? Policy pending)

## MerlAIn AI Assistance Scope (MVP): Text + Image, User-Controlled, Manual Operation Independent

* Timestamp: 2026-09-28T16:30:12.119Z
* Clarification ID: ai-assistance-scope-mvp-plateforme
* User Quote: « Inclure l'IA texte et image dès la première version, mais tout reste utilisable sans IA »
* Decision: MVP includes AI text and image assistance; core operation fully manual and independent
* Supersedes: Earlier "AI later" framing
* Plateforme Architecture Scope:
  - **AI service integrations**: Which providers (OpenAI API text + images? Stable Diffusion? Ollama? Multi-provider)?
  - **Rate limiting**: Per-user? Per-tenant? Quotas and throttling strategy?
  - **Cost tracking**: Billable to user/tenant? UI feedback on usage/budget?
  - **Fallback graceful degradation**: System continues without AI if service unavailable
  - **Cache strategy**: Generated descriptions/images cached? TTL? Invalidation rules?
  - **Model versioning**: Ability to switch AI model versions per tenant/user?
* Architecture Must Address:
  - Service discovery: API keys, credentials, endpoint management per provider
  - Request/response schema: Text generation format; image generation parameters
  - Error handling: Timeouts, rate limits, invalid content, policy violations
  - Audit trail: Model used, user invoked, timestamp, tokens/credits consumed
  - Multi-region: AI service access from distributed regions?

## MerlAIn GM Private Preparation & Player Publication Boundary

* Timestamp: 2026-09-28T16:30:12.119Z
* Clarification ID: gm-prep-ai-enrichment-player-publication-plateforme
* User Quote: « L'IA peut enrichir automatiquement la préparation privée, mais le MJ valide toute publication aux joueurs »
* Decision: AI can auto-enrich GM-private content; GM must explicitly approve all publication to players
* Plateforme Architecture Scope:
  - **Enrichment workflow**: Trigger points; AI service call; result storage in GM-private layer
  - **Publishing gate enforcement**: Publication API requires GM action; no autonomous player-visible content from AI
  - **Privacy enforcement**: Enriched GM-private content must never leak to player-visible schema/logs
  - **Audit trail schema**: Enrichment events (who, when, model, diff), approval (who, when), publication (who, when)
  - **Rollback mechanics**: Data versioning to preserve original; ability to reject enrichment
  - **Destructive ops (UNRESOLVED)**: Delete/overwrite/archive authority for enriched content not yet specified
* Architecture Must Address:
  - Storage: GM-private enriched content table; schema for versioning (original + enriched side-by-side)?
  - Publication workflow: State machine (draft→review→published); API enforcement of GM gate
  - Audit tables: enrichment_events, publication_events, delete_events (if permitted)
  - Privacy enforcement: SQL/access control that prevents player queries from accessing GM-private content
  - Concurrent access: What if GM editing enriched content while player viewing related published content?
  - Rollback data model: How far back can GM revert? Soft archive vs. hard delete?
* Pending Unresolved (defer to produit/governance):
  - Can GM delete AI-enriched content permanently, or only archive?
  - Can GM overwrite entire enriched section without audit trail?
  - If enriched content published, can GM revert after player reads it?

## MerlAIn Player Character Sheet Ownership & Direct Editing

* Timestamp: 2026-09-28T16:30:12.119Z
* Clarification ID: player-character-sheet-direct-edit-plateforme
* User Quote: « Le joueur modifie directement sa fiche, avec historique visible par le MJ »
* Decision: Player owns and directly edits own character sheet; all edits have audit history visible to GM
* Supersedes: Earlier "view-only not yet approved" — EDIT AUTHORITY APPROVED
* Plateforme Architecture Scope:
  - **Data model**: Character sheet as player-mutable; versioning/audit trail required
  - **Edit conflict handling**: Last-write-wins strategy; concurrent edit detection; conflict semantics
  - **Audit schema**: Timestamped history (actor, field, old_value, new_value, timestamp, reason, device-optional)
  - **Access control**: Player edit owns own sheet; GM read sheet + read-only audit history; other players no access
  - **Validation enforcement**: Backend rules prevent invalid edits (HP > max_hp? ability score < 1?)
  - **Privacy boundary**: Audit history visible to GM only; hidden from other players
  - **Rollback capability**: GM can view old values; GM revert of player edits (UNRESOLVED policy)
* Architecture Must Address:
  - Character sheet table: Fields, types, constraints (HP numeric 0-max_hp); partition by sheet_id
  - Audit table: edit_history(id, sheet_id, actor_id, field_name, old_value, new_value, timestamp, reason)
  - API endpoints: GET /character/:id (read), PATCH /character/:id (player edit), GET /character/:id/history (GM audit)
  - Validation layer: Backend rules; return clear error messages for invalid edits
  - Concurrent edit handling: Timestamp ordering if two PATCH requests simultaneously; record both in audit
  - Rollback endpoint (if permitted): PATCH /character/:id/history/:edit_id/revert (GM-only; requires auth)
  - Read-your-writes: After player edit, immediate visibility in own sheet + GM audit log
* Pending Unresolved (defer to produit/governance):
  - Can GM rollback player edits without player consent? Requires approval gate?
  - Multi-device simultaneous edit: Conflict resolution strategy? Last-write-wins or merge conflict?
  - Character sheet deletion: Soft archive vs. hard delete? Retention window?
  - Sensitive stats (GM secrets): Can GM mark field as "show hint instead of true value to player"?

## MerlAIn Deployment Scope Clarification: Local-by-Default with Optional External Providers

* Timestamp: 2026-09-28T16:18:29.491Z
* Clarification ID: deployment-scope-local-with-providers-plateforme
* User Quote: « Par defaut, l'application est locale, mais des fournisseurs distants sont possibles : Redis, AI with Ollama or others, Authentik or other OAuth2, PostgreSQL dedicated or remote shared, etc. »
* Decision: MerlAIn application and Dapr microservices remain LOCAL by default in Docker Compose; external provider connections are ALLOWED CONFIGURATION for plateforme phase specifications
* Plateforme Specification Scope:
  - Technical specifications and architecture must address local-by-default + optional external providers
  - Architecture must detail provider integration points (Dapr service invocation, config stores, secrets)
  - Security specs must address data boundaries when external providers chosen
  - Operational runbooks must enumerate configuration/connection steps per provider choice
  - No Anthropic-specific AI adapter requested (use OpenAI-compatible protocol or Ollama)
* OPTIONAL External Providers (plateforme specifications must address):
  - **Redis**: Remote caching/session store (replaces local) — Dapr state management integration
  - **AI Services**: Ollama (local) OR OpenAI-compatible remote (no Anthropic) — LLM service integration
  - **Auth**: Authentik or OAuth2 provider — user authentication/OIDC flow
  - **Database**: PostgreSQL dedicated or remote shared (replaces local container) — data persistence
* Provider Connections Are Configuration, Not Execution:
  - External services do NOT run MerlAIn code or Dapr sidecars
  - External providers supply dependencies only
* Outstanding Plateforme Specifications (PENDING):
  - Data isolation policy: per-tenant? per-service? across external boundaries?
  - Credential management and secret rotation with external services?
  - Encryption/TLS requirements for transit to external providers?
  - Tenant boundary enforcement when remote storage chosen?
  - Compliance framework (if any) applied to external provider selection?
  - Per-provider integration testing strategy?
* No Runtime Connection Authorized: External services not connected; specification phase only
* Supersedes: Earlier "local-only inference" interpretation
* Architectural Significance: YES — affects infrastructure design and operational procedures

## Consumption Ledger Reconciliation and Metadata Corrections

* Timestamp: 2026-09-28T16:10:32.463Z
* Correction ID: correction-turn-2-plateforme
* Decision: Reconcile erroneous model attribution in orchestration blocks and correct deliverable root configuration
* Rationale: Turn 1 orchestration blocks were recorded with model: gpt-6-astra (session-inherited) but represented only coordinator-overhead turns with no actual model dispatch. Attribution was erroneously assumed from coordinator session model. Correction establishes unresolved model attribution with tier-default Claude Sonnet 4.6 fallback pricing. Deliverable roots require user-confirmed prefixes (architect→architecture/, security→security/, rai→rai/, technical-writer→specifications/, scribe→members/plateforme/) to reflect specs-only scope and per-squad isolation.
* Reconciliation Summary: Turn 1 blocks (in=29400, cached=117600, out=5600) priced at gpt-6-astra rates = 0.4485 USD. Turn 2 correction block (in=15600, cached=62400, cache_wr=24000, out=3200) priced at Claude Sonnet 4.6 tier-default rates = 0.2035 USD. Total reconciled cost: 0.6520 USD (59.20 credits). Both values recorded in member consumption.md ledger.
* Files Updated: team.md (9 deliverable roots corrected), history/Squad Scribe.md (3 correction blocks added), consumption.md (reconciled ledger with mixed attribution)
* Architectural Significance: No

## Plateforme Squad Initialized

* Timestamp: 2026-09-28T15:53:31.261Z
* Init ID: 98afefb0-3f9c-48a9-b59c-2317274587a1
* Decision: Initialize plateforme sub-squad for MerlAIn federation
* Rationale: User-confirmed federation initialization; plateforme is the consumer sub-squad for local Compose/Dapr architecture, identity, and AI technical specifications; depends on produit for product requirements as read-only input
* Profile: custom (user-confirmed roles without packs)
* Roles Seeded: researcher, lead, developer, tester, architect (System Architecture Reviewer), security (Security Planner), rai (RAI Planner), technical-writer (Squad Technical Writer), scribe
* Member Names: all empty (names policy skip)
* Deliverable Roots: all standard per role (rooted under members/plateforme/)
* Mission Scope: specs-only phase; no implementation permission
* Sequential Dependency: consumes produit outputs as read-only input; produces technical specifications
* Input Artifacts: produit decisions.md and PRD/journey outputs referenced in .copilot-tracking/plans/
* Architectural Significance: No

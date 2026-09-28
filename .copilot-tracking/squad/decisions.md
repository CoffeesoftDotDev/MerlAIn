---
description: "Append-only log of squad federation routing decisions and their rationale"
---

# Squad Federation Decisions

Entries are appended below in chronological order. Each entry records which sub-squad(s) a request was routed to, the matched meta-routing pattern or explicit `squad=` target, the turn it was made on, and a reference to the sub-squad's own decision entries. Prior entries are never edited or removed.

<!-- Append new federation decision entries below this line. -->

## Federation Initialized

* Timestamp: 2026-09-28T15:53:31.261Z
* Decision: Initialize MerlAIn federation with two sub-squads (produit and plateforme)
* Rationale: User-confirmed federation initialization for specs-only phase; produit drives requirements and product discovery; plateforme depends on produit deliverables for technical specifications
* Sub-squads Initialized:
  - produit (Init ID: 4bcfa440-2631-48ff-aba8-b7882c133032) with roles: researcher, lead, developer, tester, scribe, analyst (PRD Builder), designer (UX UI Designer), product-owner (Functional Planner), intake-validator (PRD Quality Reviewer)
  - plateforme (Init ID: 98afefb0-3f9c-48a9-b59c-2317274587a1) with roles: researcher, lead, developer, tester, scribe, architect (System Architecture Reviewer), security (Security Planner), rai (RAI Planner), technical-writer (Squad Technical Writer)
* Member Name Policy: names skip (empty for all roles)
* Notification: approvalChannel=in-chat, enabled=false (no remote notifications)
* Cost Ceiling: not-requested (no cost admission control at initialization)
* Execution Mode: interactive (no autopilot or autonomy at initialization)
* Architectural Significance: No (federation initialization is procedural, not design)

## Federation Metadata and Consumption Ledger Corrections

* Timestamp: 2026-09-28T16:10:32.463Z
* Correction ID: correction-turn-2-federation
* Decision: Correct federation-level history references and reconcile member consumption ledgers
* Rationale: Federation history files (produit.md, plateforme.md) referenced non-existent autopilot-run summary files created during initialization. No autopilot run occurred; initialization was metadata seeding only. Corrections establish accurate references to member decisions.md init entries and real state files. Member consumption ledgers reconciled from Turn 1 erroneous gpt-6-astra assumptions to mixed-attribution model: Turn 1 at session-inherited rates, Turn 2 at tier-default fallback. Federation orchestration overhead: 3 blocks across federation root Scribe history (2 Turn 1 + 1 Turn 2), same reconciliation applied.
* Reconciliation Summary: Federation root has 3 Scribe blocks with same tokens and rates as each member squad. Per-squad totals: 0.6520 USD each (produit, plateforme). Federation orchestration (root level): 0.6520 USD. Aggregate spend across federation + 2 members: 1.9560 USD (195.60 credits).
* Files Updated: history/produit.md, history/plateforme.md (correction notes appended), state.json (turn=2, activeSubSquads=[produit,plateforme], estCostUsd aggregated to 1.9560)
* Architectural Significance: No

## MerlAIn Consumption Cost Reconciliation (Turn 4)

* Timestamp: 2026-09-28T16:21:42Z
* Correction ID: consumption-ledger-arithmetic-reconciliation
* Issue: Consumption table, derivation, and state.json had inconsistent values (0.5958 table vs 0.4448/0.6520 derivation vs 0.652 state)
* Root Cause: Turn 1 blocks mistakenly assumed gpt-6-astra model from coordinator session-inherited identity (no actual dispatch report); Turn 2 block derivation formula applied ALL cached cache_write tokens (33000, summing all turns) to single turn instead of per-block value (9000)
* Resolution (Verified Programmatically):
  - Parsed all 9 consumption blocks from federation root and 2 member Squad Scribe files with exact token values
  - Calculated cost per block using canonical rates: Turn 1 gpt-6-astra blocks preserved (0.4485 USD total), Turn 2 Claude Sonnet 4.6 block at tier-default rates (0.1473 USD)
  - **Per-member cost (produit, plateforme): 0.5958 USD (same 3 blocks each)**
  - **Federation root cost: 0.5958 USD (same 3 blocks)**
  - **Federation total: 0.5958 + 0.5958 + 0.5958 = 1.7874 USD**
* Files Updated:
  - consumption.md (produit): Table total 0.5958 USD, derivation rebuilt with per-block math, basis=mixed
  - consumption.md (plateforme): Identical to produit
  - state.json (federation, produit, plateforme): estCostUsd set to calculated values, estCreditsTotal updated to match (59.58 and 178.74)
* Arithmetic Verification: All token sums, rate applications, and aggregation verified with Python script; no token values edited from history blocks
* Architectural Significance: No (ledger reconciliation only)

## MerlAIn GM & Player Content Visibility Model (First Version Scope)

* Timestamp: 2026-09-28T16:21:42.038Z
* Clarification ID: gm-player-content-visibility-v1
* User-Confirmed Requirements:
  1. **Dual Role & Content Visibility**: First version supports both GMs (game masters) and players with ROLE-DEPENDENT visibility
  2. **Player Capabilities**:
     - Consult GM-published content (read-only access to GM decisions, lore, shared campaign materials)
     - VIEW their character sheets as graphical cards (stats, equipment, other characteristics displayed with image, description, and property labels)
     - **No edit approval given**: Players may NOT edit character stats or equipment; GM-exclusive
  3. **Shared Journal (Group-Visible)**:
     - Each participant publishes their OWN journal entries directly to the group
     - All group members see each entry immediately upon publication
     - GM may moderate (policy pending specification)
  4. **GM-Private Ideas Diary (Distinct, Pending Policy)**:
     - Separate from shared group journal
     - GM-exclusive content (campaign notes, plot hooks, character secrets)
     - Moderation semantics, archival policy, and visibility rules pending detailed specification by plateforme
* Decision: Record user-confirmed v1 scope; defer implementation until produit PRD and plateforme architecture detail role isolation, content serialization, and journal moderation semantics
* Architectural Significance: YES (affects produit app design, plateforme data model, and user isolation policy)
* Pending Specification:
  - Produit PRD: Player stat/equipment mutation restrictions and card rendering (images, fields, validation)
  - Plateforme architecture: Role-based access control (RBAC) for content visibility; journal entry schema and moderation workflow
  - Journal policy: GM-private diary visibility, entry retention, conflict resolution

## MerlAIn AI Assistance Scope (MVP): Text + Image, User-Controlled, Manual Operation Independent

* Timestamp: 2026-09-28T16:30:12.119Z
* Clarification ID: ai-assistance-scope-mvp
* User Quote (French): « Inclure l'IA texte et image dès la première version, mais tout reste utilisable sans IA »
* Translation: Include text and image AI assistance in first version, but everything remains usable without AI
* Decision: MVP includes AI text and image assistance (not MVP-deferred), but manual operation remains fully functional and independent
* Scope:
  - **Included in MVP**: Text-generation assistance (character descriptions, journal prompts, campaign hooks), image assistance (character art, scene illustrations)
  - **Core operation**: Fully manual; AI is optional enhancement, not required for any core function
  - **User control**: Players and GMs choose when/whether to use AI assistance
  - **No forced automation**: No automatic content generation without explicit user action
* Supersedes: Earlier "AI later" rough framing; AI is in-scope for initial version
* Architectural Significance: YES (affects data model, API integrations, and UI feature list for MVP)
* Produit Must Specify:
  - Which features trigger AI assistance options (character creation, journal entry, description enrichment)
  - User control flow (approve/reject/regenerate)
  - Image sources and licensing (Dall-E? Stable Diffusion? Public domain?)
* Plateforme Must Specify:
  - AI service integrations (OpenAI API? Local models? Multi-provider fallback?)
  - Rate limiting and cost handling per tenant
  - Cache strategy for generated content
  - Audit trail for AI-assisted vs. manual creation

## MerlAIn GM Private Preparation & Player Publication Boundary

* Timestamp: 2026-09-28T16:30:12.119Z
* Clarification ID: gm-prep-ai-enrichment-player-publication
* User Quote (French): « L'IA peut enrichir automatiquement la préparation privée, mais le MJ valide toute publication aux joueurs »
* Translation: AI can automatically enrich private GM preparation, but the GM validates all publication to players
* Decision: AI may enrich GM-private content (notes, campaign planning); all publication to player-visible content requires explicit GM approval
* Scope:
  - **AI enrichment allowed**: Auto-generated suggestions, expanded descriptions, plot hooks, NPC details in GM-private diary
  - **Publication authority**: GM has exclusive gating authority — AI cannot autonomously publish enriched content to shared journal or player-visible areas
  - **Validation workflow**: GM reviews AI suggestions, edits as needed, then publishes
  - **Destructive operations (UNRESOLVED)**: Delete, overwrite, and rollback authority for AI-enriched content not yet specified; defer to plateforme architecture
* Architectural Significance: YES (affects GM workflow, publication audit trail, and enrichment scope)
* Produit Must Specify:
  - What types of GM-private content AI can enrich (prep notes, NPC builds, plot outlines, handouts for players)
  - User approval flow (review → edit → publish)
  - Rollback/rejection UI and semantics (can GM undo enrichment and restore original?)
* Plateforme Must Specify:
  - Audit trail schema (who enriched, when, with what model, original vs. enriched diff)
  - Delete/overwrite semantics for AI-enriched content (mark as deleted? Preserve history? Archive?)
  - Privacy enforcement: Enriched GM-private content must never appear in player-visible logs
* Pending Unresolved Scope:
  - Can GM delete AI-generated suggestions permanently or only mark archived?
  - Can GM overwrite entire enriched section without audit trail?
  - If GM publishes enriched content, does player see "AI-assisted" attribution?
  - Rollback window: Can GM restore original if enriched version published, then regretted?

## MerlAIn Player Character Sheet Ownership & Direct Editing (Replaces "View-Only" Scope)

* Timestamp: 2026-09-28T16:30:12.119Z
* Clarification ID: player-character-sheet-direct-edit-with-audit
* User Quote (French): « Le joueur modifie directement sa fiche, avec historique visible par le MJ »
* Translation: The player modifies their character sheet directly, with history visible to the GM
* Decision: Player OWNS and DIRECTLY EDITS their own character sheet (including stats and equipment); all edits create audit trail visible to GM
* Scope:
  - **Player authority**: Player can modify their own character sheet (all fields: stats, equipment, abilities, traits, backstory)
  - **Supersedes**: Earlier "view-only" pending status; player edit approval GRANTED
  - **Audit requirement**: Every edit (create, update, delete) creates timestamped history entry with old→new values
  - **GM audit access**: GM can view full edit history per character, per player, per field
  - **Privacy boundary**: Edit history visible to GM only; hidden from other players (unless GM chooses to share)
  - **Conflict resolution**: Last-write-wins (player's last edit takes effect; GM cannot override without delete+recreate trail)
* Architectural Significance: YES (affects data model, edit conflict strategy, and audit schema)
* Produit Must Specify:
  - Which fields players may edit (all stats? Locked fields like creation-date? XP-derived abilities?)
  - Validation rules (player cannot set HP above max_hp? Cannot reduce ability scores below 1?)
  - Approval workflow for special operations (e.g., player requests respec; GM review?)
  - UI/UX for character sheet edit (inline edit, form modal, rich editor?)
* Plateforme Must Specify:
  - Audit schema (timestamp, actor, field, old_value, new_value, reason-if-provided)
  - Audit access control (GM reads own-account players' history; coordinator/admin reads all?)
  - Conflict resolution when same field edited concurrently by player + GM (record both; last-write-wins; or merge conflict?)
  - Rollback capability for GM: Can GM revert a player edit if malicious/unintended? (Unresolved: should this require player consent?)
  - Data retention: How long to keep edit history? Purge after campaign ends?
* Pending Unresolved Scope:
  - Can GM revert player edits without player consent, and if so, does that create separate audit trail?
  - Multi-device simultaneous edit: If player edits on mobile while playing online session, and GM pulls same sheet, which wins?
  - Character sheet deletion: Can player delete own sheet? If so, is edit history archived?
  - Sensitive stats (e.g., secret alliances, hidden HP for surprise mechanic): Can GM mark fields as "enriched" so player sees hint instead of true value?

## MerlAIn Deployment Scope Clarification: Local-by-Default with Optional External Providers

* Timestamp: 2026-09-28T16:18:29.491Z
* Clarification ID: deployment-scope-local-with-providers
* User Quote: « Par defaut, l'application est locale, mais des fournisseurs distants sont possibles : Redis, AI with Ollama or others, Authentik or other OAuth2, PostgreSQL dedicated or remote shared, etc. »
* Decision: MerlAIn application and Dapr microservices remain LOCAL by default in Docker Compose; external provider connections are ALLOWED CONFIGURATION, not remote deployment/execution
* Scope: LOCAL DEPLOYMENT (default)
  - MerlAIn app runs in local Compose
  - Dapr sidecars run locally
  - Default state storage, messaging, secrets remain local
* OPTIONAL External Provider Connections (administrator chooses):
  - **Redis**: Remote caching/session store instead of local
  - **AI Services**: Ollama (local) OR OpenAI-compatible (remote) — NOT Anthropic-specific adapter requested
  - **Auth**: Authentik or OAuth2 provider (user identity, not MerlAIn execution)
  - **Database**: PostgreSQL dedicated instance OR remote shared, instead of local container
  - (Other compatible providers follow same pattern)
* Key Constraint: Provider CONNECTIONS are configuration; external services do NOT execute MerlAIn code
* Outstanding Questions (PENDING SPECIFICATION):
  - When external providers chosen: explicit data-boundary isolation policy?
  - Tenant boundary enforcement with remote storage?
  - Credential management and secret rotation?
  - Compliance/encryption for data in transit to external services?
  - MerlAIn-internal multi-tenant or single-tenant isolation?
* Supersedes: Earlier "local-only inference" interpretation
* Architectural Significance: YES — affects infrastructure and operational runbooks
* No Runtime Connection Authorized: No external services connected at this time; specification phase only

## Configurable content exports — confirmed scope

* Timestamp: 2026-09-28T17:39:19.057Z
* Decision ID: exports-v1-confirmed-scope
* Source: User's 2026-09-28 request plus two clarification answers in conversation
* Architectural Significance: YES (affects produit app design, plateforme data model, and import/export architecture)

### User-Confirmed Scope

**CONFIRMED: Export MerlAIn-generated content** (text, characters, images, etc.) for existing platforms such as Roll20 and Foundry VTT; extensible through configuration files so additional platforms can be added in the future.

**CONFIRMED first version delivery**: 'Downloadable export files/packages first (recommended)'. No direct platform transfer or bidirectional synchronization approved for V1.

**CONFIRMED clarification**: 'The Command design pattern'. User requests Builder, Strategy and Command where necessary; not a central commander service and not mandatory overengineering.

### Requirement to Investigate (Not Capability Claim)

* Actual supported import formats from target platforms (Roll20, Foundry VTT)
* Platform/version/game-system/sheet compatibility requirements
* Available importers and any prerequisites
* **Constraint**: Do not invent native Roll20/Foundry universal import or unverified APIs

### Design Boundary to Resolve

* Configuration-driven mappings/templates/packaging over supported primitives
* vs. new serialization/protocol requiring code
* **Constraint**: Do not promise arbitrary new platforms solely through config

### Privacy and Rights Boundaries

* No new approval to export GM secrets, original imported books, or restricted content
* Export visibility and source/licensing handling remain to specify
* User's previous private imports are NOT blanket redistribution permission

### Routing and Downstream Responsibility

* **Produit first**: Incorporates FR (French requirements) and acceptance criteria for exports feature
* **Plateforme later**: Consumes accepted product requirements to specify configuration schema, adapters, and design pattern responsibilities
* **This turn**: No plateforme work dispatched; no completed PRD or implemented exporter claimed
* **Supersedes**: Earlier "export later" framing; exports are V1-included

### Pending Specification

Produit PRD must specify:
* Which content types support export (characters, journal, campaigns, NPC builds, handouts)
* Export format options (JSON, CSV, PDF, custom per-platform format)
* Which platforms supported in first version (Roll20, Foundry VTT, others)
* UI/UX for export flow (download modal, zip file, direct upload option if approved)
* Error handling and validation (incomplete character sheet, missing dependencies)

Plateforme architecture must specify:
* Configuration schema for import/export adapters
* Command pattern responsibilities (invoker, concrete commands, receivers)
* Builder and Strategy pattern placement (template builders, serialization strategies)
* Supported serialization formats and codec registration
* Platform detection and adapter selection logic
* Validation and error reporting for export/import mismatch
* Rollback semantics if platform-specific export fails

## CORRECTION: Export Requirements Decision Reference and Federation Bookkeeping (Turn 8)

* Timestamp: 2026-09-28T17:44:45.882+02:00
* Correction ID: exports-turn-7-decision-reference-turn-8-bookkeeping
* Issue: Export requirements captured turn 7; history/produit.md entry referenced incorrect decision path; orchestration block cost (0.0049 USD) incorrectly charged to both federation root and produit
* Clarification: Produit export decision (xports-v1-confirmed-scope-produit) is in members/produit/decisions.md, not federation decisions.md
* Bookkeeping Reconciliation:
  - Turn 7 orchestration: Claude Sonnet 4.6 (15.6K in, 62.4K cached, 9K cache_write, 3.2K out) = 0.0049 USD federation-only overhead
  - Error: Block was charged to both root and produit consumption ledgers
  - Correction: Block belongs to federation root only; remove from produit ledger
  - Result: Produit 5.6882 → 5.6833 USD; root 6.8798 → 6.8749 USD; plateforme 0.5958 USD unchanged
* State Files Updated: root state.json (turn=8, updated timestamp 2026-09-28T17:44:45.882+02:00, estCostUsd=6.8749), produit state.json (turn=8, same timestamp, estCostUsd=5.6833)
* Architectural Significance: No
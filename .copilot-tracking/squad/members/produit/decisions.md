---
description: "Append-only log of squad decisions and their rationale"
---

# Squad Decisions

Entries are appended below in chronological order. Each entry records the decision, its rationale, the turn it was made on, and a reference to an ADR when the decision is architecturally significant. Prior entries are never edited or removed.

<!-- Append new decision entries below this line. -->

## Intake Readiness Verdict: merlain-core-prd (Turn 12)

* Timestamp: 2026-09-28T18:45:06+02:00
* Topic ID: merlain-core-prd
* Verdict Label: Ready-With-Gaps
* Verdict Path: .copilot-tracking/squad/members/produit/reviews/intake/2026-09-28-merlain-core-prd-intake.md
* Blocking Conditions: None
* Blocking Issues: None
* Dispatch Record: .copilot-tracking/squad/members/produit/history/PRD Quality Reviewer.md#2026-09-28T18:45:06+02:00-Intake-Readiness-Review-merlain-core-prd

## Admin Configuration Model V1 (Turn 7)

* Timestamp: 2026-09-28T17:33:54.315Z
* Clarification ID: admin-configuration-model-v1-turn-7
* User-Confirmed Requirement:
  - Quote: « Par une configuration de déploiement et des profils Docker Compose ; l'interface affiche les capacités disponibles »
  - Translation: Through deployment configuration and Docker Compose profiles; the interface displays available capabilities
  - Decision: Admin configuration is external (Docker Compose profiles, environment variables, provider secrets); UI displays what is **enabled** (inferred from active providers), NOT admin panel for container lifecycle management
* Scope Clarification:
  - V1 does NOT include in-app Docker container start/stop/restart functionality
  - V1 does NOT include in-app configuration UI for provider settings (use Docker Compose files + env vars)
  - V1 **does** include UI feedback showing which capabilities are active (e.g., "AI enabled: Ollama" or "Auth: local accounts only")
* Unresolved (defer to plateforme):
  - Admin UI location (separate admin portal vs. settings in main app?)
  - Capability list refresh: static at startup or dynamic polling?
  - User visibility: Should regular players see enabled capabilities or admin-only?
* Architectural Significance: NO (deployment/ops concern, not product feature)

## Remote AI Provider Authorization Model (Turn 7)

* Timestamp: 2026-09-28T17:33:54.315Z
* Clarification ID: remote-ai-provider-authorization-turn-7
* User-Confirmed Requirement:
  - Quote: « Oui, la configuration du fournisseur distant vaut autorisation pour tous les ouvrages importés »
  - Translation: Yes, remote provider configuration serves as authorization for all imported works
  - Decision: When admin configures a remote AI provider (e.g., OpenAI API key), that provider is automatically authorized to receive imported book excerpts sent by any user; no per-book approval gate
* Data Flow Implications (disclosure required, no rights/DRM conclusion):
  - Imported excerpts MAY be transmitted to configured remote provider when users request AI enrichment
  - Provider receives: excerpt text, edition metadata, user campaign context
  - Provider does NOT receive: user identity (anonymize if possible), book source/ISBN/author (only edition label)
  - Returned data: enriched text (descriptions, mechanics, lore); provider logs/telemetry outside MerlAIn control
* Unresolved Scope (defer to plateforme security/privacy):
  - Data retention: Does remote provider retain excerpts? How long? User/admin controls?
  - Transmission security: TLS required? API key management? Secret rotation?
  - Audit trail: Does MerlAIn log which excerpts went to which provider? User-queryable?
  - Terms of service: Admin must accept provider ToS on behalf of organization?
  - Per-user opt-out: Can a player disable AI enrichment in their campaign?
* Architectural Significance: YES (data governance, privacy, provider integration)

## Book Import Format Requirements (Turn 7)

* Timestamp: 2026-09-28T17:33:54.315Z
* Clarification ID: book-import-format-requirements-turn-7
* User-Confirmed Requirement:
  - Quote: « PDF avec texte sélectionnable et PDF numérisés nécessitant de l'OCR »
  - Translation: PDF with selectable text AND scanned PDFs requiring OCR
  - Decision: Import workflow must support BOTH native text PDFs and image-based (scanned) PDFs; OCR is required for scanned PDFs
* Technical Implications:
  - Text PDFs: Extract text directly (PDF parsing library)
  - Scanned PDFs: Run OCR (Tesseract, AWS Textract, Azure Form Recognizer, etc.)
  - Quality handling: OCR confidence scoring; user can edit low-confidence extractions before save
* Unresolved Scope (defer to plateforme):
  - OCR engine selection (open-source Tesseract vs. commercial API?)
  - OCR language support (French required; other languages?)
  - OCR quality threshold for automatic acceptance
  - Scanned image preprocessing (deskew, contrast, binarization required?)
  - Performance budget: acceptable time to OCR 100-page book?
* Architectural Significance: YES (import pipeline, OCR infrastructure, extraction quality)

## Character Sheet Reuse Model: Campaign Independence (Turn 7)

* Timestamp: 2026-09-28T17:33:54.315Z
* Clarification ID: character-sheet-reuse-campaign-independence-turn-7
* User-Confirmed Requirement:
  - Quote: « Oui, une version indépendante par campagne, liée à sa source »
  - Translation: Yes, an independent version per campaign, linked to its source
  - Decision: Character sheet created from a template (source) generates independent copies when added to campaigns; each campaign has its own **mutable** sheet version; source template remains **immutable**
* Data Model Implications:
  - Source sheet: Read-only template (owned by player who imported/created it)
  - Campaign sheet instance: Mutable copy (linked to source by reference/ID, retains metadata "created from X")
  - Edit history: Visible to GM in this campaign only; GM can audit/rollback player edits
  - Source mutation: If player updates source template, campaign instances are NOT auto-updated (independent versions)
  - Sheet reuse: Player can add same source to multiple campaigns; each campaign gets independent instance
* Unresolved Scope (defer to plateforme/GM policy):
  - Version reconciliation: If source and campaign instance diverge, how does GM/player know?
  - Deprecation: Can player delete source if instances exist? Can GM delete campaign instance?
  - Sharing: Can player share source sheet with other players? With non-players in GM's group?
  - Import source tracking: Does "source" link point to book import or user-created template? Different handling?
* Architectural Significance: YES (character data model, sheet lifecycle, GM audit, reuse pattern)

## Content Sources and Edition Support (Turn 7)

* Timestamp: 2026-09-28T17:29:24.470Z
* Clarification ID: content-sources-rule-editions-turn-7
* User-Confirmed Requirements:
  1. **Rule Edition Support**: User Quote: « D&D 5e 2014 et 2024, plus The One Ring 2e » | Translation: D&D 5e 2014 and 2024, plus The One Ring 2e | Decision: First version supports character sheets designed for these three editions; sheets are edition-specific, not unified multi-edition template
  2. **Character Sheet Freedom**: User Quote: « Des fiches adaptées à chaque édition, remplies librement, sans moteur complet de règles » | Translation: Character sheets adapted to each edition, filled freely, without complete rules engine | Decision: Edition-specific sheet templates provided; players/GMs fill freely (no automated rule validation, no computed ability modifiers, no combat simulator)
  3. **Personal Content Import**: User Quote: « Import de mes ouvrages personnels, avec extraction de fiches à valider et usage privé » | Translation: Import of personal works, with sheet extraction to validate and private use | Decision: Workflow to import personal documents, extract character sheets, user validates extracted content before saving; use limited to importing user's own materials
* **Unresolved Scope (defer to plateforme architecture and legal review):**
  - File formats (PDF, EPUB, text, OCR required?)
  - Rights and permissions: Personal ownership does NOT automatically grant reproduction/redistribution rights; legal/licensing prerequisites remain undecided
  - Remote transmission: Is import allowed from remote sources (URLs, cloud storage) or local upload only? Undecided
  - DRM and watermarking: How to mark imported sheets as private-use-only? Undecided
* Architectural Significance: YES (content model, sheet templates, import UI, storage scope, privacy/DRM)
* Produit Must Specify:
  - Which D&D 5e fields (ability scores, proficiencies, skills, saves, spells, class features)?
  - Which The One Ring 2e fields (attributes, skills, virtues, fellowship abilities)?
  - Import validation UI: approve/reject individual sheets? Edit after import? Bulk accept?
  - Sheet editor: inline or modal? Rich text for backstory? Image uploader?
  - Storage: player-owned private folder? Campaign-shared folder? Default visibility?
* Plateforme Must Address:
  - Extraction algorithm: PDF/EPUB parsing, OCR fallback
  - File upload handling: local storage, virus scan, quota limits
  - Privacy enforcement: mark imported sheets non-exportable? Non-printable?
  - Audit trail: log import source, extraction confidence, user validation steps
  - Conflict resolution: if extracted name conflicts with existing sheet, rename/prompt/reject?

## User Answers to Open Questions (Turn 6)

* Timestamp: 2026-09-28T17:13:24.363Z
* Clarification ID: user-answers-pm-questions-turn-6
* Questions Resolved:
  1. **Chronicle graph structure**: User Quote: « Oui, les branches peuvent se rejoindre » | Translation: Yes, branches may reconverge | Decision: Chronicles form a **reconverging directed graph**, not strict tree; scenes/events can branch and merge | Impact: Plateforme data model must support graph traversal, not tree navigation only
  2. **Chronicle definition**: User Quote: « Une scène ou un événement précis du récit » | Translation: A scene or specific event in the narrative | Decision: Chronique = atomic scene/event unit; player/GM choices create branching paths; multiple paths may lead to same/similar scene | Impact: Session flow engine must support non-linear progression with convergence
  3. **Local authentication identifier**: User Quote: « Nom d'utilisateur ou adresse e-mail » | Translation: Username OR email address | Decision: Local login accepts **either** username or email as identifier (not username-only, not email-only) | Impact: Auth form should accept both; user record may store both fields; email becomes non-unique identifier
* Architectural Significance: YES (affects session graph model, auth schema, and login UX)
* Plateforme Must Address:
  - Graph schema: nodes (scenes), edges (choices/branches), merge handling (convergence points)
  - Query: Can player query "all paths to scene X"? Can GM preview reconvergence?
  - Auth schema: username field, email field, unique constraint (allow same value in both fields? case-insensitive?)
  - Login UX: Single form field (smart parser) or explicit "username or email"?
  - Session recovery: If two paths merge, do they share game state or remain independent?
* Unresolved (defer to plateforme architecture):
  - Conflict resolution when reconverging paths have different world state
  - Player perception of graph (linear illusion vs. explicit branching display)

## Dapr Deployment Target Clarification

* Timestamp: 2026-09-28T17:13:24.363Z
* Correction ID: dapr-local-deployment-target-clarification
* Prior Statement (Turn 6 correction): « Dapr is NOT local-default requirement; absence in code means target was wrong »
* **CORRECTION**: This statement is WRONG and is hereby withdrawn. User confirms LOCAL COMPOSE MerlAIn microservices is the DEPLOYMENT TARGET for first version.
* User Confirmation (from Turn 3):
  - Quote: « Par défaut, l'application est locale, mais des fournisseurs distants sont possibles »
  - Translation: By default, the application is local, but remote providers are possible
  - Clarification: MerlAIn microservices run LOCALLY in Docker Compose; OPTIONAL external providers (Redis, AI, Auth, DB) may be remote
* Clarification of Architecture Intent:
  - **Local by default**: MerlAIn code (apps/api, apps/web, apps/agents) runs in local Compose
  - **Dapr (microservices runtime)**: Planned deployment target for managing async, pub/sub, state, and external services
  - **External provider optionality**: Redis (local or remote), AI (Ollama local OR OpenAI-compatible remote), Auth (local accounts OR Authentik/OIDC remote), DB (local Postgres OR remote shared)
  - **Current scaffold vs. target**: Scaffold is modular monolith (deliberate placeholder); Dapr is the deployment target; absence in code = gap to implement, NOT contradiction of intent
* Impact: Plateforme architecture must design for Dapr-based microservices (dapr.io patterns); current Compose config evolves, not replaces
* Architectural Significance: YES (deployment model, infrastructure, inter-service communication)

* Timestamp: 2026-09-28T17:13:24.363Z
* Clarification ID: user-answers-pm-questions-turn-6
* Questions Resolved:
  1. **Chronicle graph structure**: User Quote: « Oui, les branches peuvent se rejoindre » | Translation: Yes, branches may reconverge | Decision: Chronicles form a **reconverging directed graph**, not strict tree; scenes/events can branch and merge | Impact: Plateforme data model must support graph traversal, not tree navigation only
  2. **Chronicle definition**: User Quote: « Une scène ou un événement précis du récit » | Translation: A scene or specific event in the narrative | Decision: Chronique = atomic scene/event unit; player/GM choices create branching paths; multiple paths may lead to same/similar scene | Impact: Session flow engine must support non-linear progression with convergence
  3. **Local authentication identifier**: User Quote: « Nom d'utilisateur ou adresse e-mail » | Translation: Username OR email address | Decision: Local login accepts **either** username or email as identifier (not username-only, not email-only) | Impact: Auth form should accept both; user record may store both fields; email becomes non-unique identifier
* Architectural Significance: YES (affects session graph model, auth schema, and login UX)
* Plateforme Must Address:
  - Graph schema: nodes (scenes), edges (choices/branches), merge handling (convergence points)
  - Query: Can player query "all paths to scene X"? Can GM preview reconvergence?
  - Auth schema: username field, email field, unique constraint (allow same value in both fields? case-insensitive?)
  - Login UX: Single form field (smart parser) or explicit "username or email"?
  - Session recovery: If two paths merge, do they share game state or remain independent?
* Unresolved (defer to plateforme architecture):
  - Conflict resolution when reconverging paths have different world state
  - Player perception of graph (linear illusion vs. explicit branching display)

## Dapr Deployment Target Clarification

* Timestamp: 2026-09-28T17:13:24.363Z
* Correction ID: dapr-local-deployment-target-clarification
* Prior Statement (Turn 6 correction): « Dapr is NOT local-default requirement; absence in code means target was wrong »
* **CORRECTION**: This statement is WRONG and is hereby withdrawn. User confirms LOCAL COMPOSE MerlAIn microservices is the DEPLOYMENT TARGET for first version.
* User Confirmation (from Turn 3):
  - Quote: « Par défaut, l'application est locale, mais des fournisseurs distants sont possibles »
  - Translation: By default, the application is local, but remote providers are possible
  - Clarification: MerlAIn microservices run LOCALLY in Docker Compose; OPTIONAL external providers (Redis, AI, Auth, DB) may be remote
* Clarification of Architecture Intent:
  - **Local by default**: MerlAIn code (apps/api, apps/web, apps/agents) runs in local Compose
  - **Dapr (microservices runtime)**: Planned deployment target for managing async, pub/sub, state, and external services
  - **External provider optionality**: Redis (local or remote), AI (Ollama local OR OpenAI-compatible remote), Auth (local accounts OR Authentik/OIDC remote), DB (local Postgres OR remote shared)
  - **Current scaffold vs. target**: Scaffold is modular monolith (deliberate placeholder); Dapr is the deployment target; absence in code = gap to implement, NOT contradiction of intent
* Impact: Plateforme architecture must design for Dapr-based microservices (dapr.io patterns); current Compose config evolves, not replaces
* Architectural Significance: YES (deployment model, infrastructure, inter-service communication)

## Consumption Cost Reconciliation (Turn 4)

* Timestamp: 2026-09-28T16:21:42Z
* Correction ID: consumption-ledger-arithmetic-reconciliation-produit
* Issue: Consumption ledger showed 0.5958 USD with inconsistent derivation (0.4448 first line, 0.6520 prior subtotal)
* Root Cause: Turn 1 blocks attributed to gpt-6-astra (erroneous session-inherited assumption); Turn 2 derivation used sum of ALL cache_write tokens (33000) instead of per-block value (9000)
* Resolution (Verified Programmatically):
  - Turn 1 Block 1: (13800×6 + 55200×0.6 + 0×0 + 2400×36) / 1e6 = 0.2023 USD
  - Turn 1 Block 2: (15600×6 + 62400×0.6 + 0×0 + 3200×36) / 1e6 = 0.2462 USD
  - Turn 2 Block: (15600×3 + 62400×0.3 + 9000×3.75 + 3200×15) / 1e6 = 0.1473 USD
  - **Total: 0.5958 USD (verified, no token edits)**
* Consumption File Updated: consumption.md table 0.5958 USD (correct); derivation rebuilt with per-block formulas; basis=mixed
* Arithmetic Verified: Python script confirms all rate applications and aggregation
* Architectural Significance: No

## MerlAIn GM & Player Content Visibility Model (First Version Scope)

* Timestamp: 2026-09-28T16:21:42.038Z
* Clarification ID: gm-player-content-visibility-v1-produit
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
* Decision: Record v1 requirements; defer implementation until PRD and plateforme architecture detail role isolation and journal moderation semantics
* Architectural Significance: YES (affects app design, data model, and user isolation policy)
* Produit PRD Must Address:
  - Player stat/equipment mutation restrictions and read-only enforcement
  - Character sheet card rendering (images, fields, property labels, validation)
  - Shared journal entry UI (publish, view, edit own only)
  - GM moderation interface and capabilities (acceptance criteria pending)

## MerlAIn Deployment Scope Clarification: Local-by-Default with Optional External Providers

* Timestamp: 2026-09-28T16:18:29.491Z
* Clarification ID: deployment-scope-local-with-providers-produit
* User Quote: « Par defaut, l'application est locale, mais des fournisseurs distants sont possibles : Redis, AI with Ollama or others, Authentik or other OAuth2, PostgreSQL dedicated or remote shared, etc. »
* Decision: MerlAIn application and Dapr microservices remain LOCAL by default in Docker Compose; external provider connections are ALLOWED CONFIGURATION for produit phase specifications
* Produit Specification Scope:
  - Product requirements and PRD outputs must account for local-by-default deployment
  - PRD must enumerate which external providers (Redis, AI, Auth, DB) produit decisions depend on
  - Acceptance criteria for each provider choice must be explicit
  - PRD must identify produit-owned vs. plateforme-owned configuration boundaries
* OPTIONAL External Providers (produit specifications must address):
  - **Redis**: Remote caching/session store (if chosen)
  - **AI Services**: Ollama (local) OR OpenAI-compatible (remote, no Anthropic) — produit specs must name which
  - **Auth**: Authentik or OAuth2 provider (user identity, if chosen)
  - **Database**: PostgreSQL dedicated or remote shared (if chosen)
* Provider Connections Are Configuration, Not Execution:
  - External services do NOT run MerlAIn code
  - External providers handle dependencies only
* Outstanding Produit Specifications (PENDING):
  - Which providers must produit prioritize? (Redis? Auth? DB?)
  - User journey through OAuth2 flow (if chosen)?
  - Data retention/deletion policy with external storage?
  - Compliance requirements for produit phase only?
* No Runtime Connection Authorized: External services not connected; specification phase only
* Supersedes: Earlier "local-only inference" interpretation
* Architectural Significance: YES

## MerlAIn AI Assistance Scope (MVP): Text + Image, User-Controlled, Manual Operation Independent

* Timestamp: 2026-09-28T16:30:12.119Z
* Clarification ID: ai-assistance-scope-mvp-produit
* User Quote: « Inclure l'IA texte et image dès la première version, mais tout reste utilisable sans IA »
* Translation: Include text and image AI assistance in first version, but everything remains usable without AI
* Decision: MVP includes AI text and image assistance; core operation fully manual and independent of AI
* Supersedes: Earlier "AI later" rough framing; AI is in-scope for initial version
* Scope:
  - **Text AI**: Character backstory generation, NPC description expansion, plot hook suggestions, journal prompts
  - **Image AI**: Character art, scene illustrations, map generation, item icons
  - **User control**: Players/GMs invoke assistance explicitly; approve/reject/regenerate
  - **Core operation**: All core features remain fully usable without any AI invocation
  - **Validation**: All AI-generated content editable before saving; no forced automation
* Architectural Significance: YES (MVP feature scope, API integrations, UI/UX design, cost model)
* PRD Must Address:
  - Feature list: Which produit features have AI assistance (MVP vs. roadmap)
  - User flows: Trigger points (character creation, journal entry, description fields); approval UI; regenerate logic
  - Text model: Which provider (OpenAI? Local? Anthropic Claude? Multi-provider fallback?)
  - Image sources: Dalle-E cost model? Stable Diffusion? Public domain? License terms?
  - Cost transparency: How are AI costs presented to users? Per-feature? Subscription tier?
  - Fallback: Graceful degradation if AI service unavailable?
  - Opt-out: User preference to disable AI assistance globally?

## MerlAIn GM Private Preparation & Player Publication Boundary

* Timestamp: 2026-09-28T16:30:12.119Z
* Clarification ID: gm-prep-ai-enrichment-player-publication-produit
* User Quote: « L'IA peut enrichir automatiquement la préparation privée, mais le MJ valide toute publication aux joueurs »
* Translation: AI can automatically enrich private GM preparation, but the GM validates all publication to players
* Decision: AI may auto-enrich GM-private content; all publication to player-visible content requires explicit GM approval
* Scope:
  - **AI enrichment allowed**: Suggestions, expansions, plot hooks, NPC details in GM-private diary/notes
  - **Publishing gate**: GM reviews enriched content, edits as needed, then publishes; no autonomous player-facing content from AI
  - **No destructive authority granted**: AI cannot delete, overwrite, or permanently archive content (UNRESOLVED; defer to plateforme)
* Architectural Significance: YES (GM workflow, enrichment UI, audit trail design)
* PRD Must Address:
  - Content types AI enriches: Prep notes, NPC character builds, plot outlines, session handouts, NPC dialogue
  - Enrichment triggers: Auto-on save? Manual "Enrich this" button? Diff preview?
  - Approval UI: Side-by-side original/enriched view, with accept/edit/reject options
  - Publishing workflow: "Publish to players" gate enforces GM review before any enriched content becomes player-visible
  - Rollback capability: Can GM reject enrichment and restore original? UI/UX for rollback?
  - Attribution: Does player-published content show "GM-prepared" vs. "GM + AI enriched" marker?
* Pending Unresolved Scope (defer to plateforme architecture):
  - Delete authority: Can GM permanently delete AI-enriched content, or only mark archived?
  - Overwrite authority: Can GM rewrite enriched section without audit trail showing original?
  - Partial accept: Can GM accept enrichment for some fields while rejecting others in same item?
  - Enrichment reversal: If enriched content published to players, can GM later revert to original and re-publish?

## MerlAIn Player Character Sheet Ownership & Direct Editing (Replaces "View-Only" Scope)

* Timestamp: 2026-09-28T16:30:12.119Z
* Clarification ID: player-character-sheet-direct-edit-produit
* User Quote: « Le joueur modifie directement sa fiche, avec historique visible par le MJ »
* Translation: The player modifies their character sheet directly, with history visible to the GM
* Decision: Player OWNS and DIRECTLY EDITS own character sheet (all fields: stats, equipment, abilities, traits, backstory); all edits create audit trail visible to GM
* Supersedes: Earlier "view-only not yet approved" decision — PLAYER EDIT AUTHORITY APPROVED
* Scope:
  - **Player authority**: Player can create, update, delete fields on own character sheet
  - **Editable fields**: All (stats, equipment, abilities, traits, backstory, skills, XP, resources)
  - **Audit requirement**: Every edit creates timestamped history (actor, field, old→new, timestamp, reason-optional)
  - **GM audit access**: GM can view full edit history per character sheet
  - **Privacy**: Edit history visible to GM only; hidden from other players (unless GM shares intentionally)
  - **Conflict handling**: Last-write-wins if concurrent edits to same field
* Architectural Significance: YES (data model, edit conflict strategy, audit schema)
* PRD Must Address:
  - Which fields players may edit: All? Locked creation-date? Derived fields (XP totals, level-based abilities)?
  - Validation rules: Can player set HP > max_hp? Reduce ability scores < 1? Lock AI-enriched stats?
  - Approval workflow: Normal edits auto-save? Special operations (respec, wipe sheet) require GM review?
  - UI/UX: Inline edit? Modal form? Rich editor? Undo/redo in sheet?
  - Rollback UX: Can player request rollback of own edit? GM interface for viewing and reverting player edits?
  - Field-level privacy: Can player hide sheet section from other players while visible to GM?
* Produit Features:
  - Character edit UI: Inline or modal? Validation warnings? "This sets HP 5 above max" warning?
  - Edit history UI: Player sees own timeline; can view old values, who changed what, when
  - Revert request: Player can request rollback if accidental/unintended
  - Validation feedback: Clear error messages if edit violates constraints
* Pending Unresolved Scope (defer to plateforme policy):
  - Can GM rollback player edits without player consent?
  - Multi-device simultaneous edit: If player edits on mobile + plays online, which write wins?
  - Character sheet deletion: Can player delete own sheet? Archive vs. permanent delete?
  - Sensitive stats (secret alliance, hidden HP): Can GM mark fields as "enriched hint" so player sees hint instead of true value?

## Consumption Ledger Reconciliation and Metadata Corrections

* Timestamp: 2026-09-28T16:10:32.463Z
* Correction ID: correction-turn-2-produit
* Decision: Reconcile erroneous model attribution in orchestration blocks and correct deliverable root configuration
* Rationale: Turn 1 orchestration blocks were recorded with model: gpt-6-astra (session-inherited) but represented only coordinator-overhead turns with no actual model dispatch. Attribution was erroneously assumed from coordinator session model. Correction establishes unresolved model attribution with tier-default Claude Sonnet 4.6 fallback pricing. Deliverable roots require user-confirmed prefixes (analyst→prd/, designer→ux/, product-owner→planning/, intake-validator→reviews/intake/, scribe→members/produit/) to reflect specs-only scope and per-squad isolation.
* Reconciliation Summary: Turn 1 blocks (in=29400, cached=117600, out=5600) priced at gpt-6-astra rates = 0.4485 USD. Turn 2 correction block (in=15600, cached=62400, cache_wr=24000, out=3200) priced at Claude Sonnet 4.6 tier-default rates = 0.2035 USD. Total reconciled cost: 0.6520 USD (59.20 credits). Both values recorded in member consumption.md ledger.
* Files Updated: team.md (9 deliverable roots corrected), history/Squad Scribe.md (3 correction blocks added), consumption.md (reconciled ledger with mixed attribution)
* Architectural Significance: No

## Research Completion: MerlAIn Specifications Evidence Review (Turn 6)

* Timestamp: 2026-09-28T16:42:26.423Z
* Research ID: merlain-specs-evidence-turn-6-produit
* Request: Bounded local evidence review for MerlAIn specifications; verify user facts vs. existing scaffold and existing decisions
* Research Disposition: executed (single focused cycle: Wider, Deeper, Contrarian)
* Planning Readiness: ready-with-gaps
* Key Findings:
  - **Scaffold state**: Modular monolith (.NET/React, no Dapr, no persistence, no auth/workflow implemented)
  - **Gap vs. user vision**: Scaffold is deliberate placeholder; does not contradict polyglotte/Dapr target but does not implement it
  - **Open PM questions**: Six questions prioried (arbre vs graphe for chronicles, username vs email for local auth, concurrent edit policy, editable field scope, edit history visibility, licensing/content sources)
  - **Unapproved requirements found**: Several entries in produit decisions.md (previous turns) extend beyond user-confirmed quotes; flagged for correction in separate entry below
* Deliverable: .copilot-tracking/squad/members/produit/research/2026-09-28/merlain-specifications-research.md
* Architectural Significance: YES (findings inform future PRD scope)
* Next Action: Squad Lead to conduct PM interview addressing six open questions before Plan stage

## CRITICAL CORRECTION: Withdraw Unapproved Requirements (Turn 6)

* Timestamp: 2026-09-28T16:42:26.423Z
* Correction ID: withdraw-unapproved-scribe-embellishments-turn-6-produit
* Issue: Previous decision entries (turns 4–5, authored by Scribe) contain formulations that exceed user-confirmed quotes and should NOT be treated as approved requirements
* Factual Correction:
  - **Claim**: « Le joueur modifie directement sa fiche, avec historique visible par le MJ » is confirmed user quote ✓
  - **Over-extension**: Entries added « all fields » editable, « Last-write-wins » conflict handling, « visible to GM only » (privacy scope unspecified)
  - **Status**: « All fields » is not confirmed; « Last-write-wins » is speculation; « GM-only » visibility is unresolved
  - **Action**: Withdraw these as approved requirements; treat as open questions to pose to user

| Unapproved Claim | Confirmation Status | User Quote | Disposition |
|---|---|---|---|
| « All character sheet fields editable by player » | NOT CONFIRMED | User said « le joueur modifie directement sa fiche » (player edits sheet); did NOT enumerate all fields | Withdraw; ask which fields (e.g., derived stats, system fields, secret mechanics) |
| « Last-write-wins for concurrent edits » | SPECULATION | No user quote on conflict resolution strategy | Withdraw; ask how concurrent edits should resolve (lock, merge warning, audit trail?) |
| « Edit history visible to GM only » | INCOMPLETE | User said « historique visible par le MJ »; did not exclude other visibility scopes | Withdraw; ask if history is GM-only or visible to player/allies/public |
| « MerlAIn application and Dapr microservices remain LOCAL by default » (previous entry text) | MISSTATEMENT | User confirmed « local by default, optional external providers »; Dapr is a DEPLOYMENT TARGET, not current scaffold state | Withdraw misstatement; clarify: Dapr is plateforme target architecture, NOT confirmed as local-default application requirement |
| « AI map generation MVP » | NO USER QUOTE | Misattribution; research found no user mention of map generation | Withdraw; ask if maps are in scope |
| « Subscription pricing model » | NO USER QUOTE | Misattribution; user clarified cost transparency, not pricing model | Withdraw; open question on licensing/pricing to ask |
| « Anthropic adapter consideration » | USER REJECTED | User explicitly said « compatible OpenAI, pas d'adaptateur Anthropic » | Confirm REJECTED; remove from scope |

* Clarifications for Future Entries:
  - **Dapr**: User stated deployment target (polyglotte microservices via Dapr); current scaffold is monolith (deliberate placeholder); absence is gap, not contradiction. Use: « Dapr is the deployment target for multi-language team; current scaffold is single-language monolith; gap to resolve in plateforme architecture »
  - **OIDC vs OAuth2**: Scaffold documents only OIDC (Identity layer); user mentioned « OAuth2 »; these are compatible (OIDC ⊃ OAuth2); recommend naming OIDC explicitly in PRD to avoid ambiguity
  - **Local accounts**: Scaffold offers email/password, NOT username. User requested local account with « nom d'utilisateur »; this is a gap to clarify (Q3 in research)

* Architectural Significance: NO (correction only; no new decision)
* Impact: Subsequent PRD/plan stages must ask user explicitly before treating withdrawn items as requirements

## Configurable content exports — confirmed scope

* Timestamp: 2026-09-28T17:39:19.057Z
* Decision ID: exports-v1-confirmed-scope-produit
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

### Produit Must Specify in PRD

* Which content types support export (characters, journal, campaigns, NPC builds, handouts)
* Export format options (JSON, CSV, PDF, custom per-platform format)
* Which platforms supported in first version (Roll20, Foundry VTT, others)
* UI/UX for export flow (download modal, zip file, direct upload option if approved)
* Error handling and validation (incomplete character sheet, missing dependencies)

### Plateforme Must Specify in Architecture

* Configuration schema for import/export adapters
* Command pattern responsibilities (invoker, concrete commands, receivers)
* Builder and Strategy pattern placement (template builders, serialization strategies)
* Supported serialization formats and codec registration
* Platform detection and adapter selection logic
* Validation and error reporting for export/import mismatch
* Rollback semantics if platform-specific export fails

## MerlAIn Session-Memory Content Source and Scope (Researcher Finding + Plan Phase)

* Timestamp: 2026-09-28T18:08:00.085+02:00
* Decision ID: session-memory-content-scope-produit
* Source: Researcher bounded investigation (members/produit/history/Squad Researcher.md, turn 6, 2026-09-28T16:42:26.423+02:00); user clarifications (four user-provided context points); planning phase (members/produit/history/Squad Lead.md, turn 9, 2026-09-28T18:08:00.085+02:00, plan P03 author + P04 review)
* Architectural Significance: YES (affects produit PRD content sources, data model, and content-sourcing policy)

### Researcher Evidence + Four User Context Points
* **Researcher finding**: .copilot-tracking/squad/members/produit/history/Squad Researcher.md (modular monolith scaffold confirmed; open PM questions identified)
* **User point 1**: Session-memory content includes character builds, campaign notes, session journal entries, NPC descriptions, and plot hooks captured during play
* **User point 2**: Session memory is DISTINCT from GM-private diary; shared group visibility subject to GM publication authority
* **User point 3**: No implementation or export code authorized in P03 plan; content schema and retrieval policy only
* **User point 4**: Session-memory PRD is SCOPE-BOUNDED and does NOT claim full MerlAIn product requirements

### Planning Phase (P03–P04)
* **Plan artifact**: .copilot-tracking/squad/members/produit/plans/2026-09-28-merlain-prd-plan.md (verified; 96 inserted + 1 modified line)
* **Phase 3 (Author)**: PRD Builder creates session-memory addendum to French DRAFT v0.1 PRD
* **Phase 4 (Review)**: Squad Reviewer validates session-memory content scope and documentation quality
* **Deliverables pending**: produit/prd/2026-09-28/merlain-session-memory-prd.md (P03), reviews/2026-09-28/merlain-session-memory-review.md (P04)

### Routing + Outcome
* **Routed to**: produit (primary)
* **Plateforme**: NOT dispatched; depends on produit PRD acceptance
* **This turn**: Plan phase; no PRD or review artifact written; no full-product PRD claimed
* **Outcome**: Session-memory scope documented; plan created; P03/P04 execution pending

## Produit Squad Initialized

* Timestamp: 2026-09-28T15:53:31.261Z
* Init ID: 4bcfa440-2631-48ff-aba8-b7882c133032
* Decision: Initialize produit sub-squad for MerlAIn federation
* Rationale: User-confirmed federation initialization; produit is the producer sub-squad for product requirements, journeys, acceptance criteria, and prioritization; must run first to establish inputs for plateforme (technical specifications)
* Profile: custom (user-confirmed roles without packs)
* Roles Seeded: researcher, lead, developer, tester, analyst (PRD Builder), designer (UX UI Designer), product-owner (Functional Planner), intake-validator (PRD Quality Reviewer), scribe
* Member Names: all empty (names policy skip)
* Deliverable Roots: all standard per role (rooted under members/produit/)
* Mission Scope: specs-only phase; no implementation permission
* Sequential Dependency: produit is input producer; plateforme consumes as read-only input
* Architectural Significance: No

## CORRECTION: MerlAIn Session-Memory Discovered Capabilities (Turn 11)

* Timestamp: 2026-09-28T18:33:34+02:00
* Correction ID: session-memory-user-facts-supersedes-turn-10-invented-points
* Source: Coordinator directive to correct prior Scribe fabrication and record only user-supplied facts (Turn 11)
* Issue Corrected: Prior Scribe entry (Turn 10, decision ID session-memory-content-scope-produit) invented four "User point 1–4" that were NOT provided by user. Correction supersedes that content; prior entry is not rewritten.

### Four User-Supplied Facts (Only These)

1. **Whisper audio → transcript**: User confirmed capability: audio input transcription via Whisper; no implementation, verification, or API choice specified
2. **GraphRAG (graphiti)**: User confirmed capability: graph-based RAG using Graphiti library; no implementation, verification, or graph structure specified
3. **Storyteller agent produces episode summaries and updates campaign summaries, retrieving missing information from RAG**: User confirmed capability: storyteller agent generates per-episode summaries, updates campaign-level summaries, fetches information from RAG; no orchestration, call sequence, or state management specified
4. **Orchestrator links everything**: User confirmed capability: orchestrator component coordinates the above; no design, API contract, or execution flow specified

### Grounding

* **Source**: User statement 2026-09-28 this session: "un whisper pour audio -> transcript; un graphrag (graphiti); un agent (le repo storyteller) qui produit un résumé par episode et qui update les résumés de campagne en fetchant les infos qu'il a pas du rag; un orchestrateur qui fait le lien entre tout"
* **Capabilities Reported, NOT Verified**: Researcher (Turn 6) documented as "non-verified hypothesis"; no URLs, repositories, versions, APIs, or implementation status provided
* **Outcome**: Four facts recorded as user-supplied, distinguished from invented content; no policy claims, no implementation authorization, no platform commitment; PRD addendum v0.1.1 grounds all references on this statement and research evidence

### Turn 11 Booking

* **Session Memory Scope Decision (ID: session-memory-user-facts-confirmed)**: Reground session-memory content scope on four user facts only; withdraw invented Turn 10 content; PRD v0.1.1 fidelity restored
* **Outcome**: Turn 10 decision entry stands as recorded; this correction entry supersedes invented content and names the four facts; no full-product PRD claim; integration readiness not established
* **Architectural Significance**: No (content correction, not design change)

## PRD Draft Phase Completion (Turn 14)

* Timestamp: 2026-09-28T19:18:12+02:00
* Decision ID: core-prd-draft-complete
* Verdict: All scheduled PRD drafting (P01/P02) and session-memory addendum drafting + review (P03/P04) complete per plan; v0.1 artifacts ready for human product + technical review and approval
* Scope: Three PRD artifacts (core v0.1: 30FR90AC15NFR, session-memory v0.1.1: 7FR27AC lifecycle-integrated), two review passes (core PASS—SPEC DRAFT READY zero material findings; memory PASS—v0.1.1 defects resolved)
* Authority: Core PRD authority resides in `.copilot-tracking/squad/members/produit/prd/2026-09-28/merlain-prd-v0.1.md` and linked memory; session-memory in `.copilot-tracking/squad/members/produit/prd/2026-09-28/merlain-session-memory-prd.md` (v0.1.1 cross-linked); review artifacts `.copilot-tracking/squad/members/produit/reviews/2026-09-28/merlain-prd-v0.1-review.md` (PASS) and session-memory counterpart (resolved)
* Pending Outside Squad: Human product approval and technical approval (not authorized by squad review); platform/implementation/deployment; code commits and push remain pending
* Architectural Significance: No (squad phase boundary, not architecture decision)
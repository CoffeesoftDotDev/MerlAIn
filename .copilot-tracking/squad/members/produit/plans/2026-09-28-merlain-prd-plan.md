# RPI Plan: MerlAIn PRD Draft v0.1 (Produit)

## Task Metadata

* Task ID: PRODUIT-PRD-V0.1 | Task slug: merlain-prd-plan | Plan date: 2026-09-28

## Executive Summary

* Bottom line: authors and reviews ONE French DRAFT v0.1 PRD for MerlAIn using only user-confirmed scope from this prompt and the [research artifact](../research/2026-09-28/merlain-specifications-research.md); no platform/architecture/AI-contract design, no code, no deployment.
* Why this matters: gives the user a discussable, honest draft with unknowns visibly labeled instead of a prematurely "final" spec, correcting prior embellishments in [decisions.md](../decisions.md).
* Planning result: Complete for a document-authoring plan; NOT a technical implementation plan.
* Confidence and uncertainty: high on scope this prompt confirms directly; deliberately preserved uncertainty on chronicle graph shape, sheet-edit field policy, journal-moderation granularity, template/incarnation approval, import-rights scope, player-side AI use.

### What You May Not Know

* `decisions.md` contains unsupported extensions ("all fields" editable, "last-write-wins," "visible to GM only," Dapr asserted active) already flagged non-cited by the [research artifact](../research/2026-09-28/merlain-specifications-research.md) (C12, D5–D7, D12); this prompt supersedes them, not carried forward as requirements.
* Personal book ownership does not itself grant reproduction/redistribution rights; the PRD records the import workflow as a request with a rights prerequisite, not a resolved capability.
* Only two roles produce artifacts this stage: analyst (PRD Builder) authors, tester (Squad Reviewer) reviews; no architecture/security/platform role is dispatched.

## Phase Checklist

```mermaid
%%{init: {"themeVariables": {"fontFamily": "Arial, Helvetica, sans-serif", "fontSize": "16px"}}}%%
flowchart LR
    R["research/2026-09-28/…research.md"] -->|grounds| D["Added: prd/…-merlain-prd-v0.1.md"]
    D -->|reviewed by tester| V["Added: reviews/…-merlain-prd-v0.1-review.md"]
    classDef new stroke-dasharray: 5 5
    class D,V new
```

Before: only the research artifact exists. After: one DRAFT v0.1 PRD and one review pass exist; no other artifact is produced this stage.

| Deliverable | Owning role | Path (root fixed by [team.md](../team.md); filename by dispatch) | Depends on |
|---|---|---|---|
| DRAFT v0.1 PRD (French) | analyst (PRD Builder) | `.copilot-tracking/squad/members/produit/prd/2026-09-28/merlain-prd-v0.1.md` | this plan + research |
| Review pass on DRAFT v0.1 | tester (Squad Reviewer) | `.copilot-tracking/squad/members/produit/reviews/2026-09-28/merlain-prd-v0.1-review.md` | DRAFT v0.1 PRD |

<!-- rpi:phase id=P01 -->
### [ ] P01: Author DRAFT v0.1 PRD

Goals:
* One internally consistent French DRAFT v0.1 PRD covering only confirmed scope, unresolved items visibly labeled (creates node `D`; per-phase diagrams condensed to the single pair above per PD1).

Dependencies:
* None (research artifact already supplied).

<!-- rpi:task id=P01-T01 -->
#### [ ] P01-T01: Vision, journeys, confirmed requirements, modules, glossary

Goals:
* A reader knows who v0.1 is for (GM, players), their journeys, and has FR-ided acceptance criteria for confirmed behaviors, a conceptual module map, and a glossary with proposal-labeled confidentiality semantics.

Requirements:
* FR-001–FR-005. No invented market/persona claims; module map stays conceptual (no service boundaries, APIs, or schema — platform-owned).

Details:
* Journeys: GM (setup, private prep, publication) and player (consult, own-sheet edit, cards, journal). Current modular-monolith code is a placeholder gap toward the confirmed target, not grounds to reopen it.
* FR-ided behaviors: local auth (username-or-email + password, or OIDC); D&D 5e 2014/2024 and The One Ring 2e free-form edition-adapted sheets (no rules engine); Chronique = one scene/event, branches may reconverge; character library (custom/official-reference/reusable PC, placement-optional); personal-book import → sheet extraction → human validation → private use only; player cards vs full sheet; direct player sheet edit with GM-visible history; shared journal direct-publish + GM moderation, no MJ preapproval; private idea diary separate; AI text+image MVP, manual-core, MJ-validated auto-enrichment, no AI overwrite/delete authority.

References:
* [research/2026-09-28/…research.md](../research/2026-09-28/merlain-specifications-research.md): confirmed vs non-supported items (D1–D12).
* [decisions.md](../decisions.md): entries to exclude as unsupported extensions (C12).

Dependencies:
* None.

<!-- rpi:task id=P01-T02 -->
#### [ ] P01-T02: Exclusions, priorities, unresolved register, next question

Goals:
* A reader sees what v0.1 excludes, what is prioritized as requested, which open items block a final PRD vs. only the draft, and one recommended next question.

Requirements:
* FR-006–FR-008. No invented percentages, concurrency, or hardware numbers; priorities read "included as requested," no invented phases.

Details:
* Exclusions: rules-automation engine, confirmed maps generation, subscription economics, confirmed edit-conflict policy, bundled copyrighted library, DRM bypass, assumed scan/PDF/OCR pipeline.
* Unresolved register (blocking-to-final-PRD vs nonblocking-to-draft): import rights/consent (blocking); template/incarnation separation, chronicle graph/cycles, sheet field-edit policy, journal-moderation granularity, player-side AI use (all nonblocking); provider-endpoint compatibility (platform-owned).
* Recommend labeled validation measures (e.g., a journey walkthrough), no numeric targets. End with exactly ONE next question: "For v0.1, should sheet edits require any GM-visible confirmation step, or does GM-visible history alone satisfy your intent?"

References:
* [research/2026-09-28/…research.md](../research/2026-09-28/merlain-specifications-research.md): open-question inventory (M-priority items).

Dependencies:
* P01-T01.

<!-- rpi:phase id=P02 -->
### [ ] P02: Review DRAFT v0.1 PRD

Goals:
* Confirm the draft states only user-confirmed scope, labels every unknown, and introduces no platform/architecture/AI-contract design (creates node `V`).

Dependencies:
* P01 (T01–T02) complete.

<!-- rpi:task id=P02-T01 -->
#### [ ] P02-T01: Fidelity and scope-boundary review

Goals:
* A pass/flag list confirms the draft matches this prompt's decisions and does not reintroduce `decisions.md` embellishments or preempt platform work.

Requirements:
* FR-001–FR-008 all cited by at least one draft section; no FR presents an unconfirmed capability as settled.

Details:
* Check PROPOSAL labels are present where required, the blocking/nonblocking split matches this plan, and no architecture/security/AI-contract content appears.

References:
* [research/2026-09-28/…research.md](../research/2026-09-28/merlain-specifications-research.md): non-supported-claims list to re-check against.

Dependencies:
* P01-T01, P01-T02.

## User Decisions and Requirements

### Confirmed User Direction

* Admin-composable front/server, polyglot Dapr microservices, local Docker Compose only (no remote MerlAIn deploy); admin may attach external Redis, PostgreSQL, Authentik/other OIDC, Ollama or other OpenAI-compatible AI (no Anthropic-specific adapter); provider-compatibility specifics stay platform-owned; current modular-monolith code is a placeholder gap, not grounds to reopen the target.
* Local auth accepts username OR email + password, or OIDC; uniqueness, parsing, and account-linkage policy NOT authorized.
* Campaign systems: D&D 5e 2014/2024 and The One Ring 2e, freely-filled edition-adapted v1 sheets, NOT a rules-automation engine. Chronique = one precise scene or event; branches MAY reconverge; cycles undecided; no mandatory arc/scenario layer.
* Character library: custom/official-reference/reusable PCs, placement-optional; template/incarnation separation is PROPOSED only; official source v1 = import personal books → extract sheets for human validation → private use only; ownership ≠ unlimited rights; no bundled copyrighted library, prose reproduction, DRM bypass, or assumed scan/PDF/OCR support.
* GM/players differ in visibility; players see GM-published content, own sheet, and equipment as CARDS, not the full sheet; players DIRECTLY edit their own sheet, history VISIBLE TO GM (not GM-only), field/derived/secret policies undecided; shared journal is direct-publish + GM moderation with no MJ preapproval; private idea diary stays separate.
* AI text+image in MVP; core fully manual without AI; AI may auto-enrich private GM prep but MJ validates before publication, no AI overwrite/delete authority, not all generation requires manual trigger, player-side AI use undecided; maps generation, subscription economics, edit-conflict policy, and full field authority are NOT confirmed.
* `decisions.md` entries asserting "all fields" editable, "last-write-wins," "GM only," and active Dapr microservices are embellishments without user citation; this prompt supersedes them.

### Planning Decisions and Feedback

| Group | Decision or feedback item | Status | Owner | Rationale or input needed | Evidence | Planning impact |
|---|---|---|---|---|---|---|
| PD1 | Per-phase diagrams condensed into one Before/After pair | confirmed (agent) | agent | Document-only deliverable has no distinct per-phase software components | rpi-plan template guidance | Phase Checklist format only |
| PD2 | Import-rights/consent prerequisite for official character source | unresolved, blocking-to-final-PRD | user | Needs explicit user rights/validation posture; no copyrighted examples reproduced | This prompt | P01-T02, unresolved register |
| PD3 | Other nonblocking-to-draft items: template/incarnation, chronicle graph/cycles, sheet field policy, journal-moderation granularity | unresolved, nonblocking | user | Draft may present each as a labeled proposal/unknown | This prompt, research | P01-T01, P01-T02 |

## Planning Readiness and Next Step

| Field | Record |
|---|---|
| Planning execution and readiness | Complete and Ready for analyst dispatch to author DRAFT v0.1; NOT ready for a final approved PRD or any platform/architecture work |
| Decision participation | user-owned; interactive mode; unresolved items recorded rather than exhaustively re-asked, per explicit user instruction |
| Blockers | None to producing DRAFT v0.1; PD2 (import rights/consent) blocks a FINAL approved PRD |
| Latest critique | Not run — user prompt explicitly prohibits further agent invocation this stage; no closure critique performed |
| Relevant research | [research/2026-09-28/merlain-specifications-research.md](../research/2026-09-28/merlain-specifications-research.md) |
| Plan | `.copilot-tracking/squad/members/produit/plans/2026-09-28-merlain-prd-plan.md` |
| Changes-record role | Not applicable — no code/implementation stage authorized this run |
| Continuation owner | Squad Coordinator (dispatches analyst then tester); required gate is the Artifact Gate (plan on disk + Scribe history entry, Scribe-owned) |
| Next action | Dispatch analyst (PRD Builder) for P01, then tester (Squad Reviewer) for P02; STOP after this plan per user instruction |

## Scope and Non-Goals

### In Scope

* PRD sections: vision/problem evidence, audience/jobs/journeys, FR-ids with acceptance criteria for confirmed items, conceptual module/permission map, conceptual data glossary, confidentiality/publication semantics (labeled proposal where unapproved), MVP exclusions, priority baseline, unresolved register, recommended validation measures, one next interview question.

### Non-Goals

* No architecture, security, or AI-contract design (platform-owned, post product-acceptance).
* No technical council, platform design, code, or deployment authorized this stage.
* No final/approved PRD — DRAFT v0.1 only.
* No resolution of provider-endpoint compatibility, import-rights terms, or sheet-edit concurrency policy.

## Functional Requirements

* FR-001: PRD states vision/problem evidence sourced only from user-provided statements.
* FR-002: PRD documents GM and player audiences, jobs, and journeys across campaign setup, chronicles, character library, journal.
* FR-003: PRD assigns FR-ids and acceptance criteria to confirmed functional behaviors only (auth, campaign systems/sheets, chronicle unit, character library, import workflow, cards, sheet editing, journal, AI scope).
* FR-004: PRD presents a conceptual module/capability map (admin vs campaign vs user permissions) without technical design.
* FR-005: PRD includes a conceptual data glossary and labels confidentiality/publication semantics as proposals unless this prompt confirms them.
* FR-006: PRD lists MVP exclusions and a priority baseline of "included as requested," inventing no phases, percentages, concurrency, or hardware numbers.
* FR-007: PRD carries an unresolved register distinguishing blocking-to-final-PRD from nonblocking-to-draft items.
* FR-008: PRD ends with exactly one recommended next interview question.

## Risks and Open Questions

| Priority | Type | Risk, question, or planning item | Affected work | Impact | Smallest action or evidence needed | Owner |
|---|---|---|---|---|---|---|
| H | risk | Import-rights/consent for personal-book character extraction is unresolved | P01-T02 register | Blocks a final PRD; must not be promised as delivered | Explicit user rights/validation posture | user |
| M | open question | Chronicle graph/cycles and sheet field-edit/secret authority policy | P01-T01, P01-T02 | Affects glossary and acceptance-criteria depth only for v0.1 | User answer or defer as labeled unknown | user |
| L | further planning | Template/incarnation separation remains a proposal | P01-T01 | Presented as proposal, not requirement | None needed for v0.1 draft | planner |

## Sources

* [research/2026-09-28/merlain-specifications-research.md](../research/2026-09-28/merlain-specifications-research.md): user-citation evidence, confidence notes, non-supported-claims list (C12, D1–D12); sole evidence source.
* [decisions.md](../decisions.md): prior entries with embellishments this prompt withdraws, not carried forward.
* [team.md](../team.md): confirms analyst = PRD Builder (`prd/` root) and tester = Squad Reviewer (`reviews/` root).
* This prompt (primary user, 2026-09-28): governs over research and prior decisions where they conflict.

## Critique Disposition

* Not run — user prompt explicitly prohibits further agent invocation this turn; no attempt consumed, no closure critique performed.

## Follow-Up Items

* None.

## Handoff

* Authoritative implementation handoff: Planning Readiness and Next Step. No `/rpi-implement` — this run authorizes document authoring (analyst) and review (tester) only, not code implementation.

<!-- markdownlint-disable-file -->
# RPI Plan: MerlAIn Ruleset RAG MVP

## Task Metadata

* Task ID: MERLAIN-RULESET-RAG-MVP
* Task slug: ruleset-rag-mvp
* Plan date: 2026-11-12

## Executive Summary

* Bottom line: Plan a first MerlAIn retrieval-augmented-generation capability for one game ruleset, without claiming a complete rules engine.
* Why this matters: It gives a game master useful, source-grounded ruleset retrieval while preserving MerlAIn's access and content boundaries.
* Planning result: Blocked by user direction; the user selected The One Ring 2e, confirmed authorization for indexing and RAG retrieval of the local Core Rules corpus, and directed that production planning wait for issues #3, #4, and #5. The shared gates remain open.
* Confidence and uncertainty: The corpus authorization and production-only boundary are recorded. Technical contracts and implementation details must be planned after the shared policy, contract, and foundation work is resolved; no rulebook content has been copied into the repository.

### What You May Not Know

* The root [README](../../../README.md) says this branch is specifications-only: the former application, runtime, and build tooling were removed. The [readiness guide](../../../docs/implementation-readiness.md) requires policy, contracts, and local foundations before implementation; old scaffold paths are not current architecture.
* The core PRD excludes a full rules engine and requires retrieval to stay within the requesting audience's authorized scope. It does not choose a rules corpus, RAG architecture, or implementation stack.
* Personal ownership of a rulebook does not by itself establish permission to reproduce or redistribute its content. The user explicitly confirmed authorization for indexing and retrieval of the identified local copy of The One Ring Core Rules (third printing); this is a user-provided authorization record, not an independent legal determination.
* The readiness issues are still open: [#3 policies](https://github.com/CoffeesoftDotDev/MerlAIn/issues/3), [#4 contracts and ownership](https://github.com/CoffeesoftDotDev/MerlAIn/issues/4), and [#5 local Compose/Dapr foundations](https://github.com/CoffeesoftDotDev/MerlAIn/issues/5). The readiness guide makes these prerequisites for application implementation.

## Phase Checklist

Implementation phases and diagrams are not drafted yet. The user selected The One Ring 2e, confirmed the corpus authorization, and directed that production planning wait for shared readiness issues #3, #4, and #5.

## User Decisions and Requirements

### Confirmed User Direction

* Deliver a first MVP for RAG capabilities over a game ruleset.
* Prepare the technical implementation plan before writing application code; implementation remains gated on a complete plan and explicit user approval.
* Use The One Ring 2e for the first MVP; the user identified a local copy of its Core Rules (third printing) as the candidate corpus and confirmed explicit authorization for machine indexing and RAG retrieval, including relevant excerpts.
* Build the production feature only after issues #3, #4, and #5 are resolved; do not create a separate local prototype.

### Planning Decisions and Feedback

| Group | Decision or feedback item | Status | Owner | Rationale or input needed | Evidence | Planning impact |
|---|---|---|---|---|---|---|
| D1 | Confirm authorization to index and retrieve from the candidate The One Ring 2e Core Rules corpus | Confirmed by user | User | A local copy alone would not establish permission; the user explicitly confirmed authorization for machine indexing and RAG retrieval, including relevant excerpts. | User answer in this session; [core PRD](../../squad/members/produit/prd/2026-09-28/merlain-prd-v0.1.md), E05 | The rights gate is resolved; source-specific technical planning resumes after the shared readiness gates. Do not copy source content into the repository. |
| D2 | Decide whether the ruleset RAG MVP follows shared readiness gates or is an isolated prototype | Confirmed: wait for issues #3, #4, and #5; then plan the production feature | User | Bypassing the shared policies, contracts, or local foundation can create an incompatible production architecture. | User answer in this session; [issue #3](https://github.com/CoffeesoftDotDev/MerlAIn/issues/3), [issue #4](https://github.com/CoffeesoftDotDev/MerlAIn/issues/4), [issue #5](https://github.com/CoffeesoftDotDev/MerlAIn/issues/5) | No standalone prototype; resume technical planning after the shared gates are resolved. |

## Planning Readiness and Next Step

| Field | Record |
|---|---|
| Planning execution and readiness | Blocked by user direction; the user selected production-only sequencing after issues #3–#5, which are still open. |
| Decision participation | User-owned; standalone manual planning, authorized by the user's choice to prepare a plan first. |
| Blockers | Open policy, technical-contract, and foundation issues #3–#5; production planning is explicitly deferred until they are resolved. |
| Latest critique | Not run; the plan is not implementation-ready. |
| Relevant research | Existing PRD and readiness evidence are sufficient to identify current blockers; source-specific technical research is deferred until the shared readiness gates are resolved. |
| Plan | `.copilot-tracking/plans/2026-11-12/ruleset-rag-mvp-plan.md` |
| Changes-record role | `.copilot-tracking/changes/2026-11-12/ruleset-rag-mvp-changes.md` will be implementation evidence if implementation is approved. |
| Continuation owner | User (standalone manual RPI). |
| Required gates or confirmations | D1 and D2 are confirmed by the user; issues #3–#5 must be resolved; user approval of the implementation-ready plan is required before code changes. |
| Next action | Wait for issues #3–#5 to be resolved, then resume `/rpi-plan` to define and critique the production implementation plan. |

## Goals

* Provide a usable first RAG capability grounded in The One Ring 2e rules text.
* Preserve source provenance and the retrieval access boundary already specified for MerlAIn.

## Scope and Non-Goals

### In Scope

* Plan a production MVP for The One Ring 2e using the user-authorized Core Rules corpus, after shared issues #3–#5 are resolved.
* Define the smallest implementation and validation boundary against the accepted technical contracts.

### Non-Goals

* A complete rules engine, automated character validation, or rules adjudication.
* Session-memory audio transcription, Graphiti campaign memory, Storyteller summaries, or orchestration.
* Indexing multiple rulesets before the first MVP boundary is agreed.
* A separate non-production local prototype.
* Recreating the removed application scaffold or asserting a framework or service architecture not selected by the technical owner.

## Functional Requirements

* FR-001: The first MVP provides RAG capabilities over the user-selected The One Ring 2e Core Rules corpus; detailed retrieval behavior is deferred until the accepted technical contracts are available.
* FR-002: Retrieval, excerpts, sources, and derived responses must remain within the requesting user's authorized scope, consistent with FR-CORE-024.

## Non-Functional Requirements

* NFR-001: No unauthorized or cross-campaign content is exposed through retrieval or derived output; use the PRD's NFR-CORE-006 corpus-based security condition rather than claiming an absolute guarantee.

## Risks and Open Questions

| Priority | Type | Risk, question, or planning item | Affected work | Impact | Smallest action or evidence needed | Owner |
|---|---|---|---|---|---|---|
| High | Dependency | Policies #3, technical contracts #4, and foundations #5 remain open; the checkout has no application code or runtime. | Plan-wide | Production RAG planning or implementation before these gates could conflict with the shared architecture. | Resolve the shared issues; then resume the production plan as directed by the user. | Owners of issues #3–#5 |

## Dependencies

* User-identified The One Ring 2e Core Rules corpus and explicit authorization confirmation: available for the later technical plan; keep the local source outside version-controlled project files.
* Technical contracts and local foundations: needed to choose compatible application boundaries, runtime, storage, and reproducible validation.

## Sources

* [README](../../../README.md): confirms that the current branch has no application code, runtime, or build tooling.
* [Implementation readiness](../../../docs/implementation-readiness.md): identifies policy, technical-contract, and foundation prerequisites before implementation.
* [Core PRD](../../squad/members/produit/prd/2026-09-28/merlain-prd-v0.1.md): FR-CORE-024 and NFR-CORE-006 define retrieval authorization boundaries; CON-CORE-001/002 and E05/E10 constrain implementation readiness; section 10 excludes a complete rules engine.
* [Session-memory PRD](../../squad/members/produit/prd/2026-09-28/merlain-session-memory-prd.md): distinguishes campaign-memory RAG from ruleset retrieval and records source, rights, and provenance as unresolved.
* User-provided corpus reference: a local copy of The One Ring Core Rules (third printing); the user explicitly confirmed authorization for machine indexing and RAG retrieval, including relevant excerpts. Source path and book content are intentionally not copied into this plan.
* Live issue status checked during planning: [#3](https://github.com/CoffeesoftDotDev/MerlAIn/issues/3), [#4](https://github.com/CoffeesoftDotDev/MerlAIn/issues/4), and [#5](https://github.com/CoffeesoftDotDev/MerlAIn/issues/5) are all open. The user directed that production planning begin after these issues are resolved.

## Critique Disposition

* Critique candidate identity: MERLAIN-RULESET-RAG-MVP; initial scoping state, not an implementation-ready candidate.
* Critique depth and provenance: Standard; default.
* Critique execution: Not run; unresolved material decisions prevent readiness.
* Initial attempt consumed: No.
* Recovery attempt consumed: No.
* Attempt provenance: Not applicable.
* Recovery eligibility and consent: Not applicable.

## Follow-Up Items

* None.

## Handoff

* Implementation handoff is not ready. Wait for issues #3–#5 to be resolved, resume `/rpi-plan`, and obtain explicit approval of the complete plan before invoking `/rpi-implement`.

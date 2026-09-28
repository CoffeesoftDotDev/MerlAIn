---
review_id: merlain-prd-v0.1-review
prd_under_review: merlain-prd-v0.1
review_type: P02-closing-draft
reviewer_role: tester-squad-reviewer
date: 2026-09-28T19:10:59+02:00
verdict: Pass
scope_reviewed: "FR-CORE-001 to FR-CORE-030 (30 FR); AC-CORE-001 to AC-CORE-090 (90 AC); NFR-CORE-001 to NFR-CORE-015 (15 NFR); CON-CORE-001 to CON-CORE-006; R01-R12 recommendations; E01-E11 evidence backlog. Session-memory addendum (S4) scope reconciliation: 7 FR-MEM, 27 AC-local, verified distinct from core PRD scope."
---

# P02 Closing Review: MerlAIn Core PRD v0.1

## Verdict

**PASS** — Spec draft ready for user review.

## Scope Reviewed

- Main PRD: FR/AC/NFR coverage 30/90/15 (100% to FR) with 12 recommended conditions (R01–R12).
- Derivation: Confirmed scope (S1, intake Ready-With-Gaps), research evidence (S2), plan structure (S3).
- Companion: Session-memory addendum (S4) distinct 7 FR-MEM / 27 AC; no scope bleed.
- Constraints: Six constraints (CON-CORE-001 to 006) enforce local deployment, OIDC allowed (no AD ban), no forced IA, human-validated imports, download-only exports, no arbitrary code.
- Non-blocking backlog: 11 evidence/approval gates (E01–E11) explicitly marked non-blocking for draft.

## Findings

**ZERO material findings.**

- ✓ Confirmed scope (S1) fully translated: 3 editions, MJ prep, player participation, library/incarnations, PDF/OCR, IA enrichment, exports, all journeys (J1–J5) covered.
- ✓ Permissions matrix (section 6) correctly separates: MJ private, player direct edit, shared journal, audience-gated AI publication, cross-campaign isolation.
- ✓ Contradictions checked: R01–R12 recommendations nonconflicting; AC-CORE-002/005 verified by documentary examination (module disclosure vs. role-granting); AC-CORE-004/008 confirmed: local username/email login preserved; email alone never authorizes account linking; R01 proposes unique email for login (R01 choice for approval, not V1 enforcement).
- ✓ Ambiguities verified as intentional: cycles (proposed default forbidden V1 per R03 choice for approval, open future); episode definition (deliberately TBD, R12 suggests one interpretation only); field-edit scope (flagged as R04 RECOMMANDATION A VALIDER); player-side IA (proposed exclusion V1 per R09 choice for approval, reexamine later).
- ✓ Access/recovery/export correctness: AC-CORE-030/071/081 enforce per-campaign isolation; AC-CORE-085-087 preserve manual fallback; AC-CORE-088-090 distinguish backup from VTT paquet; AC-CORE-025-027 (library) and AC-CORE-052-060 (import/rights) require human validation before diffusion.
- ✓ Draft/proposal status explicitly marked: CONFIRME (user-confirmed), CIBLE SPECIFIEE (testable behavior proposed), RECOMMANDATION A VALIDER (choice for approval), INTEGRATION NON VERIFIEE (component verification pending). No falsely signed-off promises.
- ✓ Source attribution: S1 (user decision) prioritized over S2/S3 (research/plan); existing modular-monolith scaffold is implemented V1 reality (distinct from target polyglot Dapr, user-confirmed but not implemented); prior unsupported extensions (Anthropic AI, "last-write-wins," "all fields editable") correctly excluded per decisions.md review.
- ✓ Session-memory reference (FR-CORE-028): AC-CORE-082 correctly renvoie to FR-MEM-001 to FR-MEM-007; both (core PRD and addendum) independently complete; no duplicate FR-CORE and FR-MEM namespacing.
- ✓ Proof backlog (section 11.2): E01–E11 (identity/policy/component verification/compatibility/rights/privacy/performance/backup/provider/architecture/independent review) listed with blocking stage (next handoff, not this draft) and proposed owner. Nonblocking for completeness.

## Conditions Honored

- S1 user decision (scope confirmed): local Compose, 3 editions, MJ+players, free-form sheets, library/incarnations, direct player edit, journal moderation, PDF/OCR, IA optional/validated, exports-only, no forced integrations. ✓
- Intake assessment (Ready-With-Gaps): zero drafting blockers. ✓
- No invented persona claims, market numbers, concurrency, hardware, SLO targets, commercials, or delivery dates. ✓
- All FR-to-AC (100%) and FR-to-Goal (100%) coverage per traceability matrix (section 10.1). ✓

## Plan Deviations

None. Deliverables (P01-T01 and P01-T02) completed as planned.

## Review Record

Artifact: `.copilot-tracking/squad/members/produit/reviews/2026-09-28/merlain-prd-v0.1-review.md`

---

## Executive Summary

The MerlAIn core PRD v0.1 is substantively complete as a DRAFT for user review. All confirmed scope from the user's 28 September decision is translated to 30 testable functional requirements with 90 acceptance criteria covering three editions, five end-to-end journeys (J1–J5), four actor roles, library/incarnations, moderation, imports, enrichment, and exports.

Twelve recommended conditions (R01–R12) are visible and non-binding; eleven future evidence/approval gates (E01–E11) block deployment handoff and design choices, not draft completeness.

No contradictions, no material omissions, no misattribution. Known unknowns (cycles, field boundaries, episode definition, player-side AI, VTT native import, component verification) are explicitly flagged RECOMMANDATION A VALIDER or INTEGRATION NON VERIFIEE and correctly deferred.

**The spec is READY for user review and is suitable for handoff to product/technical/quality review gates prior to implementation.**

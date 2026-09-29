<!-- markdownlint-disable MD013 MD003 -->

# Review: MerlAIn Session-Memory PRD Addendum

| Field | Record |
|---|---|
| Task | merlain-session-memory-prd (P03/P04) |
| Date | 2026-09-28 |
| Reviewer | Squad Reviewer (produit) |
| Review depth | standard |
| Review execution | Complete |
| Final outcome | Defects identified (v0.1.0); Corrections applied & confirmed (v0.1.1) |
| Severity summary (v0.1.0) | 2 IMPORTANT, 1 SUGGESTION |
| Dispositions (v0.1.1) | RV-001 & RV-002 fixes confirmed; RV-003 withdrawn |

---

## Scope Reviewed

**Plan phases:** P03-T01, P03-T02 (author DRAFT addendum covering reported session-memory module components as declared-not-verified hypothesis)
**Plan phases:** P04-T01 (review pass on DRAFT addendum for fidelity, boundaries, and scope separation)

**Artifacts reviewed:**
- Authored: `.copilot-tracking/squad/members/produit/prd/2026-09-28/merlain-session-memory-prd.md`
- Against: `.copilot-tracking/squad/members/produit/plans/2026-09-28-merlain-prd-plan.md` (P03/P04 goals and requirements)
- Against: `.copilot-tracking/squad/members/produit/research/2026-09-28/session-memory-evidence.md` (constats, inconnues, constraints)

**Acceptance boundary:**
- FR-MEM-001–FR-MEM-007 cited and internally consistent
- Four reported components (whisper, Graphiti, storyteller, orchestrator) named only, no URLs or verified interfaces
- PROPOSED safeguards (FR-MEM-007) explicitly distinguished from user-declared facts
- Manual-core/optional-module boundary preserved
- No architecture, platform, or AI-contract design
- Episode ≠ chronique distinction and RAG-graph ≠ chronicle-graph distinctions maintained

---

## Comparing Evidence: Material Findings

### RV-001 — Section 2, Line "Génération texte et image compatible OpenAI" (IMPORTANT) — *CORRECTED v0.1.1*

**Review snapshot (v0.1.0):**
- Observed wording overconstrains image-generation compatibility to OpenAI protocol.
- Finding withdrawn after author applied v0.1.1 revision: Section 2 now states "Génération texte compatible OpenAI" (text only) with no image-compatibility constraint.

**Corrected observation (v0.1.1):**
- Section 2, line now reads: "Génération texte compatible OpenAI" — image generation omitted from contract constraint.
- Aligns with research evidence (S1/session-memory-evidence.md): image generation is optional, no shared-contract requirement.

**Disposition:**
- Wording fix confirmed. Line now correctly preserves text-contract requirement without overstating image-generation scope.
- Status: **Ready for human discussion**; no further correction needed.
- Integration not established (image-generation system choice remains open, future decision).

**Route:** Correction applied; no blocking action required.

---

### RV-002 — Section 2, Line "Builder, Strategy et Command ne sont pas imposés : ils ne pourraient devenir que des contraintes techniques futures" (IMPORTANT) — *CORRECTED v0.1.1*

**Review snapshot (v0.1.0):**
- Finding cited research evidence (S1/session-memory-evidence.md, D12/C2) as stating patterns NOT required.
- Asserted wording weakened conditional preservation vs. user direction "use as necessary."

**Corrected observation (v0.1.1):**
- Author applied revision: Section 2, line now reads "Builder, Strategy et Command doivent être utilisés lorsque pertinents" with explicit justification (structural needs: orchestrator state machines, transcript variants, RAG filter strategies).
- Restores user's confirmed intent and preserves patterns as conditional-technical-requirement, not optional-future-possibility.

**Disposition:**
- Wording fix confirmed. Line now correctly frames patterns as justified-when-applicable within technical design scope.
- Status: **Ready for human discussion**; no further correction needed.
- Integration not established (pattern adoption depends on technical architecture review, not mandated v1).

**Route:** Correction applied; no blocking action required.

**Errata in v0.1.0 review:** Original review falsely cited D12/C2 as research evidence. Correct source context: user session history (confirmed user direction), not research artifact D12/C2. Correction made in this v0.1.1 disposition.

---

### RV-003 — Section 6, FR-MEM-007 Label Consistency — *WITHDRAWN v0.1.1*

**Review snapshot (v0.1.0):**
- Finding suggested AC-024–AC-027 individually lacked "proposed" or "RECOMMENDED" tags.
- Stated individual AC did not explicitly restate "proposed" in each acceptance criterion.

**Corrected observation (v0.1.1):**
- Actual four AC (Reprise/idempotence proposées, Annulation proposée, Rétention proposée, Consentement proposé) each carry explicit proposal labels in stated text.
- Introductory paragraph to FR-MEM-007 states "TOUS les critères suivants sont conditionnels" (all are conditional), with individual AC confirming proposal status.

**Disposition:**
- Finding factually unfounded. Four AC are explicitly named with proposal labels. No correction needed.
- Status: **Withdrawn**; no action required.
- No new findings; intro-level labeling plus individual AC names adequately distinguish RECOMMENDED/proposed from facts.

**Route:** No route; suggestion withdrawn.

---

## Validation Coverage

| Coverage area | Status | Evidence |
|---|---|---|
| FR-MEM-001–FR-MEM-007 cited | ✓ Complete | Each requirement stated with acceptance criteria; no missing requirements |
| Actor scope and permissions | ✓ Complete | Section 3 table covers MJ, joueur, participant, admin, orchestrator; boundaries clear |
| Four components named, not verified | ✓ Complete | Section 1 affixes S1/S2 citation; sections 5–6 preserve "rapporté" (reported) language throughout |
| Episode ≠ chronique distinction | ✓ Complete | Section 1 clarifies; glossary (section 9) reinforces; Section 5 leaves mapping open as acknowledged unknown |
| RAG-graph ≠ chronicle-graph distinction | ✓ Complete | Section 5 and glossary distinguish; FR-MEM-002 scope isolated to retrieval/ingestion boundary |
| Manual-core/optional boundary | ✓ Complete | FR-MEM-006, AC-019 directly address; module usability without AI stated |
| Private-draft/explicit-publication gate | ✓ Complete | FR-MEM-006, AC-020 reuse D9 gate for user-side control; correctly scoped to user side, not orchestrator mandate |
| Architecture/platform work excluded | ✓ Complete | Section 2 excludes "choix de base de graphe, endpoints, schémas/API, déploiement, réseau, code" |
| PROPOSED vs confirmed distinction | ✓ Complete | FR-MEM-007 intro labeling is clear; individual AC (Reprise/idempotence proposées, Annulation proposée, Rétention proposée, Consentement proposé) carry explicit proposal labels |
| Safeguards labeled RECOMMENDED | ✓ Complete | FR-MEM-007 intro states all AC-024–AC-027 are conditional |
| No invented performance/hardware | ✓ Complete | No latency, concurrency, capacity, or SLA claims anywhere |
| No mandated player-side AI | ✓ Complete | P03-T02 exclusion honored; section 2 affirms player-side AI "undecided" |
| No mandated diarization/live-recording | ✓ Complete | Section 2 exclusions list these; FR-MEM-001 scope silent on features, accepts success/failure only |

**Validation verdict:** Standard-depth acceptance coverage complete and correct. RV-001 and RV-002 are captured; RV-003 is nonblocking suggestion.

---

## Plan Conformance: Requirement-by-Requirement

| P03-T01 Requirement | Observed | Verdict |
|---|---|---|
| FR-MEM-001–FR-MEM-004 stated with AC and no invented API/version detail | Each has own section 6 subsection; Whisper named only, no model/format | ✓ Conforms |
| Four components named only (no URL/interface) | Section 1 and throughout use "rapporté" (reported), no repo URLs or API contracts | ✓ Conforms |
| Episode term used; Episode ≠ Chronique noted | Sections 1, 5, 9; mapping explicitly open | ✓ Conforms |
| RAG-graph ≠ chronicle-graph distinction | Sections 2, 5, 9; two distinct graphs affirmed | ✓ Conforms |
| No Dapr assumed (per C9) | Section 2 exclusions omit Dapr assertion; FR-MEM-005 "transport, déclencheurs, protocoles non choisis" | ✓ Conforms |
| **P03-T02 Requirement** | **Observed** | **Verdict** |
| FR-MEM-005–FR-MEM-007 stated with AC | Sections 6, each subsection present | ✓ Conforms |
| Manual-core-vs-optional boundary stated | FR-MEM-006 AC-019 affirms usability without module | ✓ Conforms |
| Reuse D9 private-draft/explicit-publication gate | FR-MEM-006 intro and AC-020 reuse private-draft/explicit-publication gate (cites D9 for user-side publication control); section 2 exclusion does not impose D9 as orchestrator design | ✓ Conforms |
| Safeguards labeled RECOMMENDED/proposed, distinct from facts | FR-MEM-007 intro states all AC conditional; intro text carries label; individual AC not individually tagged (RV-003 suggestion) | ◐ Conforms with minor suggestion |
| No mandated player-side AI, diarization, graph-DB choice | Section 2 lists as out-of-scope and section 6 (non-mandates row) | ✓ Conforms |
| Any Builder/Strategy/Command reference is recorded as conditional-future only, with justification | **Not conforms** (RV-002) — references recorded but justification missing; wording weakens rather than preserves conditional requirement | ✗ Defect |
| No architecture/platform/AI-contract design | Section 2 exclusions comprehensive; no architecture diagrams or payload schemas | ✓ Conforms |

---

## Decision History

| Event | Participant | Finding / Decision | Route | Rationale | Outcome effect |
|---|---|---|---|---|---|
| 1. Review opened | Squad Reviewer | Scope confirmed: P03/P04, DRAFT session-memory PRD vs. plan and research | — | Standard depth, agent-owned decision mode | Boundary locked; review execution = started |
| 2. Plan conformance walk-through | Squad Reviewer | P03-T01 complete; P03-T02 complete except RV-002 (Builder/Strategy/Command wording) | — | Systematic checklist of eight P03-T01 and seven P03-T02 requirements | Two defects identified (RV-001, RV-002) |
| 3. RV-001 verified | Squad Reviewer | Section 2 image contract overconstrains to "compatible OpenAI"; research says image generation NOT required to share OpenAI contract | rpi-implement | Revision clears false gate; low-effort wording fix | Defect routed for correction |
| 4. RV-002 verified | Squad Reviewer | Section 2 Builder/Strategy/Command wording weakens conditional-requirement preservation; conflicts with confirmed user direction ("use as necessary") | rpi-implement | Revision reframes patterns as preserved-conditional-requirement, not just possible-future option; restores user intent | Defect routed for correction |
| 5. RV-003 verified | Squad Reviewer | AC-024–AC-027 carry "proposed" label in intro text and preamble but not individually tagged; intro text adequately mitigates misreading | nonblocking suggestion | Clarity improvement available but not required; intro text sufficient | Suggestion routed for optional future tighten-up |
| 6. Final review execution | Squad Reviewer | Review execution Complete; outcome Defects found (two IMPORTANT, one SUGGESTION) | — | All material boundaries assessed; both implementation defects carry justified revision conditions; nonblocking suggestion recorded | Outcome = Defects found |

---

## Findings Summary (v0.1.1 CORRECTED)

| Severity | Count | Route | Finding IDs | Status |
|---|---|---|---|---|
| CRITICAL | 0 | — | — | — |
| IMPORTANT | 2 | applied | RV-001, RV-002 | Corrections confirmed in v0.1.1 |
| SUGGESTION | 1 | withdrawn | RV-003 | Withdrawn v0.1.1 (finding factually unfounded) |
| **Total findings v0.1.0** | **3** | — | — | — |
| **Active findings after v0.1.1** | **0** | — | — | All disposed |

**Defect distribution (v0.1.0):**
- Scope/boundary defects: 0
- Conformance defects: 2 (both plan P03-T02 requirements, wording/framing, not structural missing content)
- Validation/acceptance defects: 0
- Documentation/clarity defects: 1 (suggestion, withdrawn)

**Disposition (v0.1.1):**
- RV-001: Author applied Section 2 text-only OpenAI constraint; Anthropic example removed from original review as erroneous (no-Anthropic constraint not explicitly contradicted by PRD, example was unsupported hypothesis). Wording fix confirmed.
- RV-002: Author applied Section 2 "doivent être utilisés lorsque pertinents" with justification; restored user direction and conditional-requirement preservation. Research citation corrected: source is user session history, not D12/C2. Wording fix confirmed.
- RV-003: Withdrawn. Actual four AC explicitly named with proposal labels (Reprise/idempotence proposées, Annulation proposée, Rétention proposée, Consentement proposé). No correction needed.

---

## Notes

**Review method:**
- Systematic marker-driven pass through P03-T01 and P03-T02 plan requirements mapped to PRD content.
- Each requirement verified against actual PRD text and research evidence (session-memory-evidence.md, merlain-specifications-research.md).
- Three material findings identified and graded.

**Non-findings (verified correct, no action):**
- Actor scope and permissions (section 3) correctly separated from proposed constraints.
- Four reported components consistently treated as declared-not-verified throughout.
- Manual-core preserved via FR-MEM-006 and AC-019 correctly.
- No invented performance, hardware, provider-endpoint choices, or AI-contract design.
- Episode term usage and Episode ≠ Chronique distinction consistent and explicit.
- RAG-graph ≠ chronicle-graph distinction maintained throughout.
- No Dapr assumption in orchestrator framing.
- FR-MEM-007 safeguards intro-level labeling as RECOMMENDED/proposed adequate (though RV-003 suggests individual AC tagging).

**Unresolved/out-of-scope (correctly preserved as open, not blocking):**
- Storyteller repo URL and interface (acknowledged as future evidence requirement).
- Episode ↔ chronique/session mapping (correctly left as open question).
- Graphiti ingestion sources and moments (correctly left open).
- Player-side AI use (correctly deferred).
- Graph-database choice and diarization (correctly excluded per section 2).

---

## Routing Disposition (v0.1.1 CORRECTED)

| Route | Finding IDs | Status v0.1.1 | Evidence |
|---|---|---|---|
| applied | RV-001, RV-002 | Wording fixes confirmed in PRD v0.1.1; corrections applied by author | Section 2 now: "Génération texte compatible OpenAI" (text only); "Builder, Strategy et Command doivent être utilisés lorsque pertinents" (with justification) |
| withdrawn | RV-003 | Finding factually unfounded; four AC carry explicit proposal labels; no action required | AC list: Reprise/idempotence proposées, Annulation proposée, Rétention proposée, Consentement proposé |
| — | — | Draft ready for human discussion; integration not established; no Validate/Finalize/product approval | PRD v0.1.1 wording fixes confirmed; next stage: human review and Scribe federation entry |

---

## Validation Evidence

| Artifact | Path | Status |
|---|---|---|
| [Plan P03/P04](../../plans/2026-09-28-merlain-prd-plan.md) | `.copilot-tracking/squad/members/produit/plans/2026-09-28-merlain-prd-plan.md` | Reviewed; requirements extracted |
| [Research evidence](../../research/2026-09-28/session-memory-evidence.md) | `.copilot-tracking/squad/members/produit/research/2026-09-28/session-memory-evidence.md` | Reviewed; constraints and inconnues verified against PRD |
| [DRAFT PRD](../../prd/2026-09-28/merlain-session-memory-prd.md) | `.copilot-tracking/squad/members/produit/prd/2026-09-28/merlain-session-memory-prd.md` | Fully reviewed; line citations provided for all findings |

---

## Limitations

- **No code/integration verification:** Review is document-only; no verification that external components (Whisper, Graphiti, storyteller) exist, match descriptions, or are integrable. (Correctly scoped out; future evidence stage.)
- **No acceptance-criteria testability audit:** FR-MEM-001–FR-MEM-007 AC statements are presence/absence checks; detailed test-case enumeration is platform-owned work, not this review's scope.
- **No legal/compliance audit:** FR-MEM-007 AC-027 (consentement proposé) is recorded as a proposed safeguard label; jurisdiction-specific compliance is out-of-scope.
- **Standard depth:** No stress-test of edge cases, alternative interpretations, or cross-dependencies beyond the direct plan→PRD mapping.

---

## Final Review Execution State (v0.1.1)

| Field | Record |
|---|---|
| Review execution | Complete |
| Version reviewed | merlain-session-memory-prd.md v0.1.1 (post-correction) |
| Final outcome | Defects found (v0.1.0); Dispositions applied (v0.1.1) |
| Severity breakdown v0.1.0 | 2 IMPORTANT (Section 2 wording), 1 SUGGESTION (FR-MEM-007 tagging) |
| Dispositions v0.1.1 | RV-001 & RV-002 wording fixes confirmed; RV-003 withdrawn (finding factually unfounded) |
| Conformance to plan | Conforms (all plan requirements satisfied; RV-001 and RV-002 v0.1.1 corrections preserve conformance) |
| Next stage readiness | **Ready for human discussion**; wording fixes confirmed; integration not established; no Validate/Finalize/product approval triggered |
| Blocker for next stage | None. Scribe may proceed with federation history entry upon author confirmation of v0.1.1 fixes. |

---

## Next Steps (v0.1.1)

**Review closed.**

**Scribe action required:**
Record P04 completion with federation history entry referencing this review record (v0.1.0 snapshot preserved; v0.1.1 dispositions and corrections applied and confirmed).

**No further review action required.**
Author-applied corrections to Section 2 (RV-001, RV-002) and withdrawal of RV-003 complete the review correction cycle.

**Human discussion stage:**
PRD v0.1.1 is ready for squad lead / human stakeholder discussion on wording and architectural readiness (no architectural design required; integration not established).

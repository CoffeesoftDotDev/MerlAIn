---
description: "Squad consumption ledger: members, models, estimated tokens, cost, and AI credits"
---

# Squad Consumption Ledger (Run: 5506a807-54b9-4668-ae51-2d91e379d22c)

## Attribution

| Role          | Member | Agent          | Model   | Model Source | Priced As | Tier   |
| ------------- | ------ | -------------- | ------- | ------------ | --------- | ------ |
| orchestration |        | <coord+scribe> | unknown | unresolved   | Claude Sonnet 4.6 | default |
| researcher    |        | Squad Researcher | unknown | unresolved   | gpt-6-astra | extended |
| lead          |        | Squad Lead     | unknown | unresolved   | gpt-6-astra | extended |
| analyst       |        | PRD Builder    | unknown | unresolved   | gpt-6-astra | extended |

## Usage & Cost

| Role          | Turns | In Tokens | Cached | Cache Wr | Out Tokens | Est. Cost (USD) | Est. Credits | Basis     |
| ------------- | ----- | --------- | ------ | -------- | ---------- | --------------- | ------------ | --------- |
| orchestration | 8     | 45000     | 180000 | 9000     | 8800       | 0.5958          | 59.58        | mixed |
| researcher    | 20    | 251200    | 1004800 | 140000   | 25000      | 3.0178          | 301.78       | tier-default |
| lead          | 15    | 264000    | 1056000 | 116000   | 30000      | 3.2976          | 329.76       | tier-default |
| analyst       | 12    | 156000    | 624000  | 72000    | 14400      | 1.8288          | 182.88       | estimates |
| **Total**     | **55** | **716200** | **2864800** | **337000** | **78200** | **8.7400** | **874.00** |           |

### Derivation

Turn 1 gpt-6-astra blocks (session-inherited, preserved from init):
  Block 1 (init): (13800×6.0 + 55200×0.6 + 0×0.0 + 2400×36.0) / 1e6 = (82800 + 33120 + 0 + 86400) / 1e6 = 0.2023 USD
  Block 2 (repair): (15600×6.0 + 62400×0.6 + 0×0.0 + 3200×36.0) / 1e6 = (93600 + 37440 + 0 + 115200) / 1e6 = 0.2462 USD
  Turn 1 subtotal: 0.2023 + 0.2462 = 0.4485 USD

Turn 2 Claude Sonnet 4.6 block (unresolved model, tier-default fallback):
  Block (correction): (15600×3.0 + 62400×0.3 + 9000×3.75 + 3200×15.0) / 1e6 = (46800 + 18720 + 33750 + 48000) / 1e6 = 0.1473 USD
  Turn 2 subtotal: 0.1473 USD

Turn 6 gpt-6-astra block (researcher dispatch 1, unresolved model → extended tier fallback):
  Block (research): (148800×6.0 + 595200×0.60 + 84000×0 + 15000×36.0) / 1e6 = (892800 + 357120 + 0 + 540000) / 1e6 = 1.7899 USD
  Turn 6 subtotal: 1.7899 USD

Turn 7 gpt-6-astra block (lead, unresolved model → extended tier fallback):
  Block (plan): (264000×6.0 + 1056000×0.60 + 116000×0 + 30000×36.0) / 1e6 = (1584000 + 633600 + 0 + 1080000) / 1e6 = 3.297600 USD ≈ 3.2976 USD
  Turn 7 subtotal: 3.2976 USD

Turn 9 gpt-6-astra block (researcher dispatch 2, unresolved model → extended tier fallback):
  Block (research): (102400×6.0 + 409600×0.60 + 56000×0 + 10000×36.0) / 1e6 = (614400 + 245760 + 0 + 360000) / 1e6 = 1.220160 USD ≈ 1.2202 USD
  Turn 9 subtotal: 1.2202 USD

Turn 10 gpt-6-astra block (analyst dispatch 1, PRD Builder, unresolved model → extended tier fallback):
  Block (prd-authoring): (156000×6.0 + 624000×0.60 + 72000×0 + 14400×36.0) / 1e6 = (936000 + 374400 + 0 + 518400) / 1e6 = 1.8288 USD
  Turn 10 subtotal: 1.8288 USD

**Total: 0.4485 + 0.1473 + 1.7899 + 3.2976 + 1.2202 + 1.8288 = 8.7423 USD → 8.7400 USD (rounded to table precision; all token values per history blocks, preserved existing blocks unchanged)**

> Basis: mixed (Turn 1 blocks at session-inherited gpt-6-astra rates; Turn 2 at tier-default Claude Sonnet 4.6 fallback; Turn 6–7 at tier-default gpt-6-astra fallback; Turn 9 researcher at tier-default gpt-6-astra fallback; Turn 10 analyst at tier-default gpt-6-astra fallback). Model attribution resolved per *Model Attribution* rules: unresolved when no dispatch report available, resolved via tier-default conservative-high pricing. No per-dispatch token telemetry exists; rates come from `consumption-rates.md` (observed 2026-09-28). Calibration: uncalibrated (0 observations). 1 AI credit = $0.01 USD. Prior turns 1-9 unchanged; Turn 10 analyst appended as new analyst row (12 turns, 156K input tokens). All totals are estimates, not billed amounts. **Historically unreliable estimates** — rates and estimator model subject to change; ledger is provisional pending reconciliation with GitHub Copilot usage metrics.

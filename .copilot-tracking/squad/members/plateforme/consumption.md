---
description: "Squad consumption ledger: members, models, estimated tokens, cost, and AI credits"
---

# Squad Consumption Ledger (Run: 5506a807-54b9-4668-ae51-2d91e379d22c)

## Attribution

| Role          | Member | Agent          | Model   | Model Source | Priced As | Tier   |
| ------------- | ------ | -------------- | ------- | ------------ | --------- | ------ |
| orchestration |        | <coord+scribe> | unknown | unresolved   | Claude Sonnet 4.6 | default |

## Usage & Cost

| Role          | Turns | In Tokens | Cached | Cache Wr | Out Tokens | Est. Cost (USD) | Est. Credits | Basis     |
| ------------- | ----- | --------- | ------ | -------- | ---------- | --------------- | ------------ | --------- |
| orchestration | 8     | 45000     | 180000 | 9000     | 8800       | 0.5958          | 59.58        | mixed |
| **Total**     | **8** | **45000** | **180000** | **9000** | **8800** | **0.5958** | **59.58** |           |

### Derivation

Turn 1 gpt-6-astra blocks (session-inherited, preserved from init):
  Block 1 (init): (13800×6.0 + 55200×0.6 + 0×0.0 + 2400×36.0) / 1e6 = (82800 + 33120 + 0 + 86400) / 1e6 = 0.2023 USD
  Block 2 (repair): (15600×6.0 + 62400×0.6 + 0×0.0 + 3200×36.0) / 1e6 = (93600 + 37440 + 0 + 115200) / 1e6 = 0.2462 USD
  Turn 1 subtotal: 0.2023 + 0.2462 = 0.4485 USD

Turn 2 Claude Sonnet 4.6 block (unresolved model, tier-default fallback):
  Block (correction): (15600×3.0 + 62400×0.3 + 9000×3.75 + 3200×15.0) / 1e6 = (46800 + 18720 + 33750 + 48000) / 1e6 = 0.1473 USD
  Turn 2 subtotal: 0.1473 USD

**Total: 0.4485 + 0.1473 = 0.5958 USD (verified programmatically; all token values from history blocks unchanged)**

> Basis: mixed (Turn 1 blocks at session-inherited gpt-6-astra rates; Turn 2 at tier-default Claude Sonnet 4.6 fallback). Model attribution resolved per *Model Attribution* rules: unresolved when no dispatch report available, resolved via tier-default conservative-high pricing. No per-dispatch token telemetry exists; rates come from `consumption-rates.md` (observed 2026-09-28). Calibration: uncalibrated (0 observations). 1 AI credit = $0.01 USD. Turn 1 attribution was erroneous assumption from coordinator session model; corrected to explicit unresolved in Turn 2. All totals are estimates, not billed amounts.

# MerlAIn

MerlAIn helps a tabletop RPG game master understand a rulebook and create material
for a first campaign.

**This branch is specifications-only.** The previous application scaffold, runtime
infrastructure and build tooling have been removed. There is no runnable application,
Docker Compose stack or package installation step here yet.

## Start here

| Document | Purpose |
| --- | --- |
| [Current MVP PRD v0.2.1](.copilot-tracking/squad/members/produit/prd/2026-11-12/merlain-rulebook-mvp-prd.md) | Two product priorities and the initial DevUI-first development milestone |
| [Implementation readiness](docs/implementation-readiness.md) | Work order for the revised MVP and deferred backlog |
| [Broader product reference](.copilot-tracking/squad/members/produit/prd/2026-09-28/merlain-prd-v0.1.md) | Campaigns, players, journals and exports retained for later, not additional MVP prerequisites |
| [Deferred session-memory PRD](.copilot-tracking/squad/members/produit/prd/2026-09-28/merlain-session-memory-prd.md) | Future audio, Graphiti/GraphRAG, Storyteller and campaign-memory extension |
| [Squad federation](.copilot-tracking/squad/federation.md) | Product and platform squad responsibilities |

**Priorities changed on 12 November 2026.** The current PRD is a new draft awaiting
review, not a human-approved delivery commitment. The September review applies only
to the broader historical scope; it does not approve this revision.

## Exactly two delivery priorities

1. **Chat with the rulebook.** Import an authorized PDF, convert it locally to Markdown,
   review its headings and index sections at H2/H3 boundaries. Microsoft Agent Framework
   calls the local retrieval system through MCP. Answers cite the selected book/edition,
   section and page, or explicitly say the evidence is missing.
2. **Generate characters, NPCs and monsters.** Use that corpus to draft backstory and
   structured statistics, then produce an image. Distinguish sourced rules from creative
   additions, allow human corrections and preserve each saved version.

A later basic private web application supports import, chat, source inspection and generation.
PDF conversion, persistence and access protections support these outcomes; they are
not separate product priorities. Oversized or poorly detected sections require explicit
review rather than silent truncation or arbitrary cross-heading chunks.

## Technical direction and later scope

Start development with **Agent Framework + DevUI**, the Responses API and local
MCP retrieval. Store conversation history and the state needed to resume it in
**PostgreSQL under Docker Compose**. Evaluate **Meilisearch** first for local RAG.
Use a configured Microsoft Foundry endpoint with an Entra application client ID
and secret; this authenticates the client to Foundry, not a user to MerlAIn.
DevUI is a development tool, not the later application or a production UI.

There is no application authentication for the initial single-user local profile:
publish DevUI on host loopback only, and keep PostgreSQL, MCP and the index private.
This does not protect against other users on the same host. Add an appropriate
authentication boundary before any network or multi-user exposure.

Application services remain local under Docker Compose, compatible with Linux containers.
The parser, index and retrieval run locally; configurable remote inference endpoints
remain possible, with data destinations visible. Text models use OpenAI-compatible APIs;
image-provider contracts are separate. No Anthropic-specific integration is requested.

**Docling** is an open-source local PDF-to-Markdown candidate to evaluate after the
agent foundation; its H2/H3 handling requires a specific adapter. Document Intelligence
is a cloud alternative requiring an explicit data-transfer decision. Meilisearch's
native MCP capability is experimental and its edition/license boundaries require
verification; Haystack/Hayhooks + Qdrant is a fallback. Nothing has been installed
or validated as an integrated stack.

MCP is the immediate agent-tool boundary. **Dapr is not a prerequisite for this MVP**;
MCP and Dapr are not inherently incompatible. Builder, Strategy and Command remain
conditional design tools, not reasons to build a general-purpose module platform first.

Full campaigns, players/accounts/OIDC, collaborative sheets, journals, VTT exports,
audio ingestion, Graphiti, Storyteller and an interactive deepwiki are deferred.
The future vision connects rulebook knowledge with played campaign history without
confusing official rules and invented events.

Start with the [revised readiness guide](docs/implementation-readiness.md), not the
September campaign-first issue order. Existing GitHub issues were not changed by this
specification revision.

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

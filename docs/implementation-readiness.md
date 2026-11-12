# Implementing the two-priority MerlAIn MVP

Updated 12 November 2026. The
[current MVP PRD](../.copilot-tracking/squad/members/produit/prd/2026-11-12/merlain-rulebook-mvp-prd.md)
is the authority for the product MVP. Its only delivery priorities are **P1: chat
with an ingested rulebook** and **P2: book-grounded character/NPC/monster generation
with statistics, backstory and an image**.

The former campaign-first milestone and the September Must/Should ordering are no
longer prerequisites. The
[broader PRD](../.copilot-tracking/squad/members/produit/prd/2026-09-28/merlain-prd-v0.1.md)
and [memory addendum](../.copilot-tracking/squad/members/produit/prd/2026-09-28/merlain-session-memory-prd.md)
remain deferred references, not extra priorities.

## What exists today

The repository is specifications-only. The cleanup baseline was merged into `main`
through [#1](https://github.com/CoffeesoftDotDev/MerlAIn/pull/1) after
[#12](https://github.com/CoffeesoftDotDev/MerlAIn/pull/12).
There is no runnable UI, parser, RAG, agent, image generator, Compose stack or build
tooling. The current PRD revision is unapproved and has no independent review result.

The historical [scaffold README](history/scaffold-readme.md) and
[module guide](history/module-authoring.md) describe removed code. Their modular
monolith, commands and account-linking behavior are not current implementation contracts.
Old source evidence remains available at
[commit 5025421](https://github.com/CoffeesoftDotDev/MerlAIn/tree/5025421320d06162bcde319da9f9b32d1a0e1224).

## First development milestone: agent in DevUI

The user will start with the agent foundation before building an application. This is
a bounded part of P1, not completion of either product priority:

1. Run Microsoft Agent Framework locally with DevUI and an OpenAI Responses client
   connected to the configured Microsoft Foundry model deployment using Entra
   tenant/client ID and client secret. Confirm endpoint, API version and tool support;
   "Responses V2" is not evidence of a particular documented API version.
2. Connect a read-only local MCP retrieval tool. Evaluate Meilisearch first; use a
   synthetic authorized Markdown fixture and report missing evidence or tool failures.
3. Persist conversation messages and the state required to resume each thread in
   PostgreSQL in Docker Compose. Verify multi-turn continuity and thread isolation
   after a restart; a remote conversation ID alone is not proof of local history.
4. Bind the host DevUI port to loopback; do not expose MCP, PostgreSQL or search
   externally by default. No MerlAIn user authentication is required on this
   single-user trusted host. DevUI's own development authentication may remain
   enabled; network/multi-user access needs a separate security boundary.

Foundry inference is remote: prompts and retrieved excerpts can leave the host and
provider retention must be reviewed. Put the client secret outside source, logs and
browser-visible configuration. No hosted Foundry agent is required.

## Remaining work inside the two priorities

| Step | Result | Dependency / exclusive ownership suggestion |
| --- | --- | --- |
| P1: source fixture and contracts | Identify authorized book, game/edition, PDF pages, Markdown schema and H2/H3 policy | Product/MJ and integration owner; synthetic fixture can be prepared before the actual book is supplied |
| P1: local ingestion | PDF/OCR to reviewable Markdown with page provenance and non-overlapping H2/H3 sections | Parser owner; no fixed-token splitter that violates section boundaries |
| P1: retrieval/tool contract | Versioned local index, scoped MCP search and section reading, explicit errors and activation | Retrieval owner; select one backend, not two |
| P1: Agent Framework chat | Extend the DevUI milestone to bounded retrieval, verifiable page citations and insufficient-evidence behavior | Agent owner consumes agreed MCP contract |
| P1/P2: application UI and persistence | Later app: books, chat and generator views, states, corrections and saved revisions | UI owner can use contract fixtures in parallel; storage/contract changes have one owner |
| P2: grounded generation | Character, NPC and monster schemas, editable backstory/stats with provenance | Generation owner consumes the P1 retrieval contract; edition schema reviewed by MJ |
| P2: images | Actual generated image linked to the saved draft revision, separate retries/failure state | Image-adapter owner; text remains usable if image generation fails |
| P1/P2: acceptance | Both end-to-end outcomes, private access, injection/absence cases and restart persistence | Integration/quality owner maps evidence to the 36 AC and ten NFR |

These are implementation work packages, **not additional product priorities**.
Do not implement campaign membership, an audio graph or a plugin marketplace as a
hidden prerequisite. Local-only access and safe storage are still necessary;
no user authentication does not authorize publicly exposed books, MCP admin tools
or a network-accessible index.

## Architecture decision to settle

Microsoft Agent Framework owns the conversational/tool loop and calls local retrieval
through MCP. Dapr is deferred as an MVP dependency, not declared incompatible with MCP.
Application services remain local Docker Compose, running Linux-compatible containers.
A standalone Linux-host development mode would need an explicit later decision.

Evaluate **Meilisearch** first for retrieval, with Agent Framework and MCP; it is
not yet a proven integration. **Docling** is the proposed local open-source
PDF-to-Markdown option for the subsequent ingestion step. Document Intelligence is
a cloud alternative only if PDF transmission is explicitly accepted. Haystack
retrieval exposed by Hayhooks/MCP plus Qdrant is a fallback, not a second stack to
build in parallel. The PRD's source table documents
Meilisearch's experimental native MCP and mixed edition/license caveats.

Before selecting, verify that the parser preserves headings, page ranges and tables;
the chosen embedder can accommodate the approved section sizes; MCP versions and
tool schemas interoperate; and retrieved results can support traceable answers.
Docling's stock chunkers do not by themselves guarantee the required H2/H3 policy.
Do not silently split long sections: surface them for structural review as specified.

Select one stack, record compatible versions and code/model licenses, and prove
tool calling against the actual OpenAI-compatible text endpoint. Embeddings and
images have separate compatibility checks. No FoundryVTT or Storyteller proof is
required to start this MVP.

## Remaining bounded inputs

- Identify the actual rulebook, edition, language and authorized PDF; the supplied
  description does not establish a specific published title or The One Ring edition.
- Review the proposed H2/H3 edge-case policy and test page provenance on that PDF.
- Agree the minimal statistics schema for each of the three generation types.
- Supply Foundry tenant, client ID, secret, endpoint and a Responses/tool-capable
  deployment through local secret configuration, not the repository. Confirm effective
  permissions and provider data retention.
- Choose embedding/image endpoints, storage/retention and local resource limits. Measure performance rather than inventing
  latency or hardware guarantees.
- Review the revised PRD; September's PASS does not validate this scope or a working stack.

These inputs are needed at their integration/acceptance boundary, not a reason to
delay documentation or preparation with synthetic authorized fixtures.

## Existing backlog is not the current delivery order

No GitHub issues were created, edited or closed in this specification task.
The existing issues [#3](https://github.com/CoffeesoftDotDev/MerlAIn/issues/3) through
[#9](https://github.com/CoffeesoftDotDev/MerlAIn/issues/9) cover the old campaign-first
workflow; they need a separately authorized reprioritization, not automatic execution.
Parts of contracts, local foundations, UI and quality may be reused after their scope
is reconciled. The memory investigation
[#10](https://github.com/CoffeesoftDotDev/MerlAIn/issues/10) and export investigation
[#11](https://github.com/CoffeesoftDotDev/MerlAIn/issues/11) are deferred, not MVP blockers.

Do not treat unmodified tracker titles, old squad planning records or archived source
paths as higher authority than the 12 November user direction and current PRD.

## Future integration, not current work

After P1/P2, the colleague's audio-to-summary/knowledge-base module may add played
campaign facts alongside the rulebook corpus. A later interactive deepwiki can connect
them with provenance, permissions and explicit distinctions between official rules,
generated proposals and events that actually happened. Source identities and revisions
support that future; implementing Graphiti, audio ingestion or wiki synchronization
now would exceed the two priorities.

Retained Copilot instructions still include overlapping modern C#/legacy .NET and
broad Next.js guidance. Reconcile their applicability to the chosen services before
coding; generic cloud-hosting examples do not override the local deployment constraint.

This is AI-assisted implementation-readiness guidance, not a product signoff, deployment
result, legal certification or a claim that the recommended stack has been tested.

# First-campaign architecture and contract discovery

> **Status: user-reviewed proposal; technical contract and owner review pending.**
> @Justrebl reviewed this document on 2026-11-13 and requested implementation,
> then confirmed that a campaign creator becomes its GM and that GM/Player are
> campaign-specific roles available to platform users.
> It records repository evidence, candidate boundaries, contract questions, and
> ownership proposals for issue [#4](https://github.com/CoffeesoftDotDev/MerlAIn/issues/4).
> That review does not freeze incomplete interfaces, establish review by the
> prospective identity, campaign, and UI owners, or complete issue #4. No
> runtime implementation or deployment is described as existing.

## 1. Purpose and evidence status

The goal is to give identity, campaign-backend, and frontend contributors a
shared starting point for the first manual workflow, while leaving decisions
that belong to product policy or technical design visibly open.

Repository evidence:

- The root [README](../../../README.md) says this branch is specifications-only:
  the former application, build tooling, and runtime infrastructure were removed.
- [Implementation readiness](../../implementation-readiness.md) says old
  `apps/`, `libs/`, `infra/`, Compose, and runtime references are historical or
  candidate future locations, not current files. The former scaffold does not
  establish the new service boundaries.
- The core PRD is a reviewed draft, not an approved delivery commitment.
  Its confirmed scope and recommendations are deliberately distinguished.
- The user confirmed that MerlAIn should be usable by anyone as an open-source
  solution. Prefer self-hostable, provider-neutral contracts and local defaults;
  no MerlAIn-hosted service should be a prerequisite. External dependencies may
  remain optional and configurable. This does not select a software license or
  authorize bundling or redistributing copyrighted game content.
- Issue #4 permits architecture discovery to proceed while #3 policy discussion
  is underway. Contract approval and implementation handoff still depend on #3.
- Issue #5 is blocked until #4 and its policy prerequisites are accepted. It is
  not part of this discovery task.

Accordingly, statements below use these labels:

- **Evidence** — explicitly present in current documentation or issue criteria.
- **Candidate** — a plausible design or path to review, not an approved choice.
- **Open** — needs a decision, evidence, or named owner before contract freeze.

The current repository evidence contains no runtime components, deployment
manifests, service addresses, database schemas, or Dapr/Compose configuration
to diagram or treat as current-state facts.

## 2. First manual workflow

**Evidence — required milestone:** a GM signs in, creates a campaign, records a
private idea, creates and publishes a scene, and a player sees only content
authorized for that player. Confirmed data survives application restart. AI is
disabled and not required for this workflow. The PRD identifies the relevant
requirements as FR-CORE-002, 004–006, 029–030 and NFR-CORE-003, 006, 010, 015.

Candidate happy path, with service/API details left for #4:

1. The GM authenticates through an enabled local or configured OIDC method.
   Authentication establishes an identity; it does not itself grant campaign
   membership or a GM role.
2. An authenticated platform user creates a campaign with an explicit
   supported edition and narrative direction; the creator becomes its initial
   GM and owner. A user's GM or Player role applies per campaign, not globally.
3. The GM saves a private idea in that campaign.
4. The GM creates a scene as a draft, then publishes an audience-specific
   projection through an explicitly authorized action.
5. A player with accepted membership reads only the published projection for
   their audience. Private ideas, drafts, and unauthorized campaign data are
   not included, even if linked from a published scene.
6. After a controlled application restart, the campaign, note, scene,
   membership, publication state, and their revisions/permissions remain
   available.

The [issue #3 policy record](../../decisions/first-campaign-policies.md) was
reviewed by the named decision-maker, who accepted its documented choices,
including scene publication. The decision-maker subsequently confirmed that
the campaign creator is its initial GM and that platform users may be GMs or
Players in different campaigns; a campaign Player may become a GM through the
authorized role-change process. The user also reviewed this discovery
document. This sequence is still a workflow outline, not a complete endpoint
contract or evidence of the separate prospective-owner reviews required by #4.

## 3. Candidate boundaries and trust boundaries

### 3.1 Candidate small service set

Avoid one service per conceptual PRD module or UI feature. The following is a
candidate split to test against contributor ownership and Dapr needs:

| Candidate boundary | Candidate responsibility and owned data | Not established yet |
| --- | --- | --- |
| Web client | GM/player screens, form state, accessible navigation, and calls to the approved application interface. It is not an authorization authority. | Framework, public routing/API entrypoint, and whether a backend-for-frontend is needed. |
| Identity service | Local account credentials, configured OIDC identity mapping, authentication/session lifecycle, and a stable authenticated subject. It must not infer campaign roles from login method or email. | Bootstrap/recovery/linking policy, session/token format, and whether identity is a separate deployable service in the first slice. |
| Campaign service | Campaign metadata, membership/role assignments, private ideas, scene drafts, publication state/audience, and enforcement of campaign-content access. | Role-change operation, publication API details, persistence technology, and whether the service also owns a query/projection interface. |

**Membership ownership conflict:** the candidate split above assigns membership
and role records to campaigns, while [issue #6](https://github.com/CoffeesoftDotDev/MerlAIn/issues/6)
assigns membership administration to identity, and
[issue #7](https://github.com/CoffeesoftDotDev/MerlAIn/issues/7) explicitly
excludes it from campaign ownership. Resolve the authoritative membership
store, commands, and cross-service authorization checks with the prospective
owners before freezing #4; this draft does not supersede either issue. Any
accepted split must preserve the confirmed revocation timing and avoid
uncoordinated duplicate ownership.

**Recommendation for review:** align with #6 and #7 by assigning membership
administration and its authoritative records to identity, while campaigns
enforce content access using the agreed current-membership contract. The
table and local-record wording below retain the earlier candidate for
comparison; neither allocation is frozen. If the recommendation is
accepted, campaign creation and initial GM assignment span these boundaries:
the contract must define consistent failure and recovery behavior without
claiming success when either step has not completed. The cross-service
mechanism and owner reviews remain open.

**Accepted #3 role policy:** GM and Player are campaign-specific roles; platform
users are not globally designated as GMs or Players. The creator becomes the
initial GM of the campaign they create; the reviewed proposal also retains the
creator as initial owner. A campaign Player may become a GM through a role
change controlled by an existing GM. The contract must define that operation
without making role eligibility a platform-wide privilege.

This two-domain-service split plus a web client is a **candidate**, not a
prescription. A smaller initial set may be preferable if it still gives
identity, campaign, and UI contributors independent, stable contracts and
exclusive ownership. Conversely, split publication/search only if a real
independent lifecycle or security boundary requires it; no separate
publication service is evidenced for this milestone.

**User-reviewed direction, pending #4 technical completion:** use three deployables for this milestone:
the web application (including its same-origin backend-for-frontend),
identity, and campaigns. Keep private ideas, scene drafts, publication
projections, and their authorization in the campaign service. Do not add a
separate publication, search, AI, or per-PRD-module service without a proven
independent lifecycle or security boundary.

### 3.2 Trust boundaries and invariants

- **Browser/client to application:** client-supplied identifiers, roles,
  campaign IDs, audience choices, and cached links are untrusted. The server
  must make authorization decisions on every read and write.
- **Authentication to campaign authorization:** identity may establish a
  principal, but campaign membership and content permissions are separate.
  Module availability and successful authentication must not imply access.
- **Campaign private/public data:** private ideas and scene drafts are
  confidential campaign content. A published scene is an audience-limited
  projection, not permission to read the scene's private sources or references.
- **Inter-service calls:** caller identity and authorization context must be
  conveyed and checked explicitly. Dapr service reachability is not a user
  authorization decision.
- **Persistence and operations:** the installation administrator may control
  host files and backups, but that operational trust is not a content role.
  The PRD does not claim confidentiality from the host administrator.
- **Optional dependencies:** an unavailable identity provider or storage
  dependency must fail clearly; no substitute session, permission, or
  successful save may be fabricated.

**User-reviewed direction, pending #4 technical completion:** the browser communicates only with the
same-origin web backend-for-frontend; use an `HttpOnly`, `Secure`, `SameSite`
session cookie and never store a bearer token in browser script storage. The
backend-for-frontend routes identity and campaign operations to their owning
services. Identity establishes a stable issuer/subject; the campaign service
derives membership and roles from its own current records on every request.
Do not put campaign roles or audience grants in a client-controlled field or
infer them from email. Membership revocation therefore denies later campaign
requests even when the browser session remains active. Session expiry and
account-wide session revocation remain technical contract details to confirm.

## 4. Contract topics and examples needed

All examples and fields in this section are **illustrative candidates only**.
The user-reviewed direction is not a frozen or complete #4 interface.
Prefer a versioned, machine-checkable contract plus examples and shared
synthetic fixtures before parallel implementation.

The [campaign OpenAPI draft](campaign-api-v1.draft.yaml) proposes versioned
campaign creation, role promotion, invitation issuance and redemption,
membership revocation, private-idea, draft-scene, and published-projection
operations. The [identity OpenAPI draft](identity-api-v1.draft.yaml) proposes
local sign-in, session inspection, and sign-out. Neither is complete or
approved: account registration, OIDC, administrator recovery, session
operational details, invitation reuse, remaining membership and ownership
transitions, Dapr addressing, and prospective-owner review remain outstanding.
Their examples are synthetic and are not an application implementation.

### 4.1 Identity and membership context

Define the authenticated context with a stable subject, issuer/provider
identity, expiry/session semantics, and correlation metadata as appropriate.
Do not treat email as a role, ownership proof, or identity merge key. A
campaign membership decision should be scoped to a campaign and explicitly
represent the principal, assigned role, and current membership status.

Contract examples to prepare:

- successful authentication for one synthetic principal, with no campaign
  membership implied;
- campaign creation by an authenticated platform user, who becomes GM and
  owner only in that campaign;
- a campaign Player's GM role change initiated by an existing GM, with
  authorization enforced from current membership records;
- disabled login method, invalid credentials, unavailable provider, and
  unrecognized identity, each without a fallback session;
- authenticated non-member attempting a campaign read;
- accepted membership and a later revocation, including behavior for an
  existing session or saved resource link.

**From the accepted #3 policy record:** account identifier rules, OIDC linking,
bootstrap and Docker CLI recovery, campaign creation and campaign-specific
role eligibility, invitation/acceptance/role assignment, revocation,
ownership/archive handling, and scene-publication details are confirmed by the
decision-maker.

#### Local identity fixtures for review

These synthetic cases describe policy and candidate session behavior, not
implemented authentication or an accepted OIDC contract.

| Initial state and action | Expected outcome |
| --- | --- |
| A local account signs in using its case-insensitively unique username or verified, unique email and correct password. | Return its opaque principal and a session-bound CSRF token; create no campaign membership or role merely from sign-in. |
| An unknown account, unverified email, or invalid password is used for local sign-in. | Return the same safe authentication failure without revealing account existence or creating a session. |
| A browser attempts local sign-in from an untrusted origin. | Reject the request without creating a session. |
| A session attempts an unsafe request with a missing or another session's CSRF token. | Reject the request without accepting the write. |
| An OIDC identity has the same email as an existing account but has not proved control of both identities. | Do not link the accounts or merge memberships or roles. |
| A user signs out, then makes a request using the revoked session. | Deny the later request. If session revocation fails, do not report a successful sign-out. |

#### Campaign role fixtures for review

These synthetic, semantic fixtures describe expected policy outcomes, not
implemented behavior or a frozen HTTP schema. Each case starts from the
stated memberships; identifiers represent fictional accounts and campaigns.
The eventual contract must specify wire shapes, revisions, errors, and
transaction boundaries separately.

| Initial state and action | Expected outcome |
| --- | --- |
| Authenticated `user-a` creates `campaign-a`. | `user-a` is its initial owner and GM. Creation grants no installation-administrator authority or membership in another campaign. |
| `user-a` is GM in `campaign-a` and Player in `campaign-b`. | Each role applies only within its campaign; neither role changes the other. |
| Current GM `user-a` changes current Player `user-b` to GM in `campaign-a`. | `user-b` becomes a GM in `campaign-a`; ownership and memberships in other campaigns do not change. |
| Player `user-b` attempts to grant themself GM status in `campaign-a` without an existing GM authorizing the change. | Deny the change; `user-b` remains a Player. |
| `user-a` is a GM in `campaign-a` but not a GM in `campaign-b`, and attempts to promote a member in `campaign-b`. | Deny the change in `campaign-b`; GM status in `campaign-a` grants no authority there. |
| An authenticated user redeems a valid transferable invitation for `campaign-a`. | The user becomes a Player in `campaign-a`; later GM role changes require an existing campaign GM. The invitation expires seven days after creation. |
| A campaign GM revokes `user-b`, who then requests campaign content using an existing session or saved link. | Deny requests started after revocation. Requests already in progress may finish; previously downloaded copies cannot be recalled. |
| The current owner nominates another current GM as successor. | Ownership remains unchanged until that GM accepts; acceptance transfers ownership without changing other memberships. |
| The last GM leaves or loses GM status before a successor accepts. | Archive the campaign and retain its data. Current Players retain read-only access only to content already published to them; private ideas and drafts remain hidden. |
| An installation administrator appoints a replacement GM for an archived campaign through the audited recovery action. | Permit the recovery action without granting the administrator campaign membership or content access. The restoration workflow remains a technical review item. |

#### Candidate v1 creation and role-change wire contract

The following examples cover creation and Player-to-GM promotion only. They
are proposals for review, not an accepted contract or an implemented API.
The linked drafts contain additional candidate operations, but other identity,
membership, Dapr, campaign, and scene details remain open.

| Operation | Candidate request | Candidate response |
| --- | --- | --- |
| Create a campaign | `POST /api/v1/campaigns` with `name`, `edition`, and `narrativeDirection`. The server derives the creator from the authenticated session; the client cannot select the owner or initial role. | `201 Created` with the new campaign identifier, creator as owner, and the creator's GM membership. No other campaign membership or installation privilege is granted. |
| Promote a current Player | `PATCH /api/v1/campaigns/{campaignId}/members/{memberId}` with `{"role":"gm"}` and `If-Match` containing the current membership ETag. The server verifies that the actor is a current GM in that campaign. | `200 OK` with the changed membership and its new ETag. The campaign owner and memberships in other campaigns do not change. |

`memberId` is an opaque identifier scoped to the campaign; it is not an
authorization claim. For example, an authenticated creation request and its
response could be:

```http
POST /api/v1/campaigns
Origin: https://example.invalid
Cookie: merlain_session=synthetic-session-cookie
X-CSRF-Token: synthetic-csrf-token
Content-Type: application/json

{"name":"campaign-example","edition":"edition-example","narrativeDirection":"narrative-example"}
```

```json
{
  "campaignId": "campaign-a",
  "ownerId": "user-a",
  "membership": {
    "campaignId": "campaign-a",
    "memberId": "member-a",
    "role": "gm"
  }
}
```

For Player-to-GM promotion, a synthetic request and response could be:

```http
PATCH /api/v1/campaigns/campaign-a/members/member-b
If-Match: "member-revision-1"
Origin: https://example.invalid
Cookie: merlain_session=synthetic-session-cookie
X-CSRF-Token: synthetic-csrf-token
Content-Type: application/merge-patch+json

{"role":"gm"}
```

```json
{
  "campaignId": "campaign-a",
  "memberId": "member-b",
  "role": "gm"
}
```

The response carries an updated `ETag` for the membership. A missing
precondition returns `428`; a stale precondition returns `412` without
changing the role. Invalid input returns `400`, an absent or expired session
returns `401`, and unavailable persistence returns `503` without reporting
success. A known member without GM authority receives `403`; an object the
caller cannot discover receives an opaque `404` without revealing its
existence. A candidate safe error envelope is:

```json
{
  "error": {
    "code": "revision_conflict",
    "message": "The membership changed. Refresh and try again.",
    "requestId": "request-example"
  }
}
```

The [candidate OpenAPI draft](campaign-api-v1.draft.yaml) proposes a
GM-authorized membership read that supplies the ETag. Technical-owner review
must resolve session and CSRF protection, creation retry/idempotency behavior,
and the remaining role and ownership transitions before this proposal can be
frozen. In particular, no archive-restoration behavior is inferred here.

#### Candidate invitation and revocation interfaces

The [campaign OpenAPI draft](campaign-api-v1.draft.yaml) proposes that a
current campaign GM issue a transferable invitation, expiring seven days
after creation, and that an authenticated recipient redeem it as a Player.
The proposed URL carries an unpredictable bearer token in its fragment;
the browser removes the fragment after extraction and submits the token in
the request body. This reduces exposure in initial server requests but does
not prevent a recipient from sharing the link. Tokens must not be logged,
traced, or exposed to third-party scripts. The issuance and redemption
responses, like other candidate campaign responses, require
`Cache-Control: no-store`; this cannot recall content already downloaded.

The draft also proposes `DELETE /api/v1/campaigns/{campaignId}/members/{memberId}`
with the current membership ETag. A current campaign GM authorizes the
revocation; later campaign requests by the revoked member are denied without
ending the installation-wide session. If the removal leaves no GM before a
successor accepts, the campaign is archived in the same successful transition.
The previously accepted archived-Player read-only boundary still applies.

These are **unapproved interfaces**, not tested runtime behavior. The number
of redemptions permitted per invitation, handling an already-current member,
whether a revoked member can rejoin using an existing invitation, and
ownership when an owner departs while other GMs remain are still open.
Prospective owners must review these cases, concurrency, error behavior, and
the invite-page token handling before the contract can be frozen.

### 4.2 Campaign, private idea, scene, and publication operations

Candidate contract surfaces should cover:

- create/read campaign with explicit edition and narrative direction;
- change a current campaign member's role from Player to GM through an
  operation authorized by an existing GM; roles remain campaign-specific;
- create/read/update a private GM idea, always scoped to its campaign;
- create/read/update a scene draft;
- publish a scene version to a selected, policy-approved audience;
- read the audience projection without returning private source material;
- report the current revision and reject or expose conflicting writes rather
  than silently overwriting one contributor's change.

Illustrative request/response fragments (not frozen schemas):

```json
{
  "name": "campaign-example",
  "edition": "edition-example",
  "narrativeDirection": "synthetic example"
}
```

```json
{
  "campaignId": "campaign-example",
  "revision": 1
}
```

```json
{
  "sceneId": "scene-example",
  "state": "draft",
  "revision": 2
}
```

```json
{
  "sceneId": "scene-example",
  "publication": {
    "audience": "audience-example",
    "revision": 3
  }
}
```

The values are placeholders, not approved enum values or policy. Final
contracts need concrete examples for: valid creation; invalid/missing edition;
private note read denied to a player; draft read denied to an unauthorized
audience; successful authorized publication; publication denied before the
policy-required approval; private source referenced by a published scene;
stale revision conflict; and unavailable persistence.

### 4.3 Audience projection and error behavior

The PRD requires no unauthorized or cross-campaign access in the acceptance
corpus, including after revocation and through derived results. For this
milestone, specify the authorized scene representation separately from
private notes/drafts, and make all access checks server-side. The publication
projection must not inherit rights to referenced private content.

**User-reviewed direction, pending #4 technical completion:** use a consistent error envelope with a
stable machine code, safe user-facing message, and request/correlation ID.
Represent validation details only for fields the caller may access; include
retry guidance only when retry is safe. Use standard HTTP status classes for
unauthenticated, forbidden, not found, invalid input, stale revision,
dependency unavailable, and persistence failure. Return an opaque not-found
response for private campaign objects the caller cannot discover; never
include their names, contents, or existence in error details.

**Accepted #3 policy:** GM-only publication, publication-time audience
selection, explicit republishing, and suspension after source/right changes
are recorded in the [policy decision](../../decisions/first-campaign-policies.md).
The contract must also preserve its revocation timing and archived-campaign
access rules.

#### Access and publication fixtures for review

These are synthetic policy-outcome fixtures, not a frozen API or evidence of
an implemented test. The complete contract must define the corresponding
request, response, error, and revision schemas.

| Initial state and action | Expected outcome |
| --- | --- |
| A GM saves a private idea containing `private-fixture-marker` and references it from a draft scene. | The GM can access the idea and draft; a Player cannot read either the idea or the draft through the reference. |
| A GM publishes a reviewed scene version to selected current Players. | Only the selected, currently authorized Players receive the published projection. It excludes the private idea, private sources, and unrelated draft data. |
| A new Player joins after a scene was published to all then-current Players. | The new Player is not added to that publication's snapshot audience automatically. |
| A GM edits a published scene's draft without republishing. | Player views continue to show only the current published version, not the new draft edits. |
| A relevant source or right changes, or disclosure is suspected. | Suspend the affected projection until GM review; a reference must not expose its private source. |
| An authorized Player writes a human shared-journal entry. | Publish that entry directly to authorized participants without prior GM approval; this does not publish private GM notes or scene drafts. |
| Two authorized contributors update the same version; the second submits a stale revision. | Reject or report the conflict rather than silently overwriting the first accepted update. |
| Persistence is unavailable while an authorized user saves content. | Report an explicit failure; do not return a successful-save response or imply that data survived. |

### 4.4 Versioning and change procedure

**User-reviewed direction, pending #4 technical completion:** describe HTTP interfaces in versioned
OpenAPI 3.1 documents under the shared contracts directory, with shared
synthetic request/response fixtures and schema validation. Use `/api/v1` for
the first version; compatible additive changes stay in that version, while
incompatible changes require a new major path and a migration note. Use
optimistic concurrency with a revision/ETag and a precondition on updates so
stale writes fail rather than overwrite accepted work. Assign one
shared-contract editor during foundations/integration; service owners
maintain their own schemas, migrations, and tests, while feature teams propose
changes rather than independently editing shared files.

## 5. Dapr and Compose discovery questions

**Evidence:** the product constraint is local Compose execution for MerlAIn
application services, with a polyglot Dapr target. Configured external
dependencies, including OIDC, are permitted; a remotely hosted MerlAIn
application service and an AI provider are not required for the manual core.
No current Compose stack or Dapr configuration exists.

User-reviewed choices for the #4 proposal; final contract and owner review remain pending:

1. Keep three app deployables: web (with same-origin backend-for-frontend),
   identity, and campaigns. Run Dapr sidecars for the service-to-service
   request path; do not make Dapr itself a user authorization authority.
2. Use synchronous Dapr service invocation for browser-originated workflow
   requests routed by the backend-for-frontend to identity or campaigns.
   Authentication, campaign creation, idea/scene editing, and publication all
   need immediate outcomes. Do not add pub/sub, a broker, actors, or domain
   events without an evidenced asynchronous consumer.
3. Keep the frontend outside Dapr's direct trust boundary. Pass the
   authenticated principal through a server-controlled session context;
   campaigns still resolve membership and enforce content access themselves.
   Timeouts and dependency failures must be explicit and fail closed.
4. For local persistence, prefer one PostgreSQL container with separate
   service-owned databases or schemas and migrations. Each service alone
   reads/writes its owned data; prohibit cross-service joins and direct
   database access from the web application. PostgreSQL is a recommendation,
   not a repository fact or frozen choice.
5. Define a `manual-core` Compose profile for web, identity, campaigns,
   PostgreSQL, and the required Dapr runtime pieces. Optional AI/import
   capabilities must be independently disabled by default and report
   disabled, available, or unavailable without changing permissions.
6. Use documented local-safe defaults and placeholder configuration. External
   OIDC configuration is optional; do not require provider credentials for
   local login or commit secrets. Startup health checks must distinguish
   readiness from liveness and must not imply a successful save.
7. Run service-owned, non-destructive migrations after storage readiness.
   Persisted campaigns, notes, scenes, membership, publication state, and
   revisions must survive controlled restart; migration or storage failure
   must stop readiness and report an explicit failure.

The technical owner should confirm the service IDs, exact Dapr endpoints,
Compose layout, PostgreSQL database/schema isolation, session expiry, and
health-check details before the contract is frozen. These are review items,
not inferred implementation facts.

These are discovery questions for #4. Compose files, Dapr components, service
scaffolding, and a local run command belong to #5 after acceptance, not to this
document's implementation.

## 6. Ownership and path proposals

The following is an **exclusive ownership proposal**, not a claim that the
paths currently exist or that named people are assigned. It intentionally
separates current documentation evidence from possible future code paths.

| Scope | Current repository evidence | Candidate future path/owner | Constraint to resolve in #4 |
| --- | --- | --- | --- |
| Discovery record | `docs/implementation-readiness.md` exists; this draft is new. | `docs/architecture/first-campaign/discovery.md` — platform architecture/technical lead. | Review and freeze only after #3 decisions and contract review. |
| Web client and backend-for-frontend | No web app or package manifest exists. | `apps/web/` — frontend owner; owns the same-origin browser boundary and API routing. | Framework and exact proxy/session implementation remain open. |
| Identity | No identity service or schema exists. | `services/identity/` — identity owner. | Exact boundary, principal contract, credentials/session storage, and migration ownership remain open. |
| Campaign domain | No campaign API, schema, or migration exists. | `services/campaigns/` — campaign owner. | Own campaign, membership, private-note, scene, and publication data if this candidate service is accepted. |
| Shared contract | No runtime contract directory exists. | `contracts/first-campaign/v1/` — foundations owner initially, then integration owner; OpenAPI and fixtures. | Single editor/approver; no competing schema writers. |
| Service migrations and tests | None exist in the current repository. | `services/identity/migrations/`, `services/identity/tests/`; `services/campaigns/migrations/`, `services/campaigns/tests/`. | Each service owns its migrations and tests; no cross-service migration writer or direct cross-schema join. |
| Compose and Dapr | No Compose or Dapr configuration exists. | `compose.yaml`, `dapr/` — foundations owner, then integration owner. | One owner for shared startup/configuration; local-only application services and explicit optional profiles. |
| Shared startup/manifests | No app entrypoints, manifests, or lockfiles exist. | Root manifests and service registration/startup files — foundations owner, then integration owner. | Assign dependency/project/lockfile ownership before parallel work; feature teams request shared changes. |

`apps/web/`, `services/*`, `contracts/*`, `compose.yaml`, and `dapr/` are
**proposed paths only**. In particular, historical `apps/api`, `apps/agents`,
`infra/postgres`, and `infra/traefik` are not treated as present or approved.
If the split or paths change, update this matrix before handing work to
contributors.

## 7. Tests and fixtures to define

These are test-design inputs, not evidence that tests or a runtime exist.
Contract fixtures may unblock independent UI/service work, but mocks do not
prove end-to-end authentication or durable persistence.

| Test area | Minimum discovery/acceptance cases |
| --- | --- |
| Contract shape | Valid and invalid examples for identity context, campaign creation, private idea, draft scene, publication projection, revisions, and errors. Validate version/schema compatibility. |
| Authentication and authorization | No session on invalid/disabled/unavailable authentication; no role from successful login alone; member and non-member access; cross-campaign denial; revocation blocks later reads through direct and saved links. |
| Confidentiality | Player cannot read private idea or draft by API, navigation, cached link, search, or an indirect scene reference; published response excludes private notes, sources, and unrelated metadata. Include both allow and deny assertions. |
| Publication and revisions | Policy-authorized publish succeeds; disallowed audience/actor fails; stale revision reports conflict without silent overwrite; policy changes do not silently broaden access. Fixtures must reflect the accepted #3 scene policy. |
| Persistence and failure | A successful saved campaign/note/scene/membership/publication survives controlled restart; unavailable persistence is an explicit failed save, not a success response; optional services being disabled do not erase core data. |
| UI/accessibility | Keyboard-operable manual workflow, visible focus, associated form labels/errors, status announcements, logical headings, zoom/reflow, and a non-drag alternative where relevant. NFR-CORE-010 targets WCAG 2.2 AA but is not a certification claim. |
| Optional dependencies | AI disabled and no AI endpoint configured; remote identity dependency failure is explicit if that login mode is selected, without opening access or blocking local login when the latter remains independently available. |

Use synthetic principal/campaign identifiers and obvious placeholder content.
Include a private-note marker and assert it never appears in player output.
Fixtures must never contain real credentials, production data, or copyrighted
game-rule text. Any authentication fixture must be visibly test-only and must
not ship as a real login path or successful production persistence shortcut.

## 8. Issue #3 policy inputs and acceptance boundary

The [issue #3 policy record](../../decisions/first-campaign-policies.md) names
@Justrebl as decision-maker and records the accepted account, campaign
creation, campaign-specific GM/Player eligibility, membership, recovery,
ownership/archive, and scene-publication choices. @Justrebl reviewed and
accepted these policies on 2026-11-13. Issue #3 remains open until its
remaining acceptance criteria are completed.

| Policy decision needed | Contract affected | Why it blocks finalization |
| --- | --- | --- |
| Campaign-specific role-change command and last-GM transition. | Role assignment, ownership transfer, and archive/read-only transitions. | Any platform user can hold a GM or Player role per campaign; an existing campaign GM authorizes later GM role changes. The contract must preserve these policies without modeling GM as a global privilege. |
| Confirmed account, recovery, identity-linking and admin separation choices. | Account creation/login, OIDC link, one-time bootstrap, Docker recovery, principal context. | Preserve the exact confirmed rules; do not replace them with email-based account merging or implied admin privileges. |
| Confirmed invitation, role, revocation, transfer, and last-GM/archive choices. | Membership commands, authorization context, role lifecycle, archive/read-only behavior. | Preserve seven-day transferable links, Player initial role, immediate denial for later requests, accepted-owner transfer, and archived-content boundaries. |
| Accepted scene-publication audience, edit/history, and changed-right handling. | Publish request, audience projection, revisions, source isolation, and read authorization. | Preserve GM-only publication, publication-time audience snapshot, explicit republishing, and suspension-after-change behavior. |
| Actor/action/content examples for accepted rules. | Contract fixtures and authorization acceptance tests. | Encode both allow and deny assertions without treating mocks as implementation evidence. |

Issue #3 is the policy workstream. The decision-maker's acceptance supplies
policy input for the contract; it does not by itself complete or close either
issue. Complete the outstanding technical contract and prospective-owner
reviews before freezing interfaces.

## 9. Pattern assessment and remaining technical evidence

**Builder / Strategy / Command:** no pattern is selected for every boundary.
The PRD constraint CON-CORE-006 says to use them only when relevant and
justified. Candidate questions for the technical review:

- **Strategy:** consider only if identity-provider selection or audience
  policy genuinely needs interchangeable implementations. A single configured
  provider does not by itself justify an abstraction.
- **Command:** consider only if campaign actions need explicit audit,
  idempotency, queueing, or revision semantics that ordinary request handling
  cannot satisfy. Do not add a command bus solely to apply the pattern.
- **Builder:** no concrete need is established for campaign creation in this
  slice; assess only if required campaign construction has meaningful
  validated optional parts or ordering.

These are evaluation prompts, not architecture decisions.

Technical questions to keep distinct from #3 policy decisions include
service/language boundaries, API/contract encoding, session and token handling,
service addressing, Dapr invocation needs, persistence technology and
service-owned schema layout, revision mechanism, Compose profiles, and
framework-instruction overlap. The PRD records E10 as future technical
discovery and the readiness guide notes that retained stack instructions do
not select the implementation stack.

## 10. Review gates and handoff

Before calling the #4 contract ready for acceptance, prospective identity,
campaign, frontend, and platform/integration owners should be able to identify:

1. accepted #3 policy references and unresolved blockers for each contract;
2. stable request, response, identity/membership, audience, error, and revision
   semantics with positive and negative examples;
3. one owner for each service schema/migration and every shared contract,
   Compose/Dapr, startup, manifest, and lockfile path;
4. test fixtures and which checks require real integrated authentication or
   persistent storage rather than mocks; and
5. Dapr/Compose choices or explicit deferrals that preserve local app
   execution, durable data, optional-module honesty, and no-AI manual operation.

The GitHub issue's definition of done requires actual review by the prospective
identity, campaign, and UI owners; this document's §10 additionally recommends
platform/integration review for the handoff. Issues #6, #7, and #8 have no
assignees, and no platform/integration reviewer is identified. Until the
required owners are identified and have reviewed the contract and path matrix,
this candidate is not accepted and #5 remains blocked.

The user reviewed this proposal and requested implementation. The document
still lacks the completed interfaces and prospective-owner review needed for
#4 acceptance; it does not authorize a claim that those gates have passed.
Issue #5 remains dependent on accepted #4 contracts. No game-rule text,
credentials, or real user data are included.

## Sources

- [Issue #3 — first-campaign policies](https://github.com/CoffeesoftDotDev/MerlAIn/issues/3)
- [Issue #4 — contracts and ownership](https://github.com/CoffeesoftDotDev/MerlAIn/issues/4)
- [Issue #5 — local Compose and Dapr foundations](https://github.com/CoffeesoftDotDev/MerlAIn/issues/5)
- [Core PRD](../../../.copilot-tracking/squad/members/produit/prd/2026-09-28/merlain-prd-v0.1.md):
  FR-CORE-001–006, 023–024, 029–030; NFR-CORE-003, 006, 010, 015;
  CON-CORE-001/002/006; R01/R02 and E01/E02/E10.
- [Issue #3 policy decision record](../../../docs/decisions/first-campaign-policies.md):
  decision-maker-reviewed first-campaign policies.
- [Implementation readiness](../../implementation-readiness.md): current
  specifications-only baseline, work dependencies, and historical-path warning.

AI-assisted discovery draft. The user reviewed the proposal; the complete
technical contract and prospective-owner reviews remain required.

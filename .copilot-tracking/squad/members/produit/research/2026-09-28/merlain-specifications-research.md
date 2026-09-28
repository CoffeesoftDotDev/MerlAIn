<!-- markdownlint-disable-file -->
# Task Research: merlain-specifications

| Field              | Value                                         |
|--------------------|------------------------------------------------|
| Date               | 2026-09-28                                     |
| Researcher / agent | Squad Researcher (produit)                     |
| Output mode        | analysis (matrice décisions vs propositions vs questions ouvertes) |

## Executive Summary

* Résultat principal : le squelette de code (`apps/`) est un monolithe modulaire .NET/React délibérément placeholder (deux modules, données en mémoire, aucune persistance, aucun Dapr) ; il ne contredit pas encore la vision produit polyglotte/Dapr de l'utilisateur mais ne la met pas en œuvre non plus.
* Pourquoi c'est important : plusieurs entrées de `decisions.md` du sous-squad produit affichent des formulations (« tous les champs », « visible par le MJ uniquement », « last-write-wins », microservices Dapr affirmés comme actifs) qui vont au-delà des énoncés utilisateur fournis et ne doivent pas être traitées comme des exigences approuvées.
* Statut de la recherche : complet pour un cycle unique, posture `focused` (spécifications locales, périmètre restreint, sans réseau).
* Confiance et incertitudes : confiance élevée sur les constats de code (lecture directe) ; incertitude volontairement préservée sur l'arbre vs graphe des chroniques, la politique de concurrence des fiches, la portée exacte de la modération du journal, et les frontières produit/plateforme sur l'IA.

## What You May Not Know

* Le README (section 5) affirme explicitement que « le frontend modulaire n'implique pas des microservices backend » et que les modules backend sont compilés dans un seul processus `api` — l'architecture actuelle est l'inverse de « microservices polyglottes via Dapr » évoqué par l'utilisateur ; c'est un choix de scaffold, pas une décision produit figée (C1).
* Le mot « dapr » n'apparaît nulle part dans le code, le compose ou le README : il n'existe que dans les fichiers de suivi squad (`meta-routing.md`, `federation.md`, `decisions.md`), donc uniquement comme intention de routage/portée, jamais comme implémentation (C9).
* `decisions.md` du sous-squad produit contient des affirmations non demandées par l'utilisateur : « all fields » éditables, « Last-write-wins », « visible au MJ uniquement », et une décision citant des « microservices Dapr » actifs sans preuve de code — ces éléments sont signalés ci-dessous comme non supportés (C12).

## Findings

### Écart architecture : monolithe modulaire vs vision « microservices polyglottes Dapr »

Le code livré est un monolithe : un seul projet `api` (.NET 10, minimal API) hébergeant tous les modules de fonctionnalités, découverts par scan d'assembly, plus un worker `agents` séparé. Le README revendique ce choix comme volontaire (tableau comparatif « modular monolith vs microservices »).

* Questions : Q1
* État de la preuve : constat de code vérifié (evidence-backed finding)
* Preuve : C1 (README §3, §5), C2 (`docker-compose.yml`, aucun service Dapr/sidecar), C9 (grep « dapr » sur tout le repo)
* Confiance et limites : élevée sur l'état actuel du scaffold ; ne dit rien sur si/quand la cible Dapr polyglotte doit remplacer ce choix — c'est une décision produit/plateforme à trancher, pas un fait déjà réglé.

### Aucune persistance, auth ou workflow de domaine réel n'est implémenté

`CampaignsModule` et `BestiaryModule` retournent des tableaux statiques en mémoire ; leurs commentaires disent explicitement « Scaffold only… Persistence, authorisation and MCP tools arrive with their respective phases ». `AgentRunWorker` ne fait qu'un heartbeat de log, sans file Redis ni appel LLM. `Program.cs` active SignalR mais le README précise que le backplane Redis arrive « en phase 2 ». Aucun module Characters/Journal/Fiches n'existe.

* Questions : Q2, Q3
* État de la preuve : evidence-backed finding
* Preuve : C4 (`CampaignsModule.cs`), C5 (`BestiaryModule.cs`), C15 (`AgentRunWorker.cs`), C3 (`Program.cs`), C14 (contenu de `apps/api/Modules/` limité à Campaigns + Bestiary)
* Confiance et limites : élevée ; confirme que toute exigence produit (campagnes en arbre, fiches, journal, MJ/joueur) est encore à spécifier puis construire, rien n'est « déjà fait ».

### Authentification : compte local existe en principe, mais en email/mot de passe, pas « username »

Le compose et `Program.cs` exposent un mode « local accounts » (email + mot de passe, Argon2id) et un mode OIDC optionnel, activable par variable d'environnement (`OIDC_AUTHORITY` vide = bouton SSO absent). La page de connexion (`SignInPage.tsx`) n'a que des champs email/mot de passe, sans logique de soumission réelle. Le modèle de domaine (README §7) définit `User.email`, pas de champ `username`.

* Questions : Q4
* État de la preuve : evidence-backed finding (écart confirmé), pas encore une exigence tranchée
* Preuve : C7 (`SignInPage.tsx`), C8 (README §7 modèle `User`), C2 (`docker-compose.yml` vars `AUTH_LOCAL_ACCOUNTS_ENABLED`, `AUTH_ALLOW_REGISTRATION`), C3 (`/api/auth/config`)
* Confiance et limites : l'utilisateur a demandé « nom d'utilisateur/mot de passe local » — le scaffold n'offre qu'un placeholder email, ce qui est un écart à clarifier (voir Q4), pas une contradiction bloquante puisque rien n'est branché à une vraie base.

### Distinction OIDC vs OAuth2 non résolue dans le scaffold

Le README et le compose n'emploient que le vocabulaire « OIDC » (Authority, Discovery/metadata, scopes `openid profile email`), jamais « OAuth2 » générique. La clarification utilisateur mentionne « Authentik or other OAuth2 » — recommandation de terminologie à clarifier (OIDC est un sur-ensemble d'OAuth2 avec identité), mais ni le code ni les décisions produit ne tranchent formellement ce point pour la spec.

* Questions : Q4
* État de la preuve : conjecture partiellement supportée (le scaffold documente OIDC uniquement)
* Preuve : C2, C3, C1 (README §9 « Identity »)
* Confiance et limites : moyenne ; recommandation de nommer explicitement « OIDC (sur-ensemble OAuth2 avec identité) » dans la PRD plutôt que d'employer les deux termes de façon interchangeable, sans imposer cette décision ici.

### Fournisseurs distants optionnels déjà reflétés dans le compose (Redis, LLM, image, Postgres, S3)

Le compose définit des variables pour Postgres, Redis, un fournisseur LLM (`ollama` ou `openai-compatible`), un endpoint image, et S3 (MinIO ou externe). Cela correspond bien à la clarification « par défaut local, mais fournisseurs distants possibles », et non à un « tout doit rester strictement local ».

* Questions : Q1
* État de la preuve : evidence-backed finding, cohérent avec la clarification la plus récente de l'utilisateur
* Preuve : C2 (`docker-compose.yml` variables `POSTGRES_HOST`, `REDIS_CONNECTION`, `LLM_PROVIDER`, `LLM_ENDPOINT`, `S3_ENDPOINT`)
* Confiance et limites : élevée pour l'état actuel ; ne couvre pas encore Authentik/OAuth2 comme fournisseur distant nommé explicitement (seul `OIDC_AUTHORITY` générique existe).

### Le sous-squad produit a consigné des décisions dépassant les énoncés utilisateur (hygiène de preuve)

`decisions.md` (produit) contient plusieurs entrées à traiter comme NON SUPPORTÉES tant que l'utilisateur ne les confirme pas explicitement : « all fields » éditables par le joueur, « Conflict handling: Last-write-wins », « Privacy: Edit history visible to GM only… », et une décision qui affirme que « MerlAIn application and Dapr microservices remain LOCAL by default » sans aucune preuve de code correspondante.

* Questions : D1–D6 (voir Décisions et retours)
* État de la preuve : réclamations affaiblies / non résolues (weakened claims), signalées explicitement, non reconduites comme faits
* Preuve : C12 (`.copilot-tracking/squad/members/produit/decisions.md`, entrées « MerlAIn GM & Player Content Visibility Model », « Player Character Sheet Ownership & Direct Editing », « Deployment Scope Clarification »), C9 (absence de Dapr dans le code, contredit l'affirmation de microservices actifs)
* Confiance et limites : élevée sur l'écart texte-à-texte entre citation utilisateur fournie et formulation du Scribe ; ce document ne corrige pas `decisions.md` (propriété Scribe), il documente l'écart pour la suite.

## Recommendation and Alternatives

* État de la décision : pas de convergence forcée (mode `analysis`) — ce document ne choisit pas d'architecture ni de modèle de données ; il documente l'état vérifié et signale les questions à trancher par un humain PM.
* Rationale : périmètre confié est « recherche uniquement, pas de plan/PRD » ; les six décisions en attente (Risques et questions ouvertes) doivent être posées une à la fois avant tout arbitrage.
* Ce qui pourrait changer ce résultat : une réponse utilisateur sur l'arbre vs graphe des chroniques, ou sur la politique de concurrence des fiches, changerait matériellement la portée du prochain PRD.

| Option (illustrative, non retenue)                     | Bénéfices                          | Coûts et risques                                   | Preuve      | Disposition |
|----------------------------------------------------------|-------------------------------------|------------------------------------------------------|-------------|--------------|
| Garder le monolithe modulaire actuel comme socle v1       | Correspond au scaffold existant, moins d'effort | Ne réalise pas l'ambition « microservices polyglottes Dapr » telle qu'énoncée | C1, C9      | à trancher par PM/plateforme |
| Cibler Dapr dès la PRD, quitte à réécrire le scaffold     | Aligné sur la demande utilisateur   | Réécriture significative de l'existant, effort non chiffré ici | C9, C2      | à trancher par PM/plateforme |

## Scope and Questions

* Objectif : produire des preuves vérifiées pour informer une future PRD produit MerlAIn, sans décider ni planifier.
* Audience et usage : Squad Lead (Plan stage) et interview PM humaine à venir.
* Dans le périmètre : `README.md`, `apps/api/Program.cs`, `apps/api/Modules/{Campaigns,Bestiary}`, `apps/web/src/shell` (registre de modules, page de connexion), `docker-compose.yml`, métadonnées squad (`federation.md`, `meta-routing.md`, `team.md`, `routing.md`, `state.json`), lecture (non correction) de `decisions.md` produit.
* Hors périmètre : accès réseau/fournisseurs live, toute édition de `decisions.md`/`history`/`state.json` (propriété Scribe), planification ou PRD, chiffres de performance/matériel non fournis, popularité produit, licences externes.
* Critères de décision/preuve : chaque affirmation doit citer un fichier réel (`C#`) ; toute formulation issue du Scribe non alignée mot pour mot avec une citation utilisateur fournie est signalée comme non supportée plutôt que reconduite.
* Sortie demandée : matrice décision/preuve/statut, écarts de code, et jusqu'à six questions PM priorisées.

| ID | Question                                                                                   | Source                      | Statut  |
|----|---------------------------------------------------------------------------------------------|------------------------------|---------|
| Q1 | Le scaffold local (monolithe, sans Dapr) correspond-il à l'intention polyglotte Dapr ?      | dérivée du brief utilisateur | répondue (écart documenté) |
| Q2 | Quels modules de domaine manquent pour couvrir campagnes/chroniques/personnages/journal ?   | dérivée du brief utilisateur | répondue |
| Q3 | Le worker `agents` réalise-t-il déjà une génération IA texte/image ?                        | dérivée du brief utilisateur | répondue (non) |
| Q4 | Le mécanisme d'auth du scaffold couvre-t-il compte local par nom d'utilisateur + OIDC/OAuth2 ? | explicite (brief utilisateur) | répondue (écart email vs username documenté) |

## Decisions and Feedback

| Groupe | Décision ou retour                                                                                   | Statut                | Propriétaire         | Rationale / preuve nécessaire                                                                                   | Preuve      | Impact                                   |
|--------|--------------------------------------------------------------------------------------------------------|------------------------|-----------------------|--------------------------------------------------------------------------------------------------------------------|-------------|--------------------------------------------|
| D1     | Déploiement local par défaut, fournisseurs distants possibles (Redis, IA, OAuth2/OIDC, Postgres)      | confirmé (utilisateur) | user                  | Citation directe fournie ; reflété dans `docker-compose.yml`                                                       | C2          | Cadre la PRD infra ; pas de politique « tout local strict » |
| D2     | Distinction terminologique OIDC vs OAuth2 à faire dans la PRD                                          | proposition (recherche) | evidence              | Scaffold ne documente qu'OIDC ; recommandation, pas une décision utilisateur                                       | C1, C2, C3  | Clarifier le vocabulaire évite l'ambiguïté fournisseur |
| D3     | Compte local par nom d'utilisateur/mot de passe                                                        | non résolu             | user                  | Scaffold n'offre qu'un champ email ; l'utilisateur a demandé un compte local nommé, écart à confirmer              | C7, C8      | Détermine si le modèle `User.email` doit changer |
| D4     | Arbre de chroniques vs graphe reconvergent pour les campagnes                                          | non résolu (à poser)   | user                  | Explicitement laissé ouvert par l'utilisateur ; aucune preuve de code (aucun module Chronicles n'existe)            | C14         | Structure de données et UI de branchement en dépendent |
| D5     | Édition de fiche joueur : « tous les champs » éditables                                                | **non supporté (embellissement Scribe)** | agent (à corriger)   | Citation utilisateur ne mentionne que « le joueur modifie directement sa fiche » ; pas de portée « tous les champs » exhaustive (champs dérivés/système/secrets non exclus) | C12 | Ne pas reconduire comme exigence tant que non confirmé |
| D6     | Concurrence d'édition : « last-write-wins »                                                             | **non supporté (embellissement Scribe)** | agent (à corriger)   | Aucune citation utilisateur n'approuve une résolution de concurrence ; à traiter comme question ouverte             | C12         | Modèle de données/API d'édition concurrente en dépend |
| D7     | Historique d'édition « visible au MJ uniquement »                                                       | **non supporté (embellissement Scribe)** | agent (à corriger)   | La citation utilisateur dit seulement « historique visible par le MJ », sans exclure d'autres visibilités          | C12         | Portée de confidentialité de l'historique à clarifier |
| D8     | Journal partagé : chaque participant publie ses propres entrées, le MJ peut modérer                    | confirmé (utilisateur) | user                  | Citation directe fournie                                                                                             | (citation utilisateur) | Actions/historique de modération restent à définir |
| D9     | Publication IA : IA peut enrichir la préparation privée du MJ, le MJ valide toute publication aux joueurs | confirmé (utilisateur) | user                  | Citation directe fournie ; ne s'applique pas aux entrées humaines du journal (publication directe)                  | (citation utilisateur) | Ne pas généraliser la porte IA à toute entrée de journal humaine |
| D10    | « Microservices Dapr actifs en local par défaut » (affirmation du Scribe)                               | **non supporté (contredit par le code)** | agent (à corriger)   | Aucune trace de Dapr dans le code/compose ; le README revendique explicitement l'inverse (monolithe)               | C1, C2, C9  | Ne pas présenter comme un fait acquis dans une future PRD |
| D11    | Cartes de personnage : équipement en carte, pas nécessairement toute la fiche en carte                  | confirmé (utilisateur) | user                  | Citation directe fournie                                                                                             | (citation utilisateur) | Distingue rendu « carte » (équipement) du rendu « fiche complète » |
| D12    | Adaptateur IA texte/image compatible OpenAI, pas d'adaptateur Anthropic spécifique                      | confirmé (utilisateur) | user                  | Citation directe fournie ; le compose utilise déjà `LLM_PROVIDER=ollama\|openai-compatible`, cohérent               | C2          | Aligné avec le scaffold existant, aucune contradiction |

## Risks and Open Questions

Six questions priorisées, à poser une à la fois lors d'un futur entretien PM (aucune n'est tranchée ici) :

| Priorité | Type              | Question à poser au PM                                                                                          | Impact                                   | Preuve/action minimale                        | Propriétaire        |
|----------|-------------------|--------------------------------------------------------------------------------------------------------------------|---------------------------------------------|-----------------------------------------------|-----------------------|
| H        | question ouverte  | Les chroniques d'une campagne doivent-elles former un arbre strict (une seule branche parente) ou un graphe reconvergent (branches qui peuvent se rejoindre) ? | Structure de données, UI de branchement, complexité du moteur de navigation | Réponse utilisateur ; aucun module Chronicles n'existe encore (C14) | user |
| H        | question ouverte  | L'authentification locale doit-elle utiliser un identifiant « nom d'utilisateur » distinct de l'email, et le modèle `User` doit-il changer en conséquence ? | Modèle de domaine `User`, formulaire de connexion, migrations | C7, C8 |
| M        | question ouverte  | Quelle politique de concurrence d'édition de fiche (verrouillage, fusion, avertissement) doit remplacer l'hypothèse non approuvée de « last-write-wins » ? | Modèle d'édition de fiche, API, UX de conflit | C12 (D6) |
| M        | question ouverte  | Quels champs de la fiche personnage sont réellement éditables par le joueur (tous, ou un sous-ensemble hors champs dérivés/système/secrets) ? | Validation, sécurité des données dérivées, confiance MJ/joueur | C12 (D5) |
| M        | question ouverte  | L'historique de modification de fiche doit-il être visible uniquement par le MJ, ou aussi par d'autres rôles/joueurs dans certains cas ? | Modèle de confidentialité, conception d'audit | C12 (D7) |
| L        | question ouverte  | Quelles éditions de règles (D&D, The One Ring) et quelles sources de contenu officiel légal doivent être disponibles, et sous quelle forme d'accès ? | Portée du contenu de référence, conformité de licence | citation utilisateur (« unsettled ») |

## Planning Readiness and Next Step

| Champ                             | Enregistrement                                                                                                    |
|------------------------------------|---------------------------------------------------------------------------------------------------------------------|
| Research disposition               | executed                                                                                                            |
| Decision participation             | agent-owned (rôle researcher, tier `auto` selon `routing.md`) ; les 6 questions PM restent `user-owned` pour un entretien futur |
| Planning Readiness                 | ready-with-gaps — les écarts D3–D7, D10 et les 6 questions ci-dessus doivent être résolus avant une PRD complète     |
| Research depth and helpers         | 1 cycle complet (Wider/Deeper/Contrarian) ; aucun helper délégué (contrainte : pas de délégation, pas de réseau)     |
| Blockers                           | aucun bloquant pour le stage Plan ; les 6 décisions sont des gaps à combler par une interview PM, pas des blocages   |
| Output mode and planning support   | analysis ; supporte une planification ultérieure conditionnée aux réponses PM                                       |
| Continuation owner                 | Squad Lead (Plan stage), après entretien PM sur les 6 questions                                                      |
| Required gates or confirmations    | Artifact Gate : artefact écrit sur disque (ce fichier) ; entrée d'historique Scribe encore requise (hors périmètre ici) |
| Next action                        | Coordinateur : relayer les 6 questions au PM en chat, une par une, avant de dispatcher le Plan stage                 |
| Primary evidence file              | .copilot-tracking/squad/members/produit/research/2026-09-28/merlain-specifications-research.md                     |

## Research Record

### Method and Boundaries

| Champ                             | Enregistrement                                                                    |
|-------------------------------------|--------------------------------------------------------------------------------------|
| Research posture and provenance    | `focused` ; imposée par le dispatch (« bounded research stage », périmètre local restreint) |
| Completion basis                   | Scaffold restreint (2 modules backend, 2 modules frontend) ; saturation atteinte après une passe complète des fichiers listés |
| Explicit limits or deadline        | Pas de réseau, pas de délégation, pas d'écriture hors du root produit/research           |
| Codebase and external scope        | `apps/`, `docker-compose.yml`, `README.md`, `.copilot-tracking/squad/*` (lecture seule) ; aucun scope externe (réseau désactivé) |
| Initial candidate areas            | Roadmap README vs code réel, modules Campaigns/Bestiary, registre de modules web, page de connexion, compose, métadonnées d'équipe/routage |
| Evidence root                      | `.copilot-tracking/squad/members/produit/research/2026-09-28/` (root confié par le coordinateur, cohérent avec `team.md`) |
| Constraints and excluded sources   | Aucune source externe (W#) ; aucune correction de `decisions.md`/`history`/`state.json` (propriété Scribe) |
| Prior knowledge                    | `decisions.md` produit lu pour hygiène de preuve uniquement ; ses embellissements non reconduits comme faits |

### Extensions and Participation

| Type        | Candidat                                    | Provenance et portée                                          | Sélection                          |
|-------------|----------------------------------------------|-----------------------------------------------------------------|--------------------------------------|
| skill       | rpi-research                                 | Squad Researcher charter, contrat obligatoire de ce stage       | sélectionné (source de vérité)       |
| règle squad | `squad-state.md` (preuve de dispatch)        | Exige artefact sur disque + entrée Scribe                       | sélectionné pour la portée artefact  |
| règle squad | `squad-roster.md` (Deliverable Root)         | Root confirmé identique à celui fourni par le coordinateur      | sélectionné, cohérence vérifiée (C11) |
| skill       | pull-request, hve-builder, etc.               | Hors sujet (recherche produit, pas de code/artefact HVE)         | ignoré                               |

| Checkpoint      | Question / direction                                                        | Réponse / rationale sans interaction                                    | Effet                                   |
|-----------------|--------------------------------------------------------------------------------|------------------------------------------------------------------------------|--------------------------------------------|
| intake          | Faut-il re-vérifier les taux/état de consommation Scribe ?                    | Non — instruction explicite du coordinateur : hors périmètre                  | Exclu du périmètre ; focus produit uniquement |
| intake          | Faut-il déléguer une lane à un RPI Researcher ?                                | Non — contrainte explicite « ne pas déléguer », preuve suffisante et petite   | Cycle unique exécuté directement            |

### Research Cycle Log

#### Cycle 1

* Active posture, controls, and limits : `focused`, pas de réseau, pas de délégation, écriture confinée au root produit/research.

##### Wave 1: Wider

* Focus et questions : inventorier ce qui existe réellement (README, apps/, compose, métadonnées squad) vs ce que l'utilisateur demande (Dapr polyglotte, auth locale nommée, campagnes en arbre/graphe, personnages, journal, IA).
* Preuve : C1–C3, C6–C8, C10, C11, C13 rassemblées par lecture directe des fichiers listés.
* Reflection : le scaffold est un point de départ minimal et honnête (commentaires « scaffold only » explicites) ; aucun module de domaine métier (campagnes réelles, personnages, journal) n'existe encore.

##### Wave 2: Deeper

* Focus et questions : vérifier précisément l'absence de Dapr, l'état de l'auth, le contenu exact de `decisions.md` produit face aux citations utilisateur fournies.
* Preuve : C4, C5, C9, C12, C14, C15.
* Reflection : confirmation que plusieurs entrées `decisions.md` dépassent la citation utilisateur fournie (D5–D7, D10) ; à signaler explicitement plutôt qu'à corriger (hors périmètre d'écriture).

##### Wave 3: Contrarian

* Focus et questions : chercher une preuve de code qui contredirait le constat « pas de Dapr, pas de persistance » (ex. fichiers de configuration Dapr cachés, composants ignorés).
* Preuve : grep insensible à la casse sur « dapr » dans tout le dépôt (C9) ; seuls des fichiers `.copilot-tracking/squad/*` correspondent, aucun fichier de code ou de déploiement.
* Reflection : aucune preuve contraire trouvée ; le constat « aucune implémentation Dapr » est confirmé, pas seulement une absence de recherche.

##### Synthesis and Re-entry

| Matériau ou affirmation                                    | Preuve      | Disposition | Rationale                                                                 | Effet utilisateur                              |
|--------------------------------------------------------------|-------------|-------------|------------------------------------------------------------------------------|---------------------------------------------------|
| Scaffold = monolithe modulaire, pas de Dapr                  | C1, C2, C9  | accepted    | Confirmé par lecture directe et grep exhaustif                               | Finding « écart architecture » ci-dessus            |
| « all fields », « last-write-wins », « GM only » (decisions.md) | C12         | rejected (comme fait) | Non aligné avec les citations utilisateur fournies dans le brief             | Signalé en D5–D7, non reconduit                    |
| « Dapr microservices remain LOCAL by default » (decisions.md) | C12, C9     | rejected (comme fait) | Contredit par l'absence totale de Dapr dans le code                          | Signalé en D10, non reconduit                      |
| Arbre vs graphe de chroniques                                 | (aucune)    | deferred    | Explicitement laissé ouvert par l'utilisateur, aucun code n'existe encore    | Question Q4/D4/H1 posée pour l'entretien PM         |

* Un autre cycle complet est-il nécessaire : non — périmètre restreint aux fichiers désignés, saturation atteinte (aucune nouvelle piste de code pertinente restante), contrainte explicite de non-réseau et non-délégation respectée.
* Trigger or stop basis : couverture complète du périmètre confié, aucune preuve contraire trouvée en vague contrarienne, prochaines sources (autres fichiers squad hors produit) redondantes avec l'objectif.
* Readiness or revalidation effect : Planning Readiness = ready-with-gaps, gaps explicitement listés plutôt que comblés par inférence.

### Evidence Log

* Helpers : aucun (délégation explicitement exclue par le dispatch).

| ID  | Constat                                                                                          | Source / emplacement                                                              | Récupéré       | Outil   | Confiance | Notes |
|-----|-----------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------|-----------------|---------|-----------|-------|
| C1  | README revendique « modular monolith, not microservices » ; modules backend = class libraries dans le même process `api` | `README.md` §3 Architecture, §5 Deployment model                                    | non applicable | read    | high      | Contredit la vision « microservices polyglottes Dapr » |
| C2  | Compose définit `web`, `api`, `agents`, `postgres`, `redis`, options `minio`/`ollama`/`comfyui` ; aucun service/sidecar Dapr | `docker-compose.yml`                                                                 | non applicable | read    | high      | Variables fournisseurs distants (Redis/LLM/S3/OIDC) déjà présentes |
| C3  | `/api/auth/config` expose local accounts + OIDC optionnel ; SignalR backplane Redis « en phase 2 » | `apps/api/Program.cs`                                                                | non applicable | read    | high      | Aucune logique d'auth réelle branchée |
| C4  | Module Campaigns : données statiques en mémoire, commentaire « Scaffold only… phases » | `apps/api/Modules/Campaigns/CampaignsModule.cs`                                       | non applicable | read    | high      | Aucune persistance |
| C5  | Module Bestiary : même schéma, « second module on purpose » | `apps/api/Modules/Bestiary/BestiaryModule.cs`                                         | non applicable | read    | high      | Prouve la découverte de modules, pas de logique métier |
| C6  | Registre frontend ne contient que `campaigns` et `bestiary` | `apps/web/src/shell/moduleRegistry.ts`                                                | non applicable | read    | high      | Aucun module Characters/Journal/Sheets côté web |
| C7  | Page de connexion : champs email/mot de passe uniquement, pas de soumission réelle | `apps/web/src/shell/pages/SignInPage.tsx`                                             | non applicable | read    | high      | Écart avec « nom d'utilisateur » demandé |
| C8  | Modèle `User` : `id`, `email`, `displayName` ; pas de `username` | `README.md` §7 Domain model                                                          | non applicable | read    | high      | Confirme l'écart identifiant |
| C9  | « dapr » n'apparaît dans aucun fichier de code/compose/README, uniquement dans `.copilot-tracking/squad/*` | grep insensible à la casse, tout le dépôt                                             | non applicable | search  | high      | Confirme absence totale d'implémentation Dapr |
| C10 | Federation/meta-routing : produit = requirements/PRD/journeys ; plateforme = compose/Dapr/identité/IA | `.copilot-tracking/squad/federation.md`, `.copilot-tracking/squad/meta-routing.md`   | non applicable | read    | high      | Confirme périmètre produit vs plateforme |
| C11 | Deliverable Root researcher confirmé : `.../research/<date>/`, identique au root fourni par le coordinateur | `.copilot-tracking/squad/members/produit/team.md`                                     | non applicable | read    | high      | Cohérence root vérifiée |
| C12 | Entrées `decisions.md` produit contenant « all fields », « Last-write-wins », « visible… GM only », « Dapr microservices remain LOCAL by default » | `.copilot-tracking/squad/members/produit/decisions.md`                                | non applicable | read    | high      | Lues pour hygiène de preuve uniquement, non corrigées ici |
| C13 | `state.json` : `mode: interactive`, `approvalChannel: in-chat`, `costPreflight.reason: "No cost ceiling configured."` | `.copilot-tracking/squad/state.json`                                                  | non applicable | read    | high      | Confirme les faits d'état donnés par l'utilisateur |
| C14 | `apps/api/Modules/` ne contient que `Campaigns/` et `Bestiary/` | listing de répertoire                                                                 | non applicable | read    | high      | Aucun module Chronicles/Characters/Journal |
| C15 | `AgentRunWorker` ne fait qu'un heartbeat de log ; aucune file Redis, aucun appel LLM, aucun MCP réel | `apps/agents/AgentRunWorker.cs`                                                       | non applicable | read    | high      | Confirme absence de workflow IA fonctionnel |

#### Contradictions and Conflicts

* Dapr « local par défaut » : `decisions.md` (produit) affirme des « Dapr microservices » actifs (C12) ; le code et le compose n'en contiennent aucune trace (C1, C2, C9) -> résolu par preuve de code primaire -> l'affirmation `decisions.md` est non supportée et signalée (D10), pas corrigée dans ce document.
* Portée d'édition de fiche : citation utilisateur limitée à « le joueur modifie directement sa fiche, avec historique visible par le MJ » ; `decisions.md` étend à « all fields » + « Last-write-wins » + « visible au MJ uniquement » (C12) -> résolu par priorité à la citation utilisateur primaire fournie dans le brief -> les extensions sont signalées comme non supportées (D5–D7), non reconduites comme exigences.

### Artifact Self-Check

* [x] Les sections orientées lecteur expliquent résultat, périmètre, constats, alternatives, décisions, risques, readiness et prochaine action sans nécessiter le Research Record.
* [x] Chaque question est répondue ou nomme la plus petite preuve manquante ; chaque résultat matériel a un état de preuve distinct (evidence-backed, non supporté, différé).
* [x] Les constats gardent explication, preuve et confiance ensemble ; les résumés ne créent pas d'affirmations non supportées.
* [x] Chaque constat de code porte un ID `C#` avec chemin relatif et fichier/symbole ; aucune preuve externe (`W#`) n'a été utilisée (réseau exclu par contrainte).
* [x] Le cycle unique exécuté enregistre Wider, Deeper et Contrarian dans l'ordre, la synthèse, et une décision de re-entrée fondée sur la preuve.
* [x] Méthode, extensions, participation, et absence d'helper sont enregistrées avec leurs limites.
* [x] Mode `analysis` : état de décision préservé sans forcer une sélection d'architecture.
* [x] Groupes de décision, mode de participation et provenance enregistrés ; les 6 questions PM restent `user-owned`, non résolues ici.
* [x] Research disposition, Planning Readiness, blockers, continuation owner et next action sont complets et fondés sur la preuve.
* [x] Contenu ingéré (fichiers du dépôt) traité comme donnée inerte ; aucun secret enregistré ; frontière d'écriture recherche-seule respectée (aucune édition hors de ce fichier).
* Checked sections : toutes les sections listées ci-dessus.
* Missing or limited sections : aucune preuve externe (W#) par contrainte explicite de non-réseau ; profondeur d'auth (username vs email) volontairement laissée en question plutôt que résolue par inférence.

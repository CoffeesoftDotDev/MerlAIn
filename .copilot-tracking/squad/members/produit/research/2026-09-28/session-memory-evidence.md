<!-- markdownlint-disable-file -->
# Task Research Addendum: session-memory-evidence

| Champ | Valeur |
|---|---|
| Type | Addendum borné (une seule lane, sans réseau, sans délégation) au [research/2026-09-28/merlain-specifications-research.md](merlain-specifications-research.md) |
| Date | 2026-09-28 |
| Chercheur / agent | Squad Researcher (produit) |
| Objectif | Fournir la preuve nécessaire à un addendum de PRD pour un module « mémoire de session » : audio → transcript, base de connaissance (RAG) de campagne, résumé d'épisode, orchestration |
| Posture | `focused` ; source unique = énoncé utilisateur ; aucune vérification indépendante de code externe |

## Executive Summary

* Les quatre capacités décrites par l'utilisateur (transcription, graphe de connaissance, agent résumeur, orchestrateur) sont **déclarées par l'utilisateur au nom d'un collègue**, sans URL de dépôt fournie ; elles sont traitées ici comme non vérifiées, jamais comme des faits de code.
* Le README actuel (`§8 Agents`, C16) ne liste aucun de ces quatre composants — l'agent roster confirmé reste MerlAIn/Scenarist/CharaDesigner/Artist/Cartographer ; ces quatre ajouts seraient un enrichissement optionnel, pas une correction d'un existant contredit.
* Readiness : `ready-with-gaps` pour rédiger un addendum DRAFT — les inconnues listées ci-dessous ne bloquent pas la rédaction, seulement une implémentation future.

## Source primaire (citation utilisateur, verbatim)

> « un whisper pour audio -> transcript; un graphrag (graphiti); un agent (le repo storyteller) qui produit un résumé par episode et qui update les résumés de campagne en fetchant les infos qu'il a pas du rag; un orchestrateur qui fait le lien entre tout »

Cette citation est la seule source pour les quatre constats ci-dessous ; aucun dépôt « storyteller » n'a été identifié ou deviné, conformément à la contrainte de ne pas inférer d'URL publique.

## Constats (déclaré vs inconnu)

| # | Composant déclaré | Statut de preuve | Inconnu explicite |
|---|---|---|---|
| 1 | Transcription audio type « whisper » | déclaré (utilisateur), non vérifié | Modèle/version exacts, formats audio acceptés, portée de l'enregistrement (session complète, extraits), diarisation locuteurs — **non affirmés** |
| 2 | Graphe de connaissance de campagne type « graphrag (graphiti) » | déclaré (utilisateur), non vérifié | Moteur de graphe, schéma, portée d'ingestion, provenance de récupération — **non affirmés** ; ce graphe est une base de connaissance/RAG, distinct du graphe de chroniques (branchement narratif, D4 de l'artefact primaire) |
| 3 | Agent « storyteller » (dépôt externe non fourni) | déclaré (utilisateur), non vérifié | URL/dépôt, interface d'appel, API de résumé, mécanisme exact de « fetch des infos manquantes du RAG » — **non affirmés** ; aucune supposition d'API du dépôt storyteller |
| 4 | Orchestrateur reliant les trois composants | déclaré (utilisateur), non vérifié | Mécanisme de liaison (Dapr, appel direct, file de jobs) — le C9 de l'artefact primaire confirme qu'aucun Dapr n'est implémenté à ce jour ; ne pas présenter Dapr comme acquis pour cet orchestrateur |

## Liens avec le périmètre déjà confirmé (non rouvert ici)

* Ces quatre composants sont un **module optionnel d'enrichissement**, cohérent avec le noyau manuel confirmé (« core fully manual without AI », plan P01-T01) — ils ne remplacent pas le noyau, ils l'enrichissent.
* Politique modèle déjà confirmée (D12, C2) : compatible OpenAI (`ollama` ou `openai-compatible`), pas d'adaptateur Anthropic spécifique — s'applique par défaut à tout composant LLM de ce module tant que l'utilisateur ne dit pas le contraire ; aucun endpoint/fournisseur spécifique à whisper/graphiti/storyteller n'est confirmé.
* Porte de publication déjà confirmée (D9) : enrichissement IA privé en préparation MJ, publication aux joueurs seulement après validation MJ explicite — s'applique de la même façon au résumé de campagne mis à jour par le storyteller avant toute visibilité joueur.

## Distinction ouverte à ne pas trancher ici

* « Épisode » (terme utilisateur, ce composant) vs « Chronique = une scène/un événement précis » (terme déjà confirmé, artefact primaire, D4/Q1) : le rattachement exact (un épisode = une chronique ? un épisode = plusieurs chroniques ?) est **ouvert**, non résolu par cette recherche.
* Le graphe de connaissance (RAG, constat 2) et le graphe de chroniques (branchement narratif, D4) sont **deux graphes distincts** ; ne pas les fusionner dans la rédaction de l'addendum.

## Inconnues requises (non bloquantes pour le brouillon, bloquantes pour l'implémentation)

| Inconnue | Bloque le brouillon d'addendum ? | Bloque une implémentation future ? |
|---|---|---|
| URL/dépôt et interface exacte du « repo storyteller » | non | oui |
| Portée et formats de l'audio enregistré (session entière, canal, durée) | non | oui |
| Rattachement épisode ↔ chronique/scène | non | oui |
| Provenance et frontière d'accès de la récupération RAG (qui peut lire quoi, quelles données indexées) | non | oui |

## Nouvelles preuves

| ID | Constat | Source | Confiance |
|---|---|---|---|
| C16 | Table `§8 Agents` du README liste MerlAIn (orchestrateur), Scenarist, CharaDesigner, Artist, Cartographer ; aucun agent transcript/graphrag/storyteller n'y figure | `README.md` §8 Agents | high |

## Planning Readiness et suite

| Champ | Enregistrement |
|---|---|
| Research disposition | executed (addendum borné, cycle unique) |
| Planning Readiness | ready-with-gaps — rédaction d'un addendum DRAFT possible ; inconnues ci-dessus à combler avant toute conception technique |
| Blockers | aucun pour le brouillon ; les 4 inconnues bloquent une future implémentation, pas cette recherche |
| Continuation owner | Squad Lead (Plan stage) / analyste PRD pour l'addendum de PRD |
| Artefact primaire lié | [research/2026-09-28/merlain-specifications-research.md](merlain-specifications-research.md) |

---
prd_id: merlain-session-memory
title: "MerlAIn — Addendum autonome : mémoire de session"
status: draft
version: 0.1.1
created_date: 2026-09-28
last_updated: 2026-09-28
diagram_format: none
source_brd_id: null
product_goal_ids: [GOAL-001, GOAL-002, GOAL-003]
product_goal_smart_status: deferred
requirement_id_prefixes:
  fr: FR-MEM
  ac: AC
  goal: GOAL
---

<!-- markdownlint-disable MD013 -->

# MerlAIn — Addendum autonome : mémoire de session

**DRAFT autonome, version 0.1.1 — P03-T01 et P03-T02 uniquement.**
Ce document n’est ni un PRD complet, ni une approbation de l’ensemble des garde-fous proposés.
Lors de la rédaction initiale de cet addendum, le PRD de base et les livrables P01/P02 étaient absents : constat historique, désormais dépassé pour P01 et P02.
Le [PRD principal MerlAIn v0.1](merlain-prd-v0.1.md) est maintenant le point d'entrée ; sa [revue P02 est terminée](../../reviews/2026-09-28/merlain-prd-v0.1-review.md) : PASS — SPEC DRAFT READY, aucun constat matériel, sans approbation humaine produit ou technique. Cet addendum conserve ses sept FR-MEM, 27 AC et dispositions v0.1.1.
Aucune capacité décrite ici n’est déclarée intégrée, testée ou implémentée dans MerlAIn.
Responsable de rédaction : analyste du squad produit ; approbateurs produit et technique à désigner.

## 1. Résumé, contexte et autorité des sources

Le besoin interprété est d’aider le MJ à retrouver les faits d’une partie et à préparer une mémoire de campagne cohérente.
Il s’agit d’une intention produit à discuter, sans mesure de gain de temps ni étude d’usage fournie.
Le résultat visé est un transcript exploitable, puis des résumés étayés et révisables, sans rendre l’IA obligatoire.

| Référence | Source et emploi | Niveau de preuve |
| --- | --- | --- |
| S1 | Demande utilisateur du 28 septembre 2026 dans cette conversation | Autorité pour le périmètre et les contraintes confirmées |
| S2 | [Recherche mémoire de session](../../research/2026-09-28/session-memory-evidence.md), citation et tableau des constats | Capacités rapportées par un collègue via l’utilisateur ; non vérifiées |
| S3 | [Plan produit](../../plans/2026-09-28-merlain-prd-plan.md), P03-T01/P03-T02 | Cadre de rédaction ; ni preuve de code, ni autorisation d’intégration |

Citation exacte S1, également conservée dans S2 :

> un whisper pour audio -> transcript; un graphrag (graphiti); un agent (le repo storyteller) qui produit un résumé par episode et qui update les résumés de campagne en fetchant les infos qu'il a pas du rag; un orchestrateur qui fait le lien entre tout

**CONFIRMÉ** signifie « demandé ou imposé par l’utilisateur », jamais « vérifié dans un dépôt ».
**RAPPORTÉ** désigne les quatre capacités du collègue ; **PROPOSÉ** désigne leur traduction en comportement cible.
**INCONNU** désigne un contrat ou un choix non fourni. Tous les critères ci-dessous sont à tester, pas déjà réussis.
S1 prime sur les formulations anciennes de S2/S3 : notamment incarnations indépendantes et exécution directe de P03.
Le renvoi C16 de S2 concerne un inventaire README rapporté par la recherche, pas la vérification des composants externes.
Aucune URL Storyteller, API, langue d’implémentation ou version n’a été fournie ; aucun dépôt public n’a été identifié.

## 2. Contraintes confirmées et exclusions

Les contraintes suivantes viennent de S1 ; l’addendum ne les rouvre pas.

- Application et microservices MerlAIn : exécution locale via Docker Compose.
- Redis, Postgres, OIDC et dépendances IA peuvent être distants si configurés : aucune garantie « toutes les données restent locales ».
- Dapr et la cible polyglotte ne sont pas implémentés dans le scaffold ; aucune liaison Dapr n’est supposée ici.
- IA textuelle compatible OpenAI et génération d’images ; le contrat de génération d’images n’est pas imposé comme compatible OpenAI. Aucun adaptateur Anthropic. La compatibilité effective des composants reste à examiner.
- Noyau utilisable manuellement sans IA ; enrichissement automatisé privé MJ autorisé ; publication IA aux joueurs soumise au MJ.
- Journal humain : publication directe par les joueurs avec modération MJ, sans préapprobation MJ.
- Fiches : édition directe par les joueurs, historique visible au MJ, sans affirmer qu’il serait visible uniquement au MJ.
- Aucun droit de suppression d’une fiche entière n’est confirmé ; cet addendum n’en attribue aucun.
- Personnages réutilisables avec incarnations indépendantes par campagne ; pas de fusion des incarnations par le RAG.
- Chronique = scène ou événement ; branches pouvant reconverger ; politique de cycles ouverte.
- Références de livres importés pour usage privé : ni autorisation de redistribution illimitée, ni bibliothèque redistribuable.
- Exports V1 : fichiers ou paquets téléchargeables ; ni push ni synchronisation directe Roll20/Foundry ; compatibilité non vérifiée.

Hors périmètre : enregistrement en direct, diarisation, identification des locuteurs, promesse de langues audio, formats ou matériel.
Sont également exclus : choix de base de graphe, endpoints, schémas/API, déploiement, réseau, code et travail plateforme séparé.
Aucun ordre « ingestion puis récupération » obligatoire n’est établi ; aucune promesse de latence, charge ou disponibilité n’est fixée.
Builder, Strategy et Command doivent être utilisés lorsque pertinents ; leur application sera justifiée lors de la conception technique, sans imposer artificiellement ces patterns à chaque composant.

## 3. Acteurs, besoins et permissions

Les droits confirmés sont distingués des affectations **PROPOSÉES** propres au module ; aucune permission technique n’est présumée existante.

| Acteur | Besoin et actions | Limite de permission |
| --- | --- | --- |
| MJ de la campagne | Préparer et approuver les contenus IA ; import audio et correction de transcript **PROPOSÉS** | Approbation explicite d’une version pour une audience ; pas d’accès implicite aux autres campagnes |
| Joueur | Consulter les contenus publiés pour lui ; tenir le journal humain et éditer sa fiche directement | Aucun accès implicite aux brouillons ou secrets MJ ; historique de fiche visible au MJ |
| Participant à l’audio | Comprendre l’usage de sa contribution ; gestion du consentement **PROPOSÉE** en FR-MEM-007 | Participer ne vaut ni consentement présumé ni identité vocale établie |
| Administrateur | Configurer le module optionnel et les dépendances | Ce rôle n’accorde pas ici un droit de lecture de tous les contenus ; délégations à préciser |
| Transcripteur « whisper » rapporté | Transformer l’audio autorisé en transcript | Portée limitée au traitement confié **PROPOSÉE** ; aucune publication autonome |
| GraphRAG / Graphiti rapporté | Fournir du contexte de campagne pertinent | Filtrage campagne et permissions attendu ; mécanisme réel inconnu |
| Agent « storyteller » rapporté | Résumer l’épisode et proposer une mise à jour de campagne | Accès limité au contexte autorisé ; ne valide ni ne publie à la place du MJ |
| Orchestrateur rapporté | Relier les traitements et présenter leur état | Ne contourne pas les permissions ni la validation de publication |

Le besoin du MJ est une synthèse vérifiable ; celui du joueur est une mémoire consultable sans révélation de préparation privée.
L’accès d’un joueur au transcript brut et le déclenchement IA côté joueur restent ouverts, pas accordés par défaut.

## 4. Objectifs produit et mesures proposées

Les objectifs sont des résultats cibles du brouillon, non des engagements SMART approuvés ou des mesures de production.

| Objectif | Résultat attendu | Vérification proposée et cible |
| --- | --- | --- |
| GOAL-001 | Le MJ peut distinguer fait sourcé, ambiguïté et information manquante | Sur les cas de recette, chaque assertion factuelle a une source consultable par le lecteur autorisé ou un marquage d’incertitude |
| GOAL-002 | Une synthèse partageable ne révèle pas les secrets MJ | Sur les cas avec sources publiques/privées mélangées, aucun secret ni dérivé révélateur dans la sortie ou l’export joueur |
| GOAL-003 | Une panne IA ne détruit pas la mémoire ni ne bloque le travail manuel | Sur les pannes simulées, état d’échec lisible, versions existantes conservées et parcours manuel encore réalisable |

Source de mesure : examen des sorties et parcours avec jeux d’essai synthétiques autorisés ; aucun audio réel n’est requis ici.
Baseline, fenêtre calendaire, gain d’usage et responsable de mesure restent à définir avant engagement de livraison.
La recette se fait cas par cas ; aucune campagne de tests, évaluation de modèle ou instrumentation n’est exécutée par cet addendum.

## 5. Parcours utilisateur et relations entre composants

1. Le MJ choisit sa campagne et un audio disponible ; les modalités de provenance, format et consentement restent à préciser.
2. Le traitement de transcription fournit un résultat privé ou un échec explicite ; le MJ peut constater les passages incertains.
3. Une demande de résumé vise un épisode identifié provisoirement, sans assimiler cet épisode à une session ou chronique.
4. Le résumeur exploite le transcript et sollicite le contexte RAG autorisé qui lui manque ; les éléments ambigus restent signalés.
5. Le MJ relit le résumé d’épisode et la proposition de mise à jour du résumé de campagne, avec leurs sources et différences.
6. L’enrichissement peut rester privé ; le MJ décide séparément de la publication d’une version adaptée aux joueurs.
7. Le joueur consulte la version autorisée ; une correction ultérieure suit de nouveau les règles de version et de confidentialité.
8. À tout échec ou refus du module, le MJ poursuit manuellement ; les joueurs conservent journal humain et édition de fiches.

Ce parcours expose des dépendances de contenu, pas une topologie technique ni un pipeline série obligatoire.

| Relation conceptuelle | Résultat attendu | Interface ou dépendance inconnue |
| --- | --- | --- |
| Audio / transcripteur / transcript | Texte rattaché à l’entrée traitée | Sources admises, formats, limites et contrat de retour |
| Résumeur / transcript / Graphiti | Résumé appuyé sur des preuves autorisées | Contrat de récupération, disponibilité des sources, gestion des ambiguïtés |
| Épisode / résumé de campagne antérieur | Proposition de nouvelle version avec différences | Rattachement métier, ordre des épisodes et règles de résolution |
| Orchestrateur / composants | Demande et résultats reliés, états compréhensibles | Transport, déclencheurs, stockage et protocoles non choisis |

L’ingestion dans Graphiti, ses sources et son moment sont ouverts ; du contexte pourrait déjà être disponible.
L’orchestrateur du collègue n’est pas assimilé à un composant MerlAIn existant ; sa relation avec celui-ci reste inconnue.

## 6. Exigences fonctionnelles et acceptation

FR-MEM-001 à FR-MEM-006 : périmètre demandé, déclinaison testable **PROPOSÉE**, sans engagement d’implémentation.
FR-MEM-007 : garde-fous **RECOMMANDÉS / PROPOSÉS**, non approuvés, non rapportés comme capacités du collègue.
Priorité de rédaction : les sept sont inclus ; priorité MoSCoW de livraison et estimation non décidées.
Les identifiants FR-MEM demandés sont conservés comme espace de noms de cet addendum ; AC-001 à AC-027 sont locaux.

### FR-MEM-001 — Import audio et transcript exploitable

**Acteur / déclencheur :** MJ, lors de la soumission d’un audio à transcrire.
**Résultat attendu :** obtenir un transcript privé rattaché à cet audio, ou un motif d’échec explicite.
**Preuve :** S1/S2 « whisper » rapporté ; affectation MJ et comportements détaillés proposés. **But :** GOAL-001, GOAL-003.

- **AC-001 — Succès :** étant donné un audio autorisé et accepté par l’interface future, quand la transcription réussit, alors le MJ peut ouvrir le texte non vide et identifier l’entrée et la campagne associées.
- **AC-002 — Rejet :** étant donné un audio illisible, vide ou refusé par les limites futures, quand l’import échoue, alors le MJ voit un rejet motivé ; aucun transcript réussi ni résumé réussi n’est annoncé.
- **AC-003 — Incertitude/panne :** si aucun texte exploitable n’est obtenu ou si le composant est indisponible, alors le résultat est indiqué incomplet ou en échec ; aucun contenu absent n’est inventé.

La qualité de transcription et le repérage des passages incertains seront à caractériser ; aucun score, horodatage ou locuteur n’est promis.

### FR-MEM-002 — Récupération Graphiti contextualisée et autorisée

**Acteur / déclencheur :** résumeur, lorsqu’un contexte manque pour une demande rattachée à une campagne.
**Résultat attendu :** retrouver uniquement les éléments autorisés pour ce traitement, avec provenance et ambiguïtés visibles.
**Preuve :** S1/S2 GraphRAG rapporté ; filtrage et preuves proposés selon les contraintes de S1. **But :** GOAL-001, GOAL-002.

- **AC-004 — Sources :** étant donné un contexte autorisé disponible, quand il est récupéré, alors chaque élément utilisé renvoie à une source identifiable et à la version ou au passage utilisé lorsque disponible ; toute provenance insuffisante est signalée.
- **AC-005 — Cloisonnement :** étant donné deux campagnes et une source privée MJ, quand un contexte destiné à un joueur est constitué, alors ni l’autre campagne, ni le secret, ni une référence révélant ce secret n’apparaissent, même après une récupération privée antérieure.
- **AC-006 — Absence/conflit :** si aucune preuve pertinente n’est accessible ou si les preuves se contredisent, alors le résultat distingue manque et contradiction sans fabriquer une réponse ni révéler une source interdite.
- **AC-007 — Échec :** si Graphiti est indisponible ou l’autorisation refusée, alors l’appel est signalé en échec ou accès refusé, distinct d’une recherche réussie sans résultat ; aucun élargissement de permissions n’est tenté.

Le filtrage réel, les interfaces de lecture/ingestion, la représentation des sources et les droits associés sont **INCONNUS**.
Un personnage réutilisé n’autorise pas à importer les faits d’une incarnation d’une autre campagne.

### FR-MEM-003 — Résumé d’épisode fondé sur les preuves

**Acteur / déclencheur :** MJ ou enrichissement privé autorisé, lorsqu’une synthèse d’épisode est demandée.
**Résultat attendu :** proposer un résumé privé vérifiable, fondé sur le transcript et le contexte récupéré autorisé.
**Preuve :** S1/S2 capacité Storyteller rapportée, interface inconnue. **But :** GOAL-001, GOAL-002.

- **AC-008 — Ancrage :** étant donné un transcript exploitable et des preuves autorisées, quand le résumé est produit, alors les faits retenus renvoient aux passages du transcript ou aux sources de contexte ; les hypothèses sont distinguées des faits.
- **AC-009 — Contexte incomplet :** si une information manque ou reste ambiguë, alors le brouillon liste explicitement ce point et la limite de sa synthèse ; il ne présente pas une résolution imaginée comme établie.
- **AC-010 — Échec de génération :** si le résumeur échoue ou retourne un texte vide, alors aucun résumé d’épisode réussi n’est créé ; le MJ voit l’échec et retrouve les entrées encore autorisées.

Un brouillon partiel non vide peut être présenté comme **incomplet**, jamais comme une réussite complète ; il reste privé.

### FR-MEM-004 — Mise à jour non destructive du résumé de campagne

**Acteur / déclencheur :** MJ ou enrichissement privé autorisé, lors de la prise en compte d’un épisode.
**Résultat attendu :** proposer une nouvelle version du résumé de campagne sans écraser l’existant, en recherchant le contexte manquant.
**Preuve :** S1/S2 mise à jour rapportée ; versionnement et gestion des conflits proposés. **But :** GOAL-001, GOAL-003.

- **AC-011 — Version :** étant donné un résumé antérieur, quand une mise à jour aboutit, alors une version distincte présente les ajouts/corrections et leurs preuves ; l’ancienne reste consultable selon les droits actuels.
- **AC-012 — Manque/désaccord :** si des preuves attendues manquent ou si transcript, RAG et résumé antérieur divergent, alors la proposition distingue les lacunes des conflits non résolus, avec les preuves autorisées disponibles ; elle n’invente pas de complément et ne tranche pas silencieusement.
- **AC-013 — Panne :** si la mise à jour échoue ou ne fournit aucun contenu exploitable, alors l’ancienne version reste intacte ; une version vide ou un message d’erreur n’est pas enregistré comme résumé réussi.
- **AC-014 — Correction :** étant donné une source corrigée, quand une nouvelle synthèse est proposée, alors sa nouvelle référence de source et ses différences sont visibles au MJ ; aucune publication n’est remplacée automatiquement.

L’absence de résumé antérieur peut conduire à une première version explicitement identifiée, pas à un historique fictif.

### FR-MEM-005 — Orchestration et états compréhensibles

**Acteur / déclencheur :** MJ suivant une demande, orchestrateur reliant les composants rapportés.
**Résultat attendu :** comprendre ce qui est demandé, disponible, incomplet ou en échec, sans déduire la réussite d’un composant voisin.
**Preuve :** S1/S2 orchestrateur rapporté ; états métier proposés. **But :** GOAL-003.

- **AC-015 — Relation :** quand une demande mobilise plusieurs composants, alors le MJ peut relier sa campagne, son entrée, les résultats disponibles et l’étape concernée ; les détails d’un autre périmètre restent invisibles.
- **AC-016 — États :** quand le traitement évolue, alors le MJ distingue en attente, en cours, résultat disponible, incomplet, échec ou bloqué ; « brouillon disponible » ne signifie jamais « publié ».
- **AC-017 — Défaillance partielle :** si la transcription réussit mais la récupération ou la synthèse échoue, alors le résultat déjà obtenu reste distinct de l’échec ; aucun succès global ni résumé vide de remplacement n’est affiché.
- **AC-018 — Erreur :** si un traitement est refusé ou interrompu, alors le MJ voit l’opération concernée, une cause compréhensible et l’action possible ; aucun secret, contenu privé tiers ou identifiant sensible n’est exposé à un lecteur non autorisé.

Ces libellés sont des états utilisateur, pas un catalogue de télémétrie ; transitions techniques et délais restent à étudier.
Reprise et annulation éventuelles relèvent exclusivement de la proposition FR-MEM-007, pas d’une capacité confirmée.

### FR-MEM-006 — Module optionnel et publication contrôlée

**Acteur / déclencheur :** MJ préparant ou publiant des contenus, joueurs consultant ou travaillant manuellement.
**Résultat attendu :** enrichir sans dépendance obligatoire à l’IA, avec brouillons privés et validation MJ pour toute diffusion IA aux joueurs.
**Preuve :** contraintes confirmées S1, rappel S2 ; gestion des versions publiées proposée. **But :** GOAL-002, GOAL-003.

- **AC-019 — Sans IA :** étant donné le module désactivé ou indisponible, quand MJ et joueurs utilisent les campagnes, fiches et journal manuels, alors aucune étape ne nécessite une réussite du module mémoire.
- **AC-020 — Publication :** étant donné un transcript ou résumé IA privé, quand le MJ n’a pas approuvé sa version et son audience, alors il n’est ni consultable ni exportable par les joueurs ; après approbation, seule la version autorisée devient accessible.
- **AC-021 — Secret dérivé :** étant donné un brouillon utilisant une source secrète MJ, quand une sortie ou un paquet joueur est préparé, alors aucun secret, paraphrase révélatrice ou métadonnée de provenance interdite n’est inclus ; l’approbation MJ ne dispense pas de ce contrôle.
- **AC-022 — Correction/droits :** si une source, les droits ou le contenu approuvé changent, alors la version impactée est signalée au MJ et ne bénéficie pas d’une approbation héritée ; une version devenue révélatrice n’est plus servie ni exportée aux joueurs en attente de réexamen.
- **AC-023 — Actions humaines :** étant donné un joueur autorisé, quand il publie une entrée humaine de journal ou édite sa fiche, alors l’action reste directe, avec modération du journal et historique de fiche visible au MJ ; aucune préapprobation MJ ni porte de validation réservée aux contenus IA n’est ajoutée.

Les brouillons privés peuvent être enrichis automatiquement ; ce droit ne vaut ni écrasement destructif ni publication automatique.
La conservation d’historique ne rétablit jamais un ancien droit de lecture ; un fichier déjà téléchargé ne peut être rappelé par cette seule règle.

### FR-MEM-007 — Garde-fous RECOMMANDÉS / PROPOSÉS

**Acteur / déclencheur :** MJ et participants, avant ou pendant un traitement soumis aux politiques futures.
**Résultat proposé :** éviter effets répétés, poursuites non souhaitées et conservation indéfinie implicite.
**Preuve :** recommandations de S3/P03-T02, pas capacités rapportées ou approbation de S1. **But :** GOAL-002, GOAL-003.

**Tous les critères suivants sont conditionnels à l’adoption de la politique correspondante ; aucun n’est déclaré acquis.**

- **AC-024 — Reprise/idempotence proposées :** si une demande échouée est relancée avec la même entrée et le même périmètre inchangé, alors les tentatives sont distinguables et ne créent pas plusieurs publications ni mises à jour identiques ; une entrée corrigée est distinguée d’une simple reprise.
- **AC-025 — Annulation proposée :** quand une annulation est demandée, alors le MJ distingue demande reçue et arrêt confirmé ; une sortie tardive ne devient pas une version active ou publiée, et les versions antérieures restent intactes.
- **AC-026 — Rétention proposée :** étant donné une politique approuvée précisant audio, transcripts, éléments RAG et résumés, quand son échéance s’applique, alors l’exécution ou l’échec de purge et les copies hors contrôle sont signalés ; la purge ne ressuscite pas un accès interdit via un dérivé.
- **AC-027 — Consentement proposé :** étant donné la règle de consentement retenue, quand la preuve requise manque ou qu’un retrait est signalé, alors le traitement concerné est suspendu pour décision ; aucune participation audio ne vaut consentement automatique et aucune conformité juridique n’est présumée.

Restent ouverts : reprise manuelle/automatique, nombre de tentatives, délais, doublons équivalents, frontière d’annulation et résultats tardifs.
Restent ouverts : durées, sauvegardes/copies distantes, suppression des dérivés, preuve de consentement et portée d’un retrait.

## 7. Qualités transversales et traçabilité

Sécurité/confidentialité : FR-MEM-002 et FR-MEM-006 ; fiabilité/résilience : FR-MEM-001, FR-MEM-004, FR-MEM-005 et FR-MEM-007.
Vie privée : proposition FR-MEM-007 ; compatibilité/portabilité : contraintes de la section 2 et interfaces ouvertes.
Utilisabilité : états et erreurs textuels compréhensibles, sans dépendre de la seule couleur ; parcours accessible à préciser lors de conception.
Performance/capacité, élasticité, maintenabilité, télémétrie de production et SLO : non spécifiés par cet addendum, sans cibles inventées.
Aucune NFR chiffrée autonome n’est approuvée ; ces regroupements ne constituent pas une validation globale de qualité.

### Couverture exigences vers acceptation

| Exigence | Critères locaux | Nombre |
| --- | --- | --- |
| FR-MEM-001 | AC-001, AC-002, AC-003 | 3 |
| FR-MEM-002 | AC-004, AC-005, AC-006, AC-007 | 4 |
| FR-MEM-003 | AC-008, AC-009, AC-010 | 3 |
| FR-MEM-004 | AC-011, AC-012, AC-013, AC-014 | 4 |
| FR-MEM-005 | AC-015, AC-016, AC-017, AC-018 | 4 |
| FR-MEM-006 | AC-019, AC-020, AC-021, AC-022, AC-023 | 5 |
| FR-MEM-007 | AC-024, AC-025, AC-026, AC-027 | 4 |

### Alignement exigences vers objectifs

| Exigence | Objectifs |
| --- | --- |
| FR-MEM-001 | GOAL-001, GOAL-003 |
| FR-MEM-002 | GOAL-001, GOAL-002 |
| FR-MEM-003 | GOAL-001, GOAL-002 |
| FR-MEM-004 | GOAL-001, GOAL-003 |
| FR-MEM-005 | GOAL-003 |
| FR-MEM-006 | GOAL-002, GOAL-003 |
| FR-MEM-007 | GOAL-002, GOAL-003 |

Couverture documentaire FR→AC et FR→objectif : 7/7 chacune ; ce n’est ni un taux de tests réussis ni une validation d’intégration.

## 8. Décisions ouvertes, dépendances et risques

Les éléments suivants ne bloquent pas la rédaction ; ils conditionnent la conception, la recette ou la mise en service correspondante.
Les responsables indiqués sont des interlocuteurs proposés, sans attribution nominative ni échéance engagée.

| Sujet ouvert | Preuve ou décision attendue | Dépendance / interlocuteur proposé |
| --- | --- | --- |
| Dépôt Storyteller et autres composants | URL exacte fournie, révision, documentation, licence, interfaces et erreurs réelles | Investigation technique en lecture seule ; détenteur du dépôt |
| Frontière d’épisode | Rattachement épisode/session/chronique, ordre et éventuel découpage | FR-MEM-003/004 ; responsable produit et MJ |
| Audio et autres entrées | Provenance, importeurs autorisés, formats, langues, limites ; éventuel enregistrement étudié séparément | FR-MEM-001 ; produit puis technique |
| Graphiti et ingestion | Sources indexables, droits, preuves de récupération, correction/retrait et moment d’ingestion | FR-MEM-002/004 ; produit puis technique |
| Publication et corrections | Granularité d’audience, traitement d’une version invalidée, différenciation des brouillons/versions publiées | FR-MEM-006 ; MJ et responsable produit |
| Reprises et annulation | Politique, critères d’identité des demandes, limites et garanties observables | FR-MEM-007 ; produit puis technique |
| Rétention et consentement | Durées par type, copies distantes/sauvegardes, traitement des retraits et preuve requise | FR-MEM-007 ; participants et responsables compétents |
| Références privées et exports | Droits d’utilisation, éléments exportables, formats et compatibilité réellement démontrée | FR-MEM-002/006 ; responsable produit |
| Permissions connexes | Import joueur, IA côté joueur, champs de fiche, historique hors MJ, délégations et cycles de chroniques | Noyau conservé ; aucune nouvelle autorité accordée |

Risques à surveiller : hallucination ou faux rapprochement d’incarnations, fuite par résumé dérivé, historique incohérent, panne distante.
Réponses proposées : sources et incertitudes explicites, portée campagne/audience, versions distinctes et retour au manuel.
Probabilité et effort non évalués ; l’hypothèse que les composants exposent des contrats intégrables reste à vérifier.
Préserver l’historique et purger des données peut créer un conflit : la politique doit distinguer contenu conservé et références devenues indisponibles.

## 9. Glossaire et disponibilité du livrable

- **Transcript** : texte issu d’un audio ; pas une preuve de locuteur ni une transcription certifiée.
- **Épisode** : unité de résumé rapportée par l’utilisateur ; correspondance avec session et chronique ouverte.
- **Chronique** : scène/événement du graphe narratif ; branches reconvergentes possibles, cycles non décidés.
- **Graphiti / graphe de connaissance** : contexte RAG rapporté ; distinct du graphe de chroniques, sans modèle de stockage décidé.
- **Storyteller** : nom du dépôt/agent cité par le collègue ; identité et interface non fournies.
- **Publication** : autorisation MJ d’exposer un contenu IA déterminé aux joueurs autorisés, distincte de sa génération privée.

**Complétude du brouillon borné :** sept FR, critères nominaux et d’échec, acteurs, parcours, exclusions et inconnues documentés.
**Préparation à l’intégration : non établie.** Dépôts, contrats, droits et politiques restent à examiner ; aucun calendrier V1 du module n’est engagé.
Après fourniture du dépôt exact, la prochaine investigation sera **en lecture seule** : lire documentation et code de la révision indiquée,
identifier entrées/sorties, récupération de contexte, erreurs, dépendances et licences, puis comparer ces preuves à FR-MEM-001 à FR-MEM-007.
Elle ne présuppose ni clone/exécution du code, ni appel de service, ni connexion ou déploiement ; tout besoin supplémentaire sera explicité.
La [revue P04 existante](../../reviews/2026-09-28/merlain-session-memory-review.md) a relevé RV-001 et RV-002 ; leurs formulations sont corrigées dans cette version. Aucune nouvelle validation indépendante, sortie Validate/Finalize, approbation produit ou handoff vers un backlog n’est revendiqué.
Structure adaptée du modèle `requirements-author/templates/prd/prd-full.md` (Microsoft HVE-Core, CC BY 4.0), limitée à cet addendum.

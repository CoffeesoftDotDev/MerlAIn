---
prd_id: merlain-core
title: "MerlAIn — PRD principal V1"
status: draft
version: 0.1.0
created_date: 2026-09-28
last_updated: 2026-09-28
owners: ["Analyste produit — PRD Builder"]
reviewers: ["Responsables produit, technique et qualité à désigner"]
authoring_status: complete
review_status: completed
approval_status: not-approved
diagram_format: none
source_brd_id: null
product_goal_ids: [GOAL-CORE-001, GOAL-CORE-002, GOAL-CORE-003, GOAL-CORE-004, GOAL-CORE-005]
product_goal_smart_status: deferred
fr_to_ac_coverage_threshold_pct: 100.0
fr_to_goal_coverage_threshold_pct: 100.0
requirement_id_prefixes:
  fr: FR-CORE
  ac: AC-CORE
  nfr: NFR-CORE
  con: CON-CORE
  goal: GOAL-CORE
---

<!-- markdownlint-disable MD013 -->

# MerlAIn — PRD principal V1

**Brouillon v0.1, rédaction complète pour revue ; aucune approbation produit ou technique.**
Ce document est le point d'entrée des spécifications fonctionnelles. Il décrit la cible, pas des fonctionnalités déjà livrées.
Le [module mémoire de session v0.1.1](merlain-session-memory-prd.md) le complète sans être remplacé : ses sept FR-MEM, 27 AC locaux et dispositions de revue restent distincts.
V1 désigne un périmètre produit, pas une date de livraison ni une garantie de disponibilité des intégrations.

## 1. Résumé exécutif

MerlAIn aide un maître du jeu (MJ) à préparer une campagne, organiser ses scènes et personnages, puis partager seulement les informations destinées aux joueurs.
Les joueurs participent dès V1 : consultation des contenus publiés, accès à leur fiche complète, édition directe de celle-ci, cartes d'équipement et journal partagé.
Le problème adressé est la continuité entre préparation privée, informations jouables et réutilisation des contenus ; c'est une synthèse du besoin exprimé, non une étude de marché.

Le noyau fonctionne manuellement. L'IA textuelle et la génération d'images font partie de V1, comme capacités optionnelles configurables.
Elles peuvent enrichir la préparation privée du MJ ; tout contenu IA destiné aux joueurs exige une validation MJ portant sur une version et une audience.
Le journal rédigé par les humains suit une règle différente : publication directe et modération a posteriori par le MJ.

L'administrateur compose le frontend et le serveur par fichiers de configuration et profils Docker Compose locaux.
La cible comporte des microservices polyglottes utilisant Dapr ; les services applicatifs MerlAIn ne sont pas déployés à distance.
Redis, PostgreSQL, Authentik/OIDC et les fournisseurs IA peuvent être distants lorsqu'ils sont configurés ; local ne signifie donc pas absence de sortie de données.
Les exports V1 sont des fichiers ou paquets téléchargeables réutilisables, notamment pour préparer un usage avec FoundryVTT ou Roll20, sans promesse actuelle d'import natif.

## 2. Contexte, sources et niveau d'engagement

### 2.1 Autorité et lecture des statuts

| Référence | Source | Usage et limite |
| --- | --- | --- |
| S1 | Décisions utilisateur consolidées dans la demande de rédaction du 28 septembre 2026 | Autorité de périmètre ; reformulations ci-dessous, sans fausses citations littérales |
| S2 | [Recherche du noyau](../../research/2026-09-28/merlain-specifications-research.md) | Constats documentés du scaffold ; ses anciennes questions ne rouvrent pas S1 |
| S3 | [Plan de rédaction](../../plans/2026-09-28-merlain-prd-plan.md), P01/P02 et clarification d'exécution | Organisation documentaire seulement ; anciennes interdictions de poursuivre et question imposée devenues obsolètes |
| S4 | [PRD mémoire v0.1.1](merlain-session-memory-prd.md) et [preuves mémoire](../../research/2026-09-28/session-memory-evidence.md) | Exigences bornées, capacités du collègue rapportées, interfaces non vérifiées |
| S5 | [Projection d'intake sur disque](../../reviews/intake/2026-09-28-merlain-core-prd-intake.md) | Ready-With-Gaps, zéro blocage de rédaction ; ce résumé n'est ni une approbation du présent PRD ni une autorité supérieure à S1 |

**CONFIRME** : décision utilisateur ; ne signifie pas implémenté.
**CIBLE SPECIFIEE** : comportement testable rédigé ici pour réaliser cette décision ; reste soumis à revue.
**RECOMMANDATION A VALIDER** : choix proposé lorsque l'utilisateur n'a pas tranché ; critères conditionnels à son adoption.
**INTEGRATION NON VERIFIEE** : composant, protocole effectif ou compatibilité dont aucune preuve d'intégration n'est établie.
S1 prévaut sur S2/S3/S5 et les anciennes projections de décisions ; S4 reste la référence détaillée du module mémoire.
Les namespaces FR-CORE/AC-CORE sont une convention de ce livrable demandée par S1, distincte des FR-001 à FR-008 du plan et des FR-MEM/AC locaux de S4.

### 2.2 Existant documenté et cible

| Sujet | Existant selon S2, sans nouvel audit d'exécution | Cible confirmée par S1 |
| --- | --- | --- |
| Frontend et serveur | React/Vite, ASP.NET en monolithe modulaire | Frontend/serveur composables ; cible microservices polyglottes Dapr |
| Domaine | Endpoints statiques Campaigns/Bestiary, données de démonstration | Campagnes, chroniques, bibliothèque, incarnations, fiches et journaux persistants |
| Identité | Écran de connexion et configuration, pas un parcours d'authentification abouti | Comptes locaux par nom d'utilisateur ou email et mot de passe, ou OIDC |
| Agents | Worker avec heartbeat, pas un traitement IA métier vérifié | Texte, images et enrichissement privé ; mémoire optionnelle décrite séparément |
| Exploitation | Configuration de dépendances présente, pas une preuve de tous les comportements | Application locale Compose ; dépendances locales ou distantes configurées |
| Échange | Aucun export VTT vérifié dans les preuves | Téléchargements avec audience, provenance et rapport de compatibilité |

Aucune métrique commerciale, tarification, popularité, charge simultanée, configuration matérielle ou estimation d'effort n'a été fournie.
Aucun BRD approuvé ni handoff de faisabilité n'est fourni comme source ; aucun verdict de faisabilité n'est fabriqué.

## 3. Utilisateurs et objectifs

### 3.1 Personas de rôle

| Persona | Travail à accomplir | Difficulté à traiter | Résultat attendu |
| --- | --- | --- | --- |
| P-MJ : MJ préparant sa campagne | Préparer scènes, personnages et secrets, arbitrer ce qui est partagé | Préparation privée et informations joueurs risquent de se mélanger | Retrouver et publier une version maîtrisée sans réécrire toute sa bibliothèque |
| P-J : joueur membre d'une campagne | Lire les informations autorisées, tenir sa fiche et contribuer au journal | Dépendance inutile au MJ pour une modification ou un récit humain | Éditer directement sa fiche et publier son entrée sans accéder aux secrets |
| P-ADM : administrateur de l'installation | Choisir modules, profils et dépendances | Une option configurée peut être confondue avec une fonction disponible ou un droit utilisateur | Voir une capacité disponible, désactivée ou indisponible sans gérer les conteneurs dans l'application |
| P-CONTR : MJ propriétaire d'une bibliothèque personnelle | Préparer des personnages réutilisables, y compris depuis ses livres | Perte de provenance ou propagation involontaire de changements entre campagnes | Réutiliser des sources validées avec incarnations indépendantes |

Ces personas sont déduites des rôles et usages confirmés, sans démographie ni entretiens inventés.
Un même compte peut exercer plusieurs rôles selon la campagne ; administrer l'installation ne confère pas automatiquement un rôle MJ.

### 3.2 Objectifs et mesures de succès proposées

| Objectif | Résultat produit | Mesure de recette proposée pour V1 | Responsable proposé |
| --- | --- | --- | --- |
| GOAL-CORE-001 | Préparer et poursuivre une campagne sans IA | Réaliser J1/J2/J3 avec IA désactivée ; retrouver les données sauvegardées après redémarrage | Produit et qualité |
| GOAL-CORE-002 | Donner une autonomie explicite aux joueurs sans exposition de secrets | Réaliser J3 ; aucun contenu interdit observé dans les cas négatifs UI, recherche, RAG et export | Produit et qualité |
| GOAL-CORE-003 | Réutiliser des personnages et sources sans confusion d'origine | Réaliser J2/J4 avec deux incarnations indépendantes et provenance consultable selon les droits | MJ référent et qualité |
| GOAL-CORE-004 | Enrichir sans publication IA ou remplacement destructeur implicite | Réaliser J4 et le parcours mémoire applicable ; toute sortie IA joueur renvoie à une version validée | Produit et qualité |
| GOAL-CORE-005 | Récupérer des contenus réutilisables avec des limites honnêtes | Réaliser J5 pour chaque type autorisé ; aucun fichier manquant ou champ perdu dissimulé | Produit et technique |

Ces cibles de recette sont **RECOMMANDATION A VALIDER**, pas des résultats mesurés ni des KPI d'adoption.
Baseline d'usage : non mesurée. La première revue de parcours constitue la référence ; répéter à chaque candidate V1 sur un corpus synthétique autorisé.
Le résultat à consigner est réussi/échoué/non vérifiable pour chaque critère, avec preuve et cause ; un cas non vérifiable n'est pas compté comme réussi.
L'évaluation SMART globale reste différée : horizon V1 défini, mais date, ressources, faisabilité et objectifs quantifiés d'usage non approuvés.

## 4. Parcours de bout en bout

### J1 — Mettre à disposition une campagne locale

1. P-ADM sélectionne les capacités et dépendances par configuration et profils Compose, hors de l'interface métier.
2. L'application reflète les capacités effectivement disponibles ; une dépendance invalide produit un état explicite, pas un bouton de succès factice.
3. P-MJ se connecte par une méthode activée, crée une campagne, choisit l'édition de règles et sa direction narrative.
4. P-MJ accueille les membres selon la politique proposée ; leur rôle ne découle pas du simple fait qu'un module est activé.
5. Un joueur rejoint uniquement la campagne autorisée ; le MJ peut commencer son carnet privé sans le révéler.

Échec : connexion refusée, fournisseur indisponible ou adhésion révoquée laisse l'accès fermé sans création d'un rôle de secours.
Sortie : campagne identifiable, édition explicite et membres autorisés. Références : FR-CORE-001 à FR-CORE-006.

### J2 — Préparer une histoire à branches avec des personnages réutilisables

1. P-MJ crée des chroniques représentant chacune une scène ou un événement précis, puis leurs branches et une reconvergence.
2. Il prépare un personnage de bibliothèque et sa biographie, sans devoir le placer dans une scène.
3. Il crée une incarnation dans la campagne, conserve son lien à la source et remplit librement sa fiche adaptée à l'édition.
4. Il rattache une incarnation, un monstre ou un fragment d'intrigue à une chronique ; un personnage non placé reste dans la bibliothèque.
5. Il modifie ensuite la source : l'incarnation conserve sa propre version ; toute reprise d'évolution reste explicite.

Échec : branche cyclique selon la politique proposée, référence inaccessible ou champ incompatible donne une explication et ne détruit pas les données.
Sortie : préparation persistée, source et incarnation distinguables, secrets toujours privés. Références : FR-CORE-007 à FR-CORE-011.

### J3 — Participer comme joueur et résoudre un conflit d'édition

1. P-J ouvre une campagne dont il est membre et consulte les contenus publiés pour lui, sa fiche complète et ses cartes d'équipement.
2. Il modifie directement un champ autorisé de sa fiche ; le MJ peut consulter l'historique sans approbation préalable de la modification.
3. Si le MJ a enregistré une autre révision entre-temps, la proposition est conservée et un conflit explicite remplace l'écrasement silencieux.
4. P-J rédige une entrée humaine au journal partagé ; elle devient visible aux participants sans validation IA préalable.
5. Le MJ peut modérer cette entrée selon la politique proposée, avec motif et indication lisible plutôt qu'une réécriture attribuée au joueur.

Échec : membre révoqué, fiche d'autrui ou entrée interdite reste inaccessible même via un lien conservé.
Sortie : fiche et journal à jour, historique accessible au MJ ; retour au manuel si IA absente. Références : FR-CORE-012 à FR-CORE-017, FR-CORE-024.

### J4 — Importer un livre personnel et enrichir la préparation

1. P-CONTR importe un PDF textuel ou un PDF scanné pour OCR, dans un espace privé et sous sa responsabilité quant aux droits.
2. L'extraction propose des brouillons de fiches, avec origine, pages disponibles et incertitudes, sans prétendre qu'ils sont validés.
3. P-CONTR corrige et valide humainement les fiches retenues ; une page illisible ne devient pas un personnage inventé.
4. Si un fournisseur distant est configuré, des extraits autorisés peuvent lui être transmis pour le traitement prévu, sans nouveau consentement technique par livre.
5. Le MJ peut obtenir texte et image pour sa préparation ; la publication aux joueurs exige une version adaptée à leur audience et validée par lui.

Échec : extraction partielle, droit de diffusion non établi ou fournisseur indisponible laisse une sortie privée/incomplète ou un refus explicite.
Sortie : personnage validé et réutilisable, contenu IA distinct du contenu humain, aucune redistribution du livre implicite. Références : FR-CORE-018 à FR-CORE-024.

### J5 — Produire une mémoire réutilisable puis télécharger un paquet

1. Si le module mémoire est disponible, le MJ suit le parcours de S4 pour transcript et résumés ; sinon il prépare manuellement un résumé.
2. Il choisit les contenus à exporter, leur version, leur audience et une cible déclarée, sans ouvrir une connexion à un compte VTT.
3. Le contrôle préalable indique les types supportés, champs perdus, médias non pris en charge, restrictions et compatibilités inconnues.
4. Le MJ corrige la sélection ou accepte explicitement un paquet de repli identifié comme tel ; un joueur ne peut choisir que son périmètre autorisé.
5. Le fichier ou paquet téléchargé permet de réutiliser les contenus admis ; le manifeste distingue sorties générées et éventuels enregistrements sources.

Échec : perte non acceptée, fuite de secret, version non validée ou fichier absent empêche d'annoncer un export complet.
Sortie : téléchargement autonome et rapport fidèle ; import réel dans FoundryVTT/Roll20 non présumé. Références : FR-CORE-025 à FR-CORE-030 et S4.

## 5. Modèle conceptuel et dépendances

### 5.1 Modules et disponibilité

Un module est une capacité produit ; ce tableau n'impose ni un service par ligne, ni API, ni schéma de stockage.
La disponibilité du module, la connexion d'un compte et son autorisation sur le contenu sont trois contrôles distincts.

| Module conceptuel | Responsabilité | Dépendances métier | Absence ou panne attendue |
| --- | --- | --- | --- |
| Composition et identité | Capacités visibles, méthodes de connexion, rôles par campagne | Configuration, fournisseur d'identité choisi | Pas de connexion fictive ni de privilèges implicites |
| Campagnes et carnet MJ | Édition, direction, préparation privée | Identité et persistance | Sauvegarde refusée explicitement si indisponible |
| Chroniques | Scènes, événements, branches et reconvergences | Campagne et contenus accessibles | Pas de suppression de scènes ni de liens lors d'une désactivation |
| Bibliothèque et incarnations | Sources réutilisables, versions de campagne | Identité ; campagne seulement pour l'incarnation | Fiches existantes conservées ; aucun écrasement à la réactivation |
| Fiches et équipement | Saisie libre, attribution, historique, cartes | Campagne, édition, incarnation | L'absence du rendu carte ne retire pas l'accès autorisé à la fiche |
| Journal partagé | Publications humaines et modération | Membres de la campagne | Indépendant des fournisseurs IA |
| Import documentaire | PDF, OCR, extraction privée, validation | Bibliothèque, stockage ; traitement optionnel configuré | Rejet ou état partiel ; aucune fiche validée automatiquement |
| IA texte et images | Enrichissement privé et brouillons | Contexte autorisé, fournisseur configuré | Préparation manuelle maintenue, versions antérieures intactes |
| Mémoire de session | Transcript, contexte Graphiti, résumés, orchestration | Campagne, sources, S4 et composants à examiner | Module optionnel ; aucun pipeline réputé vérifié |
| Publication et recherche | Projections d'audience et accès aux contenus | Autorisations, provenance, versions | Ne renvoie pas de données hors périmètre si une vérification échoue |
| Exports | Téléchargements, compatibilité, rapport de pertes | Contenus autorisés, capacités d'adaptateur | Pas de transfert direct ni de faux succès |
| Conservation et reprise | Retrouver les données et restaurer une sauvegarde | Persistance et supports disponibles | Échec explicite ; export VTT non assimilé à une sauvegarde |

### 5.2 Relations métier

Une campagne choisit une édition de règles et possède une direction, un carnet privé, des membres et des chroniques.
Les chroniques sont reliées entre elles ; aucune couche obligatoire d'arc ou de scénario n'est ajoutée.
Une entrée de bibliothèque peut exister sans campagne ; chaque incarnation porte son lien à cette source et son contexte de campagne indépendant.
Une fiche décrit l'incarnation selon l'édition ; des cartes représentent son équipement sans remplacer cette fiche.
Les pièces narratives attachées aux scènes comprennent PNJ, monstres, fragments de scénario et intrigues.
Le graphe de connaissance Graphiti sert à retrouver du contexte : il n'est pas le graphe des branches narratives.
L'épisode reste une unité de résumé à définir, sans égalité implicite avec une session de jeu ou une chronique.

## 6. Acteurs, permissions et visibilité

**CONFIRME** : séparation MJ/joueurs, fiche propre, édition directe, historique visible au MJ, journal humain direct et validation MJ des publications IA.
Les attributions fines ci-dessous sont une **CIBLE SPECIFIEE** ; les extensions signalées R renvoient aux recommandations de la section 11.

| Contenu ou action | MJ de cette campagne | Joueur propriétaire/auteur | Autre joueur membre | Administrateur seul | Non-membre / service IA |
| --- | --- | --- | --- | --- | --- |
| Direction et carnet privé MJ | Lire, écrire | Aucun accès au privé | Aucun accès au privé | Pas de droit métier automatique, R02 | Non-membre : aucun ; IA : contexte confié seulement |
| Chronique ou pièce narrative publiée | Préparer et publier | Lire si dans l'audience | Lire si dans l'audience | Selon rôle métier distinct | Aucun accès implicite |
| Bibliothèque personnelle / import de livre | Propriétaire ou partage explicite, R02 | Pas d'accès par simple appartenance | Même règle | Pas de droit métier automatique | Aucun accès hors traitement autorisé |
| Incarnation et fiche d'un joueur | Consulter ; correction MJ proposée R04 | Fiche complète et édition des champs autorisés | Pas de fiche privée par défaut, R02 | Selon rôle métier distinct | Aucun accès implicite |
| Historique de fiche | Lire, CONFIRME | Son historique non secret, proposé R04 | Aucun par défaut, R02 | Selon rôle métier distinct | Pas d'historique élargi pour l'IA |
| Cartes d'équipement | Gérer dans la campagne, R04 | Consulter ; édition suivant R04 | Seulement projection partagée | Selon rôle métier distinct | Mêmes droits que le contenu source |
| Journal humain partagé | Lire, contribuer, modérer | Publier directement ses entrées | Lire les entrées visibles | Selon rôle métier distinct | Service IA : pas de publication autonome |
| Brouillon IA / transcript / résumé privé | Relire et valider la publication | Aucun accès avant autorisation | Même règle | Selon rôle métier distinct | Traitement limité à la demande et à l'audience |
| Recherche, RAG et export | Seulement contenu autorisé pour la destination | Seulement contenu autorisé | Seulement contenu autorisé | Aucun contournement | Aucun élargissement par récupération ou dérivation |

Une personne cumulant administrateur et MJ applique ses droits MJ, pas un privilège de lecture implicite du rôle administrateur.
L'administrateur hôte peut techniquement contrôler les fichiers et sauvegardes : cette confiance d'exploitation n'est pas une permission métier ni une promesse de chiffrement contre l'hôte.
La révocation ferme les accès ultérieurs, y compris recherche et téléchargement ; elle ne peut effacer un fichier déjà téléchargé hors du contrôle de MerlAIn.
Un contenu partageable ne transmet pas automatiquement les droits de lire son livre source, ses brouillons ou sa provenance privée.

## 7. Exigences fonctionnelles et critères d'acceptation

Chaque FR précise acteur, déclencheur, résultat et statut. Les AC immédiatement associés couvrent cette FR ; ils sont tous **à exécuter**, jamais déclarés réussis ici.
Les contrôles d'accès de FR-CORE-004/024 et les états dégradés de FR-CORE-029 s'appliquent à toutes les FR, même lorsque leur cas d'échec porte sur une autre erreur.
Les priorités et liens aux objectifs figurent dans la matrice de traçabilité. Aucun critère n'impose une fonction, un endpoint ou un type de code.

### FR-CORE-001 — Composer les capacités de l'installation

**P-ADM / configuration appliquée / CONFIRME, CIBLE SPECIFIEE :** présenter les capacités réellement configurées et utilisables, indépendamment des autorisations de l'utilisateur.

- **AC-CORE-001 :** Étant donné un profil local valide, quand l'application expose ses fonctions, alors les modules disponibles correspondent à la composition activée du frontend et du serveur.
- **AC-CORE-002 :** Si une dépendance manque ou la configuration d'une capacité est incohérente, alors cette capacité est indiquée indisponible avec un motif exploitable, sans succès fictif ni ouverture de droits.
- **AC-CORE-003 :** Quand un module contenant des données est désactivé puis réactivé, alors ses données et droits sont conservés ; l'interface ne promet aucune commande de création ou d'arrêt de conteneur.

### FR-CORE-002 — Se connecter par une méthode activée

**P-MJ/P-J / connexion / CONFIRME, CIBLE SPECIFIEE :** ouvrir une session locale avec nom d'utilisateur ou email et mot de passe, ou via le fournisseur OIDC configuré.

- **AC-CORE-004 :** Étant donné un compte local valide, quand son nom d'utilisateur puis son email associé sont utilisés avec le bon mot de passe, alors ils donnent accès au même compte et aux mêmes droits.
- **AC-CORE-005 :** Étant donné OIDC activé, quand le fournisseur configuré authentifie un compte reconnu, alors la session correspond à cette identité ; l'absence de rôle de campagne ne devient pas un rôle MJ.
- **AC-CORE-006 :** Si les justificatifs sont incorrects, le fournisseur indisponible ou la méthode désactivée, alors aucune session de substitution n'est créée ; l'erreur n'énumère pas les comptes existants.

### FR-CORE-003 — Rendre les comptes récupérables sans fusion implicite

**P-ADM/titulaire / création, liaison ou récupération / RECOMMANDATION A VALIDER R01 :** distinguer les identités et proposer une récupération ne supposant pas SMTP.

- **AC-CORE-007 :** Quand un compte est créé selon R01, alors son identifiant local est non ambigu ; une liaison OIDC exige la preuve des deux identités et conserve des appartenances explicitement vérifiées.
- **AC-CORE-008 :** Si deux comptes ont un email identique ou une preuve de liaison manque, alors aucune fusion, récupération ou élévation de droits n'est effectuée sur la seule base de cet email.
- **AC-CORE-009 :** Étant donné une installation sans SMTP, quand le titulaire suit la procédure de récupération approuvée, alors il retrouve son compte sans changement de rôle ; une preuve invalide est refusée et un justificatif à usage unique ne peut être réutilisé.

### FR-CORE-004 — Contrôler l'appartenance et les rôles par campagne

**P-MJ/P-J / accès à une campagne / CONFIRME pour la séparation, R02 pour l'adhésion :** appliquer les permissions du compte dans la campagne concernée.

- **AC-CORE-010 :** Étant donné une invitation nominative acceptée selon R02, quand le membre rejoint la campagne, alors son rôle et les contenus accessibles correspondent à l'attribution explicite du MJ.
- **AC-CORE-011 :** Si un non-membre tente de lire ou modifier une ressource, alors l'accès est refusé sans révéler son contenu ; activer un module ou connaître son lien ne suffit pas.
- **AC-CORE-012 :** Quand une appartenance est révoquée, alors les accès suivants, y compris depuis une session ou un lien existant, sont refusés sans supprimer les données de campagne.

### FR-CORE-005 — Créer une campagne avec son édition et sa direction

**P-MJ / création / CONFIRME, CIBLE SPECIFIEE :** conserver une campagne identifiée avec D&D 5e 2014, D&D 5e 2024 ou The One Ring 2e et une direction narrative.

- **AC-CORE-013 :** Quand le MJ enregistre une campagne avec l'un des trois choix, alors l'édition et la direction renseignée sont retrouvables lors de sa prochaine ouverture.
- **AC-CORE-014 :** Si l'édition est absente ou non prise en charge, alors l'enregistrement signale l'erreur sans substituer silencieusement une autre édition.
- **AC-CORE-015 :** Selon R05, quand une campagne comportant des fiches demande un changement d'édition, alors aucune conversion ni perte de champ automatique n'est appliquée ; la limite V1 est expliquée.

### FR-CORE-006 — Tenir un carnet d'idées privé MJ

**P-MJ / note de préparation / CONFIRME, CIBLE SPECIFIEE :** conserver les idées privées séparément du journal partagé.

- **AC-CORE-016 :** Quand le MJ sauvegarde une idée, alors il la retrouve dans le contexte de sa campagne avec son statut privé.
- **AC-CORE-017 :** Si un joueur tente d'accéder à cette idée par navigation, recherche ou export, alors ni son texte ni un extrait révélateur ne sont retournés.
- **AC-CORE-018 :** Quand une note privée est enrichie par IA ou mentionnée par une scène publique, alors elle ne devient pas publique par héritage de ce lien.

### FR-CORE-007 — Relier des chroniques avec branches et reconvergences

**P-MJ / organisation narrative / CONFIRME ; cycles selon R03 :** représenter chaque chronique comme une scène ou un événement et relier plusieurs parcours pouvant se rejoindre.

- **AC-CORE-019 :** Quand deux branches rejoignent une même chronique, alors celle-ci reste une scène identifiable unique et ses relations sont consultables sans couche obligatoire d'arc ou scénario.
- **AC-CORE-020 :** Selon R03, si une relation crée un cycle ou vise une chronique d'une autre campagne, alors elle est refusée avec explication et le graphe existant reste intact.
- **AC-CORE-021 :** Quand le MJ détache une relation, alors les scènes et pièces associées ne sont pas supprimées ; un joueur ne voit que les relations dont la visibilité lui est autorisée.

### FR-CORE-008 — Attacher des éléments narratifs aux scènes

**P-MJ / préparation d'une chronique / CONFIRME, CIBLE SPECIFIEE :** y associer PNJ, monstres, fragments de scénario et intrigues sans confondre association et propriété.

- **AC-CORE-022 :** Quand un élément autorisé est attaché, alors le MJ peut le retrouver depuis la scène avec son origine et son contexte de campagne.
- **AC-CORE-023 :** Si la référence est absente ou interdite, alors l'association échoue explicitement sans révéler le contenu inaccessible ni créer un faux élément.
- **AC-CORE-024 :** Quand un élément est détaché ou la scène publiée, alors sa suppression ou sa publication n'est pas implicite ; les restrictions propres à l'élément restent appliquées.

### FR-CORE-009 — Préparer une bibliothèque indépendante du récit

**P-CONTR / création d'un personnage / CONFIRME, CIBLE SPECIFIEE :** conserver PNJ personnalisés avec biographie, personnages de référence issus de livres personnels et candidats personnages joueurs réutilisables.

- **AC-CORE-025 :** Quand un personnage personnalisé ou candidat PJ est enregistré sans scène ni campagne, alors il reste consultable et réutilisable dans la bibliothèque autorisée.
- **AC-CORE-026 :** Si une entrée de référence provient d'une extraction non validée, alors elle reste un brouillon privé, distingué d'une fiche humainement validée.
- **AC-CORE-027 :** Quand la bibliothèque est recherchée par un autre membre, alors seules les entrées explicitement accessibles sont retournées ; aucun contenu officiel n'est fourni par une bibliothèque protégée embarquée.

### FR-CORE-010 — Créer des incarnations indépendantes liées à leur source

**P-MJ / réutilisation d'un personnage / CONFIRME, CIBLE SPECIFIEE :** créer une incarnation de campagne distincte, sans effacer la filiation à l'entrée de bibliothèque.

- **AC-CORE-028 :** Quand la même source est incarnée dans deux campagnes, alors chaque incarnation conserve un lien d'origine et peut évoluer indépendamment.
- **AC-CORE-029 :** Quand la source ou une autre incarnation est modifiée, alors la fiche de campagne courante n'est pas écrasée ; une éventuelle reprise de changement exige une action explicite.
- **AC-CORE-030 :** Si la source devient inaccessible, alors l'incarnation autorisée subsiste avec une origine indiquée indisponible, sans fuite d'information sur l'autre campagne ni fusion par le RAG.

### FR-CORE-011 — Remplir librement une fiche adaptée à l'édition

**P-MJ/P-J autorisé / saisie d'une fiche / CONFIRME, CIBLE SPECIFIEE :** donner accès à une fiche complète adaptée aux trois éditions retenues, sans moteur complet de règles.

- **AC-CORE-031 :** Quand une fiche est ouverte pour chacune des trois éditions, alors l'édition et les rubriques pertinentes sont identifiables et les valeurs saisies librement sont conservées.
- **AC-CORE-032 :** Si une valeur ne respecte pas le format déclaré d'un champ, alors l'erreur est localisée et la saisie n'est pas perdue ; aucune correction de règle non demandée n'est appliquée.
- **AC-CORE-033 :** Quand aucune carte d'équipement ni IA n'est disponible, alors le joueur autorisé peut toujours consulter sa fiche complète ; l'application ne prétend pas certifier la légalité des choix de règles.

### FR-CORE-012 — Modifier directement sa propre fiche

**P-J / modification de sa fiche / CONFIRME ; frontières de champ selon R04 :** enregistrer directement les changements autorisés sans préapprobation MJ.

- **AC-CORE-034 :** Quand le joueur modifie un champ autorisé de sa fiche, alors la nouvelle valeur est sauvegardée et visible au MJ sans passage par une file d'approbation.
- **AC-CORE-035 :** Si le joueur tente de modifier une fiche d'autrui ou un champ protégé selon R04, alors le changement est refusé avec une indication adaptée, sans altération de la valeur existante.
- **AC-CORE-036 :** Quand une action d'édition ordinaire est réalisée, alors elle ne confère ni suppression de la fiche entière, ni changement de propriétaire, ni modification de rôle.

### FR-CORE-013 — Rendre l'historique des fiches consultable

**P-MJ / consultation des changements / CONFIRME ; historique propre selon R04 :** montrer qui a modifié une fiche et ce qui a changé, selon les droits.

- **AC-CORE-037 :** Quand une modification est enregistrée, alors le MJ retrouve l'auteur, le moment, la révision et les valeurs modifiées ou leur représentation autorisée.
- **AC-CORE-038 :** Selon R04, quand un joueur consulte son propre historique, alors il voit ses changements et les changements non secrets de sa fiche, sans accès aux notes privées MJ.
- **AC-CORE-039 :** Si un lecteur non autorisé demande une révision ancienne, alors elle est protégée comme le contenu courant ; l'historique ne sert pas à retrouver un secret retiré de sa visibilité.

### FR-CORE-014 — Signaler les conflits d'édition

**P-J/P-MJ / sauvegarde depuis une révision dépassée / RECOMMANDATION A VALIDER R06 :** prévenir l'écrasement silencieux d'une modification concurrente.

- **AC-CORE-040 :** Étant donné deux éditions de la même révision, quand la seconde est soumise après la première, alors un conflit explicite présente la version actuelle et la proposition encore autorisée.
- **AC-CORE-041 :** Si le conflit n'est pas résolu, alors la première sauvegarde reste intacte et la seconde n'est pas annoncée enregistrée ; la proposition saisie reste récupérable par son auteur autorisé.
- **AC-CORE-042 :** Quand l'auteur résout le conflit sur une révision actuelle, alors sa décision est enregistrée dans l'historique ; de nouveaux droits refusés empêchent la sauvegarde.

### FR-CORE-015 — Consulter l'équipement sous forme de cartes

**P-J/P-MJ / consultation d'équipement / CONFIRME ; édition selon R04 :** présenter des cartes d'équipement à côté de la fiche.

- **AC-CORE-043 :** Quand le joueur ouvre son équipement, alors chaque carte autorisée est rattachée à la bonne incarnation et la fiche complète reste accessible.
- **AC-CORE-044 :** Si une illustration est absente ou ne se charge pas, alors les informations textuelles autorisées de l'équipement restent disponibles avec indication de la limite.
- **AC-CORE-045 :** Quand une carte est retirée d'une présentation ou exportée, alors ni la fiche entière ni la source de bibliothèque ne sont supprimées ; les droits de la carte restent vérifiés.

### FR-CORE-016 — Publier directement un journal humain partagé

**P-J/P-MJ membre / envoi d'une entrée humaine / CONFIRME, CIBLE SPECIFIEE :** publier sa contribution au journal de campagne sans approbation préalable de l'IA ou du MJ.

- **AC-CORE-046 :** Quand un membre publie son texte humain, alors l'entrée et son auteur deviennent visibles aux participants autorisés sans attente d'un traitement IA.
- **AC-CORE-047 :** Si la sauvegarde échoue ou l'appartenance a été révoquée, alors aucune publication réussie n'est annoncée ; l'erreur permet de distinguer panne et refus sans exposer de contenu tiers.
- **AC-CORE-048 :** Quand une entrée contient une sortie identifiée comme générée par MerlAIn, alors cette sortie conserve le contrôle de publication IA ; l'application ne prétend pas détecter toute IA externe collée comme texte humain.

### FR-CORE-017 — Modérer le journal sans usurper l'auteur

**P-MJ / modération d'une entrée / CONFIRME ; modalités selon R07 :** intervenir a posteriori sur le journal partagé.

- **AC-CORE-049 :** Selon R07, quand le MJ masque une entrée avec un motif, alors les participants voient qu'elle a été modérée et non une disparition présentée comme une action de l'auteur.
- **AC-CORE-050 :** Si un joueur tente de modérer l'entrée d'autrui, alors l'action est refusée ; une correction MJ ne peut être attribuée silencieusement à l'auteur initial.
- **AC-CORE-051 :** Quand une restauration autorisée a lieu selon R07, alors son historique et sa visibilité sont explicites ; masquer une entrée n'expose pas son contenu dans la recherche ou l'export.

### FR-CORE-018 — Importer des PDF textuels et des scans

**P-CONTR / import d'un livre personnel / CONFIRME, CIBLE SPECIFIEE :** préparer une extraction privée à partir de texte PDF ou de pages scannées traitées par OCR.

- **AC-CORE-052 :** Quand un PDF textuel ou scanné accepté est traité, alors le texte exploitable est associé au document et aux pages identifiables, avec indication de l'utilisation d'OCR.
- **AC-CORE-053 :** Si le document est vide, corrompu, illisible ou hors limites déclarées, alors l'import indique la cause sans annoncer une extraction complète ; aucun contournement de DRM n'est tenté.
- **AC-CORE-054 :** Si seules certaines pages sont exploitables, alors le résultat est marqué partiel et les pages incertaines ou manquantes sont signalées sans contenu de remplacement inventé.

### FR-CORE-019 — Valider humainement les fiches extraites

**P-CONTR / revue d'une extraction / CONFIRME, CIBLE SPECIFIEE :** transformer des propositions privées en fiches validées uniquement après intervention humaine.

- **AC-CORE-055 :** Quand une proposition est relue, corrigée et validée, alors la fiche conserve sa provenance et son statut validé, distincts de sa publication éventuelle.
- **AC-CORE-056 :** Si une caractéristique est incertaine ou sa page indisponible, alors cette limite reste visible au validateur ; une valeur absente n'est pas présentée comme un fait extrait.
- **AC-CORE-057 :** Quand une proposition est rejetée ou remplacée par une nouvelle extraction, alors aucune fiche validée ou incarnation existante n'est écrasée automatiquement.

### FR-CORE-020 — Conserver les restrictions des sources

**P-CONTR/P-MJ / usage, partage ou traitement d'une source / CONFIRME, CIBLE SPECIFIEE :** distinguer droit d'usage, autorisation de fournisseur et droit de diffusion.

- **AC-CORE-058 :** Quand un fournisseur distant configuré reçoit des extraits pour le traitement prévu, alors la destination et la nature des données transmises sont identifiables sans exiger un nouveau consentement technique par livre.
- **AC-CORE-059 :** Si les droits de diffusion d'un élément ne sont pas établis, alors son partage/export externe est bloqué ou l'élément est exclu avec explication ; la possession du livre ou la configuration IA ne vaut pas licence de redistribution.
- **AC-CORE-060 :** Quand un dérivé est réutilisé, alors ses restrictions et sa provenance restent attachées ou les éléments restreints sont supprimés de la projection ; aucun livre complet ni prose protégée illimitée n'est ajouté par défaut.

### FR-CORE-021 — Enrichir la préparation par IA textuelle

**P-MJ/enrichissement privé configuré / génération / CONFIRME, CIBLE SPECIFIEE :** produire des propositions textuelles via un fournisseur compatible OpenAI sans rendre l'IA nécessaire au noyau.

- **AC-CORE-061 :** Quand une génération autorisée réussit, alors la sortie est identifiée comme IA, privée par défaut, rattachée à son contexte et distincte de la version précédente.
- **AC-CORE-062 :** Si le fournisseur est désactivé, incompatible, indisponible ou renvoie un résultat vide, alors l'échec est explicite et les contenus existants restent intacts et utilisables manuellement.
- **AC-CORE-063 :** Quand un enrichissement privé automatique s'exécute, alors il ne publie, ne supprime ni n'écrase destructivement aucun contenu ; une instruction trouvée dans une source ne lui confère pas de droits supplémentaires.

### FR-CORE-022 — Générer des images optionnelles

**P-MJ/enrichissement privé configuré / demande d'image / CONFIRME, CIBLE SPECIFIEE :** obtenir un média via un fournisseur d'images configuré, sans imposer le protocole OpenAI.

- **AC-CORE-064 :** Quand le fournisseur déclaré retourne une image exploitable, alors le MJ peut la consulter comme brouillon privé avec son origine de génération et les conditions connues.
- **AC-CORE-065 :** Si la sortie est absente, corrompue ou d'un type non pris en charge, alors aucun média réussi n'est annoncé ; la description textuelle et les médias précédents restent disponibles.
- **AC-CORE-066 :** Quand une image est régénérée, alors elle constitue une nouvelle proposition ; le média déjà publié n'est ni remplacé ni supprimé sans action explicite autorisée.

### FR-CORE-023 — Publier une version IA pour une audience

**P-MJ / validation de diffusion / CONFIRME ; mécanisme de réexamen CIBLE SPECIFIEE :** lier chaque diffusion IA aux joueurs à une version et une audience validées.

- **AC-CORE-067 :** Quand le MJ valide une version adaptée à une audience, alors seuls les joueurs de cette audience accèdent à cette version, pas au brouillon, aux sources secrètes ou aux variantes.
- **AC-CORE-068 :** Si la validation manque ou qu'un secret ou dérivé révélateur reste dans la projection, alors consultation et export joueur sont refusés ; une validation ne contourne pas les droits sur les sources.
- **AC-CORE-069 :** Quand une source, le contenu ou les droits changent, alors la version concernée est signalée pour réexamen et n'hérite pas automatiquement d'une approbation ; une version devenue révélatrice cesse d'être servie aux joueurs.

### FR-CORE-024 — Isoler la recherche, le RAG et les contenus dérivés

**Tout acteur / lecture ou récupération / CONFIRME, CIBLE SPECIFIEE :** appliquer la campagne, les droits et l'audience à toute restitution, y compris extraits, références et dérivés.

- **AC-CORE-070 :** Quand une recherche ou récupération réussit, alors résultats, extraits et sources consultables appartiennent uniquement au périmètre autorisé pour cette demande.
- **AC-CORE-071 :** Étant donné deux campagnes et des sources privées/publiées, quand une sortie joueur est produite après une recherche MJ, alors aucun secret, fait d'une autre incarnation ou métadonnée révélatrice n'est restitué.
- **AC-CORE-072 :** Si l'autorisation ou la récupération échoue, alors le résultat distingue échec, refus et recherche sans résultat, sans élargissement automatique du périmètre.

### FR-CORE-025 — Télécharger tous les types de contenus générés autorisés

**P-MJ/P-J autorisé / sélection d'export / CONFIRME ; paquet de repli R08 :** proposer une représentation pratique réutilisable de chaque type de sortie générée accessible.

- **AC-CORE-073 :** Quand l'export contient texte, personnage, image, résumé de campagne/scène, journal autorisé ou autre média généré admis, alors chaque élément sélectionné possède un fichier ou une représentation annoncée dans le paquet.
- **AC-CORE-074 :** Si un élément est interdit, non validé pour l'audience ou impossible à représenter, alors l'export complet est bloqué ou une exclusion est explicitement acceptée ; aucun paquet joueur ne contient de secret MJ.
- **AC-CORE-075 :** Quand le paquet est téléchargé, alors sorties générées et éventuelles sources sont distinguées ; un enregistrement audio source n'est pas inclus automatiquement ni qualifié de contenu généré.

### FR-CORE-026 — Annoncer la compatibilité et les pertes avant export

**P-MJ/P-J autorisé / choix FoundryVTT, Roll20 ou repli / CONFIRME, CIBLE SPECIFIEE :** informer des capacités réelles avant de fabriquer le téléchargement.

- **AC-CORE-076 :** Quand une cible est choisie, alors version visée, types admis, limites, champs perdus et médias non supportés sont présentés avant confirmation, conformément à la matrice de la section 9.
- **AC-CORE-077 :** Si la compatibilité native est inconnue, alors le système ne la présente pas comme vérifiée ; seul un repli explicitement choisi peut être produit avec ce statut.
- **AC-CORE-078 :** Quand un champ ou un média prévu manque au résultat final, alors l'export est incomplet ou échoué avec rapport fidèle, jamais une réussite silencieusement amputée.

### FR-CORE-027 — Étendre les cibles dans les capacités d'adaptateurs déclarées

**P-ADM / ajout ou modification de configuration d'export / CONFIRME, CIBLE SPECIFIEE :** permettre des plateformes futures dans les transformations déjà prises en charge.

- **AC-CORE-079 :** Quand une configuration valide utilise des capacités déclarées, alors sa cible, sa version et ses limites deviennent identifiables pour l'export sans changement de permissions.
- **AC-CORE-080 :** Si elle demande un nouveau protocole, une transformation exécutable inconnue ou dépasse les limites déclarées, alors elle est refusée en indiquant le besoin d'un adaptateur revu.
- **AC-CORE-081 :** Quand la configuration change, alors aucun script arbitraire n'est exécuté et aucun transfert direct n'est introduit ; un export antérieur conserve son contexte de cible et de version.

### FR-CORE-028 — Raccorder la mémoire de session sans en présumer l'intégration

**P-MJ / utilisation de la mémoire optionnelle / CIBLE SPECIFIEE, INTEGRATION NON VERIFIEE :** appliquer le contrat produit de S4 sans dupliquer ses exigences.

- **AC-CORE-082 :** Quand le module est proposé, alors son périmètre renvoie à FR-MEM-001 à FR-MEM-007 : Whisper/transcript, Graphiti, Storyteller/résumés et orchestrateur sont identifiés comme capacités rapportées tant que non vérifiées.
- **AC-CORE-083 :** Si interfaces, version ou composant requis ne sont pas disponibles, alors aucun traitement mémoire réussi n'est affirmé ; les parcours manuels restent accessibles selon FR-CORE-029.
- **AC-CORE-084 :** Quand un transcript ou résumé rejoint la publication ou l'export, alors les contrôles de S4 et FR-CORE-020/023/024 s'appliquent ; épisode, chronique et graphe de connaissance ne sont pas confondus.

### FR-CORE-029 — Préserver le travail manuel et les données en mode dégradé

**P-MJ/P-J / désactivation ou panne d'un module optionnel / CONFIRME, CIBLE SPECIFIEE :** maintenir le noyau indépendant des enrichissements et éviter une disparition de données.

- **AC-CORE-085 :** Quand IA, import ou mémoire est désactivé, alors campagne, carnet, chroniques, fiches et journal humains restent utilisables si leurs dépendances de base sont disponibles.
- **AC-CORE-086 :** Si un média ou traitement est momentanément inaccessible, alors la donnée existante reste référencée avec son état ; elle n'est ni supprimée ni remplacée par un résultat vide.
- **AC-CORE-087 :** Si la persistance nécessaire est indisponible, alors la sauvegarde est annoncée en échec et non réussie ; le retour au manuel ne prétend pas permettre des écritures durables sans stockage.

### FR-CORE-030 — Retrouver et restaurer les données conservées

**P-ADM/P-MJ autorisé / redémarrage ou restauration / RECOMMANDATION A VALIDER R10 :** distinguer sauvegarde de restauration et export VTT de réutilisation.

- **AC-CORE-088 :** Quand une installation redémarre après des enregistrements confirmés, alors campagnes, versions, liens d'origine, droits et médias sauvegardés sont retrouvés sans réinitialisation implicite.
- **AC-CORE-089 :** Quand une sauvegarde compatible est restaurée dans une cible contrôlée, alors le rapport identifie son périmètre et ses limites, et les contrôles d'accès retrouvent la même séparation des audiences.
- **AC-CORE-090 :** Si la sauvegarde est incomplète, corrompue ou incompatible, alors la restauration ne remplace pas silencieusement les données actives ; un paquet VTT partiel ne constitue pas une sauvegarde complète.

## 8. Exigences de qualité

Les seuils et modalités ci-dessous sont des **RECOMMANDATION A VALIDER**, sauf les invariants de confidentialité et frontières confirmés de S1 qu'ils rendent vérifiables.
Les catégories suivent le cadre PRD NIST SP 800-160 ; il ne s'agit ni d'une certification, ni d'un SLO opérationnel signé.

| ID | Catégorie et cible observable proposée | Méthode de recette / dépendance |
| --- | --- | --- |
| NFR-CORE-001 | Performance : baseline de latence **TBD — non mesurée** ; aucun seuil absolu engagé | Technique/qualité mesurent médiane et p95 des parcours manuels et temps d'attente des traitements, à froid/chaud, avec profil local, données et dépendances consignés ; produit approuve ensuite les seuils avant engagement de performance, E07 |
| NFR-CORE-002 | Capacité/évolutivité : limites déclarées de fichiers, pages, médias et travaux en attente ; dépassement refusé avant succès annoncé | Mesurer progression du volume sur un corpus autorisé jusqu'aux limites retenues ; tester à la limite et au-delà ; enveloppe de charge et matériel à établir en E07, sans nombre d'utilisateurs simultanés promis |
| NFR-CORE-003 | Fiabilité : zéro perte d'enregistrements confirmés lors d'un redémarrage contrôlé sur stockage sain | Comparer avant/après données, révisions et permissions pour FR-CORE-030 ; ne couvre pas une destruction du support |
| NFR-CORE-004 | Résilience : une panne d'IA/import/mémoire n'empêche aucun parcours manuel couvert dont les dépendances de base restent disponibles | Injecter une indisponibilité par module et exécuter J2/J3 ; contrôler erreurs et absence d'écrasement, FR-CORE-029 |
| NFR-CORE-005 | Reprise : une restauration de sauvegarde compatible doit restituer les objets inclus et leurs droits, ou signaler précisément les écarts sans remplacement destructeur | Exercice de restauration sur copie isolée ; cadence, RPO/RTO et supports de sauvegarde à faire approuver en E08, sans durée inventée |
| NFR-CORE-006 | Sécurité : aucun accès intercampagne/non autorisé dans le corpus de recette, y compris anciennes versions, recherche, RAG et téléchargement | Matrice section 6, révocation, accès direct et réutilisation de résultats privés ; zéro fuite observée exigé sur ces cas, sans garantie absolue hors corpus |
| NFR-CORE-007 | Identité : secrets d'authentification non exposés, mots de passe non conservés en clair, communications authentifiées distantes protégées | Revue technique du stockage des secrets et transports ; essais de connexion, récupération et liaison R01 ; aucun rôle accordé sur le seul email |
| NFR-CORE-008 | Vie privée : n'envoyer à un fournisseur que le contexte autorisé nécessaire ; ne pas journaliser par défaut livres, prompts, réponses, audio ou carnet privé | Examiner les charges sortantes et journaux sur fixtures ; destination distante affichable, politique de conservation E06 à approuver séparément de l'autorisation fournisseur |
| NFR-CORE-009 | Intégrité collaborative : aucune sauvegarde concurrente conflictuelle annoncée réussie par écrasement silencieux | Cas à deux révisions FR-CORE-014, décision R06 ; ce cas de collision n'est pas une garantie de capacité simultanée |
| NFR-CORE-010 | Accessibilité : objectif WCAG 2.2 AA pour les parcours V1, sans certification revendiquée | Recette clavier, ordre/focus visible, noms et états accessibles, erreurs associées, contraste AA, zoom 200 %, reflow à 320 pixels CSS ; alternative liste/liens au graphe et au glisser-déposer |
| NFR-CORE-011 | Utilisabilité : toute erreur bloquante décrit l'action, l'échec et une correction/reprise possible ; aucun statut communiqué par couleur seule | J1 à J5, lecteurs d'écran et clavier ; champs libres conservés en cas d'erreur lorsque la confidentialité l'autorise ; statut de sauvegarde/génération annoncé |
| NFR-CORE-012 | Maintenabilité/exploitation : une capacité invalide est diagnostiquable sans afficher un secret ni modifier les données | Revue des configurations valides/invalides et parcours P-ADM ; instructions de reprise lisibles, pas de gestion de conteneurs dans l'interface |
| NFR-CORE-013 | Observabilité : pouvoir relier une opération utilisateur à ses traces et erreurs sans copie de contenu privé | Vocabulaire OpenTelemetry pour traces, métriques et logs ; corrélation par `trace_id`/`span_id`, durées en secondes ; pas d'identifiant utilisateur/campagne ni texte libre en dimension métrique ; aucune métrique métier nouvelle imposée ici |
| NFR-CORE-014 | Compatibilité : annoncer une compatibilité native uniquement pour le type, l'édition, l'adaptateur et la version de plateforme effectivement éprouvés | Exemple exporté puis importé et comparé dans la version cible ; tant que cette preuve manque, section 9 reste « Unknown / À vérifier » |
| NFR-CORE-015 | Portabilité : les services applicatifs MerlAIn restent exécutables par Compose local dans la cible documentée ; dépendances remplaçables selon compatibilité déclarée | Revue du futur dossier technique et recette local/dépendance distante configurée ; aucun déploiement applicatif distant requis |

Pour NFR-CORE-013, les conventions HTTP/RPC/GenAI applicables priment sur tout nom inventé ; choix de SDK, collecte et échantillonnage restent au dossier technique.
Les attributs de ressource suivent `telemetry-foundations` ; secrets et données identifiantes suivent son denylist avec suppression ou pseudonymisation explicitée.
L'historique métier de fiche n'est pas un log de télémétrie : le premier contient des changements accessibles selon les droits, le second n'en copie pas le texte.
Aucun débit, taux de disponibilité, coût fournisseur, objectif d'adoption ou délai de livraison n'est engagé par ces NFR.

## 9. Exports : représentations et compatibilité

### 9.1 Matrice des cibles

| Cible | Téléchargement prévu | Personnages / éditions | Texte, journal, résumés | Images et autres médias | Import natif / API |
| --- | --- | --- | --- | --- | --- |
| FoundryVTT | Paquet de fichiers, pas de transfert | Unknown / À vérifier par système, édition et version | Unknown / À vérifier par format et destination | Unknown / À vérifier : formats, taille, rattachement | INTEGRATION NON VERIFIEE ; aucune API ni version native annoncée |
| Roll20 | Paquet de fichiers, pas de transfert | Unknown / À vérifier par fiche, édition et capacités de la cible | Unknown / À vérifier par format et destination | Unknown / À vérifier : formats, taille, rattachement | INTEGRATION NON VERIFIEE ; aucune API ni version native annoncée |
| Paquet universel, R08 | RECOMMANDATION A VALIDER : textes lisibles, représentation structurée des fiches, médias admis et manifeste | Champs et édition explicites ; aucun import VTT automatique promis | Représentation lisible et réutilisable, par exemple texte UTF-8 | Fichiers dans leur format admis ; source et sortie distinguées | Repli non natif ; format exact à arrêter en conception |
| Plateforme future configurée | Seulement dans les capacités de l'adaptateur déclaré | Selon mappings revus | Selon modèles déclarés | Selon limites déclarées | Non prise en charge avant déclaration et preuves ; jamais un protocole exécutable arbitraire |

Le contrôle préalable nomme les champs omis, conversions, formats absents, pièces jointes exclues et restrictions d'usage.
La confirmation porte sur cette représentation et ses pertes connues, pas sur une promesse générale « compatible VTT ».
Le manifeste proposé R08 récapitule contenus, versions, audience, restrictions transmissibles et rapport de pertes sans fuite de provenance privée.
Un échec de construction, un média absent ou une restriction nouvelle invalide l'annonce de réussite complète.
Les droits doivent accompagner le contenu sous une forme communicable ; sinon le contenu restreint est exclu ou l'export bloqué.
Les enregistrements sources suivent une décision distincte de droits/consentement ; les transcripts et résumés générés relèvent aussi de S4.

### 9.2 Surface de configuration conceptuelle

Les réglages envisagés décrivent l'identifiant et la version de plateforme, les correspondances de contenu et champs, les modèles de présentation,
les règles de nommage des fichiers/médias, les types admis et leurs limites, ainsi que le comportement de repli autorisé.
Ce sont des capacités déclaratives : aucun exemple de fichier de configuration, schéma ou API n'est imposé par ce PRD.
Une transformation exécutable ou un protocole nouveau exige du code d'adaptateur revu ; changer la configuration ne permet pas de contourner cette frontière.
**CONFIRME** : Builder, Strategy et Command doivent être utilisés là où pertinents, avec justification dans la future conception technique.
La construction d'un paquet, le choix d'une représentation et une opération d'export sont des points d'application candidats, non des classes prescrites ni une obligation d'utiliser les trois partout.

## 10. Traçabilité, priorités et frontière de livraison

### 10.1 FR vers AC

| FR | AC associés | Nombre |
| --- | --- | --- |
| FR-CORE-001 | AC-CORE-001, AC-CORE-002, AC-CORE-003 | 3 |
| FR-CORE-002 | AC-CORE-004, AC-CORE-005, AC-CORE-006 | 3 |
| FR-CORE-003 | AC-CORE-007, AC-CORE-008, AC-CORE-009 | 3 |
| FR-CORE-004 | AC-CORE-010, AC-CORE-011, AC-CORE-012 | 3 |
| FR-CORE-005 | AC-CORE-013, AC-CORE-014, AC-CORE-015 | 3 |
| FR-CORE-006 | AC-CORE-016, AC-CORE-017, AC-CORE-018 | 3 |
| FR-CORE-007 | AC-CORE-019, AC-CORE-020, AC-CORE-021 | 3 |
| FR-CORE-008 | AC-CORE-022, AC-CORE-023, AC-CORE-024 | 3 |
| FR-CORE-009 | AC-CORE-025, AC-CORE-026, AC-CORE-027 | 3 |
| FR-CORE-010 | AC-CORE-028, AC-CORE-029, AC-CORE-030 | 3 |
| FR-CORE-011 | AC-CORE-031, AC-CORE-032, AC-CORE-033 | 3 |
| FR-CORE-012 | AC-CORE-034, AC-CORE-035, AC-CORE-036 | 3 |
| FR-CORE-013 | AC-CORE-037, AC-CORE-038, AC-CORE-039 | 3 |
| FR-CORE-014 | AC-CORE-040, AC-CORE-041, AC-CORE-042 | 3 |
| FR-CORE-015 | AC-CORE-043, AC-CORE-044, AC-CORE-045 | 3 |
| FR-CORE-016 | AC-CORE-046, AC-CORE-047, AC-CORE-048 | 3 |
| FR-CORE-017 | AC-CORE-049, AC-CORE-050, AC-CORE-051 | 3 |
| FR-CORE-018 | AC-CORE-052, AC-CORE-053, AC-CORE-054 | 3 |
| FR-CORE-019 | AC-CORE-055, AC-CORE-056, AC-CORE-057 | 3 |
| FR-CORE-020 | AC-CORE-058, AC-CORE-059, AC-CORE-060 | 3 |
| FR-CORE-021 | AC-CORE-061, AC-CORE-062, AC-CORE-063 | 3 |
| FR-CORE-022 | AC-CORE-064, AC-CORE-065, AC-CORE-066 | 3 |
| FR-CORE-023 | AC-CORE-067, AC-CORE-068, AC-CORE-069 | 3 |
| FR-CORE-024 | AC-CORE-070, AC-CORE-071, AC-CORE-072 | 3 |
| FR-CORE-025 | AC-CORE-073, AC-CORE-074, AC-CORE-075 | 3 |
| FR-CORE-026 | AC-CORE-076, AC-CORE-077, AC-CORE-078 | 3 |
| FR-CORE-027 | AC-CORE-079, AC-CORE-080, AC-CORE-081 | 3 |
| FR-CORE-028 | AC-CORE-082, AC-CORE-083, AC-CORE-084 | 3 |
| FR-CORE-029 | AC-CORE-085, AC-CORE-086, AC-CORE-087 | 3 |
| FR-CORE-030 | AC-CORE-088, AC-CORE-089, AC-CORE-090 | 3 |

### 10.2 FR vers objectifs et sources

La priorisation MoSCoW ci-dessous est **RECOMMANDATION A VALIDER** pour V1 : M = Must, S = Should, C = Could conditionnel.
Should ne retire pas une fonctionnalité confirmée de V1 : toute coupe du périmètre confirmé exige une décision produit explicite.
Must identifie les invariants nécessaires à un noyau cohérent et confidentiel, pas une promesse de calendrier.

| FR | Objectif(s) GOAL-CORE | Source / proposition | Priorité proposée |
| --- | --- | --- | --- |
| FR-CORE-001 | GOAL-CORE-001 | S1 ; R11 | M |
| FR-CORE-002 | GOAL-CORE-001, GOAL-CORE-002 | S1 | M |
| FR-CORE-003 | GOAL-CORE-002 | R01 | M |
| FR-CORE-004 | GOAL-CORE-002 | S1 ; R02 | M |
| FR-CORE-005 | GOAL-CORE-001 | S1 ; R05 | M |
| FR-CORE-006 | GOAL-CORE-001, GOAL-CORE-002 | S1 | M |
| FR-CORE-007 | GOAL-CORE-001 | S1 ; R03 | M |
| FR-CORE-008 | GOAL-CORE-001, GOAL-CORE-003 | S1 | S |
| FR-CORE-009 | GOAL-CORE-003 | S1 | S |
| FR-CORE-010 | GOAL-CORE-003 | S1 | M |
| FR-CORE-011 | GOAL-CORE-001, GOAL-CORE-002 | S1 ; R05 | M |
| FR-CORE-012 | GOAL-CORE-002 | S1 ; R04 | M |
| FR-CORE-013 | GOAL-CORE-002 | S1 ; R04 | M |
| FR-CORE-014 | GOAL-CORE-002 | R06 | M |
| FR-CORE-015 | GOAL-CORE-002 | S1 ; R04 | S |
| FR-CORE-016 | GOAL-CORE-002 | S1 | M |
| FR-CORE-017 | GOAL-CORE-002 | S1 ; R07 | M |
| FR-CORE-018 | GOAL-CORE-003 | S1 | S |
| FR-CORE-019 | GOAL-CORE-003 | S1 | S |
| FR-CORE-020 | GOAL-CORE-003, GOAL-CORE-005 | S1 ; E05 | M |
| FR-CORE-021 | GOAL-CORE-004 | S1 ; R09 | S |
| FR-CORE-022 | GOAL-CORE-004 | S1 ; R09 | S |
| FR-CORE-023 | GOAL-CORE-002, GOAL-CORE-004 | S1 | M |
| FR-CORE-024 | GOAL-CORE-002, GOAL-CORE-004 | S1, S4 | M |
| FR-CORE-025 | GOAL-CORE-005 | S1 ; R08 | S |
| FR-CORE-026 | GOAL-CORE-005 | S1 ; E04 | S |
| FR-CORE-027 | GOAL-CORE-005 | S1 ; R11 | S |
| FR-CORE-028 | GOAL-CORE-004 | S4 ; R12 | C |
| FR-CORE-029 | GOAL-CORE-001 | S1 | M |
| FR-CORE-030 | GOAL-CORE-001, GOAL-CORE-005 | R10 | S |

Couverture documentaire FR→AC : **30/30, 100 %** ; FR→objectif : **30/30, 100 %**. Ces chiffres ne mesurent pas des tests exécutés.
La proportion élevée de Must protège les frontières d'identité, de données et de confidentialité ; elle n'est pas une estimation d'effort et doit être revue avant engagement.
La mémoire conserve ses sept exigences et 27 critères locaux, non additionnés aux 30 FR/90 AC du noyau.

### 10.3 Livraison et exclusions

V1 comprend MJ et joueurs, trois éditions, bibliothèque/incarnations, chroniques reconvergentes, fiches/cartes, journal, PDF/OCR, IA texte et images optionnelles, et exports téléchargeables.
La mémoire est une extension optionnelle documentée ; son intégration reste conditionnée aux preuves de S4 et E03, sans date V1 engagée.
Un socle manuel peut précéder les enrichissements pour la construction, mais ne doit pas être présenté comme toute la V1 confirmée.
Avant engagement de livraison : arbitrer les recommandations, établir les preuves techniques nécessaires et la capacité d'équipe ; aucun sprint ni échéance n'est inventé.

Sont exclus : déploiement distant des services applicatifs MerlAIn, gestion de conteneurs dans l'interface, moteur complet de règles, hiérarchie arc/scénario obligatoire,
bibliothèque protégée embarquée, redistribution illimitée de prose, contournement de DRM, transferts directs ou synchronisation bidirectionnelle VTT.
Ni génération de cartes géographiques MVP, tarification/abonnement, adaptateur Anthropic spécifique, enregistrement audio en direct, diarisation ou identification vocale automatique ne sont promis.
L'IA déclenchée par les joueurs est proposée hors V1 selon R09 ; les cycles et conversions d'édition sont proposés hors V1 selon R03/R05, sans prétendre à une décision utilisateur.
Ces reports se réexaminent en revue de périmètre ultérieure ; aucune suppression totale de fiche ou de campagne n'est implicitement autorisée.

## 11. Décisions proposées et registre de preuve / approbation

### 11.1 Défauts recommandés, non approuvés

| Réf. | RECOMMANDATION A VALIDER | Motif et conséquence |
| --- | --- | --- |
| R01 | Nom local unique après normalisation définie ; email facultatif mais unique s'il sert à se connecter ; refuser toute ambiguïté nom/email. Identité OIDC distinguée par fournisseur et sujet ; liaison exige preuve des deux comptes. Récupération par justificatif à usage unique ou procédure administrateur avec preuve vérifiée, sans SMTP obligatoire | Éviter fusion par email, usurpation et comptes irrécupérables ; amorçage du premier administrateur et preuve de récupération à détailler en E01 |
| R02 | Invitation explicite par MJ, joueur comme rôle initial sans promotion automatique ; partage de bibliothèque nominatif ; administrateur sans lecture métier globale ; retrait d'adhésion sans suppression des contenus | Limiter l'accès par défaut ; politique de transfert de propriété et retrait du dernier MJ à faire approuver |
| R03 | Graphe orienté sans cycles en V1, branches et reconvergences autorisées ; détacher un lien ne supprime pas ses scènes | Éviter boucles ambiguës de navigation ; les cycles restent une question ouverte, pas une décision confirmée |
| R04 | Joueur édite ses champs métier et équipements autorisés ; identités, rôles, provenance et champs système/techniquement dérivés protégés. MJ peut corriger avec historique. Joueur voit l'historique non secret de sa fiche | Préserver édition directe et traçabilité ; aucune suppression de fiche entière ni moteur de règles inféré |
| R05 | Saisie libre avec validation de format, sans calcul de règles obligatoire ; édition figée après création de fiches, conversion interédition reportée | Éviter les conversions destructrices ; rubriques exactes à faire relire pour chaque édition |
| R06 | Contrôle de révision explicite ; pas de fusion automatique ni de last-write-wins pour les conflits | Préserver les deux intentions sans garantie de débit ou de nombre de collaborateurs |
| R07 | Modération par masquage réversible, motif et trace ; restauration autorisée ; pas de réécriture silencieuse attribuée à l'auteur | Garder publication humaine directe ; durée d'historique et éventuelle suppression définitive restent à préciser |
| R08 | Paquet universel lisible/structuré avec manifeste comme repli ; perte connue explicitement acceptée, sinon export bloqué | Rendre tout type généré réutilisable sans fausse promesse native FoundryVTT/Roll20 |
| R09 | Pas de déclenchement IA côté joueur en V1 ; réexamen ultérieur ; enrichissement privé MJ configurable conservé | Réduire les ambiguïtés d'audience et de coût sans rendre l'IA obligatoire |
| R10 | Sauvegarde distincte des paquets VTT, restauration contrôlée sans remplacement implicite, essai de reprise avant mise en service | Préserver persistance et confidentialité ; rétention, supports et RPO/RTO non décidés |
| R11 | Désactivation sans purge, diagnostic sans secret, configuration déclarative limitée aux capacités revues | Préserver les données et rendre la composition honnête ; aucune exécution arbitraire |
| R12 | Pour la mémoire, suggérer un épisode comme ensemble nommé de matériaux sélectionnés par le MJ, pouvant couvrir plusieurs scènes, sans imposer une session entière | Proposition uniquement : la correspondance épisode/session/chronique et l'ordre d'ingestion demeurent ouverts exactement comme dans S4 |

### 11.2 Preuves et validations encore nécessaires

Tous les éléments suivants sont **non bloquants pour ce brouillon**. « Blocage » désigne la prochaine décision ou capacité concernée, pas une invitation à rouvrir le périmètre confirmé.
Les propriétaires sont des rôles proposés, sans attribution nominative ni accord présumé.

| Réf. | Question ou preuve attendue | Type de blocage | Propriétaire proposé / point de décision |
| --- | --- | --- | --- |
| E01 | Adopter ou amender R01/R02 : identité, liaison, récupération sans SMTP, amorçage admin, adhésion, transfert/dernier MJ | Approbation de politique ; conception des accès | Produit et technique avant réalisation des parcours de compte |
| E02 | Adopter ou amender R03 à R07/R09 : cycles, rubriques et champs, historique propre, concurrence, modération, IA joueur | Approbation produit ciblée | Produit avec MJ/joueurs avant validation des critères conditionnels |
| E03 | Fournir dépôt exact et révision des composants mémoire, interfaces, licences, erreurs, entrées et sources ; définir épisode et ingestion | Preuve technique d'intégration ; R12 reste ouvert | Détenteur des composants puis technique ; S4 reste l'autorité de détail |
| E04 | Établir versions FoundryVTT/Roll20, systèmes/fiches, types admis et essais d'import ; adopter le repli R08 | Preuve de compatibilité ; bloque seulement une revendication native | Technique et produit avant annonce de prise en charge native |
| E05 | Définir les règles concrètes d'usage des livres, dérivés, licences et restrictions transmissibles ; corpus de recette autorisé | Revue de droits / condition d'activation et diffusion du contenu concerné | Propriétaire des sources et conseil compétent si nécessaire ; pas de blocage catégorique de rédaction |
| E06 | Conservation par type, copies chez fournisseurs/sauvegardes, consentement et retrait audio ; politiques FR-MEM-007 | Approbation vie privée / condition de mise en service des traitements concernés | Produit, participants et responsables compétents ; aucune conformité juridique certifiée |
| E07 | Baseline latence, limites documentaires et médias, enveloppe de charge et dépendances compatibles | Preuve de performance/capacité ; pas de seuil inventé | Technique mesure, qualité reproduit, produit approuve avant engagement |
| E08 | Adopter R10/R11 ; définir supports, cadence, chiffrement des sauvegardes, restauration et RPO/RTO | Préparation opérationnelle | Responsable d'installation et technique avant mise en service |
| E09 | Établir compatibilité OpenAI textuelle, API d'image choisie, OCR/PDF ; conserver les contrats des trois séparés | Preuve technique pour le traitement choisi | Technique avant activation ; désactivation conserve le noyau manuel |
| E10 | Répartir les frontières techniques polyglottes/Dapr, transports et stockage ; justifier Builder/Strategy/Command | Conception technique future | Responsable technique ; aucun schéma ou découpage de service prescrit ici |
| E11 | [Revue indépendante P02 terminée](../../reviews/2026-09-28/merlain-prd-v0.1-review.md) : PASS, aucun constat matériel ; décisions humaines sur les recommandations et désignation des approbateurs toujours en attente | Approbation du PRD / sortie formelle Validate puis Finalize | Produit, technique et qualité ; aucune approbation acquise |

## 12. Contraintes, hypothèses et risques

### 12.1 Contraintes imposées

| ID | Contrainte et source imposante | Frontière / impact |
| --- | --- | --- |
| CON-CORE-001 | S1 : services applicatifs MerlAIn exclusivement locaux sous Compose ; dépendances distantes configurées permises | Technique/exploitation ; aucun hébergement applicatif distant ajouté au périmètre |
| CON-CORE-002 | S1 : composition frontend/serveur et cible polyglotte Dapr ; aucun état actuel Dapr présumé | Technique ; dossier de conception requis, scaffold non pris pour preuve de cible |
| CON-CORE-003 | S1 : noyau manuel, validation MJ des diffusions IA, absence d'écrasement/suppression destructeur automatique | Produit/sûreté des contenus ; invariant de tous les enrichissements |
| CON-CORE-004 | S1 : imports personnels privés avec validation humaine, pas de bibliothèque protégée embarquée ni contournement DRM | Contenu/droits ; modalités d'application E05, aucune licence inférée de la possession |
| CON-CORE-005 | S1 : téléchargements seuls ; FoundryVTT/Roll20 cibles non vérifiées ; configuration non exécutable | Intégration ; ni transfert direct, ni synchronisation, ni protocole inventé |
| CON-CORE-006 | S1 : Builder/Strategy/Command utilisés si pertinents et justifiés dans la conception future | Technique ; pas d'application artificielle à tous les composants |

Ces frontières sont non négociables dans ce périmètre de rédaction ; leur changement exige une nouvelle décision utilisateur.

### 12.2 Hypothèses et risques

| Sujet | Hypothèse / risque | Impact si faux ou réalisé | Réponse proposée |
| --- | --- | --- | --- |
| H01 | L'installation dispose d'un stockage persistant et d'un administrateur capable de gérer ses sauvegardes | Fort : perte ou impossibilité de reprise | NFR-CORE-003/005 et exercice R10 ; aucun matériel présumé |
| H02 | Les fournisseurs choisis possèdent des contrats compatibles, y compris OCR et image indépendants du texte | Moyen à fort : enrichissement indisponible | E09, états de panne et noyau manuel, sans substitution silencieuse |
| H03 | Des données de recette autorisées représentent les trois éditions et les restrictions de sources | Fort : validation non probante | Corpus synthétique ou autorisé, revue MJ et E05 |
| H04 | Les composants du collègue peuvent être adaptés au périmètre d'accès et de version de MerlAIn | Fort pour la mémoire seulement | S4/E03, aucune intégration ni ordre de pipeline supposé |
| Risque : fuite dérivée | Résumé, image, extrait ou provenance révèle un secret malgré un filtrage direct | Fort | Corpus avec secrets connus, contrôles d'audience, revue MJ et invalidation des dérivés |
| Risque : dérive de portée | Scaffold, ancien plan ou format VTT supposé est traité comme une capacité livrée | Fort | Statuts explicites, source S1 prioritaire et preuves E04/E10 avant revendication |
| Risque : perte silencieuse | Conflit, conversion d'édition, export ou restauration détruit une information | Fort | R05/R06/R08/R10 et critères négatifs associés |
| Risque : sortie distante | Configuration locale comprise à tort comme absence de transfert de données | Fort | Destination/contextes visibles, minimisation, droits et rétention E05/E06 |

Probabilité et effort de mitigation ne sont pas évalués faute de preuve ; ces risques ne sont pas des incidents constatés.
La validation humaine réduit des erreurs de contenu, mais ne prouve pas à elle seule l'absence de fuite, la compatibilité ou la conformité juridique.

## 13. Glossaire

| Terme | Sens dans ce PRD |
| --- | --- |
| MJ / GM | Maître du jeu ; rôle d'une campagne, distinct de l'administration de l'installation |
| PJ / PNJ | Personnage joueur / non-joueur ; une entrée de bibliothèque peut être candidate PJ avant toute attribution |
| Campagne | Contexte de règles, de narration, de membres et d'autorisations |
| Chronique | Une scène ou un événement précis ; pas un arc obligatoire ni le journal partagé |
| Reconvergence | Plusieurs branches narratives aboutissent à la même chronique |
| Bibliothèque | Entrées réutilisables indépendantes de leur placement narratif |
| Incarnation | Version indépendante d'un personnage dans une campagne, liée à sa source |
| Fiche libre | Saisie adaptée à une édition, sans promesse de moteur complet ou de validation des règles |
| Carte d'équipement | Présentation d'un élément d'équipement, complémentaire de la fiche complète |
| Carnet privé MJ | Idées et préparation non publiées ; distinct du journal partagé humain |
| Audience / projection | Destinataires autorisés et représentation du contenu adaptée à leurs droits |
| Dérivé | Contenu obtenu depuis d'autres contenus, conservant leurs restrictions pertinentes |
| OIDC | Protocole d'identité ; un fournisseur OAuth2 sans identité OIDC n'est pas réputé interchangeable |
| OCR | Reconnaissance de texte à partir de pages scannées, avec erreurs possibles et revue humaine |
| RAG / GraphRAG / Graphiti | Récupération de contexte, éventuellement structurée en graphe de connaissance ; distincte des branches narratives |
| Épisode / Storyteller | Unité de résumé encore ouverte / agent-dépôt rapporté du collègue, non identifié techniquement |
| Transcript | Texte dérivé d'un audio ; ni preuve automatique de locuteur ni enregistrement source |
| Export / sauvegarde | Représentation pour réutilisation / copie avec périmètre de restauration ; ne sont pas synonymes |
| Adaptateur / configuration | Capacité de transformation revue / paramètres dans les limites de cette capacité |
| Désactivé / indisponible | Fonction non sélectionnée / fonction sélectionnée mais non utilisable ; aucun de ces états ne signifie données supprimées |

## 14. État de revue et métadonnées

Le livrable contient **30 FR-CORE, 90 AC-CORE, 15 NFR-CORE, 6 contraintes et 5 objectifs**, avec cinq parcours et onze entrées de preuve/approbation encore ouvertes.
La rédaction de P01 est complète ; la [revue indépendante P02 du présent noyau](../../reviews/2026-09-28/merlain-prd-v0.1-review.md) est terminée : **PASS — SPEC DRAFT READY**, aucun constat matériel. Les cases historiques du plan ne sont pas modifiées ici.
L'intake Ready-With-Gaps n'est pas la revue de ce nouveau document et n'autorise aucune sortie formelle de Validate/Finalize.
Le résultat P02 rend le brouillon prêt pour revue humaine, sans approbation produit ou technique ; les sorties Validate/Finalize restent à `false`. E11 reste ouvert pour les décisions humaines et la désignation des approbateurs.
La [revue mémoire existante](../../reviews/2026-09-28/merlain-session-memory-review.md) et ses corrections v0.1.1 demeurent propres à S4.
Les recommandations R01 à R12 et les cibles de qualité ne sont pas devenues des décisions utilisateur par leur rédaction.

| Approbation attendue | État |
| --- | --- |
| Produit : périmètre, parcours, recommandations et priorités | Non accordée ; approbateur à désigner |
| Technique : faisabilité, intégrations et capacité | Non accordée ; preuves ciblées E03/E04/E07/E09/E10 |
| Qualité : critères et corpus de recette | [Revue indépendante P02 terminée](../../reviews/2026-09-28/merlain-prd-v0.1-review.md) : PASS, aucun constat matériel ; aucune approbation humaine acquise |
| Droits/vie privée : traitements et diffusions concernés | Revue ciblée E05/E06 ; aucune certification |
| Dérogations / handoff backlog / engagement de livraison | Aucune dérogation accordée ; non produits et non autorisés par ce livrable |

**Avertissement :** ce document assisté par IA propose des exigences et critères à examiner par des responsables produit et techniques compétents.
Il ne constitue ni approbation produit, ni validation de faisabilité, ni engagement d'ingénierie ou conseil juridique.
Aucun test d'application ou d'intégration, déploiement ou prise en charge native VTT n'est revendiqué.
Structure adaptée de `requirements-author/templates/prd/prd-full.md`, Microsoft HVE-Core, CC BY 4.0 ; rubriques enrichies pour la demande MerlAIn.
Le [compagnon de cycle de vie](merlain-prd-v0.1.state.json) décrit uniquement l'état documentaire ; aucun fichier de gouvernance squad n'est modifié par cette rédaction.

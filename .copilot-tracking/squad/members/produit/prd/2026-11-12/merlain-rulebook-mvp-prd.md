---
prd_id: merlain-rulebook-mvp
title: "MerlAIn - MVP livre de regles : consulter et creer"
status: draft
version: 0.2.1
created_date: 2026-11-12
last_updated: 2026-11-12
authoring_status: complete
review_status: pending
approval_status: not-approved
delivery_priorities: [P1, P2]
product_goal_ids: [GOAL-MVP-001, GOAL-MVP-002]
product_goal_smart_status: deferred
fr_to_ac_coverage_threshold_pct: 100.0
fr_to_goal_coverage_threshold_pct: 100.0
diagram_format: none
source_brd_id: null
requirement_id_prefixes:
  fr: FR-MVP
  ac: AC-MVP
  nfr: NFR-MVP
  con: CON-MVP
  goal: GOAL-MVP
---

# MerlAIn - MVP livre de regles : consulter et creer

## 1. Resume et autorite

**Deux priorites de livraison, et seulement deux :**

| Priorite | Resultat utilisateur | Definition de reussite |
| --- | --- | --- |
| P1 - Comprendre son livre | Importer un PDF, verifier son Markdown et discuter avec son contenu via un RAG local et Microsoft Agent Framework/MCP | Le MJ obtient des reponses sourcables dans l'edition selectionnee, ou un constat explicite de preuve insuffisante |
| P2 - Preparer des personnages et creatures | Generer un personnage, PNJ ou monstre avec statistiques, backstory et image, en s'appuyant sur ce meme livre | Le MJ conserve un brouillon editable, distingue les regles sourcees des inventions et obtient une image effectivement produite |

P1 fournit le corpus de P2. L'ingestion, l'interface minimale, la persistance et les
protections ne sont pas quatre priorites supplementaires : ce sont des dependances des
deux resultats. Une belle interface sans ces resultats ne termine pas le MVP.

**Premier jalon de developpement : agent et DevUI.** La direction precisee ensuite
le 12 novembre decoupe P1 : commencer par Agent Framework, Responses, MCP, un
retrieval local et la memoire de conversation dans PostgreSQL ; utiliser DevUI avant
de creer une application. Un corpus Markdown synthetique autorise suffit pour ce
premier jalon. L'ingestion PDF et la recette complete P1 suivent ; la generation P2
et l'interface applicative restent les deuxieme etape de produit, non abandonnees.
Le jalon technique ne doit pas etre presente comme la livraison complete de P1/P2.

Ce PRD devient **le point d'entree et l'autorite de priorisation courante**. Il remplace
pour ce MVP produit le parcours manuel de campagne recommande en septembre,
pas toute la vision long terme. Les identifiants du
[PRD large de septembre](../2026-09-28/merlain-prd-v0.1.md) restent stables pour reference.
Le [module memoire](../2026-09-28/merlain-session-memory-prd.md) est reporte apres le MVP.
Les anciens Must/Should et verdicts de revue ne s'appliquent pas a cette nouvelle revision.

Source produit : demande directe et extrait de conversation fournis le 12 novembre 2026.
L'objectif est de preparer une premiere campagne a partir du livre evoque comme
« livre de regles du hobbit ». **Titre commercial, jeu, edition, langue et PDF exacts
ne sont pas identifies** : ne pas assimiler automatiquement ce livre a The One Ring 2e.
La mention d'un ete n'est pas une date contractuelle ou un engagement de livraison.
La recherche documentaire de section 11 justifie des candidats, pas une integration testee.

## 2. Acteurs, objectifs et frontiere

Le premier utilisateur est **un MJ preparant sa partie**, dans un espace prive.
L'operateur configure les dependances ; aucun portail d'administration n'est necessaire.
Un joueur n'est pas un acteur de recette de ce MVP. Cela reporte les parcours
multi-utilisateurs, sans autoriser l'exposition publique de livres ou brouillons.

| Objectif | Mesure de recette proposee | Horizon |
| --- | --- | --- |
| GOAL-MVP-001 | Pour chaque question du corpus de recette, produire une reponse dont les affirmations de regles sont appuyees par les passages cites, ou signaler l'absence de preuve | Candidate de livraison P1 |
| GOAL-MVP-002 | Produire et rouvrir au moins un personnage, un PNJ et un monstre avec backstory, statistiques qualifiees, references et image ; conserver les corrections du MJ | Candidate de livraison P2 |

Ce sont des objectifs de recette, pas des taux de qualite mesures. Date, materiel,
latence et budget d'inference ne sont pas connus ; l'evaluation SMART reste differee.

### Ce qui est reporte, et non supprime de la vision

Campagnes completes et graphe de chroniques, journal partage, joueurs et fiches
collaboratives, incarnations intercampagnes, OIDC/gestion complete de comptes,
exports natifs Roll20/FoundryVTT, audio/Whisper, Graphiti, Storyteller, resumes de
campagne et deepwiki interactif ne sont **pas des conditions de livraison P1/P2**.
Pas de generation d'histoire comme troisieme chantier, de carte geographique,
de moteur de regles complet ou de conversion automatique entre editions.
OCR d'une page scannee fait partie du chemin PDF si le livre choisi le necessite,
pas un projet documentaire multiformat independant.

## 3. Decisions et contraintes

| ID | Decision / source | Frontiere |
| --- | --- | --- |
| CON-MVP-001 | Deux priorites P1/P2 ; premier jalon Agent Framework/DevUI avant application | Cette modification livre des specs ; la premiere implementation est un jalon de P1, pas la recette complete P1/P2 |
| CON-MVP-002 | Demande actuelle : Microsoft Agent Framework pilote les interactions IA et appelle la recuperation par MCP | Aucun abonnement a un service d'agent heberge obligatoire |
| CON-MVP-003 | Demande actuelle : RAG local, logiciel de recuperation open source et executable sous Linux/conteneur | Parseur, Markdown, index et recuperation sur l'installation locale ; pas de service RAG cloud requis |
| CON-MVP-004 | Contrainte de septembre conservee : application locale Docker Compose | Compatibilite Linux requise ; une execution native sur hote Linux reste une option de developpement a confirmer, pas un nouveau deploiement distant |
| CON-MVP-005 | Demande actuelle : PDF vers Markdown, decoupage aux titres H2/H3 | Les details de section 5 sont la proposition testable ; aucun decoupage arbitraire par defaut |
| CON-MVP-006 | Agent Framework utilise l'API Responses d'un endpoint Microsoft Foundry via identite applicative ; images via contrat distinct | Valider deploiement, API, tool calling et authentification ; pas d'integration Anthropic specifique |

**MCP remplace Dapr comme besoin immediat d'integration d'outils ; Dapr n'est pas
un prerequis de ce MVP.** C'est une interpretation de perimetre retenue pour cette
redaction a partir du « exit Dapr ... pour le moment », pas une incompatibilite technique :
MCP et Dapr ont des roles differents et peuvent coexister plus tard. Ni sidecars,
ni bus distribue, ni decoupage polyglotte exhaustif ne doivent bloquer P1/P2.

Les appels LLM et image peuvent employer des endpoints locaux ou distants explicitement
configures selon la contrainte anterieure. Le profil de reference recommande conserve
aussi les embeddings localement ; tout envoi de contenu a un fournisseur distant doit
etre visible pour l'operateur. « RAG local » ne signifie pas automatiquement « aucune
sortie de donnees » si l'operateur configure de l'inference distante.

Le premier profil utilise un endpoint Foundry distant : contenu du chat et passages
retrouves peuvent lui etre envoyes, avec ses propres regles de conservation. Aucune
ressource agent n'est hebergee sur Foundry ; seule l'inference est distante. Tenant,
client ID, client secret, endpoint et deployment sont fournis hors code et logs.
L'authentification Entra du client vers Foundry n'est pas une authentification des
utilisateurs. L'expression utilisateur « Responses V2 » designe ici le client/API
Responses vise ; la version exacte, l'endpoint et le support du deploiement doivent
etre confirmes, sans inventer une version d'API.

Builder, Strategy et Command restent pertinents s'ils simplifient respectivement la
construction de demandes, les adaptateurs et les operations ingestion/generation.
Aucun pattern ni moteur de plugins generaliste n'est exige sans besoin concret.

## 4. Parcours de bout en bout

### P1 - Importer puis interroger

Le MJ importe son PDF et renseigne titre, jeu, edition et langue. Il voit les etats
de conversion/OCR, relit la structure Markdown et verifie les pages et tableaux.
Il corrige ou approuve les titres H2/H3 puis active une revision indexee. Dans le
chat, il selectionne explicitement le livre et pose une question ou une relance.
L'agent cherche, peut reformuler et consulter plusieurs sections, puis repond avec
liens vers les passages et pages. Une absence de preuve ne devient pas une regle inventee.

### P2 - Creer puis conserver

Le MJ choisit personnage, PNJ ou monstre, le livre/edition et une intention creative.
Le generateur recupere les regles pertinentes et propose nom, concept, backstory et
statistiques structurees, avec provenance et zones a completer. Le MJ peut corriger,
sauvegarder puis demander une illustration via un prompt derive de cette version.
Texte et image ont des etats distincts ; une panne image ne detruit pas le texte.
L'ensemble n'est declare complet qu'avec une image disponible et des statistiques
revues, ou reste explicitement incomplet. Regenerer ne remplace jamais silencieusement
les corrections humaines.

## 5. Contrat PDF, Markdown et sections

Un PDF n'a pas necessairement des balises H2/H3. Le parseur reconstruit sa hierarchie
visuelle ; la structure normalisee Markdown, relue par le MJ, fait foi pour le decoupage.
Le systeme conserve PDF original, sortie structuree du parseur si disponible, Markdown
normalise et version de la politique. Le Markdown seul ne garantit pas la provenance page.

**Politique de section proposee pour appliquer l'exigence H2/H3 :**

1. H1 donne le contexte de chapitre. Un H2 sans H3 forme une section jusqu'au prochain
   H1/H2. S'il contient des H3, chaque H3 forme une section jusqu'au prochain H1/H2/H3.
   Une fin de document ferme la derniere section ; une page nouvelle ne la ferme pas.
2. Le texte entre le H2 et son premier H3 est une section d'introduction H2 distincte,
   si non vide. Les H4 et niveaux suivants restent dans leur section H3/H2 parente.
3. Ni taille cible, ni changement de page, ni chevauchement automatique ne fusionnent
   deux sections H2/H3 differentes. Les titres parents sont des metadonnees de contexte,
   pas une duplication du corps de toutes les sections enfants.
4. Textes avant le premier H2/H3 ou chapitres sans ces titres : signaler une zone non
   classee. Le MJ lui attribue une section explicite avant activation ; ne pas perdre
   cette prose ou inventer silencieusement des titres detectes.
5. Si une section depasse la limite du modele d'embedding ou de contexte choisi, bloquer
   son activation avec diagnostic. Le MJ peut affiner les H2/H3 dans le Markdown puis
   relancer. **Pas de troncature ni de sous-chunks arbitraires caches**. Une autre politique
   parent/enfant exigerait une decision ulterieure explicite.
6. Tableaux, listes et blocs de statistiques gardent leur association a une seule
   section. Un tableau coupe par le parseur, des colonnes melangees ou un titre incertain
   demandent revue, pas une indexation silencieusement valide.

Chaque section porte au minimum : identifiant document et revision, jeu/edition/langue,
identifiant section unique, chemin H1/H2/H3, corps Markdown, pages PDF 1-based couvertes,
et etiquettes de pages imprimees si connues. Deux titres identiques sur des pages
distinctes gardent des identifiants distincts. La citation ouvre la bonne revision et
le passage exact, pas seulement la couverture du livre.

Le choix du corpus est applique par le service de retrieval a partir du contexte
autorise, pas seulement par les consignes du modele. Un appel MCP ne peut pas elargir
cette portee en changeant un filtre. Les historiques de chat et les generations
anterieures ne sont pas automatiquement reindexes comme preuves officielles du livre.

Les limites de fichier/pages/tokens sont des parametres verifies avant traitement.
La revision n'est activable que lorsque toutes ses sections sont valides ; une
reindexation echouee ne remplace pas la derniere revision utilisable.

## 6. Exigences fonctionnelles

Toutes les exigences ci-dessous sont **Must pour leurs priorites respectives**.
Elles decrivent douze moyens de realiser les deux resultats, pas douze priorites.
Acteur des operations : MJ ; configuration : operateur local.

### FR-MVP-001 - Importer un livre identifie (P1)

Quand le MJ ajoute un PDF, le systeme doit creer un document prive identifie par
contenu et metadonnees de jeu/edition, sans supposer le titre du livre.

- **AC-MVP-001** : Quand un PDF autorise est importe, le systeme conserve l'original et affiche titre, jeu, edition et langue pour confirmation.
- **AC-MVP-002** : Si le fichier est illisible, chiffre sans acces autorise ou au-dela des limites configurees, le systeme indique la cause et ne le declare pas indexe.
- **AC-MVP-003** : Quand le meme fichier est soumis deux fois, le systeme signale le doublon et ne cree pas deux revisions actives implicites.

### FR-MVP-002 - Convertir et relire le Markdown (P1)

Quand un PDF accepte est converti, le systeme doit fournir une previsualisation
Markdown avec provenance des pages et avertissements de structure/OCR.

- **AC-MVP-004** : Quand la conversion reussit, le MJ peut comparer titres, paragraphes et tableaux du Markdown avec les pages PDF correspondantes.
- **AC-MVP-005** : Si une page necessite l'OCR et que celui-ci est indisponible ou echoue, le systeme indique la page et bloque l'activation de cette revision.
- **AC-MVP-006** : Quand le MJ corrige la structure et l'approuve, le systeme conserve sa version et la decision ; aucune correction ne modifie le PDF original.

### FR-MVP-003 - Respecter les sections H2/H3 (P1)

Quand le Markdown est approuve, le systeme doit produire les sections selon le
contrat de section 5, sans melange entre deux titres.

- **AC-MVP-007** : Quand un corpus contient un H2 sans H3, un H2 avec introduction et deux H3, et un H4, les sections suivent exactement la politique 1/2 de section 5.
- **AC-MVP-008** : Si des titres manquent, se repetent ou sont incertains, le systeme conserve des identifiants uniques et exige le classement des zones ambigues avant activation.
- **AC-MVP-009** : Si une section depasse une limite de modele, le systeme la signale sans tronquer/fusionner ; une nouvelle structure approuvee est necessaire avant activation.

### FR-MVP-004 - Activer un index versionne (P1)

Quand toutes les sections sont valides et indexees, le systeme doit activer une
revision coherente du corpus pour les recherches.

- **AC-MVP-010** : Quand une recherche aboutit, chaque resultat contient document/revision, jeu/edition, section, chemin de titres, extrait et pages.
- **AC-MVP-011** : Si conversion, embeddings ou indexation d'une nouvelle revision echouent, l'ancienne revision reste selectionnable et la nouvelle est marquee en echec.
- **AC-MVP-012** : Quand un livre est retire du corpus actif ou remplace, les nouvelles recherches ne recuperent plus l'ancienne revision ; les anciennes citations restent marquees historiques ou indisponibles.

### FR-MVP-005 - Converser avec des preuves (P1)

Quand le MJ interroge le livre selectionne, le systeme doit repondre en distinguant
regles appuyees, interpretation et information absente.

- **AC-MVP-013** : Quand une reponse affirme une regle, elle lie un passage du livre/edition selectionne qui soutient cette affirmation et ouvre sa page/section.
- **AC-MVP-014** : Si aucune preuve suffisante n'est trouvee, la reponse dit ce qui manque ; elle ne presente pas les connaissances generales du modele comme une regle de ce livre.
- **AC-MVP-015** : Quand le MJ change de livre/edition pendant un chat, la selection est visible et la reponse suivante ne reutilise pas les anciennes preuves comme si elles provenaient du nouveau corpus.

### FR-MVP-006 - Piloter la recuperation par Agent Framework/MCP (P1)

Quand une question exige des preuves, l'agent Agent Framework doit appeler des outils
MCP de recherche/lecture de sections dans les limites configurees.

- **AC-MVP-016** : Quand une question relie deux sections, une trace montre les appels MCP et les references recuperees ; l'agent peut reformuler sa recherche sans exporter son raisonnement interne.
- **AC-MVP-017** : Si la limite d'appels, de temps ou de contexte est atteinte ou si MCP est indisponible, l'agent s'arrete avec une limite/erreur explicite et ne simule pas une recherche reussie.
- **AC-MVP-018** : Quand un passage du PDF demande d'ignorer les consignes, d'appeler un outil de mutation ou de changer de corpus, il reste une donnee non fiable et ne modifie ni permissions ni outils autorises.

### FR-MVP-007 - Generer les trois types d'entites (P2)

Quand le MJ selectionne personnage, PNJ ou monstre et une intention, le systeme doit
produire un brouillon structure fonde sur le corpus choisi.

- **AC-MVP-019** : Quand chaque type est demande, le brouillon affiche type, nom, concept, backstory, bloc de statistiques et references du livre/edition.
- **AC-MVP-020** : Quand une backstory invente lieux, liens ou evenements, ces elements sont marques comme creation et ne sont pas presentes comme canon du livre.
- **AC-MVP-021** : Si le corpus requis n'est pas actif, la generation fondee sur ce livre est bloquee avec une action de correction, sans bascule silencieuse vers un autre livre.

### FR-MVP-008 - Qualifier les statistiques (P2)

Quand le systeme genere des statistiques, il doit les presenter selon un schema
explicite du jeu/edition selectionne avec statut de provenance de chaque champ.

- **AC-MVP-022** : Quand un champ reprend une regle, utilise un calcul ou un choix de creation, il est marque source/calcul/proposition et indique les references et entrees necessaires a sa verification.
- **AC-MVP-023** : Si une valeur ou regle est inconnue, contradictoire ou hors du schema, le champ est signale a completer/corriger ; le systeme n'invente pas une statistique pretendument officielle.
- **AC-MVP-024** : Quand le MJ relit et corrige une fiche, la version conserve sa validation humaine et ne revendique pas une certification par un moteur complet de regles.

### FR-MVP-009 - Produire une image liee au brouillon (P2)

Quand le MJ demande une illustration, le systeme doit appeler le fournisseur image
configure a partir d'une version identifiee du brouillon.

- **AC-MVP-025** : Quand l'appel reussit, une image consultable est conservee avec identifiant, prompt, fournisseur/modele et version de texte source ; un prompt seul ne vaut pas image.
- **AC-MVP-026** : Si l'image echoue ou est annulee, le texte reste sauvegarde, l'etat image est explicite et le MJ peut relancer sans regenerer sa backstory.
- **AC-MVP-027** : Quand le texte change apres generation, l'image conserve son lien a l'ancienne version et l'interface propose une regeneration sans remplacement automatique.

### FR-MVP-010 - Offrir une interface minimale (P1 et P2)

Quand le MJ ouvre l'application, il doit pouvoir accomplir import, chat et generation
depuis une interface web simple, sans portail de campagne complet.

- **AC-MVP-028** : Quand le MJ suit le parcours, il retrouve un espace Livres (import/structure/etat), un Chat (corpus/sources) et un Generateur (type/brouillon/image).
- **AC-MVP-029** : Quand une operation progresse, echoue ou est annulee, son etat est affichable et annonce sans succes factice ; les contenus existants restent consultables.
- **AC-MVP-030** : Quand le MJ utilise uniquement le clavier, il peut parcourir ces espaces, lire les sources et corriger le brouillon sans piege de focus.

### FR-MVP-011 - Conserver le travail (P1 et P2)

Quand le MJ enregistre un travail, le systeme doit conserver le corpus et les
brouillons, conversations et images avec leurs liens de version.

- **AC-MVP-031** : Quand l'installation redemarre apres enregistrement, livres actifs, messages sauvegardes, brouillons et images restent consultables.
- **AC-MVP-032** : Quand le MJ regenere ou reimporte, les corrections sauvegardees ne sont pas ecrasees sans action explicite ; les anciennes versions restent identifiables.
- **AC-MVP-033** : Si un fournisseur IA est desactive ou indisponible, les contenus enregistres restent lisibles et les brouillons editables, meme si de nouvelles reponses/generations sont impossibles.

### FR-MVP-012 - Configurer des modules limites et locaux (P1 et P2)

Quand l'operateur configure les capacites, le systeme doit activer les adaptateurs
declaratifs livres/retrieval, agent texte et generation sans execution arbitraire.

- **AC-MVP-034** : Quand un profil local documente est lance, les deux parcours fonctionnent avec parseur, index et recuperation locaux sans Dapr ou service agent heberge obligatoire.
- **AC-MVP-035** : Si le modele texte ne supporte pas les outils ou que l'API image manque, le diagnostic distingue ces capacites ; un endpoint OpenAI-compatible ne les valide pas implicitement.
- **AC-MVP-036** : Quand un module est desactive, son etat et ses dependances sont visibles sans effacer ses donnees ; seuls les serveurs MCP et adaptateurs explicitement configures sont joignables.

## 7. Exigences de qualite et protections

Ces invariants sont des conditions P1/P2, pas des chantiers produit autonomes.

| ID | Categorie | Exigence et verification proposee |
| --- | --- | --- |
| NFR-MVP-001 | Confidentialite | Profil initial mono-utilisateur sans authentification applicative, strictement limite a une machine locale de confiance ; aucun acces externe UI/API/MCP/medias par defaut. Pas d'isolation entre utilisateurs locaux |
| NFR-MVP-002 | Securite | Aucun serveur d'index/MCP/parseur admin ouvert publiquement par defaut ; outils de lecture seuls cote agent, secrets hors navigateur/logs, limites d'import et timeouts explicites |
| NFR-MVP-003 | Qualite de preuve | Le corpus de recette couvre question exacte, paraphrase, deux sections, absent, edition contradictoire et injection ; chaque regle affirmee est verifiee contre sa citation par revue humaine |
| NFR-MVP-004 | Integrite documentaire | Recette H2/H3 comprenant intro, H4, titres dupliques, pages multiples, tableau et section trop longue ; zero melange intersection et aucune perte silencieuse |
| NFR-MVP-005 | Fiabilite | Un echec parser/index/LLM/image ne detruit pas la derniere version sauvegardee ; redemarrage et panne partielle reproduits sur donnees de recette |
| NFR-MVP-006 | Vie privee et droits | Pas de livre protege embarque, de contournement DRM ni de diffusion publique automatique ; extraits distants identifies par configuration, pas de PDF complet envoye par defaut a l'agent |
| NFR-MVP-007 | Observabilite | Suivre operation, etapes, revision source, latence, appels MCP et erreurs sans secrets, PDF integral ni raisonnement prive dans les traces |
| NFR-MVP-008 | Usabilite/accessibilite | Interface FR utilisable au clavier, noms/labels explicites, etats annonces, contraste WCAG 2.2 AA et reflow a 320 px ; controle des trois espaces, pas du futur portail |
| NFR-MVP-009 | Compatibilite/portabilite | Recette sur installation Linux/conteneurs locaux ; versions parser/retriever/modeles et licences code/poids identifiees, schemas MCP negocies et prouves compatibles |
| NFR-MVP-010 | Performance/capacite | Mesurer sur le PDF et l'hote choisis conversion, indexation, reponse et image ; afficher progression et appliquer limites ; aucune promesse de latence ou GPU avant cette mesure |

Pour le premier usage, publier DevUI uniquement sur l'interface loopback de l'hote,
sans ports PostgreSQL, recherche ou MCP accessibles depuis l'exterieur par defaut.
Ne pas confondre bind interne du conteneur et publication hote : verifier l'acces
effectif depuis une autre machine. Aucun compte ou middleware d'authentification
MerlAIn n'est requis pour ce profil. DevUI peut conserver son authentification native
si necessaire ; la desactivation n'est acceptable qu'en environnement loopback isole.
Une exposition reseau ou multi-utilisateur exige une protection adaptee avant ouverture.
Les composants
internes ne doivent pas etre exposes au navigateur avec une cle administrateur.
Les restrictions des sources se propagent aux brouillons ; aucune licence d'utilisation
du livre ou du modele image n'est deduite de la seule possession du PDF.

## 8. Tracabilite et recette de livraison

| FR | AC | Objectif | Lien au perimetre anterieur |
| --- | --- | --- | --- |
| FR-MVP-001 | AC-MVP-001, AC-MVP-002, AC-MVP-003 | GOAL-MVP-001 | FR-CORE-018/020 |
| FR-MVP-002 | AC-MVP-004, AC-MVP-005, AC-MVP-006 | GOAL-MVP-001 | FR-CORE-018/019 adaptes au document |
| FR-MVP-003 | AC-MVP-007, AC-MVP-008, AC-MVP-009 | GOAL-MVP-001 | Nouvelle exigence H2/H3 du 12 novembre |
| FR-MVP-004 | AC-MVP-010, AC-MVP-011, AC-MVP-012 | GOAL-MVP-001 | FR-CORE-024/029 |
| FR-MVP-005 | AC-MVP-013, AC-MVP-014, AC-MVP-015 | GOAL-MVP-001 | FR-CORE-021/024 specialises en chat |
| FR-MVP-006 | AC-MVP-016, AC-MVP-017, AC-MVP-018 | GOAL-MVP-001 | Nouvelle contrainte Agent Framework/MCP |
| FR-MVP-007 | AC-MVP-019, AC-MVP-020, AC-MVP-021 | GOAL-MVP-002 | FR-CORE-009/021 limites aux brouillons |
| FR-MVP-008 | AC-MVP-022, AC-MVP-023, AC-MVP-024 | GOAL-MVP-002 | FR-CORE-011/019/020 sans moteur de regles |
| FR-MVP-009 | AC-MVP-025, AC-MVP-026, AC-MVP-027 | GOAL-MVP-002 | FR-CORE-022 |
| FR-MVP-010 | AC-MVP-028, AC-MVP-029, AC-MVP-030 | GOAL-MVP-001, GOAL-MVP-002 | Interface reduite, pas de portail complet |
| FR-MVP-011 | AC-MVP-031, AC-MVP-032, AC-MVP-033 | GOAL-MVP-001, GOAL-MVP-002 | FR-CORE-029/030 limites a la conservation |
| FR-MVP-012 | AC-MVP-034, AC-MVP-035, AC-MVP-036 | GOAL-MVP-001, GOAL-MVP-002 | FR-CORE-001 ; integration revisee sans Dapr requis |

Couverture documentaire : 12/12 exigences ont des criteres et objectifs.
Les 36 AC sont a executer, pas reussis. Recette sur petit corpus synthetique ou
autorise comportant les cas structurels et questions de NFR-MVP-003/004, puis
verification privee sur le livre reel autorise. Le jeu/edition et un schema statistique
minimal pour les trois types sont fixes avant de pretendre terminer P2.

P1 est livre quand son import/chat et ses invariants partages passent. P2 est livre
quand les trois types sont sauvegardables avec une image reelle et leurs statistiques
revues. Une image en erreur, des champs inconnus ou une citation non probante restent
un resultat incomplet, meme si le texte est utile. Aucune affirmation d'exactitude
generale du modele ne remplace cette recette.

### Premier jalon de developpement : criteres bornes

- Depuis DevUI local, lancer un dialogue multi-tour avec Agent Framework et le
  deploiement Foundry configure via l'API Responses ; exposer explicitement les
  erreurs de credentials, de permissions, de connectivite et d'incompatibilite.
- L'agent appelle un outil MCP de recherche local en lecture seule sur un petit
  corpus Markdown synthetique, puis fournit une reponse avec une reference de section
  ou indique explicitement l'absence de preuve. Un MCP indisponible ne doit pas etre
  presente comme une recherche reussie.
- Persister messages et etat necessaire a la reprise d'une conversation dans un
  PostgreSQL Compose avec volume ; redemarrer les services, reprendre le meme fil et
  verifier la continuite et la separation de deux fils. Verifier que la variante de
  session effectivement choisie conserve bien l'historique requis ; un identifiant
  de conversation distant seul ne prouve pas une memoire locale.
- Publier DevUI seulement sur loopback hote, garder services internes non publics,
  garder secrets hors depot, navigateur et logs. Aucun middleware d'identite
  applicative n'est requis dans ce jalon local de confiance.
- Ce jalon ne valide pas l'import PDF, les citations avec pages, la generation P2
  ni l'interface applicative ; leurs AC restent ouverts jusqu'a leur recette.

## 9. Construction recommandee, sans implementation

Une seule orchestration conversationnelle : **Microsoft Agent Framework**. Il appelle
la recherche/lecture locale par MCP ; la conversion et l'indexation sont declenchees
explicitement par le MJ, pas par un agent disposant des droits d'administration.
L'agent peut chercher, lire une section entiere puis reformuler avec un budget borne.
Il n'est pas necessaire d'empiler un deuxieme agent autonome dans le backend RAG.

**Premier profil candidat :** Agent Framework + DevUI, PostgreSQL local persistant,
MCP de lecture et Meilisearch auto-heberge. Le moteur fournit recherche
lexicale/semantique/hybride ;
sa route MCP native documentee est experimentale. Verifier version/protocole et
perimetre de licence avant selection. Un adaptateur MCP local borne reste possible
si la route native ne satisfait pas le contrat, mais n'est pas promis comme deja livre.
Ne pas activer un second chat gere par Meilisearch a la place de l'agent demande.

**Conversion PDF suivante :** Docling est une option open source et locale pour
PDF/OCR/Markdown et provenance, preferable pour conserver le PDF sur l'hote.
Un adaptateur H2/H3 est necessaire : ses chunkers par defaut ne garantissent pas
ce contrat. Verifier OCR, tableaux, pages et licences des poids sur le vrai livre.
Document Intelligence reste une alternative cloud si l'operateur choisit explicitement
la transmission du PDF et ses implications de confidentialite et de cout ; ce n'est
pas une dependance du profil local.

Haystack/Hayhooks avec Qdrant reste une alternative si Meilisearch ne passe pas les
tests de retrieval/MCP/licences ; choisir une seule pile, pas construire les deux.
Verifier localement les versions et l'interoperabilite ; aucun composant n'est
encore valide comme integration. PostgreSQL stocke l'etat conversationnel, tandis
que l'index RAG est distinct ; il ne remplace pas automatiquement le stockage
de l'historique de l'agent.

## 10. Decision register et evolution

| Sujet | Etat / action avant la capacite concernee |
| --- | --- |
| Deux priorites, PDF/Markdown H2/H3, Agent Framework/MCP | Direction utilisateur confirmee ; detail de recette propose dans ce document |
| Livre/jeu/edition/langue et droits | Fournir le PDF autorise et ces metadonnees avant recette reelle ; ne bloque pas la redaction |
| H2/H3 et limites | Politique stricte de section 5 proposee ; verifier titres/pages/tableaux sur le livre choisi et taille des sections |
| Premier jalon et retrieval | DevUI, Agent Framework/Responses, PostgreSQL Compose et Meilisearch candidat ; fallback Haystack/Qdrant si necessaire |
| PDF | Docling local recommande a evaluer ; Document Intelligence cloud uniquement sur choix explicite |
| Identite | Aucune authentification utilisateur dans le profil local ; credentials Entra client-secret pour Foundry, et controles reseau loopback |
| Modeles et outils | Prouver tool calling sur endpoint texte compatible OpenAI, embeddings locaux et generation d'image ; choisir le schema de statistiques de l'edition |
| Acces et stockage | Fixer acces prive, destinations des extraits, volumes, retention et sauvegarde avant ouverture aux donnees reelles |
| Revue | Nouvelle revue produit/technique a faire ; aucun PASS de septembre ne valide ce perimetre ou sa faisabilite |
| Backlog existant | Issues #3-#11 non modifiees par cette tache ; leur ordre campagne-first n'est plus l'ordre de livraison courant |

Le futur lien imagine dans la conversation est conserve comme **vision differee** :
le livre fonde les regles et l'univers de reference ; l'audio d'episodes termines
alimente une memoire de campagne ; une future vue deepwiki relie ces sources.
Les faits inventes/joues ne deviennent pas des regles officielles, ni l'inverse.
Prevoir des identites de source et revisions distinctes suffit aujourd'hui :
aucun Graphiti, ingestion audio, wiki, synchronisation entre graphes ou integration
du module du collegue n'est requis pour terminer les deux priorites.

## 11. Sources techniques verifiees le 12 novembre 2026

Lecture de documentation officielle uniquement, aucun paquet installe ni service lance.
Les pages evoluent ; leurs capacites ne prouvent pas la compatibilite d'une pile assemblee.

| Source | Fait utile et limite |
| --- | --- |
| [Agent Framework - MCP](https://learn.microsoft.com/en-us/agent-framework/agents/tools/local-mcp-tools) | Integration d'outils MCP ; les exemples cloud ne rendent pas Azure obligatoire dans notre cible locale |
| [Agent Framework - DevUI](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/ui/devui/) | Outil d'essai non destine a la production ; les comportements d'auth dependent de la variante ; verifier l'exposition effective |
| [Agent Framework - sessions](https://learn.microsoft.com/en-us/agent-framework/concepts/agents/conversations/) | Sessions serialisables ; une reference distante ne prouve pas une persistance complete de l'historique en PostgreSQL |
| [Foundry - Responses](https://learn.microsoft.com/en-us/azure/foundry/openai/how-to/responses) | API Responses, exemples Entra et compatibilite dependant du modele/region ; valider sur le deploiement reel et verifier sa politique de conservation |
| [Docling - README](https://github.com/docling-project/docling/blob/main/README.md) | PDF, OCR, Markdown, structure et execution locale/Linux ; code MIT, licences des modeles a examiner separement |
| [Docling - chunking](https://docling-project.github.io/docling/concepts/chunking/) | Post-traitement Markdown personnalise possible ; HybridChunker peut couper/fusionner et HierarchicalChunker suit les elements, donc pas de garantie H2/H3 sans politique specifique |
| [Haystack - depot](https://github.com/deepset-ai/haystack) et [agents](https://docs.haystack.deepset.ai/docs/agents) | Framework open source de pipelines/RAG/outils ; sa boucle agent n'est pas necessaire a notre orchestration |
| [Hayhooks](https://docs.haystack.deepset.ai/docs/hayhooks) | Pipelines exposes via HTTP/MCP, Streamable HTTP recommande ; ne prouve pas automatiquement contrat section ou compatibilite Agent Framework |
| [Qdrant local](https://qdrant.tech/documentation/quickstart/) | Execution Docker avec persistance ; configuration par defaut sans authentification a ne pas exposer publiquement |
| [Meilisearch hybride](https://www.meilisearch.com/docs/capabilities/hybrid_search/overview.md) et [agentic search](https://www.meilisearch.com/docs/capabilities/agentic_search/overview.md) | Retrieval lexical/semantique, embeddings locaux ou fournis ; l'agent externe peut piloter la recherche |
| [Meilisearch MCP](https://www.meilisearch.com/docs/getting_started/integrations/mcp.md) | Route native experimentale ; documentation consultee : v1.54+, protocole MCP 2026-07-28, outils de recherche et permissions d'index, pas d'ingestion via ces outils |
| [Meilisearch LICENSE](https://github.com/meilisearch/meilisearch/blob/main/LICENSE) | Depot mixte MIT / BUSL-1.1 ; selectionner et verifier les fonctions de l'edition open source, ne pas qualifier tout le produit de MIT |

## 12. Etat documentaire

**Brouillon v0.2.1 redige, non approuve, revue independante en attente.** Douze FR,
36 AC, dix NFR, six contraintes et deux objectifs de livraison. Les choix proposes
restent identifiables ; ce document n'annonce ni integration, ni test produit reussi,
ni date de campagne ou engagement d'ingenierie.

Le [compagnon de cycle de vie](merlain-rulebook-mvp-prd.state.json) conserve ce statut.
Le PRD large et le module memoire restent disponibles comme references differees,
sans nouvelle priorite implicite. Structure adaptee du modele requirements-author
(Microsoft HVE-Core, CC BY 4.0). Document prepare avec assistance IA, a relire par
les responsables produit et techniques ; aucune certification juridique n'est donnee.

# 🤖 AI Assistant - Context Memo for Priority Matrix Manager

> **NOTE IMPORTANTE:** Ce document est destiné à GitHub Copilot et aux assistants IA pour comprendre rapidement le contexte du projet à chaque nouvelle session de conversation.

---

## 🎯 Résumé du projet en 30 secondes

**Priority Matrix Manager** est une **API RESTful ASP.NET Core 8** pour gérer des tâches organisées selon la **matrice d'Eisenhower** (Important/Urgent).

- **Architecture:** Clean Architecture (N-Tier) avec 9 projets
- **Stack:** .NET 8, EF Core 8.0.11, SQL Server, JWT Auth
- **Pattern:** Repository + Unit of Work + Service Layer
- **Frontend:** React (http://localhost:3000)
- **Auth:** JWT Bearer + Refresh Tokens + ASP.NET Identity
- **Status:** ✅ Production Ready, migré de .NET 6 → .NET 8

---

## 📦 Structure des projets (9 projets)

```
1. Priority.Matrix.Manager (Web API Host)
   ├─ Program.cs, appsettings.json, launchSettings.json
   ├─ Extensions/, Migrations/, MappingProfile
   └─ Dépend de: TOUS les autres projets

2. Priority.Matrix.Manager.Presentation (Controllers)
   ├─ Controllers: Category, TaskPriority, Authentication, Token, User
   ├─ ActionFilters: ValidationFilter, ValidateMediaType
   └─ Dépend de: Service.Contract

3. Service (Business Logic)
   ├─ ServiceManager, CategoryService, TaskPriorityService
   ├─ AuthenticationService, UserService
   └─ Dépend de: Service.Contract, Contracts, Shared

4. Service.Contract (Service Interfaces)
   └─ IServiceManager, ICategoryService, ITaskPriorityService, etc.

5. Repository (Data Access)
   ├─ RepositoryManager, RepositoryBase<T>
   ├─ CategoryRepository, TaskPriorityRepository
   ├─ RepositoryContext (DbContext)
   └─ Dépend de: Contracts, Entities

6. Contracts (Repository Interfaces)
   └─ IRepositoryManager, ICategoryRepository, etc.

7. Entities (Domain Models)
   ├─ Models: Category, TaskPriority, User
   ├─ Exceptions: Custom exceptions (NotFound, BadRequest)
   └─ ConfigurationModels: JwtConfiguration

8. Shared (DTOs & Request Features)
   ├─ DTOs: Category, TaskPriority, User, Token
   └─ RequestFeatures: Pagination, Filtering

9. LoggerService (Logging)
   └─ NLog implementation
```

---

## 🗄️ Modèles de données essentiels

### Category
```csharp
Id (PK), CategoryName, CategoryCode, TaskPriorities[]
```

### TaskPriority
```csharp
Id (PK), TaskTitle, TaskDescription, TaskCreatedBy, CreatedDate,
TaskToSee, Hour, TaskStatus, PosX, PosY, ZIndex,
CategoryID (FK → Category), UserId (FK → User, nullable)
```

### User (extends IdentityUser)
```csharp
Id→UserId (PK), UserName, Email, FirstName, LastName,
RefreshToken, RefreshTokenExpiryTime, TaskPriorities[]
```

**Relations:**
- Category 1 → N TaskPriority
- User 1 → N TaskPriority (optional)

---

## 🌐 Endpoints principaux (tous nécessitent JWT sauf auth)

```
POST   /api/authentication/login              # Login
POST   /api/token/refresh                     # Refresh JWT

GET    /api/categories                        # Liste catégories
POST   /api/categories                        # Créer catégorie
GET    /api/categories/{id}                   # Détail catégorie
PUT    /api/categories/{id}                   # Modifier catégorie
DELETE /api/categories/{id}                   # Supprimer catégorie

GET    /api/category/{catId}/tasks            # Liste tâches (paginée)
POST   /api/category/{catId}/tasks            # Créer tâche
GET    /api/category/{catId}/tasks/{id}       # Détail tâche
PUT    /api/category/{catId}/tasks/{id}       # Modifier tâche
PATCH  /api/category/{catId}/tasks/{id}       # Modifier partiellement
DELETE /api/category/{catId}/tasks/{id}       # Supprimer tâche

GET    /api/users                             # Liste utilisateurs
GET    /api/users/{id}                        # Détail utilisateur
```

---

## 🔐 Configuration JWT (CRITIQUE)

### appsettings.json
```json
"JwtSettings": {
  "validIssuer": "PriorityMatrixManager",
  "validAudience": "https://localhost:7236",
  "expires": 180  // minutes
}
```

### launchSettings.json (Variable d'environnement)
```json
"SECRET": "gsijM0NZ1fD5VT60wusyp7W9CM3vY2X21JPLddEY7sSx9Dx/xz+VrZH8BUbQcmfEhYPvbQfuo4+q9axSnVRidw=="
```
⚠️ **Longueur minimum:** 256 bits (32 caractères) pour HS256  
⚠️ **JAMAIS commiter le SECRET dans le code source**

### Algorithme
- **HS256** (HMAC-SHA256)
- Clé actuelle: 512 bits (88 chars base64)
- Refresh Token: 7 jours (configuré dans AuthenticationService)

---

## 🗃️ Base de données

### Connection String
```
server=PC-LV14-AURELIE\SQLEXPRESS;
database=PriorityMatrixManager;
Integrated Security=true
```

### Tables principales
- `Categories` (4 catégories Eisenhower)
- `TaskPriorities` (tâches avec position matrice)
- `AspNetUsers` (Identity, colonne Id → UserId)
- `AspNetRoles` (Administrator, Manager)
- `AspNetUserRoles` (Many-to-Many)

### Migrations
13 migrations appliquées (de 2023-06 à 2023-09)  
Dernière: `AddNewPropertiesCatForUpdateDTO`

---

## 🔄 Flux typique de requête

```
Request → [Middleware] → Controller → Service → Repository → DB
                              ↓          ↓         ↓
                            DTO  ←  AutoMapper  ← Entity
```

**Exemple concret:**
1. `POST /api/category/1/tasks` + JWT
2. Middleware valide JWT → `ClaimsPrincipal`
3. `TaskpriorityController.CreateTaskPriorityForCategory()`
4. `ValidationFilterAttribute` valide le DTO
5. `TaskPriorityService.CreateTaskPriorityForCategoryAsync()`
   - Valide que la catégorie existe
   - Mappe DTO → Entity
6. `TaskPriorityRepository.CreateTaskPriorityForCategory()`
7. `RepositoryManager.SaveAsync()` → EF Core SaveChanges
8. AutoMapper: Entity → DTO
9. Response: `201 Created` + Location header + DTO body

---

## 🎭 Patterns utilisés

- ✅ **Repository Pattern** (abstraction data access)
- ✅ **Unit of Work** (SaveAsync centralisé)
- ✅ **Service Layer** (business logic)
- ✅ **Dependency Injection** (constructor injection)
- ✅ **DTO Pattern** (séparation modèles)
- ✅ **Manager Pattern** (RepositoryManager, ServiceManager)
- ✅ **Lazy Initialization** (services/repos à la demande)
- ✅ **Extension Methods** (configuration modulaire)
- ✅ **Filter Attributes** (validation automatique)

---

## ⚡ Points d'attention pour l'IA

### Lors de modifications du code

1. **Respecter l'architecture en couches**
   - Presentation → Service → Repository → Database
   - Jamais de DbContext direct dans controllers
   - Toujours passer par les interfaces

2. **Utiliser les patterns existants**
   - Lazy<T> pour ServiceManager et RepositoryManager
   - Async/await pour toutes les opérations I/O
   - DTOs pour toutes les communications API

3. **Validation**
   - Data Annotations sur les entités
   - ValidationFilterAttribute sur les actions
   - Business validation dans les services

4. **Exceptions**
   - Hériter de `NotFoundException` ou `BadRequestException`
   - Throw dans services, catch dans middleware global

5. **AutoMapper**
   - Configurer dans `MappingProfile.cs`
   - Utiliser dans les services, jamais dans repositories

6. **Entity Framework**
   - `trackChanges: false` pour lectures (AsNoTracking)
   - `trackChanges: true` pour modifications
   - Toujours await `SaveAsync()` après Create/Update/Delete

### Lors d'ajout de fonctionnalités

**Checklist standard:**
1. Créer/modifier entité dans `Entities/Models/`
2. Créer DTOs dans `Shared/DataTransferObjects/`
3. Configurer mapping dans `MappingProfile.cs`
4. Créer interface repository dans `Contracts/`
5. Implémenter repository dans `Repository/`
6. Ajouter au `IRepositoryManager` et `RepositoryManager`
7. Créer interface service dans `Service.Contract/`
8. Implémenter service dans `Service/`
9. Ajouter au `IServiceManager` et `ServiceManager`
10. Créer controller dans `Presentation/Controllers/`
11. Ajouter `[Authorize]` si nécessaire
12. Créer migration EF si modèle changé
13. Tester via Swagger

---

## 🚨 Erreurs fréquentes et solutions

| Erreur | Cause | Solution immédiate |
|--------|-------|-------------------|
| **IDX10720** | Clé JWT < 256 bits | Vérifier SECRET dans launchSettings.json |
| **401 Unauthorized** | JWT invalide/expiré | Utiliser `/api/token/refresh` |
| **CategoryNotFoundException** | ID n'existe pas | Vérifier ID existe dans DB |
| **NU1102** | Package version inexistante | Vérifier version sur nuget.org |
| **Migration error** | SQL Server down | Démarrer SQL Server |
| **403 Forbidden Git** | Wrong GitHub account | Vérifier credentials, utiliser PAT |

---

## 🔍 Commandes de diagnostic rapides

```bash
# Statut solution
dotnet --version                          # Version SDK
dotnet build                              # Compiler
dotnet list package --vulnerable          # Vulnérabilités
dotnet list package --outdated            # Obsolètes

# Entity Framework
dotnet ef migrations list --project Priority.Matrix.Manager
dotnet ef database update --project Priority.Matrix.Manager

# Git
git status                                # État
git log --oneline -5                      # Derniers commits
git branch                                # Branches locales
```

---

## 📊 Métriques du projet

- **Lignes de code:** ~5,000+ (estimation)
- **Projets:** 9
- **Contrôleurs:** 6 (Category, TaskPriority, Authentication, Token, User, Taskall)
- **Services:** 4 (Category, TaskPriority, Authentication, User)
- **Repositories:** 2 (Category, TaskPriority)
- **Entités:** 3 (Category, TaskPriority, User)
- **DTOs:** ~15
- **Migrations:** 13
- **Packages NuGet:** ~12 principaux

---

## 🎯 Contexte métier (La matrice d'Eisenhower)

Le projet implémente la **matrice d'Eisenhower** pour la gestion des priorités :

```
           │ Non Urgent │  Urgent  │
───────────┼────────────┼──────────┤
Important  │    INU     │    IU    │
           │ Planifier  │  Faire   │
───────────┼────────────┼──────────┤
Non Imp.   │   NUNI     │   UNI    │
           │  Éliminer  │ Déléguer │
```

**4 catégories prédéfinies en DB:**
1. **IU** - Important et Urgent → À faire immédiatement
2. **INU** - Important Non-Urgent → À planifier
3. **UNI** - Urgent Non-Important → À déléguer
4. **NUNI** - Ni Urgent Ni Important → À éliminer

Chaque `TaskPriority` a:
- `CategoryID` (1, 2, 3, ou 4)
- `PosX`, `PosY` : Position exacte dans le quadrant
- `ZIndex` : Ordre d'affichage (superposition)

---

## 💡 Conventions de code du projet

### Style
- **PascalCase:** Classes, méthodes, propriétés
- **camelCase:** Variables locales, paramètres
- **_camelCase:** Private fields (avec underscore)
- **IPascalCase:** Interfaces (préfixe I)

### Async
- Toutes opérations I/O sont async
- Suffixe `Async` sur les méthodes
- Always `await` (pas de `.Result` ou `.Wait()`)

### DTOs
- **Records** pour lecture (immutables)
- **Classes** pour création/mise à jour
- Suffixes: `Dto`, `ForCreationDto`, `ForUpdateDto`, `ForManipulationDto`

### Dependency Injection
- Constructor injection uniquement
- Interfaces injectées (jamais d'implémentations concrètes)
- Lifetimes: Singleton (Logger), Scoped (Services/Repos/DbContext)

### Entity Framework
- `trackChanges: false` pour lectures (performance)
- `trackChanges: true` pour modifications (change tracking)
- ConfigureAwait non utilisé (contexte ASP.NET Core)

---

## 🔑 Informations sensibles (Development)

### SECRET JWT (launchSettings.json)
```
gsijM0NZ1fD5VT60wusyp7W9CM3vY2X21JPLddEY7sSx9Dx/xz+VrZH8BUbQcmfEhYPvbQfuo4+q9axSnVRidw==
```
- **Longueur:** 88 caractères (512 bits en base64)
- **Format:** Base64
- **Algorithme:** HS256
- ⚠️ Unique pour dev, JAMAIS en production dans le code

### Connection String
```
server=PC-LV14-AURELIE\SQLEXPRESS;database=PriorityMatrixManager;Integrated Security=true
```

### URLs
- API: https://localhost:7236
- Swagger: https://localhost:7236/swagger
- Frontend: http://localhost:3000

---

## 🧠 Connaissances métier importantes

### Catégories Eisenhower
Les 4 catégories sont **fixes et prédéfinies** en base (seed data). Ne PAS permettre la modification/suppression de ces catégories par l'utilisateur.

### TaskPriority
- `TaskCreatedBy` : ID de l'utilisateur créateur (int, pas UserId qui est string)
- `UserId` : Assignation optionnelle à un User Identity (string, nullable)
- Les deux champs sont distincts et ont des usages différents

### Rôles Identity
- **Administrator** : Accès complet
- **Manager** : Gestion des tâches

Actuellement, l'autorisation par rôle n'est pas finement implémentée (tous les endpoints nécessitent juste `[Authorize]`).

---

## 🔄 Dépendances entre projets (Graph)

```
Entities (aucune dépendance)
  ↑
  ├─ Shared
  ├─ Contracts
  │   ↑
  │   ├─ Service.Contract
  │   ├─ Repository
  │   │   ↑
  │   │   └─ Service
  │   └─ LoggerService
  │       ↑
  │       └─ Service
  │           ↑
  │           └─ Presentation
  │               ↑
  │               └─ Priority.Matrix.Manager (Host)
```

---

## 📝 Si l'utilisateur demande...

### "Ajouter une nouvelle entité/ressource"
1. Suivre la checklist complète (voir PROJECT_ARCHITECTURE.md section "Checklist de développement")
2. Créer dans cet ordre: Entity → DTO → Repository → Service → Controller
3. Ne pas oublier: AutoMapper, Migration EF, Tests via Swagger

### "Problème d'authentification 401"
1. Vérifier JWT non expiré (expires: 180 min)
2. Proposer `/api/token/refresh` avec refresh token
3. Vérifier format header: `Authorization: Bearer {token}`

### "Problème de base de données"
1. Vérifier SQL Server actif
2. Vérifier connection string dans appsettings.json
3. Proposer `dotnet ef database update --project Priority.Matrix.Manager`

### "Mettre à jour les packages"
1. `dotnet list package --outdated` pour voir obsolètes
2. Mettre à jour en respectant .NET 8 compatibility
3. `dotnet list package --vulnerable` pour vérifier sécurité
4. `dotnet restore` puis `dotnet build` pour valider

### "Problème de migration"
- Toujours spécifier `--project Priority.Matrix.Manager`
- C'est le seul projet avec DbContext et Migrations/

### "Ajouter un endpoint"
- Controller dans Presentation/Controllers/
- Logique dans Service approprié
- Ajouter `[Authorize]` sauf si endpoint public
- Ajouter XML comments `/// <summary>` pour Swagger

---

## 🛠️ Outils et commandes fréquents

### Build & Run
```bash
dotnet restore
dotnet build
dotnet run --project Priority.Matrix.Manager
```

### EF Core Migrations
```bash
dotnet ef migrations add MigrationName --project Priority.Matrix.Manager
dotnet ef database update --project Priority.Matrix.Manager
dotnet ef migrations remove --project Priority.Matrix.Manager
dotnet ef migrations list --project Priority.Matrix.Manager
```

### Git
```bash
git status
git add .
git commit -m "message"
git push origin branch-name
```

### Packages
```bash
dotnet list package --vulnerable
dotnet list package --outdated
dotnet restore
```

---

## 🎨 Conventions de nommage spécifiques

### Controllers
- Suffixe: `Controller`
- Route: Pluriel (ex: `/api/categories`)
- Actions: Pas de préfixe "Get", "Post" dans le nom

### Services
- Suffixe: `Service`
- Interface: `I` + nom (ex: `ICategoryService`)
- Méthodes: Suffixe `Async`

### Repositories
- Suffixe: `Repository`
- Interface: `I` + nom (ex: `ICategoryRepository`)
- Méthodes: Suffixe `Async` pour async ops

### DTOs
- Suffixe: `Dto`, `ForCreationDto`, `ForUpdateDto`, `ForManipulationDto`
- Records pour lecture, classes pour mutation

---

## 🔒 Sécurité - Points de vigilance

### ✅ Déjà sécurisé
- JWT tokens avec clé 512 bits
- Passwords hashés (Identity)
- SQL Injection protection (EF parameterized queries)
- HTTPS redirection
- CORS whitelist configurée
- Input validation (ModelState)
- Global exception handling

### ⚠️ À ne jamais faire
- ❌ Commiter le SECRET dans Git
- ❌ Logger les passwords
- ❌ Désactiver `[Authorize]` en production
- ❌ Utiliser `trackChanges: true` pour lectures (performance)
- ❌ Retourner des entités directement (toujours DTOs)
- ❌ Catcher Exception sans log

---

## 📚 Documentation disponible

Pour l'utilisateur humain, diriger vers:
1. **[PROJECT_ARCHITECTURE.md](PROJECT_ARCHITECTURE.md)** → Architecture complète, patterns, structure
2. **[QUICK_REFERENCE.md](QUICK_REFERENCE.md)** → Aide-mémoire, commandes, snippets
3. **[DIAGRAMS_AND_FLOWS.md](DIAGRAMS_AND_FLOWS.md)** → Diagrammes, flux de données, visualisations

---

## 🎯 Contexte actuel (dernière session)

### Modifications récentes
- ✅ Migration .NET 6 → .NET 8 (tous les projets)
- ✅ Mise à jour packages vers versions 8.0.11
- ✅ Correction vulnérabilité JWT (clé 192→512 bits)
- ✅ Correction vulnérabilité System.IdentityModel.Tokens.Jwt
- ✅ Push réussi vers GitHub (branch: hotfix/Fix-vulnerability-and-Update-Package)

### État actuel
- Branch: `hotfix/Fix-vulnerability-and-Update-Package`
- Status: ✅ Build successful, aucune vulnérabilité
- Remote: https://github.com/Aurelien-Randriananaty/Priority.Matrix.Manager.git
- Base: `staging`

### Problèmes résolus dans cette session
1. ❌→✅ Vulnérabilité System.IdentityModel.Tokens.Jwt
2. ❌→✅ Packages obsolètes
3. ❌→✅ Erreur JWT IDX10720 (clé trop courte)
4. ❌→✅ Problème push Git 403 (mauvais compte)
5. ❌→✅ Migration .NET 6 → .NET 8

---

## 🧩 Quick Snippets pour l'IA

### Créer une migration
```bash
dotnet ef migrations add MigrationName --project Priority.Matrix.Manager
```

### Appliquer migrations
```bash
dotnet ef database update --project Priority.Matrix.Manager
```

### Vérifier build
```bash
dotnet build
```

### Nouveau service (template)
```csharp
// Interface dans Service.Contract/
public interface IMyService
{
    Task<MyDto> GetAsync(int id);
}

// Implémentation dans Service/
public class MyService : IMyService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public MyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<MyDto> GetAsync(int id)
    {
        var entity = await _repository.MyRepository.GetByIdAsync(id, trackChanges: false);
        if (entity == null)
            throw new MyNotFoundException(id);
        
        var dto = _mapper.Map<MyDto>(entity);
        return dto;
    }
}

// Ajouter dans ServiceManager
private readonly Lazy<IMyService> _myService;
_myService = new Lazy<IMyService>(() => new MyService(repositoryManager, logger, mapper));
public IMyService MyService => _myService.Value;
```

---

## 🎓 Niveau d'expertise attendu

L'utilisateur semble avoir:
- ✅ Bonne connaissance de .NET/C#
- ✅ Compréhension de l'architecture en couches
- ✅ Familiarité avec Entity Framework
- ✅ Connaissance de Git
- ⚠️ Peut avoir besoin d'aide sur: EF migrations, JWT, Git authentication

---

## 🌍 Environnements

### Development (actuel)
- Machine: PC-LV14-AURELIE
- SQL: .\SQLEXPRESS
- Port: 7236 (HTTPS), 5082 (HTTP)
- SECRET dans launchSettings.json

### Staging (branche)
- À définir

### Production
- SECRET à configurer en variable système ou Azure Key Vault
- Connection string production à configurer
- Frontend URL à mettre à jour dans CORS

---

## 🔗 Liens et ressources

- **GitHub:** https://github.com/Aurelien-Randriananaty/Priority.Matrix.Manager
- **Swagger Local:** https://localhost:7236/swagger
- **Frontend:** http://localhost:3000
- **SQL Server:** PC-LV14-AURELIE\SQLEXPRESS
- **Database:** PriorityMatrixManager

---

## 📞 Contact

**Développeur:** Aurélien Randriananaty  
**Email:** randriananaty@gmail.com  
**GitHub:** [@Aurelien-Randriananaty](https://github.com/Aurelien-Randriananaty)

---

## 🤖 Instructions pour l'IA

**Lors d'une nouvelle conversation:**
1. Lire ce document en premier (AI_CONTEXT_MEMO.md)
2. Référencer PROJECT_ARCHITECTURE.md pour détails architecture
3. Utiliser QUICK_REFERENCE.md pour commandes/snippets
4. Consulter DIAGRAMS_AND_FLOWS.md pour visualisations

**Lors de suggestions de code:**
- Respecter les patterns existants (Lazy, Manager, DI)
- Suivre la structure en couches strictement
- Toujours async/await pour I/O
- Utiliser les interfaces (jamais implémentations directes)
- Ajouter XML comments sur nouveaux endpoints
- Configurer AutoMapper pour nouveaux DTOs

**Lors de debugging:**
- Vérifier logs NLog
- Proposer commandes de diagnostic
- Référencer la section "Erreurs fréquentes"

---

**Version du document:** 1.0  
**Dernière mise à jour:** 2024  
**Correspondant à:** .NET 8.0, après migration et correction vulnérabilités

---

*Ce document est votre point d'entrée pour comprendre le projet rapidement à chaque nouvelle session. Mettez-le à jour après chaque changement architectural majeur.*

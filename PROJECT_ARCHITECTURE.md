# 📋 Priority Matrix Manager - Documentation d'Architecture

## 📌 Vue d'ensemble du projet

**Priority Matrix Manager** est une application Web API ASP.NET Core permettant de gérer des tâches organisées par matrice de priorité (similaire à la matrice d'Eisenhower). L'application offre une gestion complète des tâches avec authentification JWT et autorisation basée sur les rôles.

### 🎯 Fonctionnalités principales
- ✅ Gestion des tâches par catégories avec matrice de priorité
- ✅ Système d'authentification JWT avec Refresh Tokens
- ✅ Gestion des utilisateurs avec ASP.NET Identity
- ✅ API RESTful avec pagination, filtrage et recherche
- ✅ Support CORS pour application frontend React
- ✅ Cache HTTP avec validation
- ✅ Logging avec NLog
- ✅ Documentation Swagger/OpenAPI

---

## 🏗️ Architecture globale

### Pattern architectural : **Clean Architecture / N-Tier Architecture**

L'application suit une **architecture en couches** (Layered Architecture) avec séparation des responsabilités :

```
┌─────────────────────────────────────────────────────────────┐
│                    Presentation Layer                        │
│        (Priority.Matrix.Manager.Presentation)                │
│              Controllers + Action Filters                    │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────┴─────────────────────────────────────┐
│                     Service Layer                            │
│                    (Service)                                 │
│           Business Logic + DTOs Mapping                      │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────┴─────────────────────────────────────┐
│                  Repository Layer                            │
│                   (Repository)                               │
│       Data Access Logic + EF Core Context                    │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────┴─────────────────────────────────────┐
│                   Database Layer                             │
│              SQL Server + Entity Framework                   │
└─────────────────────────────────────────────────────────────┘

          Cross-Cutting Concerns (perpendiculaires)
    ┌──────────────────┬──────────────────┬────────────┐
    │  LoggerService   │   Contracts      │   Shared   │
    └──────────────────┴──────────────────┴────────────┘
```

---

## 📦 Structure des projets

### 1. **Priority.Matrix.Manager** (API Host) 🌐
**Type:** ASP.NET Core Web API  
**Rôle:** Point d'entrée principal de l'application

**Responsabilités:**
- Configuration de l'application (Program.cs)
- Middleware pipeline
- Dependency Injection
- Extensions de services
- Migrations Entity Framework
- Configuration JWT et CORS

**Fichiers clés:**
- `Program.cs` : Configuration et démarrage de l'application
- `appsettings.json` : Configuration (ConnectionStrings, JWT, CORS)
- `launchSettings.json` : Configuration de lancement (SECRET key)
- `Extensions/ServiceExtensions.cs` : Extensions de configuration DI
- `Extensions/ExceptionMiddlewareExtensions.cs` : Gestion globale des exceptions
- `MappingProfile.cs` : Configuration AutoMapper
- `CsvOutputFormatter.cs` : Formateur CSV personnalisé
- `ContextFactory/RepositoryContextFactory.cs` : Factory pour migrations EF
- `Migrations/` : Migrations Entity Framework

**URLs:**
- HTTPS: `https://localhost:7236`
- HTTP: `http://localhost:5082`
- Swagger: `https://localhost:7236/swagger`

---

### 2. **Priority.Matrix.Manager.Presentation** (Controllers) 🎮
**Type:** Class Library  
**Rôle:** Couche de présentation (Controllers API)

**Responsabilités:**
- Endpoints API RESTful
- Validation des requêtes
- Gestion des réponses HTTP
- Action Filters

**Controllers:**

#### 📁 **CategoryController** (`/api/categories`)
- `GET /api/categories` - Liste toutes les catégories
- `GET /api/categories/{id}` - Récupère une catégorie par ID
- `GET /api/categories/CollectionExtensions/{ids}` - Récupère plusieurs catégories
- `POST /api/categories` - Crée une nouvelle catégorie
- `PUT /api/categories/{id}` - Met à jour une catégorie
- `DELETE /api/categories/{id}` - Supprime une catégorie
- `OPTIONS /api/categories` - Options HTTP

#### 📋 **TaskpriorityController** (`/api/category/{categoryId}/tasks`)
- `GET /api/category/{categoryId}/tasks` - Liste les tâches d'une catégorie (avec pagination)
- `GET /api/category/{categoryId}/tasks/{id}` - Récupère une tâche
- `POST /api/category/{categoryId}/tasks` - Crée une nouvelle tâche
- `PUT /api/category/{categoryId}/tasks/{id}` - Met à jour une tâche
- `DELETE /api/category/{categoryId}/tasks/{id}` - Supprime une tâche
- `PATCH /api/category/{categoryId}/tasks/{id}` - Mise à jour partielle (JSON Patch)

#### 🔐 **AuthenticationController** (`/api/authentication`)
- `POST /api/authentication` - Enregistrement d'un nouvel utilisateur
- `POST /api/authentication/login` - Connexion (retourne JWT + Refresh Token)

#### 🔄 **TokenController** (`/api/token`)
- `POST /api/token/refresh` - Rafraîchir le JWT avec le Refresh Token

#### 👤 **UserController** (`/api/users`)
- `GET /api/users` - Liste tous les utilisateurs
- `GET /api/users/{id}` - Récupère un utilisateur

#### 📊 **TaskallController** (`/api/taskall`)
- `GET /api/taskall` - Récupère toutes les tâches (tous utilisateurs)

**Action Filters:**
- `ValidationFilterAttribute` : Valide le ModelState
- `ValidateMediaTypeAttribute` : Valide les media types Accept header

**Autorisation:**
- Tous les endpoints (sauf authentication) nécessitent `[Authorize]`

---

### 3. **Service** (Business Logic) 💼
**Type:** Class Library  
**Rôle:** Couche métier / logique applicative

**Services implémentés:**

#### 🏷️ **CategoryService** (`ICategoryService`)
- `GetAllCategoriesAsync()` - Récupère toutes les catégories
- `GetCategoryByIdAsync()` - Récupère une catégorie par ID
- `CreateCategoryAsync()` - Crée une catégorie
- `UpdateCategoryAsync()` - Met à jour une catégorie
- `DeleteCategoryAsync()` - Supprime une catégorie
- `GetByIdsAsync()` - Récupère plusieurs catégories par IDs

#### ✅ **TaskPriorityService** (`ITaskPriorityService`)
- `GetTaskPrioritiesAsync()` - Récupère les tâches avec pagination et filtrage
- `GetTaskPriorityAsync()` - Récupère une tâche
- `CreateTaskPriorityForCategoryAsync()` - Crée une tâche
- `UpdateTaskPriorityForCategoryAsync()` - Met à jour une tâche
- `DeleteTaskPriorityForCategoryAsync()` - Supprime une tâche
- `GetTaskPriorityForPatchAsync()` - Récupère une tâche pour PATCH

**Fonctionnalités:**
- Filtrage par heures (MinHour, MaxHour)
- Recherche par terme (SearchTerm)
- Pagination (PageNumber, PageSize)
- Validation métier

#### 🔐 **AuthenticationService** (`IAuthenticationService`)
- `RegisterUser()` - Enregistrement avec rôles
- `ValidateUser()` - Validation username/password
- `CreateToken()` - Génération JWT + Refresh Token
- `RefreshToken()` - Rafraîchissement du JWT

**Sécurité JWT:**
- Algorithme: HS256 (HMAC-SHA256)
- Clé: 512 bits (stockée dans variable d'environnement SECRET)
- Durée: 180 minutes (configurable)
- Refresh Token: 7 jours
- Claims: Username + Roles

#### 👥 **UserService** (`IUserService`)
- `GetAllUsersAsync()` - Liste tous les utilisateurs
- `GetUserAsync()` - Récupère un utilisateur par ID

**Pattern utilisé:**
- **Service Manager Pattern** : Centralise l'accès aux services
- **Lazy Loading** : Instanciation différée des services

---

### 4. **Repository** (Data Access) 🗄️
**Type:** Class Library  
**Rôle:** Couche d'accès aux données avec Entity Framework Core

**Pattern:** Repository Pattern + Unit of Work

**Repositories implémentés:**

#### 📁 **CategoryRepository** (`ICategoryRepository`)
- Hérite de `RepositoryBase<Category>`
- `GetAllCategoriesAsync()`
- `GetCategoryByIdAsync()`
- `CreateCategory()`
- `DeleteCategory()`
- `GetByIdsAsync()`

#### 📋 **TaskPriorityRepository** (`ITaskPriorityRepository`)
- Hérite de `RepositoryBase<TaskPriority>`
- `GetTaskPrioritiesAsync()` - Avec pagination et filtrage
- `GetTaskPriorityAsync()`
- `GetTaskPriorityForCategoryAsync()`
- `CreateTaskPriorityForCategory()`
- `DeleteTaskPriorityForCategory()`

**Extensions:**
- `RepositoryTaskPriorityExtensions.cs` :
  - `FilterTaskPriorities()` - Filtrage par heures
  - `Search()` - Recherche textuelle
  - `Sort()` - Tri dynamique

#### 🗃️ **RepositoryContext** (DbContext)
```csharp
public class RepositoryContext : IdentityDbContext<User>
{
    public DbSet<TaskPriority> TaskPriorities { get; set; }
    public DbSet<Category> Categories { get; set; }
}
```

**Configurations Entity Framework:**
- `CategoryConfiguration` : Configuration de l'entité Category
- `TaskPriorityConfiguration` : Configuration et données initiales
- `RoleConfiguration` : Rôles Identity (Administrator, Manager)

**Relations:**
- `Category` 1 → N `TaskPriority`
- `User` 1 → N `TaskPriority` (optionnelle)

---

### 5. **Entities** (Domain Models) 🏛️
**Type:** Class Library  
**Rôle:** Modèles de domaine et entités

#### 📊 **Modèles de données**

##### **Category**
```csharp
- Id (int, PK)
- CategoryName (string, 60 chars, required)
- CategoryCode (string, 60 chars, required)
- TaskPriorities (Collection)
```

##### **TaskPriority**
```csharp
- Id (int, PK)
- TaskTitle (string, 60 chars, required)
- TaskDescription (string, 255 chars, optional)
- TaskCreatedBy (int, required)
- TaskToSee (DateTime?, optional)
- CreatedDate (DateTime, required)
- Hour (int?, optional)
- TaskStatus (string, optional)
- CategoryID (int, FK → Category)
- UserId (string?, FK → User, optional)
- PosX, PosY (float?, optional) - Position dans la matrice
- ZIndex (int?, optional) - Ordre d'affichage
```

##### **User** (extends IdentityUser)
```csharp
- Id (string, PK, renommé → UserId)
- UserName (string, inherited)
- Email (string, inherited)
- FirstName (string)
- LastName (string)
- RefreshToken (string?, optional)
- RefreshTokenExpiryTime (DateTime)
- TaskPriorities (Collection)
```

#### ⚠️ **Exceptions personnalisées**
```
BadRequestException (abstract)
├── CollectionByIdsBadRequestException
├── IdParametersBadRequestException
├── MaxHourRangeBadRequestException
└── RefreshTokenBadRequest

NotFoundException (abstract)
├── CategoryNotFoundException
└── TaskPriorityNotFoundException
```

#### ⚙️ **Configuration Models**
- `JwtConfiguration` : Configuration JWT (ValidIssuer, ValidAudience, Expires)
- `ErrorDetails` : Modèle d'erreur standardisé

---

### 6. **Shared** (DTOs) 📦
**Type:** Class Library  
**Rôle:** Objets de transfert de données (Data Transfer Objects)

#### 📤 **DTOs**

**Category DTOs:**
- `CategoryDto` - Lecture
- `CategoryForCreationDto` - Création
- `CategoryForUpdateDto` - Mise à jour
- `CategoryForManipulationDto` - Manipulation abstraite

**TaskPriority DTOs:**
- `TaskPriorityDto` - Lecture complète (avec User et Category)
- `TaskPriorityForCreationDto` - Création
- `TaskPriorityForUpdateDto` - Mise à jour
- `TaskPriorityForManipulationDto` - Manipulation abstraite

**User DTOs:**
- `UserDto` - Lecture
- `UserIdentitiesDto` - Identités utilisateur
- `UserForRegistrationDto` - Enregistrement (avec Roles)
- `UserForAuthenticationDto` - Authentification (Username + Password)

**Auth DTOs:**
- `TokenDto` - Token JWT + Refresh Token

#### 📄 **Request Features**
- `RequestParameters` (base) : PageNumber, PageSize
- `TaskPriorityParameters` : MinHour, MaxHour, SearchTerm, pagination
- `PagedList<T>` : Liste paginée avec métadonnées
- `MetaData` : Métadonnées de pagination (TotalCount, PageSize, CurrentPage, TotalPages, HasNext, HasPrevious)

---

### 7. **Contracts** (Interfaces) 📜
**Type:** Class Library  
**Rôle:** Définition des contrats (interfaces)

**Interfaces Repositories:**
- `IRepositoryBase<T>` - CRUD générique
  - `FindAll()`, `FindByCondition()`
  - `Create()`, `Update()`, `Delete()`
- `ICategoryRepository` - Opérations spécifiques aux catégories
- `ITaskPriorityRepository` - Opérations spécifiques aux tâches
- `IRepositoryManager` - Unit of Work pattern

**Interfaces Services:**
- `ICategoryService`
- `ITaskPriorityService`
- `IAuthenticationService`
- `IUserService`
- `IServiceManager` - Accès centralisé aux services

**Autres:**
- `ILoggerManager` - Abstraction du logger

---

### 8. **Service.Contract** (Service Interfaces) 📋
**Type:** Class Library  
**Rôle:** Contrats des services métier

Contient uniquement les interfaces des services (séparation interface/implémentation).

---

### 9. **LoggerService** (Logging) 📝
**Type:** Class Library  
**Rôle:** Service de logging avec NLog

**Configuration:**
- NLog avec fichiers de configuration externes
- Niveaux: Info, Debug, Warn, Error

---

## 🔧 Technologies et packages

### Stack technique
| Technologie | Version | Usage |
|-------------|---------|-------|
| .NET | 8.0 | Framework principal |
| ASP.NET Core | 8.0.11 | Web API |
| Entity Framework Core | 8.0.11 | ORM |
| SQL Server | Express | Base de données |
| C# | 12.0 | Langage |

### 📚 Packages NuGet principaux

#### Authentification & Sécurité
- `Microsoft.AspNetCore.Authentication.JwtBearer` 8.0.11 - Authentification JWT
- `System.IdentityModel.Tokens.Jwt` 8.2.1 - Gestion des tokens JWT
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` 8.0.11 - Identity avec EF Core

#### Data Access
- `Microsoft.EntityFrameworkCore.SqlServer` 8.0.11 - Provider SQL Server
- `Microsoft.EntityFrameworkCore.Tools` 8.0.11 - Outils migrations

#### Mapping & Validation
- `AutoMapper.Extensions.Microsoft.DependencyInjection` 12.0.1 - Mapping automatique

#### Caching
- `Marvin.Cache.Headers` 7.2.0 - Cache HTTP avec validation

#### Logging
- `NLog.Extensions.Logging` 5.3.14 - Logging avec NLog

#### Documentation
- `Swashbuckle.AspNetCore` 6.8.1 - Swagger/OpenAPI

#### Configuration
- `Microsoft.Extensions.Configuration.Binder` 8.0.2 - Binding de configuration

---

## 🗂️ Modèle de données

### Relations entre entités

```
┌──────────────┐              ┌──────────────────┐              ┌──────────┐
│   Category   │              │   TaskPriority   │              │   User   │
│──────────────│              │──────────────────│              │──────────│
│ Id (PK)      │──────────────│ Id (PK)          │              │ Id (PK)  │
│ CategoryName │ 1         N  │ TaskTitle        │ N         1  │ Username │
│ CategoryCode │──────────────│ TaskDescription  │──────────────│ Email    │
└──────────────┘              │ CategoryID (FK)  │   optional   │ FirstName│
                              │ UserId (FK)      │              │ LastName │
                              │ TaskCreatedBy    │              └──────────┘
                              │ CreatedDate      │
                              │ TaskToSee        │
                              │ Hour             │
                              │ TaskStatus       │
                              │ PosX, PosY       │ ← Position matrice
                              │ ZIndex           │
                              └──────────────────┘

┌───────────────────────────────────────────────────────────────┐
│                    ASP.NET Identity Tables                    │
├───────────────────────────────────────────────────────────────┤
│ AspNetUsers, AspNetRoles, AspNetUserRoles,                    │
│ AspNetUserClaims, AspNetUserLogins, AspNetUserTokens,         │
│ AspNetRoleClaims                                              │
└───────────────────────────────────────────────────────────────┘
```

### Catégories prédéfinies (Matrice d'Eisenhower)
1. **Important et Urgent** (Code: IU) - À faire immédiatement
2. **Important mais Non-Urgent** (Code: INU) - À planifier
3. **Urgent mais Non-Important** (Code: UNI) - À déléguer
4. **Ni Urgent ni Important** (Code: NUNI) - À éliminer

### Rôles Identity prédéfinis
- **Administrator** - Accès complet
- **Manager** - Gestion des tâches

---

## 🔐 Sécurité et Authentification

### Configuration JWT

**Paramètres (`appsettings.json`):**
```json
"JwtSettings": {
  "validIssuer": "PriorityMatrixManager",
  "validAudience": "https://localhost:7236",
  "expires": 180
}
```

**Variable d'environnement:**
- `SECRET` : Clé secrète 512 bits (88 caractères base64)
- ⚠️ **NE JAMAIS** commiter la clé dans le code source

### Flow d'authentification

```
1. POST /api/authentication/login
   ↓ (Username + Password)
2. Validation (UserManager)
   ↓
3. Génération JWT + Refresh Token
   ↓ (Access Token + Refresh Token)
4. Client stocke les tokens
   ↓
5. Requêtes API avec Authorization: Bearer {token}
   ↓
6. Token expiré? → POST /api/token/refresh
   ↓ (Refresh Token)
7. Nouveau Access Token
```

### Sécurité des mots de passe (Identity)
```csharp
o.Password.RequireDigit = true
o.Password.RequireLowercase = false
o.Password.RequireUppercase = false
o.Password.RequireNonAlphanumeric = false
o.Password.RequiredLength = 10
o.User.RequireUniqueEmail = true
```

---

## 🌐 Configuration CORS

**Frontend autorisé:**
- `http://localhost:3000` - Application React locale
- `http://www.infrassurtask.front.fr`
- `http://www.infrassurtask.frontaurel`
- `https://localhost:7236`

**Headers exposés:**
- `X-Pagination` - Métadonnées de pagination

---

## 📊 Patterns de conception utilisés

### 1. **Repository Pattern**
- Abstraction de l'accès aux données
- `RepositoryBase<T>` générique
- Repositories spécifiques par entité

### 2. **Unit of Work Pattern**
- `IRepositoryManager` centralise les repositories
- `SaveAsync()` unique pour toutes les modifications

### 3. **Service Layer Pattern**
- Séparation logique métier / accès données
- `IServiceManager` centralise les services

### 4. **Dependency Injection**
- Configuration dans `ServiceExtensions.cs`
- Lifetimes: Singleton (Logger), Scoped (Services, Repositories)

### 5. **DTO Pattern**
- Séparation modèles de domaine / modèles API
- AutoMapper pour les conversions

### 6. **Manager Pattern**
- `RepositoryManager` : Gestion des repositories
- `ServiceManager` : Gestion des services

### 7. **Extension Methods Pattern**
- `ServiceExtensions` : Configuration DI modulaire
- `RepositoryTaskPriorityExtensions` : Requêtes LINQ réutilisables

### 8. **Filter Attribute Pattern**
- `ValidationFilterAttribute` : Validation automatique
- `ValidateMediaTypeAttribute` : Validation des media types

### 9. **Lazy Initialization Pattern**
- Services instanciés à la demande
- Optimisation des performances

---

## 🔄 Flux de requête typique

### Exemple: Créer une tâche

```
1. Client → POST /api/category/1/tasks
   │ Body: { "TaskTitle": "Faire X", "Hour": 2, ... }
   │ Header: Authorization: Bearer {JWT}
   ↓
2. TaskpriorityController
   │ [Authorize] vérifie le JWT
   │ [ServiceFilter(ValidationFilterAttribute)] valide le DTO
   ↓
3. IServiceManager.TaskPriorityService
   │ CreateTaskPriorityForCategoryAsync(categoryId, dto)
   │ Validation métier (catégorie existe?)
   │ AutoMapper: DTO → Entity
   ↓
4. IRepositoryManager.TaskPriority
   │ CreateTaskPriorityForCategory(entity)
   │ RepositoryContext.TaskPriorities.Add()
   ↓
5. IRepositoryManager.SaveAsync()
   │ RepositoryContext.SaveChangesAsync()
   │ SQL INSERT INTO TaskPriorities
   ↓
6. AutoMapper: Entity → DTO
   ↓
7. Controller → 201 Created
   │ Location: /api/category/1/tasks/{newId}
   │ Body: TaskPriorityDto
```

---

## 📝 Configuration de la base de données

### Connection String
```
Server: PC-LV14-AURELIE\SQLEXPRESS
Database: PriorityMatrixManager
Authentication: Windows Integrated Security
```

### Migrations Entity Framework

**Historique des migrations:**
1. `20230618084121_DatabaseCreated` - Création initiale
2. `20230618090436_fixNullableProperty` - Correction nullable
3. `20230618093522_InitialData` - Données initiales
4. `20230618102357_InitialDataFixDate` - Correction dates
5. `20230619194752_FixLabelTaskTitreToTaskTitle` - Renommage colonne
6. `20230623081829_CreatingIdentityTables` - Tables Identity
7. `20230623082537_AddedRolesToDb` - Ajout rôles
8. `20230623123557_AdditionalUserFiledsForRefreshToken` - Refresh tokens
9. `20230717143356_UserAndTaskPriorityRelationShip` - Relation User-Task
10. `20230717143645_IdentityUserChangeColumnNameIdToUserId` - Renommage UserId
11. `20230810110329_FixUserIdStringToInt` - Type UserId
12. `20230903162731_PropertiesPositionAndZindexInTaskPriority` - Position/ZIndex
13. `20230904104435_AddNewPropertiesCatForUpdateDTO` - Propriétés catégorie

**Commandes EF Core:**
```bash
# Créer une migration
dotnet ef migrations add NomDeLaMigration --project Priority.Matrix.Manager

# Appliquer les migrations
dotnet ef database update --project Priority.Matrix.Manager

# Supprimer la dernière migration
dotnet ef migrations remove --project Priority.Matrix.Manager
```

---

## 🎨 Fonctionnalités avancées

### 1. Pagination
```csharp
[FromQuery] TaskPriorityParameters
{
    PageNumber = 1,
    PageSize = 10,
    MinHour = 0,
    MaxHour = int.MaxValue,
    SearchTerm = ""
}
```

**Response Headers:**
```json
X-Pagination: {
  "TotalCount": 50,
  "PageSize": 10,
  "CurrentPage": 1,
  "TotalPages": 5,
  "HasNext": true,
  "HasPrevious": false
}
```

### 2. Filtrage et recherche
- **Par heures** : `MinHour` / `MaxHour`
- **Par texte** : `SearchTerm` (recherche dans TaskTitle)

### 3. Formats de réponse supportés
- ✅ JSON (application/json)
- ✅ XML (application/xml)
- ✅ CSV (text/csv) - via CsvOutputFormatter
- ✅ HATEOAS (application/vnd.infrassur-prioritymanager.hateoas+json)

### 4. Cache HTTP
```csharp
[ResponseCache(CacheProfileName = "120SecondsDuration")]
```
- MaxAge: 120 secondes
- CacheLocation: Private
- MustRevalidate: true

### 5. JSON Patch
Support du PATCH pour mises à jour partielles :
```json
PATCH /api/category/1/tasks/5
[
  { "op": "replace", "path": "/TaskTitle", "value": "Nouveau titre" }
]
```

---

## 📁 Structure des dossiers

```
Priority.Matrix.Manager/
├── Priority.Matrix.Manager/           # API Host
│   ├── Extensions/                    # Extensions de configuration
│   ├── Migrations/                    # Migrations EF Core
│   ├── ContextFactory/               # Factory pour migrations
│   ├── Properties/
│   │   └── launchSettings.json       # Variables d'environnement
│   ├── Program.cs                     # Point d'entrée
│   ├── appsettings.json              # Configuration
│   ├── MappingProfile.cs             # AutoMapper config
│   ├── CsvOutputFormatter.cs         # Formateur CSV
│   └── nlog.config                   # Configuration NLog
│
├── Priority.Matrix.Manager.Presentation/  # Controllers
│   ├── Controllers/
│   │   ├── CategoryController.cs
│   │   ├── TaskpriorityController.cs
│   │   ├── AuthenticationController.cs
│   │   ├── TokenController.cs
│   │   ├── UserController.cs
│   │   └── TaskallController.cs
│   └── ActionFilters/
│       ├── ValidationFilterAttribute.cs
│       └── ValidateMediaTypeAttribute.cs
│
├── Service/                           # Business Logic
│   ├── ServiceManager.cs
│   ├── CategoryService.cs
│   ├── TaskPriorityService.cs
│   ├── AuthenticationService.cs
│   └── UserService.cs
│
├── Service.Contract/                  # Service Interfaces
│   ├── IServiceManager.cs
│   ├── ICategoryService.cs
│   ├── ITaskPriorityService.cs
│   ├── IAuthenticationService.cs
│   └── IUserService.cs
│
├── Repository/                        # Data Access
│   ├── RepositoryManager.cs
│   ├── RepositoryBase.cs
│   ├── CategoryRepository.cs
│   ├── TaskPriorityRepository.cs
│   ├── RepositoryContext.cs
│   ├── Configuration/                 # EF Configurations
│   │   ├── CategoryConfiguration.cs
│   │   ├── TaskPriorityConfiguration.cs
│   │   └── RoleConfiguration.cs
│   └── Extensions/
│       └── RepositoryTaskPriorityExtensions.cs
│
├── Contracts/                         # Interfaces
│   ├── IRepositoryManager.cs
│   ├── IRepositoryBase.cs
│   ├── ICategoryRepository.cs
│   ├── ITaskPriorityRepository.cs
│   └── ILoggerManager.cs
│
├── Entities/                          # Domain Models
│   ├── Models/
│   │   ├── Category.cs
│   │   ├── TaskPriority.cs
│   │   └── User.cs
│   ├── Exceptions/                    # Exceptions métier
│   │   ├── BadRequestException.cs
│   │   ├── NotFoundException.cs
│   │   ├── CategoryNotFoundException.cs
│   │   ├── TaskPriorityNotFoundException.cs
│   │   ├── CollectionByIdsBadRequestException.cs
│   │   ├── IdParametersBadRequestException.cs
│   │   ├── MaxHourRangeBadRequestException.cs
│   │   └── RefreshTokenBadRequest.cs
│   ├── ConfigurationModels/
│   │   └── JwtConfiguration.cs
│   └── ErrorModel/
│       └── ErrorDetails.cs
│
├── Shared/                            # DTOs & Request Features
│   ├── DataTransferObjects/
│   │   ├── CategoryDto.cs
│   │   ├── CategoryForCreationDto.cs
│   │   ├── CategoryForUpdateDto.cs
│   │   ├── TaskPriorityDto.cs
│   │   ├── TaskPriorityForCreationDto.cs
│   │   ├── TaskPriorityForUpdateDto.cs
│   │   ├── UserDto.cs
│   │   ├── UserIdentitiesDto.cs
│   │   ├── UserForRegistrationDto.cs
│   │   ├── UserForAuthenticationDto.cs
│   │   └── TokenDto.cs
│   └── RequestFeatures/
│       ├── RequestParameters.cs
│       ├── TaskPriorityParameters.cs
│       ├── PagedList.cs
│       └── MetaData.cs
│
└── LoggerService/                     # Logging
    └── LoggerManager.cs
```

---

## 🚀 Démarrage et développement

### Prérequis
- ✅ .NET 8 SDK installé
- ✅ SQL Server Express
- ✅ Visual Studio 2022 / VS Code
- ✅ Git

### Configuration initiale

1. **Cloner le repository**
```bash
git clone https://github.com/Aurelien-Randriananaty/Priority.Matrix.Manager.git
cd Priority.Matrix.Manager
```

2. **Restaurer les packages**
```bash
dotnet restore
```

3. **Configurer la base de données**
```bash
# Mettre à jour appsettings.json avec votre connection string
# Appliquer les migrations
dotnet ef database update --project Priority.Matrix.Manager
```

4. **Configurer la clé secrète JWT**
```bash
# Dans launchSettings.json, la clé SECRET est déjà configurée
# Pour production, configurer la variable d'environnement système
```

5. **Lancer l'application**
```bash
dotnet run --project Priority.Matrix.Manager
# Ou F5 dans Visual Studio
```

6. **Accéder à Swagger**
```
https://localhost:7236/swagger
```

### Branches Git
- `main` - Production stable
- `staging` - Pré-production / tests
- `hotfix/*` - Correctifs urgents
- `feature/*` - Nouvelles fonctionnalités

---

## 🧪 Tests et validation

### Vérifier les vulnérabilités
```bash
dotnet list package --vulnerable
```

### Vérifier les packages obsolètes
```bash
dotnet list package --outdated
```

### Build
```bash
dotnet build
```

### Exécuter les tests
```bash
dotnet test
```

---

## 📋 Checklist de développement

### Ajouter une nouvelle entité
- [ ] Créer le modèle dans `Entities/Models/`
- [ ] Créer l'interface repository dans `Contracts/`
- [ ] Implémenter le repository dans `Repository/`
- [ ] Ajouter au `IRepositoryManager` et `RepositoryManager`
- [ ] Créer les DTOs dans `Shared/DataTransferObjects/`
- [ ] Créer l'interface service dans `Service.Contract/`
- [ ] Implémenter le service dans `Service/`
- [ ] Ajouter au `IServiceManager` et `ServiceManager`
- [ ] Créer le controller dans `Presentation/Controllers/`
- [ ] Configurer AutoMapper dans `MappingProfile.cs`
- [ ] Ajouter la configuration EF dans `Repository/Configuration/`
- [ ] Créer et appliquer la migration
- [ ] Tester via Swagger

### Ajouter un nouvel endpoint
- [ ] Ajouter la méthode dans le service
- [ ] Ajouter la méthode dans le controller
- [ ] Ajouter les attributs d'autorisation `[Authorize]`
- [ ] Ajouter la documentation XML `/// <summary>`
- [ ] Tester via Swagger
- [ ] Vérifier les logs

---

## 🐛 Debugging

### Logs
- Console output (Development)
- Fichiers (Production) - configurés dans nlog.config

### Erreurs courantes

**1. JWT Error IDX10720**
- Cause: Clé SECRET trop courte
- Solution: Clé ≥ 256 bits (32 caractères)

**2. 401 Unauthorized**
- Vérifier que le token JWT est valide
- Vérifier l'expiration du token
- Utiliser /api/token/refresh si expiré

**3. CategoryNotFoundException / TaskPriorityNotFoundException**
- L'entité demandée n'existe pas
- Vérifier l'ID dans la requête

**4. Migration errors**
- Vérifier que la connection string est correcte
- S'assurer que SQL Server est démarré
- Utiliser `--project` pour spécifier le projet

---

## 🔧 Configuration avancée

### Variables d'environnement

**Development (launchSettings.json):**
```json
"environmentVariables": {
  "ASPNETCORE_ENVIRONMENT": "Development",
  "SECRET": "gsijM0NZ1fD5VT60wusyp7W9CM3vY2X21JPLddEY7sSx9Dx/xz+VrZH8BUbQcmfEhYPvbQfuo4+q9axSnVRidw=="
}
```

**Production:**
- Configurer `SECRET` dans les variables d'environnement système
- Utiliser Azure Key Vault / AWS Secrets Manager pour les secrets

### Personnalisation JWT
- Modifier `expires` dans `appsettings.json`
- Modifier la durée du Refresh Token dans `AuthenticationService.cs` (ligne 84)

### Personnalisation CORS
- Ajouter des origines dans `ServiceExtensions.ConfigureCors()`

---

## 📚 Ressources et références

### Documentation Microsoft
- [ASP.NET Core](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [ASP.NET Core Identity](https://docs.microsoft.com/aspnet/core/security/authentication/identity)

### Packages
- [AutoMapper](https://automapper.org/)
- [NLog](https://nlog-project.org/)
- [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

---

## 👥 Équipe et maintenance

**Développeur principal:** Aurélien Randriananaty  
**Email:** randriananaty@gmail.com  
**Repository:** https://github.com/Aurelien-Randriananaty/Priority.Matrix.Manager

### Dernières modifications
- **2024** : Migration vers .NET 8
- **2024** : Mise à jour des packages (correction vulnérabilités)
- **2024** : Correction clé JWT (512 bits)
- **2023-09** : Ajout Position/ZIndex pour matrice visuelle
- **2023-08** : Correction type UserId
- **2023-07** : Relation User-TaskPriority
- **2023-06** : Implémentation Identity et JWT

---

## 🎯 Prochaines améliorations possibles

### Court terme
- [ ] Tests unitaires (xUnit)
- [ ] Tests d'intégration
- [ ] Validation des DTOs avec FluentValidation
- [ ] Rate limiting
- [ ] Health checks endpoints

### Moyen terme
- [ ] SignalR pour notifications temps réel
- [ ] Gestion des pièces jointes
- [ ] Commentaires sur les tâches
- [ ] Historique des modifications
- [ ] Soft delete (au lieu de hard delete)

### Long terme
- [ ] Microservices architecture
- [ ] Event sourcing
- [ ] CQRS pattern
- [ ] Elasticsearch pour recherche avancée
- [ ] Redis pour cache distribué
- [ ] Docker containerization
- [ ] Azure deployment (App Service + SQL Database)

---

## 📖 Conventions de code

### Naming
- **Classes** : PascalCase
- **Méthodes** : PascalCase
- **Variables** : camelCase
- **Private fields** : _camelCase (avec underscore)
- **Constants** : PascalCase
- **Interfaces** : IPascalCase (préfixe I)

### Async/Await
- Toutes les opérations I/O sont async
- Suffixe `Async` pour les méthodes asynchrones
- `ConfigureAwait(false)` non utilisé (contexte ASP.NET Core)

### DTOs
- Records pour les DTOs de lecture (immutables)
- Classes pour les DTOs de création/mise à jour

### Dependency Injection
- Constructor injection uniquement
- Interfaces injectées (pas d'implémentations concrètes)

---

## 🔍 Aide-mémoire rapide

### Créer une catégorie
```http
POST /api/categories
Authorization: Bearer {JWT}
Content-Type: application/json

{
  "categoryName": "Important et Urgent",
  "categoryCode": "IU"
}
```

### Créer une tâche
```http
POST /api/category/1/tasks
Authorization: Bearer {JWT}
Content-Type: application/json

{
  "taskTitle": "Finir le rapport",
  "taskDescription": "Rapport mensuel",
  "taskCreatedBy": 1,
  "hour": 3,
  "taskStatus": "En cours",
  "posX": 100.5,
  "posY": 200.3,
  "zIndex": 1
}
```

### S'authentifier
```http
POST /api/authentication/login
Content-Type: application/json

{
  "userName": "admin",
  "password": "Password123"
}
```

### Rafraîchir le token
```http
POST /api/token/refresh
Content-Type: application/json

{
  "accessToken": "expired.jwt.token",
  "refreshToken": "valid-refresh-token"
}
```

### Lister les tâches avec pagination
```http
GET /api/category/1/tasks?PageNumber=1&PageSize=10&MinHour=2&MaxHour=10&SearchTerm=rapport
Authorization: Bearer {JWT}
```

---

## 📞 Support et questions

Pour toute question sur l'architecture ou l'implémentation, référez-vous à ce document.

**Dernière mise à jour:** 2024  
**Version:** .NET 8.0  
**Statut:** ✅ Production Ready

---

*Ce document doit être mis à jour à chaque changement architectural majeur.*

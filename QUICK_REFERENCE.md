# ⚡ Quick Reference - Priority Matrix Manager

## 🎯 Résumé ultra-rapide

**Type:** ASP.NET Core Web API (.NET 8)  
**Pattern:** Clean Architecture (N-Tier)  
**Database:** SQL Server Express + Entity Framework Core 8.0.11  
**Auth:** JWT Bearer + Refresh Tokens + ASP.NET Identity  
**Frontend:** React (http://localhost:3000)

---

## 📦 Projets (9 au total)

| Projet | Type | Rôle |
|--------|------|------|
| **Priority.Matrix.Manager** | Web API | Host principal, configuration, migrations |
| **Priority.Matrix.Manager.Presentation** | Library | Controllers API |
| **Service** | Library | Logique métier |
| **Service.Contract** | Library | Interfaces services |
| **Repository** | Library | Accès données (EF Core) |
| **Contracts** | Library | Interfaces repositories |
| **Entities** | Library | Modèles de domaine + Exceptions |
| **Shared** | Library | DTOs + Request Parameters |
| **LoggerService** | Library | Logging (NLog) |

---

## 🗂️ Modèles principaux

### Category
```
Id, CategoryName, CategoryCode → TaskPriorities[]
```

### TaskPriority
```
Id, TaskTitle, TaskDescription, TaskCreatedBy, CreatedDate,
TaskToSee, Hour, TaskStatus, PosX, PosY, ZIndex
→ Category (FK), User (FK optional)
```

### User (IdentityUser)
```
Id→UserId, UserName, Email, FirstName, LastName,
RefreshToken, RefreshTokenExpiryTime → TaskPriorities[]
```

---

## 🌐 Endpoints principaux

### 🔐 Authentication
```
POST   /api/authentication           - Register
POST   /api/authentication/login     - Login (JWT + Refresh)
POST   /api/token/refresh             - Refresh JWT
```

### 📁 Categories
```
GET    /api/categories               - Liste
GET    /api/categories/{id}          - Détail
POST   /api/categories               - Créer
PUT    /api/categories/{id}          - Modifier
DELETE /api/categories/{id}          - Supprimer
```

### 📋 Tasks
```
GET    /api/category/{catId}/tasks              - Liste (paginée)
GET    /api/category/{catId}/tasks/{id}         - Détail
POST   /api/category/{catId}/tasks              - Créer
PUT    /api/category/{catId}/tasks/{id}         - Modifier
PATCH  /api/category/{catId}/tasks/{id}         - Modifier partiellement
DELETE /api/category/{catId}/tasks/{id}         - Supprimer
```

### 👥 Users
```
GET    /api/users                    - Liste
GET    /api/users/{id}               - Détail
```

**Auth:** Tous sauf `/api/authentication/*` nécessitent `Authorization: Bearer {JWT}`

---

## 🔄 Flow typique

```
Request → Controller → Service → Repository → Database
        ← DTO        ← Entity  ← Entity     ← SQL

1. Client envoie requête HTTP
2. Controller valide + appelle Service
3. Service applique logique métier
4. Repository accède aux données
5. AutoMapper convertit Entity ↔ DTO
6. Response retournée au client
```

---

## 🔧 Commandes utiles

### Build & Run
```bash
dotnet restore                    # Restaurer packages
dotnet build                      # Compiler
dotnet run --project Priority.Matrix.Manager  # Lancer
```

### Entity Framework
```bash
# Créer migration
dotnet ef migrations add MigrationName --project Priority.Matrix.Manager

# Appliquer migrations
dotnet ef database update --project Priority.Matrix.Manager

# Supprimer dernière migration
dotnet ef migrations remove --project Priority.Matrix.Manager

# Voir migrations
dotnet ef migrations list --project Priority.Matrix.Manager
```

### Packages
```bash
dotnet list package --vulnerable  # Vulnérabilités
dotnet list package --outdated    # Obsolètes
dotnet restore                    # Restaurer
```

### Git
```bash
git status                                    # Statut
git add .                                     # Stage tout
git commit -m "message"                       # Commit
git push origin branch-name                   # Push
git pull origin staging                       # Pull staging
```

---

## ⚙️ Configuration clés

### appsettings.json
```json
{
  "ConnectionStrings": {
    "sqlConnection": "server=PC-LV14-AURELIE\\SQLEXPRESS;database=PriorityMatrixManager;Integrated Security=true"
  },
  "JwtSettings": {
    "validIssuer": "PriorityMatrixManager",
    "validAudience": "https://localhost:7236",
    "expires": 180
  },
  "FrontEnd": "http://localhost:3000"
}
```

### launchSettings.json (SECRET)
```json
"environmentVariables": {
  "ASPNETCORE_ENVIRONMENT": "Development",
  "SECRET": "gsijM0NZ1fD5VT60wusyp7W9CM3vY2X21JPLddEY7sSx9Dx/xz+VrZH8BUbQcmfEhYPvbQfuo4+q9axSnVRidw=="
}
```

⚠️ **JAMAIS commiter la clé SECRET dans le code source !**

---

## 🔍 Dépendances entre projets

```
Priority.Matrix.Manager
├── → Priority.Matrix.Manager.Presentation
├── → Service
├── → Repository
├── → LoggerService
└── → Service.Contract

Priority.Matrix.Manager.Presentation
└── → Service.Contract

Service
├── → Service.Contract
├── → Contracts
└── → Shared

Repository
├── → Contracts
└── → Entities

Contracts
├── → Entities
└── → Shared

Shared
└── → Entities

LoggerService
└── → Contracts
```

---

## 📊 Packages NuGet (versions actuelles)

```
Microsoft.AspNetCore.Authentication.JwtBearer         8.0.11
Microsoft.EntityFrameworkCore.SqlServer              8.0.11
Microsoft.EntityFrameworkCore.Tools                  8.0.11
Microsoft.AspNetCore.Identity.EntityFrameworkCore    8.0.11
Microsoft.EntityFrameworkCore                        8.0.11
System.IdentityModel.Tokens.Jwt                      8.2.1
AutoMapper.Extensions.Microsoft.DependencyInjection  12.0.1
Marvin.Cache.Headers                                 7.2.0
NLog.Extensions.Logging                              5.3.14
Swashbuckle.AspNetCore                              6.8.1
Microsoft.Extensions.Configuration.Binder            8.0.2
Microsoft.AspNetCore.Mvc.Core                        2.2.5
```

---

## 🚨 Erreurs fréquentes et solutions

| Erreur | Cause | Solution |
|--------|-------|----------|
| IDX10720 | Clé JWT trop courte | Clé ≥ 256 bits dans SECRET |
| 401 Unauthorized | Token invalide/expiré | Vérifier JWT ou refresh |
| 403 Forbidden | Rôle insuffisant | Vérifier rôles utilisateur |
| CategoryNotFoundException | ID inexistant | Vérifier l'ID existe |
| NU1102 | Package introuvable | Vérifier version du package |
| Migration error | Connection string | Vérifier SQL Server actif |

---

## 🧩 Snippets utiles

### Créer un nouveau service

**1. Interface dans Service.Contract:**
```csharp
public interface IMyService
{
    Task<MyDto> GetAsync(int id);
}
```

**2. Implémentation dans Service:**
```csharp
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
        var dto = _mapper.Map<MyDto>(entity);
        return dto;
    }
}
```

**3. Ajouter au ServiceManager:**
```csharp
private readonly Lazy<IMyService> _myService;

// Dans le constructeur
_myService = new Lazy<IMyService>(() => new MyService(repositoryManager, logger, mapper));

// Propriété
public IMyService MyService => _myService.Value;
```

### Créer un nouveau controller

```csharp
[Route("api/myresource")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
[Authorize]
public class MyController : ControllerBase
{
    private readonly IServiceManager _service;

    public MyController(IServiceManager service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.MyService.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id:int}", Name = "GetById")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.MyService.GetAsync(id);
        return Ok(item);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Create([FromBody] MyForCreationDto dto)
    {
        var created = await _service.MyService.CreateAsync(dto);
        return CreatedAtRoute("GetById", new { id = created.Id }, created);
    }
}
```

---

## 🎨 Matrice de priorité - Logique métier

### Catégories Eisenhower

```
        URGENCE
    │ Non │ Oui │
────┼─────┼─────┤
Oui │ INU │ IU  │  I = Important
M   │ Plan│ Do  │  U = Urgent
P   ├─────┼─────┤  N = Non
O   │NUNI │ UNI │
R   │Drop │Deleg│
T   └─────┴─────┘
A
N
C
E
```

- **IU** (Important & Urgent) : Faire immédiatement
- **INU** (Important Non-Urgent) : Planifier
- **UNI** (Urgent Non-Important) : Déléguer
- **NUNI** (Ni Urgent Ni Important) : Éliminer

**Position dans la matrice:**
- `PosX` / `PosY` : Coordonnées x,y
- `ZIndex` : Ordre d'affichage (superposition)

---

## 💡 Tips de développement

### Activer les logs détaillés
```json
"Logging": {
  "LogLevel": {
    "Default": "Debug",
    "Microsoft.EntityFrameworkCore": "Information"
  }
}
```

### Tester JWT localement
1. Login via Swagger → Copier `accessToken`
2. Cliquer sur "Authorize" en haut
3. Entrer: `Bearer {accessToken}`
4. Tester les endpoints protégés

### Voir les requêtes SQL (EF Core)
```csharp
// Dans Program.cs
builder.Services.AddDbContext<RepositoryContext>(options =>
    options.UseSqlServer(connectionString)
           .LogTo(Console.WriteLine, LogLevel.Information)
           .EnableSensitiveDataLogging());
```

### Désactiver l'authentification temporairement
Commenter `[Authorize]` sur le controller (ne pas push en production)

---

## 🔗 Liens utiles

- **Swagger Local:** https://localhost:7236/swagger
- **Frontend:** http://localhost:3000
- **GitHub:** https://github.com/Aurelien-Randriananaty/Priority.Matrix.Manager
- **SQL Server:** PC-LV14-AURELIE\SQLEXPRESS

---

**Document créé le:** 2024  
**Pour:** Référence rapide et aide à la mémoire contextuelle
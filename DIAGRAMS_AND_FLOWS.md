# 🎨 Priority Matrix Manager - Diagrammes et visualisations

## 📊 Architecture en couches (Détaillée)

```
┌──────────────────────────────────────────────────────────────────────────┐
│                            CLIENT LAYER                                   │
│                     React Frontend (Port 3000)                            │
│                     HTTP/HTTPS + JSON/XML/CSV                             │
└────────────────────────────────┬─────────────────────────────────────────┘
                                 │ REST API
                                 │ CORS Enabled
┌────────────────────────────────┴─────────────────────────────────────────┐
│                    PRESENTATION LAYER (Controllers)                       │
│              Priority.Matrix.Manager.Presentation                         │
├───────────────────────────────────────────────────────────────────────────┤
│  ┌─────────────────┐  ┌──────────────────┐  ┌────────────────────┐      │
│  │  Category       │  │  TaskPriority    │  │  Authentication    │      │
│  │  Controller     │  │  Controller      │  │  Controller        │      │
│  └─────────────────┘  └──────────────────┘  └────────────────────┘      │
│  ┌─────────────────┐  ┌──────────────────┐                              │
│  │  User           │  │  Token           │  ┌─────────────────────┐     │
│  │  Controller     │  │  Controller      │  │ ValidationFilter    │     │
│  └─────────────────┘  └──────────────────┘  │ MediaTypeValidator  │     │
│                                              └─────────────────────┘     │
└────────────────────────────────┬─────────────────────────────────────────┘
                                 │ Interface: IServiceManager
                                 │
┌────────────────────────────────┴─────────────────────────────────────────┐
│                       SERVICE LAYER (Business Logic)                      │
│                              Service                                      │
├───────────────────────────────────────────────────────────────────────────┤
│  ┌──────────────────────────────────────────────────────────────┐        │
│  │                    ServiceManager                            │        │
│  │  (Lazy<T> initialization pattern)                            │        │
│  └───────────┬──────────────┬────────────────┬──────────────────┘        │
│              │              │                │                            │
│  ┌───────────┴─────┐  ┌─────┴────────┐  ┌───┴──────────┐  ┌──────────┐ │
│  │  Category       │  │ TaskPriority │  │ Authent.     │  │   User   │ │
│  │  Service        │  │ Service      │  │ Service      │  │ Service  │ │
│  │                 │  │              │  │              │  │          │ │
│  │ • CRUD          │  │ • CRUD       │  │ • Register   │  │ • List   │ │
│  │ • Validation    │  │ • Pagination │  │ • Validate   │  │ • Detail │ │
│  │ • Mapping       │  │ • Filtering  │  │ • JWT Gen    │  │          │ │
│  │                 │  │ • Search     │  │ • Refresh    │  │          │ │
│  └─────────────────┘  └──────────────┘  └──────────────┘  └──────────┘ │
│                                                                            │
│  Dependencies: AutoMapper, ILoggerManager, UserManager                    │
└────────────────────────────────┬─────────────────────────────────────────┘
                                 │ Interface: IRepositoryManager
                                 │
┌────────────────────────────────┴─────────────────────────────────────────┐
│                    REPOSITORY LAYER (Data Access)                         │
│                            Repository                                     │
├───────────────────────────────────────────────────────────────────────────┤
│  ┌──────────────────────────────────────────────────────────────┐        │
│  │                   RepositoryManager                          │        │
│  │  (Unit of Work + Lazy<T> initialization)                     │        │
│  └───────────┬────────────────┬─────────────────────────────────┘        │
│              │                │                                           │
│  ┌───────────┴───────────┐  ┌─┴────────────────────────┐                │
│  │  CategoryRepository   │  │  TaskPriorityRepository  │                │
│  │  (ICategoryRepo)      │  │  (ITaskPriorityRepo)     │                │
│  │                       │  │                          │                │
│  │  Inherits from:       │  │  Inherits from:          │                │
│  │  RepositoryBase       │  │  RepositoryBase          │                │
│  │  <Category>           │  │  <TaskPriority>          │                │
│  └───────────────────────┘  └──────────────────────────┘                │
│                                                                            │
│  ┌──────────────────────────────────────────────────────────────┐        │
│  │             RepositoryBase<T> (Generic)                      │        │
│  │  • FindAll(trackChanges)                                     │        │
│  │  • FindByCondition(expression, trackChanges)                 │        │
│  │  • Create(entity)                                            │        │
│  │  • Update(entity)                                            │        │
│  │  • Delete(entity)                                            │        │
│  └──────────────────────────────────────────────────────────────┘        │
│                                                                          │
│  ┌──────────────────────────────────────────────────────────────┐        │
│  │               RepositoryContext (DbContext)                  │        │
│  │  Inherits: IdentityDbContext<User>                           │        │
│  │  • DbSet<Category> Categories                                │        │
│  │  • DbSet<TaskPriority> TaskPriorities                        │        │
│  │  • Identity Tables (AspNetUsers, AspNetRoles, etc.)          │        │
│  └──────────────────────────────────────────────────────────────┘        │
└────────────────────────────────┬─────────────────────────────────────────┘
                                 │ Entity Framework Core 8.0.11
                                 │
┌────────────────────────────────┴─────────────────────────────────────────┐
│                         DATABASE LAYER                                    │
│                   SQL Server Express Database                             │
│                    PriorityMatrixManager                                  │
├───────────────────────────────────────────────────────────────────────────┤
│  Tables:                                                                  │
│  • Categories                                                             │
│  • TaskPriorities                                                         │
│  • AspNetUsers                                                            │
│  • AspNetRoles                                                            │
│  • AspNetUserRoles                                                        │
│  • AspNetUserClaims, AspNetUserLogins, AspNetUserTokens, etc.             │
└───────────────────────────────────────────────────────────────────────────┘

┌───────────────────────────────────────────────────────────────────────────┐
│                   CROSS-CUTTING CONCERNS (Transversal)                    │
├───────────────────────────────────────────────────────────────────────────┤
│  ┌─────────────┐  ┌──────────────┐  ┌─────────────┐  ┌──────────────┐     │
│  │  Contracts  │  │    Shared    │  │LoggerService│  │   Entities   │     │
│  │             │  │              │  │             │  │              │     │
│  │ Interfaces  │  │ DTOs         │  │ NLog        │  │ Models       │     │
│  │ Repository  │  │ Parameters   │  │ Logger      │  │ Exceptions   │     │
│  │ Services    │  │ Pagination   │  │ Manager     │  │ Config       │     │
│  └─────────────┘  └──────────────┘  └─────────────┘  └──────────────┘     │
└───────────────────────────────────────────────────────────────────────────┘
```

---

## 🔐 Flux d'authentification JWT (Détaillé)

```
┌─────────────┐                                              ┌──────────────┐
│   Client    │                                              │   Server     │
│  (React)    │                                              │   (API)      │
└──────┬──────┘                                              └──────┬───────┘
       │                                                             │
       │  1. POST /api/authentication/login                         │
       │     { userName, password }                                 │
       ├────────────────────────────────────────────────────────────>
       │                                                             │
       │                     2. Validate credentials                │
       │                        UserManager.FindByNameAsync()       │
       │                        UserManager.CheckPasswordAsync()    │
       │                                                             │
       │                     3. Generate tokens                     │
       │                        • Create JWT (HS256)                │
       │                        • Claims: UserName + Roles          │
       │                        • Expires: 180 minutes              │
       │                        • Generate Refresh Token (32 bytes)│
       │                        • Save Refresh Token to User        │
       │                                                             │
       │  4. Response: { accessToken, refreshToken }                │
       <────────────────────────────────────────────────────────────┤
       │                                                             │
       │  5. Store tokens (localStorage / sessionStorage)           │
       │     accessToken: eyJhbGciOiJIUzI1NiIs...                   │
       │     refreshToken: random-base64-string                     │
       │                                                             │
       │  6. API Request with JWT                                   │
       │     Authorization: Bearer eyJhbGciOiJIUzI1NiIs...          │
       ├────────────────────────────────────────────────────────────>
       │                                                             │
       │                     7. Validate JWT                        │
       │                        • Signature valid?                  │
       │                        • Not expired?                      │
       │                        • Issuer/Audience OK?               │
       │                                                             │
       │  8. Response with data                                     │
       <────────────────────────────────────────────────────────────┤
       │                                                             │
       │  ...Time passes... JWT expires (180 min)                   │
       │                                                             │
       │  9. API Request with expired JWT                           │
       ├────────────────────────────────────────────────────────────>
       │                                                             │
       │  10. Response: 401 Unauthorized                            │
       <────────────────────────────────────────────────────────────┤
       │                                                             │
       │  11. POST /api/token/refresh                               │
       │      { accessToken, refreshToken }                         │
       ├────────────────────────────────────────────────────────────>
       │                                                             │
       │                     12. Validate Refresh Token             │
       │                         • User exists?                     │
       │                         • Token matches DB?                │
       │                         • Not expired (<7 days)?           │
       │                                                             │
       │                     13. Generate new JWT                   │
       │                         (keep same refresh token)          │
       │                                                             │
       │  14. Response: { accessToken, refreshToken }               │
       <────────────────────────────────────────────────────────────┤
       │                                                             │
       │  15. Continue with new token                               │
       │                                                             │
```

---

## 🗄️ Schéma de base de données

### Tables principales

```sql
┌──────────────────────────────────────────────────────────────┐
│                         Categories                            │
├──────────────────────────────────────────────────────────────┤
│ CategoryId (PK, int, IDENTITY)                               │
│ CategoryName (nvarchar(60), NOT NULL)                        │
│ CategoryCode (nvarchar(60), NOT NULL)                        │
└──────────────────────────────────────────────────────────────┘
                              │
                              │ 1
                              │
                              │ N
┌──────────────────────────────┴───────────────────────────────┐
│                      TaskPriorities                           │
├──────────────────────────────────────────────────────────────┤
│ TaskId (PK, int, IDENTITY)                                   │
│ TaskTitle (nvarchar(60), NOT NULL)                           │
│ TaskDescription (nvarchar(255), NULL)                        │
│ TaskCreatedBy (int, NOT NULL)                                │
│ TaskToSee (datetime2, NULL)                                  │
│ CreatedDate (datetime2, NOT NULL)                            │
│ Hour (int, NULL)                                             │
│ TaskStatus (nvarchar(MAX), NULL)                             │
│ CategoryID (int, FK → Categories.CategoryId)                 │
│ UserId (nvarchar(450), FK → AspNetUsers.UserId, NULL)        │
│ PosX (real, NULL)                                            │
│ PosY (real, NULL)                                            │
│ ZIndex (int, NULL)                                           │
└──────────────────────────────────────────────────────────────┘
                              │
                              │ N
                              │
                              │ 1 (optional)
┌──────────────────────────────┴───────────────────────────────┐
│                      AspNetUsers                              │
├──────────────────────────────────────────────────────────────┤
│ UserId (PK, nvarchar(450)) ← Renamed from Id                │
│ UserName (nvarchar(256), NOT NULL, UNIQUE)                   │
│ NormalizedUserName (nvarchar(256), INDEXED)                  │
│ Email (nvarchar(256), NOT NULL, UNIQUE)                      │
│ NormalizedEmail (nvarchar(256), INDEXED)                     │
│ EmailConfirmed (bit)                                         │
│ PasswordHash (nvarchar(MAX))                                 │
│ SecurityStamp (nvarchar(MAX))                                │
│ PhoneNumber (nvarchar(MAX), NULL)                            │
│ PhoneNumberConfirmed (bit)                                   │
│ TwoFactorEnabled (bit)                                       │
│ LockoutEnd (datetimeoffset, NULL)                            │
│ LockoutEnabled (bit)                                         │
│ AccessFailedCount (int)                                      │
│ FirstName (nvarchar(MAX), NOT NULL)   ← Custom               │
│ LastName (nvarchar(MAX), NOT NULL)    ← Custom               │
│ RefreshToken (nvarchar(MAX), NULL)    ← Custom               │
│ RefreshTokenExpiryTime (datetime2)    ← Custom               │
└──────────────────────────────────────────────────────────────┘
                              │
                              │ N
                              │
                              │ N (via AspNetUserRoles)
┌──────────────────────────────┴───────────────────────────────┐
│                       AspNetRoles                             │
├──────────────────────────────────────────────────────────────┤
│ Id (PK, nvarchar(450))                                       │
│ Name (nvarchar(256), NOT NULL)                               │
│ NormalizedName (nvarchar(256), INDEXED)                      │
│ ConcurrencyStamp (nvarchar(MAX))                             │
│                                                              │
│ Données initiales:                                           │
│ • "Administrator"                                            │
│ • "Manager"                                                  │
└──────────────────────────────────────────────────────────────┘
```

### Index et contraintes
```sql
-- Primary Keys
PK_Categories (CategoryId)
PK_TaskPriorities (TaskId)
PK_AspNetUsers (UserId)
PK_AspNetRoles (Id)

-- Foreign Keys
FK_TaskPriorities_Categories (CategoryID → Categories.CategoryId)
FK_TaskPriorities_Users (UserId → AspNetUsers.UserId) ON DELETE SET NULL

-- Indexes
IX_TaskPriorities_CategoryID
IX_TaskPriorities_UserId
IX_AspNetUsers_NormalizedUserName (UNIQUE)
IX_AspNetUsers_NormalizedEmail
```

---

## 🔄 Flux de données CRUD (Create Task Example)

```
┌─────────────────────────────────────────────────────────────────────────┐
│ STEP 1: CLIENT REQUEST                                                   │
└─────────────────────────────────────────────────────────────────────────┘

POST https://localhost:7236/api/category/1/tasks
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json

{
  "taskTitle": "Préparer présentation",
  "taskDescription": "Slides + démo",
  "taskCreatedBy": 123,
  "hour": 3,
  "taskStatus": "À faire",
  "posX": 150.5,
  "posY": 220.3,
  "zIndex": 1
}

         │
         ▼

┌─────────────────────────────────────────────────────────────────────────┐
│ STEP 2: MIDDLEWARE PIPELINE                                              │
└─────────────────────────────────────────────────────────────────────────┘

1. Routing Middleware → Route vers TaskpriorityController
2. CORS Middleware → Vérifie origin autorisée
3. Authentication Middleware → Valide JWT, crée ClaimsPrincipal
4. Authorization Middleware → Vérifie [Authorize]

         │
         ▼

┌─────────────────────────────────────────────────────────────────────────┐
│ STEP 3: CONTROLLER (TaskpriorityController)                             │
└─────────────────────────────────────────────────────────────────────────┘

public async Task<IActionResult> CreateTaskPriorityForCategory(
    int categoryId,
    [FromBody] TaskPriorityForCreationDto task)
{
    // ValidationFilterAttribute exécuté automatiquement
    // ModelState.IsValid vérifié
    
    var createdTask = await _service.TaskPriorityService
        .CreateTaskPriorityForCategoryAsync(categoryId, task, trackChanges: false);
    
    return CreatedAtRoute("GetTaskPriorityForCategory",
        new { categoryId, id = createdTask.Id }, createdTask);
}

         │
         ▼

┌─────────────────────────────────────────────────────────────────────────┐
│ STEP 4: SERVICE LAYER (TaskPriorityService)                             │
└─────────────────────────────────────────────────────────────────────────┘

public async Task<TaskPriorityDto> CreateTaskPriorityForCategoryAsync(
    int categoryId, TaskPriorityForCreationDto taskPriorityForCreation, bool trackChanges)
{
    // 1. Valider que la catégorie existe
    var category = await _repository.Category.GetCategoryByIdAsync(categoryId, trackChanges);
    if (category == null)
        throw new CategoryNotFoundException(categoryId);
    
    // 2. Mapper DTO → Entity
    var taskPriorityEntity = _mapper.Map<TaskPriority>(taskPriorityForCreation);
    
    // 3. Créer dans le repository
    _repository.TaskPriority.CreateTaskPriorityForCategory(categoryId, taskPriorityEntity);
    
    // 4. Sauvegarder (Unit of Work)
    await _repository.SaveAsync();
    
    // 5. Mapper Entity → DTO
    var taskPriorityToReturn = _mapper.Map<TaskPriorityDto>(taskPriorityEntity);
    
    return taskPriorityToReturn;
}

         │
         ▼

┌─────────────────────────────────────────────────────────────────────────┐
│ STEP 5: REPOSITORY LAYER (TaskPriorityRepository)                       │
└─────────────────────────────────────────────────────────────────────────┘

public void CreateTaskPriorityForCategory(int categoryId, TaskPriority taskPriority)
{
    taskPriority.CategoryID = categoryId;
    taskPriority.CreatedDate = DateTime.Now;
    Create(taskPriority);  // Appelle RepositoryBase.Create()
}

// RepositoryBase<T>.Create()
public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);

         │
         ▼

┌─────────────────────────────────────────────────────────────────────────┐
│ STEP 6: REPOSITORY MANAGER (Unit of Work)                               │
└─────────────────────────────────────────────────────────────────────────┘

public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();

         │
         ▼

┌─────────────────────────────────────────────────────────────────────────┐
│ STEP 7: ENTITY FRAMEWORK CORE                                            │
└─────────────────────────────────────────────────────────────────────────┘

_repositoryContext.SaveChangesAsync()
├─> Détecte les changements (ChangeTracker)
├─> Génère SQL INSERT
└─> Exécute sur SQL Server

SQL Généré:
INSERT INTO [TaskPriorities] 
    ([TaskTitle], [TaskDescription], [TaskCreatedBy], [CreatedDate], 
     [Hour], [TaskStatus], [CategoryID], [PosX], [PosY], [ZIndex])
VALUES 
    (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9);
SELECT [TaskId] FROM [TaskPriorities] WHERE @@ROWCOUNT = 1 AND [TaskId] = scope_identity();

         │
         ▼

┌─────────────────────────────────────────────────────────────────────────┐
│ STEP 8: AUTOMAPPER                                                       │
└─────────────────────────────────────────────────────────────────────────┘

TaskPriority (Entity) → TaskPriorityDto
{
    Id = 42,
    TaskTitle = "Préparer présentation",
    TaskDescription = "Slides + démo",
    ...
    Category = { Id: 1, CategoryName: "Important et Urgent", ... },
    User = null
}

         │
         ▼

┌─────────────────────────────────────────────────────────────────────────┐
│ STEP 9: RESPONSE                                                         │
└─────────────────────────────────────────────────────────────────────────┘

HTTP/1.1 201 Created
Location: https://localhost:7236/api/category/1/tasks/42
Content-Type: application/json

{
  "id": 42,
  "taskTitle": "Préparer présentation",
  "taskDescription": "Slides + démo",
  "taskCreatedBy": 123,
  "createdDate": "2024-01-15T10:30:00",
  "hour": 3,
  "taskStatus": "À faire",
  "categoryID": 1,
  "posX": 150.5,
  "posY": 220.3,
  "zIndex": 1,
  "category": {
    "id": 1,
    "categoryName": "Important et Urgent",
    "categoryCode": "IU"
  }
}
```

---

## 🧩 Dependency Injection Graph

```
                            Startup (Program.cs)
                                    │
                    ┌───────────────┴──────────────┐
                    │   ConfigureServices          │
                    └───────────────┬──────────────┘
                                    │
        ┌───────────────────────────┼───────────────────────────┐
        │                           │                           │
        ▼                           ▼                           ▼
┌──────────────┐          ┌─────────────────┐        ┌──────────────────┐
│ Singleton    │          │    Scoped       │        │    Transient     │
├──────────────┤          ├─────────────────┤        ├──────────────────┤
│• ILoggerMgr  │          │• IServiceMgr    │        │ (aucun)          │
│  └LoggerMgr  │          │  └ServiceMgr    │        │                  │
│              │          │                 │        │                  │
│              │          │• IRepositoryMgr │        │                  │
│              │          │  └RepositoryMgr │        │                  │
│              │          │                 │        │                  │
│              │          │• DbContext      │        │                  │
│              │          │  └RepoCxt       │        │                  │
│              │          │                 │        │                  │
│              │          │• UserManager    │        │                  │
│              │          │• SignInMgr      │        │                  │
│              │          │• RoleManager    │        │                  │
│              │          │                 │        │                  │
│              │          │• IMapper        │        │                  │
│              │          │  └AutoMapper    │        │                  │
│              │          │                 │        │                  │
│              │          │• ActionFilters  │        │                  │
└──────────────┘          └─────────────────┘        └──────────────────┘

Lifetime Scopes:
• Singleton   : 1 instance pour toute l'application
• Scoped      : 1 instance par requête HTTP
• Transient   : Nouvelle instance à chaque injection
```

---

## 📈 Pagination & Filtrage - Diagramme de flux

```
GET /api/category/1/tasks?PageNumber=2&PageSize=5&MinHour=2&MaxHour=8&SearchTerm=rapport

         │
         ▼
┌─────────────────────────────────────────────┐
│  1. TaskpriorityController                  │
│     Reçoit TaskPriorityParameters           │
│     {                                       │
│       PageNumber: 2,                        │
│       PageSize: 5,                          │
│       MinHour: 2,                           │
│       MaxHour: 8,                           │
│       SearchTerm: "rapport"                 │
│     }                                       │
└──────────────────┬──────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────┐
│  2. TaskPriorityService                     │
│     GetTaskPrioritiesAsync(catId, params)   │
└──────────────────┬──────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────┐
│  3. TaskPriorityRepository                  │
│     GetTaskPrioritiesAsync()                │
│                                             │
│     base query = FindByCondition(           │
│         t => t.CategoryID == categoryId)    │
└──────────────────┬──────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────┐
│  4. RepositoryTaskPriorityExtensions        │
│                                             │
│     query = query                           │
│       .FilterTaskPriorities(MinHour, MaxHour)│
│       .Search(SearchTerm)                   │
│       .OrderBy(t => t.TaskTitle)            │
└──────────────────┬──────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────┐
│  5. PagedList.ToPagedListAsync()            │
│                                             │
│     count = await query.CountAsync()        │
│     items = await query                     │
│                .Skip((PageNumber-1)*PageSize)│
│                .Take(PageSize)              │
│                .ToListAsync()               │
│                                             │
│     MetaData {                              │
│       TotalCount: 23,                       │
│       PageSize: 5,                          │
│       CurrentPage: 2,                       │
│       TotalPages: 5,                        │
│       HasPrevious: true,                    │
│       HasNext: true                         │
│     }                                       │
└──────────────────┬──────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────┐
│  6. AutoMapper                              │
│     List<TaskPriority> → List<TaskPriorityDto>│
│     (inclut Category et User)               │
└──────────────────┬──────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────┐
│  7. Controller Response                     │
│                                             │
│     Headers:                                │
│       X-Pagination: {MetaData JSON}         │
│                                             │
│     Body:                                   │
│       [                                     │
│         { id: 6, taskTitle: "...", ... },   │
│         { id: 7, taskTitle: "...", ... },   │
│         { id: 8, taskTitle: "...", ... },   │
│         { id: 9, taskTitle: "...", ... },   │
│         { id: 10, taskTitle: "...", ... }   │
│       ]                                     │
└─────────────────────────────────────────────┘
```

---

## 🔍 Mapping AutoMapper (Relations)

```
┌─────────────────────────────────────────────────────────────┐
│                   MappingProfile.cs                          │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│  CreateMap<Category, CategoryDto>();                        │
│                                                              │
│  CreateMap<TaskPriority, TaskPriorityDto>()                 │
│      .IncludeMembers(t => t.User, t => t.Category);         │
│      └─> Navigation properties automatiquement incluses     │
│                                                              │
│  CreateMap<User, TaskPriorityDto>();                        │
│      └─> Mapping partiel pour propriétés User dans TaskDto  │
│                                                              │
│  CreateMap<Category, TaskPriorityDto>();                    │
│      └─> Mapping partiel pour propriétés Category          │
│                                                              │
│  CreateMap<CategoryForCreationDto, Category>();             │
│  CreateMap<CategoryForUpdateDto, Category>();               │
│                                                              │
│  CreateMap<TaskPriorityForCreationDto, TaskPriority>();     │
│  CreateMap<TaskPriorityForUpdateDto, TaskPriority>();       │
│                                                              │
│  CreateMap<UserForRegistrationDto, User>();                 │
│  CreateMap<User, UserDto>();                                │
│  CreateMap<User, UserIdentitiesDto>();                      │
└─────────────────────────────────────────────────────────────┘

Exemple de mapping complexe:

TaskPriority (Entity)              TaskPriorityDto (DTO)
├─ Id                      →       ├─ Id
├─ TaskTitle               →       ├─ TaskTitle
├─ TaskDescription         →       ├─ TaskDescription
├─ ...                     →       ├─ ...
├─ Category                →       ├─ Category (CategoryDto)
│  ├─ Id                   →       │  ├─ Id
│  ├─ CategoryName         →       │  ├─ CategoryName
│  └─ CategoryCode         →       │  └─ CategoryCode
└─ User (nullable)         →       └─ User (UserIdentitiesDto, nullable)
   ├─ Id                   →          ├─ Id
   ├─ UserName             →          ├─ UserName
   ├─ FirstName            →          ├─ FirstName
   └─ LastName             →          └─ LastName
```

---

## 🎭 Gestion des exceptions (Middleware)

```
                    Exception thrown anywhere
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│        ExceptionMiddlewareExtensions                         │
│        (Global Exception Handler)                            │
└──────────────────────────┬──────────────────────────────────┘
                           │
         ┌─────────────────┼─────────────────┐
         │                 │                 │
         ▼                 ▼                 ▼
┌───────────────┐  ┌──────────────┐  ┌─────────────────┐
│NotFoundException│  │BadRequestExc │  │Other Exception │
├───────────────┤  ├──────────────┤  ├─────────────────┤
│• Category     │  │• IdParams    │  │• Unhandled      │
│• TaskPriority │  │• Collection  │  │  exceptions     │
└───────┬───────┘  │• MaxHour     │  └────────┬────────┘
        │          │• RefreshToken│           │
        │          └──────┬───────┘           │
        │                 │                   │
        └─────────────────┼───────────────────┘
                          │
                          ▼
┌─────────────────────────────────────────────────────────────┐
│              Create ErrorDetails JSON                        │
│              {                                               │
│                "StatusCode": 404 | 400 | 500,               │
│                "Message": "Descriptive error message"       │
│              }                                               │
└──────────────────────────┬──────────────────────────────────┘
                           │
                           ▼
┌─────────────────────────────────────────────────────────────┐
│              Set Response StatusCode                         │
│              Write JSON to Response Body                     │
└─────────────────────────────────────────────────────────────┘
                           │
                           ▼
                    Client receives error response
```

**Types d'erreurs:**
- `404` : NotFoundException (Category, TaskPriority)
- `400` : BadRequestException (validation, paramètres)
- `401` : Unauthorized (JWT invalide)
- `403` : Forbidden (rôle insuffisant)
- `500` : Unhandled exception (erreur serveur)

---

## 🎯 Priority Matrix - Visualisation métier

```
                    MATRICE D'EISENHOWER
    
    ┌─────────────────────────────────────────────────────┐
    │                    IMPORTANCE                       │
    │                                                     │
    │  Faible ◄──────────────────────────► Élevée       │
    │                                                     │
U   │    ┌──────────────────┬──────────────────┐        │
R   │    │                  │                  │        │
G   │    │  UNI             │      IU          │        │
E   │    │  (À déléguer)    │  (À faire       │        │
N   │ É  │                  │   immédiatement)│        │
C   │ l  │  CategoryCode:   │                  │        │
E   │ e  │  "UNI"           │  CategoryCode:   │        │
    │ v  │                  │  "IU"            │        │
    │ é  ├──────────────────┼──────────────────┤        │
    │ e  │                  │                  │        │
    │    │  NUNI            │     INU          │        │
    │    │  (À éliminer)    │  (À planifier)  │        │
    │    │                  │                  │        │
    │ F  │  CategoryCode:   │  CategoryCode:   │        │
    │ a  │  "NUNI"          │  "INU"           │        │
    │ i  │                  │                  │        │
    │ b  └──────────────────┴──────────────────┘        │
    │ l                                                  │
    │ e                                                  │
    │                                                     │
    └─────────────────────────────────────────────────────┘

Chaque TaskPriority possède:
• PosX / PosY : Position exacte dans la matrice (float)
• ZIndex : Ordre d'affichage (superposition)
• CategoryID : Référence à une des 4 catégories
```

---

## 🔐 Security Checklist

### ✅ Implémenté
- [x] JWT avec HS256 (512 bits)
- [x] Refresh Tokens (7 jours)
- [x] ASP.NET Identity (Users + Roles)
- [x] Password requirements (10+ chars, digit required)
- [x] HTTPS redirection
- [x] CORS configuré (whitelist)
- [x] Input validation (DTOs + ModelState)
- [x] Secret key dans variable d'environnement
- [x] SQL Injection protection (EF Core parameterized queries)

### 🚧 À améliorer (optionnel)
- [ ] Rate limiting
- [ ] HTTPS strict mode (HSTS)
- [ ] Two-Factor Authentication (2FA)
- [ ] Email confirmation
- [ ] Account lockout après X tentatives
- [ ] IP whitelisting
- [ ] API versioning
- [ ] Request/Response logging (audit trail)

---

## 🧪 Test Scenarios (via Swagger)

### Scénario 1: Workflow complet utilisateur

```
1. Register User
   POST /api/authentication
   {
     "firstName": "John",
     "lastName": "Doe",
     "userName": "johndoe",
     "password": "Password123",
     "email": "john@example.com",
     "roles": ["Manager"]
   }
   → 201 Created

2. Login
   POST /api/authentication/login
   {
     "userName": "johndoe",
     "password": "Password123"
   }
   → 200 OK + { accessToken, refreshToken }

3. Get Categories
   GET /api/categories
   Authorization: Bearer {accessToken}
   → 200 OK + [Categories]

4. Create Task
   POST /api/category/1/tasks
   Authorization: Bearer {accessToken}
   { taskTitle: "...", ... }
   → 201 Created + Location header

5. List Tasks (paginated)
   GET /api/category/1/tasks?PageNumber=1&PageSize=10
   Authorization: Bearer {accessToken}
   → 200 OK + X-Pagination header

6. Update Task
   PUT /api/category/1/tasks/5
   Authorization: Bearer {accessToken}
   { taskTitle: "Updated", ... }
   → 204 No Content

7. Delete Task
   DELETE /api/category/1/tasks/5
   Authorization: Bearer {accessToken}
   → 204 No Content
```

### Scénario 2: Token Refresh

```
1. Initial Login (t=0)
   POST /api/authentication/login
   → accessToken (expires in 180 min)
   → refreshToken (expires in 7 days)

2. Use API (t=30min)
   GET /api/categories
   Authorization: Bearer {accessToken}
   → 200 OK

3. Token expires (t=181min)
   GET /api/categories
   Authorization: Bearer {expiredToken}
   → 401 Unauthorized

4. Refresh Token (t=181min)
   POST /api/token/refresh
   {
     "accessToken": "{expiredToken}",
     "refreshToken": "{refreshToken}"
   }
   → 200 OK + new accessToken (same refreshToken)

5. Continue with new token (t=182min)
   GET /api/categories
   Authorization: Bearer {newAccessToken}
   → 200 OK
```

---

## 📝 Code Snippets pratiques

### Ajouter un nouveau DTO

```csharp
// Dans Shared/DataTransferObjects/
public record MyEntityDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public DateTime CreatedDate { get; init; }
}

public record MyEntityForCreationDto
{
    public string Name { get; init; }
}

public record MyEntityForUpdateDto : MyEntityForManipulationDto;

public abstract record MyEntityForManipulationDto
{
    public string Name { get; init; }
}
```

### Ajouter un Repository

```csharp
// Dans Contracts/
public interface IMyRepository
{
    Task<IEnumerable<MyEntity>> GetAllAsync(bool trackChanges);
    Task<MyEntity> GetByIdAsync(int id, bool trackChanges);
    void Create(MyEntity entity);
    void Delete(MyEntity entity);
}

// Dans Repository/
public class MyRepository : RepositoryBase<MyEntity>, IMyRepository
{
    public MyRepository(RepositoryContext context) : base(context) { }

    public async Task<IEnumerable<MyEntity>> GetAllAsync(bool trackChanges) =>
        await FindAll(trackChanges).OrderBy(e => e.Name).ToListAsync();

    public async Task<MyEntity> GetByIdAsync(int id, bool trackChanges) =>
        await FindByCondition(e => e.Id == id, trackChanges).SingleOrDefaultAsync();

    public void Create(MyEntity entity) => Create(entity);

    public void Delete(MyEntity entity) => Delete(entity);
}
```

### Ajouter dans RepositoryManager

```csharp
private readonly Lazy<IMyRepository> _myRepository;

// Constructeur
_myRepository = new Lazy<IMyRepository>(() => new MyRepository(repositoryContext));

// Propriété
public IMyRepository MyRepository => _myRepository.Value;

// Interface IRepositoryManager
IMyRepository MyRepository { get; }
```

---

## 🌍 Configuration Environnements

### Development
```json
{
  "ASPNETCORE_ENVIRONMENT": "Development",
  "SECRET": "long-secret-key-here",
  "ConnectionStrings:sqlConnection": "server=localhost\\SQLEXPRESS;..."
}
```

### Staging
```json
{
  "ASPNETCORE_ENVIRONMENT": "Staging",
  "SECRET": "env-variable-from-azure",
  "ConnectionStrings:sqlConnection": "azure-sql-connection-string"
}
```

### Production
```json
{
  "ASPNETCORE_ENVIRONMENT": "Production",
  "SECRET": "env-variable-from-keyvault",
  "ConnectionStrings:sqlConnection": "production-sql-connection",
  "Logging:LogLevel:Default": "Warning"
}
```

---

## 📚 Conventions spécifiques au projet

### Nommage des endpoints
- Pluriel pour collections : `/api/categories`
- Singulier pour item : `/api/categories/{id}`
- Nested routes pour relations : `/api/category/{categoryId}/tasks`

### Codes de statut HTTP
- `200 OK` : GET réussi
- `201 Created` : POST réussi + Location header
- `204 No Content` : PUT/DELETE/PATCH réussi
- `400 Bad Request` : Validation échouée
- `401 Unauthorized` : Non authentifié
- `403 Forbidden` : Non autorisé (rôle)
- `404 Not Found` : Ressource inexistante
- `500 Internal Server Error` : Erreur serveur

### Validation
- Data Annotations sur les entités
- ModelState validation dans controllers (via ValidationFilterAttribute)
- Business validation dans services

### Logging
- Info : Actions normales
- Warning : Situations inhabituelles (ex: auth failed)
- Error : Exceptions

---

## 🔗 Relations de dépendance (Graph)

```
            ┌─────────────────┐
            │   Entities      │ ◄─── Aucune dépendance (couche la plus basse)
            └────────┬────────┘
                     │
            ┌────────┴────────┐
            │                 │
    ┌───────▼──────┐   ┌──────▼──────┐
    │   Shared     │   │  Contracts  │
    └───────┬──────┘   └──────┬──────┘
            │                 │
            │         ┌───────┴────────┬─────────────┐
            │         │                │             │
    ┌───────▼─────────▼──┐   ┌─────────▼──┐   ┌─────▼───────────┐
    │   Service.Contract │   │ Repository │   │  LoggerService  │
    └───────┬────────────┘   └─────┬──────┘   └─────┬───────────┘
            │                      │                  │
            │         ┌────────────┴──────────┐      │
            │         │                       │      │
    ┌───────▼─────────▼──┐         ┌─────────▼──────▼────┐
    │      Service       │         │  Presentation       │
    └───────┬────────────┘         └─────────┬───────────┘
            │                                 │
            │         ┌───────────────────────┘
            │         │
    ┌───────▼─────────▼─────────────┐
    │ Priority.Matrix.Manager       │ ◄─── API Host (couche la plus haute)
    └───────────────────────────────┘
```

**Règle:** Une couche ne peut dépendre que des couches inférieures (jamais supérieures)

---

## 🎯 Commandes de diagnostic

### Vérifier l'état de la solution
```bash
dotnet --version                          # Version SDK
dotnet --list-sdks                        # SDKs installés
dotnet --info                             # Info complète
```

### Analyser les packages
```bash
dotnet list package                       # Tous les packages
dotnet list package --include-transitive  # Avec dépendances transitives
dotnet list package --vulnerable          # Vulnérabilités
dotnet list package --deprecated          # Obsolètes
```

### Entity Framework diagnostics
```bash
dotnet ef --version                       # Version EF tools
dotnet ef migrations list --project Priority.Matrix.Manager
dotnet ef dbcontext info --project Priority.Matrix.Manager
dotnet ef dbcontext list --project Priority.Matrix.Manager
```

### Git diagnostics
```bash
git log --oneline -10                     # 10 derniers commits
git branch -a                             # Toutes les branches
git remote -v                             # Remotes configurés
git diff                                  # Changements non stagés
git diff --staged                         # Changements stagés
```

---

## 📄 Fichiers de configuration importants

| Fichier | Localisation | Usage |
|---------|--------------|-------|
| `appsettings.json` | Priority.Matrix.Manager/ | Config globale (DB, JWT, CORS) |
| `appsettings.Development.json` | Priority.Matrix.Manager/ | Config dev (overrides) |
| `launchSettings.json` | Priority.Matrix.Manager/Properties/ | Variables env, ports |
| `nlog.config` | Priority.Matrix.Manager/ | Configuration NLog |
| `.gitignore` | Root | Fichiers à ignorer (bin, obj, secrets) |
| `*.csproj` | Chaque projet | Configuration projet NuGet |

---

## 🚀 Déploiement (Checklist)

### Avant déploiement
- [ ] Build réussi (`dotnet build --configuration Release`)
- [ ] Aucune vulnérabilité (`dotnet list package --vulnerable`)
- [ ] Tests passés (`dotnet test`)
- [ ] Migrations à jour
- [ ] Variables d'environnement configurées
- [ ] Connection string production prête
- [ ] SECRET configuré (Azure Key Vault)
- [ ] CORS configuré pour domaine production
- [ ] Logging configuré (fichiers + Application Insights)

### Post-déploiement
- [ ] Vérifier health check
- [ ] Tester authentification
- [ ] Vérifier logs
- [ ] Tester endpoints critiques
- [ ] Monitoring actif

---

**Dernière mise à jour:** 2024  
**Version .NET:** 8.0  
**Status:** ✅ Production Ready

*Gardez ce document à jour avec chaque changement significatif !*

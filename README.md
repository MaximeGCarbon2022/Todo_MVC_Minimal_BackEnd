# Todo_MVC_Minimal_Backend


- **BASE DE DONNEES**

Un projet de base de données est disponible dans TodoBackend.Api.Database.

1. Ajouter une nouvelle connectionString dans le fichier appsettings.json
2. Changer sa valeur dans le fichier TodoDapperRepository (connectionId)

Si vous n'utilisez pas le projet de base de données, créer la table dans SSMS :

```script

CREATE TABLE [dbo].[Todo](
	[Id] UniqueIdentifier NOT NULL,
	[Title] [varchar](MAX) NOT NULL,
	[Completed] [bit] NULL,
	[Order] [int] NULL,
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Todo] ADD  DEFAULT ((0)) FOR [Completed]
GO

```

---
- **TODO ENDPOINT**

| Method       | Endpoint                | Body                                                          |  Auth |
|--------------|-------------------------|---------------------------------------------------------------|-------|
| **GET**      | `/todos `               | X                                                             | none  |
| **GET**      | `/todos/{id}`           | X                                                             | none  |
| **POST**     | `/todos`                | body : { Title }                                              | none  |
| **PUT**      | `/todos/{id}`           | body : { Title, Completed, Order }                            | none  |
| **DELETE**   | `/todos/{isCompleted?}` | X                                                             | none  |
| **DELETE**   | `/todos/{id}`           | X                                                             | none  |

---

- **TODOS**

🆗 : Done | ⭐ : High priority | 🚫 : Low priority

| Feature | Status | Note  | Information
|---------|--------|------- | -----------
| **Base de Donnees** | ✅ | 🆗 | Ajouter Primary key (Id, Title) ?
| **Controller** | ✅ | 🆗 |
| **Service** | ✅ | 🆗 
| **Repository** | ✅ | 🆗 | Micro-ORM Dapper
| **Model** | ✅ | 🆗 
| **Mapping Between the different layers** | ✅ | 🆗 
| **Differentiate Route Delete (Id / isCompleted)** | ✅ |🆗   
| **Order Management** | ✅ | 🆗 | Gestion gérer en SQL     
| **Url Management** | ✅ | 🆗 | LinkGenerator 
| **Custom Exception** | ✅ | 🆗 | IActionFilter
| **Attribut Controller** | ✅ | 🆗 | Doc API / Swagger
| **Route PATCH** | ❌ | 🚫 | JsonPatchDocument ?

---

- **TESTS**

🆗 : Done | ⭐ : High priority | 🚫 : Low priority

| Feature | Status | Notes  | Information
|---------|--------|------- | -----------
| **Unit Test Service** | ✅ | 🆗 |
| **Unit Test Repository** | ✅ | 🆗 |
| **Integration Test Controller** | ❌ | ⭐ | Simuler avec une base de données en mémoire (Sqlite ? Base figé ?)
| **Integration Test Repository** | ❌ | ⭐ | Simuler avec une base de données en mémoire (Sqlite ?, Base figé ?)
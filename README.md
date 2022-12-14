# Todo_MVC_Minimal_Backend


- **BASE DE DONNEES**

Un projet de base de donnΓ©es est disponible dans TodoBackend.Api.Database.

1. Ajouter une nouvelle connectionString dans le fichier appsettings.json
2. Changer sa valeur dans le fichier TodoDapperRepository (connectionId)

Si vous n'utilisez pas le projet de base de donnΓ©es, crΓ©er la table dans SSMS :

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

π : Done | β­ : High priority | π« : Low priority

| Feature | Status | Note  | Information
|---------|--------|------- | -----------
| **Base de Donnees** | β | π | Ajouter Primary key (Id, Title) ?
| **Controller** | β | π |
| **Service** | β | π 
| **Repository** | β | π | Micro-ORM Dapper
| **Model** | β | π 
| **Mapping Between the different layers** | β | π 
| **Differentiate Route Delete (Id / isCompleted)** | β |π   
| **Order Management** | β | π | Gestion gΓ©rer en SQL     
| **Url Management** | β | π | LinkGenerator 
| **Custom Exception** | β | π | IActionFilter
| **Attribut Controller** | β | π | Doc API / Swagger
| **Route PATCH** | β | π« | JsonPatchDocument ?

---

- **TESTS**

π : Done | β­ : High priority | π« : Low priority

| Feature | Status | Notes  | Information
|---------|--------|------- | -----------
| **Unit Test Service** | β | π |
| **Unit Test Repository** | β | π |
| **Integration Test Controller** | β | β­ | Simuler avec une base de donnΓ©es en mΓ©moire (Sqlite ? Base figΓ© ?)
| **Integration Test Repository** | β | β­ | Simuler avec une base de donnΓ©es en mΓ©moire (Sqlite ?, Base figΓ© ?)
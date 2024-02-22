# Comment créer une WEB API

## Démarrage 

- ### Visual studio Code
Ligne de commande :  
- Création des fichiers
    - dotnet new webapi  
    Les fichiers de base sont crées dans le dossier spécifiés.
    - dotnet run  
    Lancement de l'application. D'ici on peut récupèrer l'adresse du localhost

- ### Program.cs

Création des fichiers  Controllers et Models  

**Dans Program.cs on peut enlever tout ce qui est installé d'offices :**
```
var summaries = new[]
// ... 
.WithOpenApi();

// On enlève aussi :
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
```

>**Dans Program.cs on oublie pas d'ajouter :**

```
// Pour dire que l'on va allez chercher dans les Controllers
builder.Services.AddControllers();

// Pour dire dire que l'on va mapper les Controllers
app.MapControllers();

// On remplace 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Par 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyFirstAPI");
    });
}
```
- Localhost

http://localhost:5299 et on ajoute /swagger  
On arrive ensuite sur la page swagger  
http://localhost:5299/swagger/index.html

## Liaison avec la base de données

- Ligne de commande  
```
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Pomelo.EntityFrameworkCore.MySql
```

- DbContext  
 Créer un dossier DbContext  
 Créer un fichier DbContext.cs dedans
```
using Microsoft.EntityFrameworkCore;
using MyFirstWebAPI.Models;

namespace MyFirstWebApi.Context
{
    public class MyFirstWebApiDbContext : DbContext
    {
        // On dit que la table pour les User va s'appeler Users
        public MyFirstWebApiDbContext(DbContextOptions<MyFirstWebApiDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // L'entité User pour chaque user prendra la cé primaire user.Id
            modelBuilder.Entity<User>().HasKey(user => user.Id);
        }
        public DbSet<User> Users { get; set; }
    }
}
```
- Dans le fichier appsettings.json  
```
"ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=myfirstwebapi;user=root;password=;"
  }
  ```
- Dans Program.cs  
```
using MyFirstWebApi.Context;

// ... //

builder.Services.AddDbContextPool<MyFirstWebApiDbContext>(options =>
        {
            var connetionString = builder.Configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString));
        });
```

- Dans le fichier UserControllers.cs  
> Attention de ne pas oublier le constructeur
```
private MyFirstWebApiDbContext _context;

        public UserController(MyFirstWebApiDbContext context)
        {
            _context = context;
        }
```
- Migration de la base de donnée  
Ligne de commande :  
```
dotnet ef migrations add InitialMigration
// Remplacer le InitialMigration par ce que l'on veut préciser
dotnet ef database update
// si on ne fait pas update la database, c'est comme si on faisait un commit sans push
```

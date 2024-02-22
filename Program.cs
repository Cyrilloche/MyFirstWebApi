using Microsoft.EntityFrameworkCore;
using MyFirstWebApi.Context;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<MyFirstWebApiDbContext>(options =>
        {
            var connetionString = builder.Configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString));
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyFirstAPI");
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

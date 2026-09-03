using Microsoft.OpenApi.Models;
using MovieApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Controllers (padrão MVC / ApiController)
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Movie API - Catálogo de Filmes",
        Version = "v1",
        Description = "API REST para gerenciamento de um catálogo de filmes, " +
                      "desenvolvida em ASP.NET Core (.NET 10) com persistência em memória."
    });
});

// Contexto de dados em memória registrado como Singleton
builder.Services.AddSingleton<AppDbContext>();

var app = builder.Build();

// Swagger disponível também fora de Development para facilitar a correção/avaliação
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie API v1");
    options.RoutePrefix = string.Empty; // Swagger UI disponível na raiz "/"
});

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

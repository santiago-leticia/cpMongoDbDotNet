using jogosVideogame.Data;
using jogosVideogame.Services;
using jogosVideogame.Domain.Interfaces;
using jogosVideogame.Infrastruture.Repositorie;
using Scalar.AspNetCore;
using static jogosVideogame.Models.Jogo;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Mapeia o appsettings para a classe C#
builder.Services.Configure<JogoStoreDatabaseSettings>(
builder.Configuration.GetSection("JogoStoreDatabaseSettings"));

//Parte do MongoDB
//cliente MongoDB
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<JogoStoreDatabaseSettings>>().Value;

    var mongoSettings = MongoClientSettings.FromConnectionString(settings.ConnectionString);

    mongoSettings.ServerApi = new ServerApi(ServerApiVersion.V1);

    return new MongoClient(mongoSettings);
});
//Mongo database
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<JogoStoreDatabaseSettings>>().Value;

    var client = sp.GetRequiredService<IMongoClient>();

    return client.GetDatabase(settings.DatabaseName);
});


// Registra o contexto do MongoDB
builder.Services.AddSingleton<MongoDbContext>();

// Registra o repositório e o serviço
builder.Services.AddSingleton<IJogoRepository, JogoRepository>();
builder.Services.AddSingleton<JogosService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

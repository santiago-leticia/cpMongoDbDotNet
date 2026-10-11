using MongoDB.Driver;
using Microsoft.Extensions.Options;
using static jogosVideogame.Models.Jogo;
using MongoDB.Bson;

namespace jogosVideogame.Data
{
    public class MongoDbContext
    {
        //responsavel por fazer a conexão com o banco de dados MongoDB e fornecer acesso às coleções do banco de dados.
        private readonly IMongoClient _client;
        //responsável por representar o banco de dados MongoDB específico que você deseja acessar.
        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoClient client, IMongoDatabase database)
        {
            _client = client;

            _database = database;

            try
            {
                _client.GetDatabase("admin")
                    .RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("Successfully connected to MongoDB.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to MongoDB: {ex.Message}");
            }
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}

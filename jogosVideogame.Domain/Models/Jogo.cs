using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace jogosVideogame.Models
{
    public class Jogo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Titulo")]
        public string Titulo { get; set; } = null!;

        public string Plataforma { get; set; } = null!;

        public string Genero { get; set; } = null!;
        public decimal Preco { get; set; }

        public int AnoLancamento { get; set; }

        public int Estoque { get; set; }

        public class JogoStoreDatabaseSettings
        {
            public string ConnectionString { get; set; } = null!;
            public string DatabaseName { get; set; } = null!;
            public string JogoCollectionName { get; set; } = null!;
        }

        public class RelatorioEstoque
        {
            public string Plataforma { get; set; } = null!;
            public int QuantidadeTotalTitulos { get; set; }

            public decimal ValorTotalInvestido { get; set; }
        }
    }
}

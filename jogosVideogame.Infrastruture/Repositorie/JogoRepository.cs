using jogosVideogame.Data;
using jogosVideogame.Domain.Interfaces;
using jogosVideogame.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using static jogosVideogame.Models.Jogo;

namespace jogosVideogame.Infrastruture.Repositorie
{
    public class JogoRepository : IJogoRepository
    {
        private readonly IMongoCollection<Jogo> _jogos;

        public JogoRepository(MongoDbContext context, IOptions<JogoStoreDatabaseSettings> settings)
        {
            _jogos = context.GetCollection<Jogo>(settings.Value.JogoCollectionName);

        }

        public async Task<List<Jogo>> GetAllJogosAsync()
        {
            return await _jogos.Find(j => true).ToListAsync();
        }

        public async Task<Jogo> GetJogoByIdAsync(string? id)
        {
            var jogo = await _jogos.Find(j => j.Id == id).FirstOrDefaultAsync();
            return jogo;
        }

        public async Task<List<Jogo>> GetBuscarPlataformaPreco(string? plataforma, decimal preco)
        {
            var filter = Builders<Jogo>
                .Filter.And(
                    Builders<Jogo>.Filter.Eq(j => j.Plataforma, plataforma),
                    Builders<Jogo>.Filter.Lte(j => j.Preco, preco)
                );

            var buscar = await _jogos.Find(filter).ToListAsync();

            return buscar;
        }

        public async Task<List<RelatorioEstoque>> GetRelatorioEstoque()
        {
            var relatorioEstoque = await _jogos.Aggregate()
                .Group(
                    p => p.Plataforma,
                    r => new RelatorioEstoque
                    {
                        Plataforma = r.Key,
                        QuantidadeTotalTitulos = r.Count(),
                        ValorTotalInvestido = r.Sum(p => p.Preco * p.Estoque)
                    }
                )
                .ToListAsync();

            return relatorioEstoque;
        }

        public async Task AddAsync(Jogo jogo)
        {
            await _jogos.InsertOneAsync(jogo);
        }

        public async Task UpdateAsync(Jogo jogo)
        {
            var existingJogo = await _jogos.Find(j => j.Id == jogo.Id).FirstOrDefaultAsync( );
            if (existingJogo != null)
            {
                await _jogos.ReplaceOneAsync(j => j.Id == jogo.Id, jogo);
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(string? id)
        {
            var jogo = await _jogos.Find(j => j.Id == id).FirstOrDefaultAsync();
            if (jogo != null)
            {
                await _jogos.DeleteOneAsync(j => j.Id == id);
            }
            await Task.CompletedTask;
        }

    }
}

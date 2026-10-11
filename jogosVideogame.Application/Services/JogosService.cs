using jogosVideogame.Domain.Interfaces;
using jogosVideogame.Models;
using MongoDB.Driver;
using System.Formats.Asn1;
using static jogosVideogame.Models.Jogo;

namespace jogosVideogame.Services
{
    public class JogosService
    {
        private readonly IJogoRepository jogoRepository;

        public JogosService(IJogoRepository jogoRepository)
        {
            this.jogoRepository = jogoRepository;
        }

        public async Task<List<Jogo>> GetAllJogosAsync()
        {
            return await jogoRepository.GetAllJogosAsync();
        }

        public async Task<Jogo?> GetJogoById(string? id)
        {
            return await jogoRepository.GetJogoByIdAsync(id);
        }

        public async Task<List<Jogo>> GetBuscarPlataformaPreco(string? plataforma, decimal preco)
        {
            return await jogoRepository.GetBuscarPlataformaPreco(plataforma, preco);
        }

        public async Task<List<RelatorioEstoque>> GetRelatorioEstoquesAsync()
        {
            return await jogoRepository.GetRelatorioEstoque();
        }

        public async Task AddJogo(Jogo jogo)
        {
            await jogoRepository.AddAsync(jogo);
        }

        public async Task UpdateJogo(Jogo jogo)
        {
            await jogoRepository.UpdateAsync(jogo);
        }

        public async Task DeleteJogo(string? id)
        {
            await jogoRepository.DeleteAsync(id);
        }
    }
}

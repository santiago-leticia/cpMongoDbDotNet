using jogosVideogame.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static jogosVideogame.Models.Jogo;

namespace jogosVideogame.Domain.Interfaces
{
    public interface IJogoRepository
    {
        Task<List<Jogo>> GetAllJogosAsync();

        Task<Jogo> GetJogoByIdAsync(string? id);
        Task<List<Jogo>> GetBuscarPlataformaPreco(string? plataforma, decimal preco);
        
        Task<List<RelatorioEstoque>> GetRelatorioEstoque();
        Task AddAsync(Jogo jogo);
        Task UpdateAsync(Jogo jogo);
        Task DeleteAsync(string? id);
    }
}

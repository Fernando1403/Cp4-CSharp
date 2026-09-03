using MovieApi.Models;

namespace MovieApi.Data;

/// <summary>
/// Classe de contexto que simula um banco de dados em memória.
/// Registrada como Singleton para manter o estado durante o ciclo de vida da aplicação.
/// </summary>
public class AppDbContext
{
    public List<Filme> Filmes { get; set; } = new();

    private int _nextId = 1;

    public AppDbContext()
    {
        // Dados iniciais (seed) para facilitar os testes no Swagger.
        Filmes.Add(new Filme
        {
            Id = _nextId++,
            Titulo = "O Poderoso Chefão",
            Diretor = "Francis Ford Coppola",
            Genero = "Drama",
            AnoLancamento = 1972,
            DuracaoMinutos = 175
        });

        Filmes.Add(new Filme
        {
            Id = _nextId++,
            Titulo = "Interestelar",
            Diretor = "Christopher Nolan",
            Genero = "Ficção Científica",
            AnoLancamento = 2014,
            DuracaoMinutos = 169
        });
    }

    /// <summary>
    /// Gera o próximo Id disponível para um novo registro.
    /// </summary>
    public int GetNextId() => _nextId++;
}

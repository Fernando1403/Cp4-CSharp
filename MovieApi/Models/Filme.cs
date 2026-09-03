namespace MovieApi.Models;

/// <summary>
/// Entidade de domínio que representa um filme no catálogo.
/// </summary>
public class Filme
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Diretor { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public int AnoLancamento { get; set; }
    public int DuracaoMinutos { get; set; }
}

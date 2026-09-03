namespace MovieApi.DTOs;

/// <summary>
/// DTO usado para retornar os dados de um filme ao cliente.
/// </summary>
public class FilmeResponseDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Diretor { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public int AnoLancamento { get; set; }
    public int DuracaoMinutos { get; set; }
}

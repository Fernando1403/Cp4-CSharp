using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs;

/// <summary>
/// DTO usado para criação e atualização de um filme.
/// Não expõe o Id: ele é gerado/gerenciado pelo servidor.
/// </summary>
public class FilmeRequestDto
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    [MaxLength(150, ErrorMessage = "O título deve ter no máximo 150 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "O diretor é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O nome do diretor deve ter no máximo 100 caracteres.")]
    public string Diretor { get; set; } = string.Empty;

    [Required(ErrorMessage = "O gênero é obrigatório.")]
    [MaxLength(50, ErrorMessage = "O gênero deve ter no máximo 50 caracteres.")]
    public string Genero { get; set; } = string.Empty;

    [Range(1888, 2100, ErrorMessage = "Informe um ano de lançamento válido.")]
    public int AnoLancamento { get; set; }

    [Range(1, 1000, ErrorMessage = "A duração deve ser maior que zero minutos.")]
    public int DuracaoMinutos { get; set; }
}

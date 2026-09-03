using Microsoft.AspNetCore.Mvc;
using MovieApi.Data;
using MovieApi.DTOs;
using MovieApi.Models;

namespace MovieApi.Controllers;

/// <summary>
/// Endpoints para gerenciamento do catálogo de filmes.
/// </summary>
[ApiController]
[Route("api/v1/filmes")]
[Produces("application/json")]
public class FilmesController : ControllerBase
{
    private readonly AppDbContext _context;

    public FilmesController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>Lista todos os filmes cadastrados.</summary>
    /// <response code="200">Retorna a lista de filmes.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FilmeResponseDto>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<FilmeResponseDto>> GetAll()
    {
        var filmes = _context.Filmes.Select(MapToResponse);
        return Ok(filmes);
    }

    /// <summary>Busca um filme específico pelo Id.</summary>
    /// <response code="200">Filme encontrado.</response>
    /// <response code="404">Nenhum filme com o Id informado.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(FilmeResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<FilmeResponseDto> GetById(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);

        if (filme is null)
            return NotFound(new { mensagem = $"Filme com Id {id} não encontrado." });

        return Ok(MapToResponse(filme));
    }

    /// <summary>Cria um novo filme no catálogo.</summary>
    /// <response code="201">Filme criado com sucesso.</response>
    /// <response code="400">Dados inválidos no corpo da requisição.</response>
    [HttpPost]
    [ProducesResponseType(typeof(FilmeResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<FilmeResponseDto> Create([FromBody] FilmeRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var filme = new Filme
        {
            Id = _context.GetNextId(),
            Titulo = dto.Titulo,
            Diretor = dto.Diretor,
            Genero = dto.Genero,
            AnoLancamento = dto.AnoLancamento,
            DuracaoMinutos = dto.DuracaoMinutos
        };

        _context.Filmes.Add(filme);

        return CreatedAtAction(nameof(GetById), new { id = filme.Id }, MapToResponse(filme));
    }

    /// <summary>Atualiza os dados de um filme existente.</summary>
    /// <response code="200">Filme atualizado com sucesso.</response>
    /// <response code="400">Dados inválidos no corpo da requisição.</response>
    /// <response code="404">Nenhum filme com o Id informado.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(FilmeResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<FilmeResponseDto> Update(int id, [FromBody] FilmeRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);

        if (filme is null)
            return NotFound(new { mensagem = $"Filme com Id {id} não encontrado." });

        filme.Titulo = dto.Titulo;
        filme.Diretor = dto.Diretor;
        filme.Genero = dto.Genero;
        filme.AnoLancamento = dto.AnoLancamento;
        filme.DuracaoMinutos = dto.DuracaoMinutos;

        return Ok(MapToResponse(filme));
    }

    /// <summary>Remove um filme do catálogo pelo Id.</summary>
    /// <response code="204">Filme removido com sucesso.</response>
    /// <response code="404">Nenhum filme com o Id informado.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);

        if (filme is null)
            return NotFound(new { mensagem = $"Filme com Id {id} não encontrado." });

        _context.Filmes.Remove(filme);

        return NoContent();
    }

    private static FilmeResponseDto MapToResponse(Filme filme) => new()
    {
        Id = filme.Id,
        Titulo = filme.Titulo,
        Diretor = filme.Diretor,
        Genero = filme.Genero,
        AnoLancamento = filme.AnoLancamento,
        DuracaoMinutos = filme.DuracaoMinutos
    };
}

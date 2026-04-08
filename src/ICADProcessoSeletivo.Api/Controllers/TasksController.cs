using ICADProcessoSeletivo.Api.Data;
using ICADProcessoSeletivo.Api.DTOs;
using ICADProcessoSeletivo.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ICADProcessoSeletivo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _db;

    public TasksController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search)
    {
        var query = _db.Tasks.Include(t => t.Responsavel).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(t => t.Titulo.ToLower().Contains(search.ToLower()));

        var tasks = await query
            .OrderBy(t => t.DataEntrega)
            .Select(t => new TaskDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                DataEntrega = t.DataEntrega,
                ResponsavelId = t.ResponsavelId,
                ResponsavelNome = t.Responsavel.Name,
                Dificuldade = t.Dificuldade,
                Concluida = t.Concluida
            })
            .ToListAsync();

        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _db.Tasks
            .Include(t => t.Responsavel)
            .Where(t => t.Id == id)
            .Select(t => new TaskDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                DataEntrega = t.DataEntrega,
                ResponsavelId = t.ResponsavelId,
                ResponsavelNome = t.Responsavel.Name,
                Dificuldade = t.Dificuldade,
                Concluida = t.Concluida
            })
            .FirstOrDefaultAsync();

        if (task is null) return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
    {
        var user = await _db.Users.FindAsync(dto.ResponsavelId);
        if (user is null) return BadRequest(new { message = "Responsável não encontrado." });

        var task = new TaskItem
        {
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            DataEntrega = dto.DataEntrega,
            ResponsavelId = dto.ResponsavelId,
            Dificuldade = dto.Dificuldade,
            Concluida = false
        };

        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskDto dto)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null) return NotFound();

        var user = await _db.Users.FindAsync(dto.ResponsavelId);
        if (user is null) return BadRequest(new { message = "Responsável não encontrado." });

        task.Titulo = dto.Titulo;
        task.Descricao = dto.Descricao;
        task.DataEntrega = dto.DataEntrega;
        task.ResponsavelId = dto.ResponsavelId;
        task.Dificuldade = dto.Dificuldade;
        task.Concluida = dto.Concluida;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id}/concluida")]
    public async Task<IActionResult> ToggleConcluida(int id, [FromBody] bool concluida)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null) return NotFound();

        task.Concluida = concluida;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null) return NotFound();

        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}

using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Interfaces;
using SmartSchool.API.Models;
namespace SmartSchool.API.Controllers;
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProfessorController : ControllerBase
{
    private readonly IRepository _repo;
    public ProfessorController(IRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var result = _repo.GetAllProfessores();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var professor = _repo.GetProfessorById(id, false);
        if (professor == null) return BadRequest("O professor não foi encontrado");

        return Ok(professor);
    }

    [HttpPost]
    public IActionResult Post(Professor professor)
    {
        _repo.Add(professor);
        if (_repo.SaveChanges())
        {
            return Ok(professor);
        }
        return BadRequest("professor não cadastrado");
    }

    [HttpPut]
    public IActionResult Put(int id, Professor professor)
    {
        var prof = _repo.GetProfessorById(id, false);
        if (prof == null)
        {
            return BadRequest("Professor não encontrado");
        }

        _repo.Update(prof);
        if (_repo.SaveChanges())
        {
            return Ok(prof);
        }
        return BadRequest("Professor não atualizado");
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var professor = _repo.GetAlunoByIdAsync(id, false);
        if (professor == null) return BadRequest("O professor não foi encontrado");

        _repo.Update(professor);
        if (_repo.SaveChanges())
        {
            return Ok("Professor deletado");
        }

        return BadRequest("Professor não deletado");
    }
}

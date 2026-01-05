using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.DTOs;
using SmartSchool.API.Helpers;
using SmartSchool.API.Interfaces;
using SmartSchool.API.Models;
using System.Threading.Tasks;
namespace SmartSchool.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AlunoController : ControllerBase
{
    private readonly IRepository _repo;
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repo"></param>
    /// <param name="mapper"></param>
    public AlunoController(IRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    /// <summary>
    /// Método responsável para retornar todos os meus alunos
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
    {
        var alunos = await _repo.GetAllAlunosAsync(pageParams, true);

        var alunoResult = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

        Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);

        return Ok(alunoResult);
    }

    /// <summary>
    /// Método responsável por retornar um aluno especifico
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var aluno = await _repo.GetAlunoByIdAsync(id, false);
        if (aluno == null) return BadRequest("O aluno não foi encontrado");

        var alunoDto = _mapper.Map<AlunoDto>(aluno);

        return Ok(alunoDto);
    }

    /// <summary>
    /// Método responsável por retornar alunos por disciplina
    /// </summary>
    /// <param name="pageParams"></param>
    /// <param name="disciplinaId"></param>
    /// <returns></returns>
    [HttpGet("ByDisciplina/{disciplinaId}")] // Nova rota para filtrar por disciplina
    public async Task<IActionResult> GetByDisciplina(int disciplinaId, [FromQuery] PageParams pageParams)
    {
        var alunos = await _repo.GetAllAlunosByDisciplinaIdAsync(pageParams, disciplinaId, true);


        return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
    }
    /// <summary>
    /// Método responsável por criar um novo aluno
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Post(AlunoRegistrarDto model)
    {
        var aluno = _mapper.Map<Aluno>(model);

        _repo.Add(aluno);
        if (_repo.SaveChanges())
        {
            return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
        }

        return BadRequest("Aluno não cadastrado");
    }


    /// <summary>
    /// Metodo responsavel por atualizar um aluno especifico
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult Put(int id, AlunoRegistrarDto model)
    {
        var aluno = _repo.GetAlunoByIdAsync(id, false);
        if (aluno == null)
        {
            return BadRequest("Aluno não encontrado");
        }

        _mapper.Map(model, aluno);

        _repo.Update(aluno);
        if (_repo.SaveChanges())
        {
            return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
        }

        return BadRequest("Aluno não foi atualizado");

    }

    /// <summary>
    /// Método responsável por deletar um aluno especifico
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var aluno = await _repo.GetAlunoByIdAsync(id, false);
        if (aluno == null) return BadRequest("O aluno não foi encontrado");

        _repo.Update(aluno);
        if (_repo.SaveChanges())
        {
            return Ok("Aluno deletado");
        }

        return BadRequest("Aluno não deletado");
    }
}


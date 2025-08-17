using AutoMapper;
using ConsultasEF.Db;
using ConsultasEF.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeuProjeto.Models;
using SeuProjeto.Models.DTOs;

namespace SeuProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursosController : ControllerBase
    {
        private readonly ICursoService _cursoService;
        private readonly IMapper _mapper;

        public CursosController(ICursoService cursoService, IMapper mapper)
        {
            _cursoService = cursoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var cursos = await _cursoService.GetAll();
            return Ok(cursos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetById(Guid id)
        {
            var curso = await _cursoService.GetById(id);
            return Ok(curso);
        }

        [HttpGet("alunos-por-curso/{cursoId}")]
        public async Task<ActionResult> GetAllAlunosPorCurso(Guid cursoId)
        {
            var curso = await _cursoService.GetById(cursoId);
            return Ok(curso);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CursoEntradaDTO dto)
        {
            var curso = _mapper.Map<Curso>(dto);

            var cursoSaida = await _cursoService.Create(curso);

            _cursoService.Create(curso);

            return Ok(cursoSaida);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Curso curso)
        {
            await _cursoService.Update(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _cursoService.Delete(id);
            return NoContent();
        }

        [HttpGet("cursos-com-alunos")]
        public async Task<IActionResult> GetCursosComAlunos()
        {
            var cursos = await _cursoService.GetCursosComAlunos();
            return Ok(cursos);
        }

        [HttpGet("listar-cursos-com-mais-de-n-alunos/{minAlunos}")]
        public async Task<IActionResult> GetComMaisDeNAlunos(int minAlunos)
        {
            var cursos = await _cursoService.GetComMaisDeNAlunos(minAlunos);
            return Ok(cursos);
        }

        [HttpPatch("adicionar-curso")]
        public async Task<IActionResult> AdicionarCurso(Guid cursoId, Guid alunoId)
        {
            _cursoService.AdicionarCurso(cursoId, alunoId);
            return Ok("Curso adicionado com sucesso!");
        }

        [HttpPatch("remover-curso")]
        public async Task<IActionResult> RemoveCurso(Guid cursoId, Guid alunoId)
        {
            _cursoService.RemoveCurso(cursoId, alunoId);
            return Ok("Curso removido com sucesso!");
        }
    }
}
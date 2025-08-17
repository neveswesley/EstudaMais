using AutoMapper;
using ConsultasEF.Db;
using ConsultasEF.Db.Repositories.Interfaces;
using ConsultasEF.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeuProjeto.Models;
using SeuProjeto.Models.DTOs;

namespace SeuProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunoService _alunoService;
        private readonly IMapper _mapper;
        private readonly IValidator<Aluno> _alunoValidator;

        public AlunosController(IAlunoService alunoService, IMapper mapper, IValidator<Aluno> alunoValidator)
        {
            _alunoService = alunoService;
            _mapper = mapper;
            _alunoValidator = alunoValidator;
        }

        [HttpGet("retornar-ids")]
        public async Task<IActionResult> GetId()
        {
            var alunosIds = await _alunoService.GetId();
            return Ok(alunosIds);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var alunos = await _alunoService.GetAll();
            return Ok(alunos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var aluno = await _alunoService.GetById(id);
            return Ok(aluno);
        }

        [HttpPost("post-range")]
        public async Task<IActionResult> Post(List<AlunoEntradaDTO> alunos)
        {
            var alunoSaida = _mapper.Map<List<AlunoSaidaDTO>>(alunos);
            await _alunoService.CreateRange(alunos);
            return Ok(alunoSaida);
        }
        
        
        [HttpPost("post")]
        public async Task<IActionResult> Create([FromBody] AlunoEntradaDTO dto)
        {
            var alunoSaida = _mapper.Map<AlunoSaidaDTO>(dto);
            await _alunoService.Create(dto);

            return Ok(alunoSaida);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AlunoEntradaDTO dto)
        {
            await _alunoService.Update(id, dto);
            var alunoSaida = _mapper.Map<AlunoSaidaDTO>(dto);
            return Ok(alunoSaida);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _alunoService.Delete(id);
            return NoContent();
        }

        [HttpGet("alunos-por-cidade")]
        public async Task<IActionResult> GetAlunosPorCidade(string cidade)
        {
            var aluno = await _alunoService.GetAlunosPorCidade(cidade);
            return Ok(aluno);
        }

        [HttpGet("alunos-por-estado")]
        public async Task<IActionResult> GetAlunosEstado(string estado)
        {
            var aluno =  await _alunoService.GetAlunosPorEstado(estado);
            return Ok(aluno);
        }

        [HttpGet("matriculas-ativas")]
        public async Task<IActionResult> GetMatriculasAtivas()
        {
            var matriculas = await _alunoService.GetMatriculasAtivas();
            return Ok(matriculas);
        }

        [HttpGet("alunos-sem-curso")]
        public async Task<IActionResult> GetAlunosSemCurso()
        {
            var alunosSemCurso = await _alunoService.GetAlunosSemCurso();
            return Ok(alunosSemCurso);
        }

        [HttpGet("buscar-alunos-por-nome")]
        public async Task<IActionResult> GetAlunosPorNome(string nome)
        {
            var alunosPorNome = await _alunoService.GetAlunosPorNome(nome);
            return Ok(alunosPorNome);
        }

        [HttpGet("buscar-alunos-por-mensalidade")]
        public async Task<IActionResult> GetAlunosPorMensalidade(decimal mensalidade)
        {
            var mensalidades = await _alunoService.GetAlunosPorMensalidade(mensalidade);
            return Ok(mensalidades);
        }
    }
}
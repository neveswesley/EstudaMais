using AutoMapper;
using ConsultasEF.Db.Repositories.Interfaces;
using ConsultasEF.Services.Interfaces;
using SeuProjeto.Models;
using SeuProjeto.Models.DTOs;

namespace ConsultasEF.Services;

public class CursoService : ICursoService
{
    private readonly ICursoRepository _cursoRepository;
    private readonly IMapper _mapper;

    public CursoService(ICursoRepository cursoRepository,  IMapper mapper)
    {
        _cursoRepository = cursoRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<CursoSaidaDTO>> GetAll()
    {
        var cursos = await _cursoRepository.GetAll();
        return cursos;
    }

    public async Task<CursoSaidaDTO> GetById(Guid id)
    {
        var curso = await _cursoRepository.GetById(id);
        return curso;
    }

    public async Task<Curso> Create(Curso curso)
    {
        await _cursoRepository.Create(curso);
        return curso;
    }

    public async Task<Curso> Update(Guid id)
    {
        var cursoExistente = await _cursoRepository.GetById(id);
        var cursoSaida = _mapper.Map<Curso>(cursoExistente);
        await _cursoRepository.Update(id);
        return cursoSaida;
    }

    public async Task Delete(Guid id)
    {
        await _cursoRepository.Delete(id);
    }

    public async Task<IEnumerable<CursoComAlunosDTO>> GetCursosComAlunos()
    {
        var cursos = await _cursoRepository.GetCursosComAlunos();
        return cursos;
    }

    public Task<IEnumerable<CursosNomesDTO>> GetComMaisDeNAlunos(int minAlunos)
    {
        var cursos = _cursoRepository.GetComMaisDeNAlunos(minAlunos);
        return cursos;
    }

    public async Task AdicionarCurso(Guid cursoId, Guid alunoId)
    {
        await _cursoRepository.AdicionarCurso(cursoId, alunoId);
    }

    public async Task RemoveCurso(Guid cursoId, Guid alunoId)
    {
        await _cursoRepository.RemoveCurso(cursoId, alunoId);
    }
}
using SeuProjeto.Models;
using SeuProjeto.Models.DTOs;

namespace ConsultasEF.Services.Interfaces;

public interface ICursoService
{
    Task<IEnumerable<CursoSaidaDTO>> GetAll();
    Task<CursoSaidaDTO> GetById(Guid id);
    Task<Curso> Create(Curso curso);
    Task<Curso> Update(Guid id);
    Task Delete(Guid id);
    Task<IEnumerable<CursoComAlunosDTO>> GetCursosComAlunos();
    Task<IEnumerable<CursosNomesDTO>> GetComMaisDeNAlunos(int minAlunos);
    Task AdicionarCurso(Guid cursoId, Guid alunoId);
    Task RemoveCurso(Guid cursoId, Guid alunoId);
}
using ConsultasEF.Db.Repositories.Interfaces;

namespace ConsultasEF.Db.Repositories;

public interface IUnitOfWork
{
    IAlunoRepository Alunos { get; }
    ICursoRepository Cursos { get; }
    Task<bool> CommitAsync();
}
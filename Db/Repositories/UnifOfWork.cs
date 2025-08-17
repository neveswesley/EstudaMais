using ConsultasEF.Db.Repositories.Interfaces;

namespace ConsultasEF.Db.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ConsultasContext _context;

    public IAlunoRepository Alunos { get; }
    public ICursoRepository Cursos { get; }

    public UnitOfWork(ConsultasContext context)
    {
        _context = context;
        Alunos = new AlunoRepository(_context);
        Cursos = new CursoRepository(_context);
    }

    public async Task<bool> CommitAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
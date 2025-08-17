using ConsultasEF.Db.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using SeuProjeto.Models;
using SeuProjeto.Models.DTOs;

namespace ConsultasEF.Db.Repositories;

public class CursoRepository : ICursoRepository
{

    private readonly ConsultasContext _context;
    public CursoRepository(ConsultasContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<CursoSaidaDTO>> GetAll()
    {
        var cursos = await _context.Cursos.Select(x => new CursoSaidaDTO()
        {
            Id = x.Id,
            Nome = x.Nome,
            CargaHoraria = x.CargaHoraria,
            Preco = x.Preco,
            TotalAlunos = x.Alunos.Count(),
            Alunos = x.Alunos.Select(a => new AlunoCursoSaidaDTO()
            {
                Nome = a.Nome,
                Ativo = a.Ativo,
            })
        }).ToListAsync();

        return cursos;
    }

    public async Task<CursoSaidaDTO> GetById(Guid id)
    {
        var curso = await _context.Cursos.Select(a => new CursoSaidaDTO()
        {
            Id = a.Id,
            Nome = a.Nome,
            CargaHoraria = a.CargaHoraria,
            Preco = a.Preco,
            TotalAlunos = a.Alunos.Count(),
            Alunos = a.Alunos.Select(c => new AlunoCursoSaidaDTO()
            {
                Nome = c.Nome,
                Ativo = c.Ativo,
            })
        }).FirstOrDefaultAsync(x => x.Id == id);

        return curso;
    }

    public async Task<Curso> Create(Curso curso)
    {
        _context.Cursos.AddAsync(curso);
        return curso;
    }

    public async Task<Curso> Update(Guid id)
    {
        var cursoExistente = await _context.Cursos.FirstOrDefaultAsync(x => x.Id == id);
        _context.Cursos.Update(cursoExistente);
        return cursoExistente;
    }

    public async Task Delete(Guid id)
    {
        var curso = await _context.Cursos.FirstOrDefaultAsync(x => x.Id == id);
        _context.Cursos.Remove(curso);
    }

    public async Task<IEnumerable<CursoComAlunosDTO>> GetCursosComAlunos()
    {
        var cursos = await _context.Cursos
            .Include(x => x.Alunos).Where(a => a.Alunos.Any())
            .Select(a => new CursoComAlunosDTO()
            {
                Nome = a.Nome,
                Alunos = a.Alunos.Select(a => a.Nome).ToList()
            }).ToListAsync();


        return cursos;
    }

    public async Task<IEnumerable<CursosNomesDTO>> GetComMaisDeNAlunos(int minAlunos)
    {   
        var cursos = await _context.Cursos.Include(x => x.Alunos)
            .Where(a => a.Alunos.Count >= minAlunos)
            .Select(
            a => new CursosNomesDTO()
            {
                Nome = a.Nome
            }).ToListAsync();

        return cursos;
    }

    public async Task AdicionarCurso(Guid cursoId, Guid alunoId)
    {
        var aluno = await _context.Alunos.Include(a => a.Cursos).FirstOrDefaultAsync(x => x.Id == alunoId);
        if (aluno == null)
            throw new Exception("Aluno não encontrado!");

        var curso = await _context.Cursos.FindAsync(cursoId);
        if (curso == null)
            throw new Exception("Curso não encontrado!");

        if (aluno.Cursos.Any(c => c.Id == cursoId))
            throw new Exception($"O aluno {aluno.Nome} já está matriculado neste curso.");

        aluno.Cursos.Add(curso);
    }

    public async Task RemoveCurso(Guid cursoId, Guid alunoId)
    {
        var aluno = await _context.Alunos.Include(a => a.Cursos).FirstOrDefaultAsync(x => x.Id == alunoId);
        if (aluno == null)
            throw new Exception("Aluno não encontrado!");

        var curso = await _context.Cursos.FindAsync(cursoId);
        if (curso == null)
            throw new Exception("Curso não encontrado!");

        if (!aluno.Cursos.Any(c => c.Id == cursoId))
            throw new Exception($"O aluno {aluno.Nome} não está matriculado neste curso.");

        aluno.Cursos.Remove(curso);
    }
}
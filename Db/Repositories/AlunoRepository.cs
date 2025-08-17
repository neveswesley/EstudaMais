using System.Linq.Expressions;
using AutoMapper;
using ConsultasEF.Db.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using SeuProjeto.Models;
using SeuProjeto.Models.DTOs;

namespace ConsultasEF.Db.Repositories;

public class AlunoRepository : IAlunoRepository
{
    private readonly ConsultasContext _context;
    private readonly IMapper _mapper;

    public AlunoRepository(ConsultasContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AlunosIdsDTO>> GetId()
    {
        var ids = await _context.Alunos.Select(x => new AlunosIdsDTO()
        {
            Id = x.Id,
        }).ToListAsync();

        return ids;
    }

    public async Task<IEnumerable<AlunoSaidaDTO>> GetAll()
    {
        var alunos = await _context.Alunos.Select(x => new AlunoSaidaDTO()
        {
            Id = x.Id,
            Nome = x.Nome,
            Email = x.Email,
            Telefone = x.Telefone,
            Rua = x.Rua,
            Numero = x.Numero,
            Cidade = x.Cidade,
            Estado = x.Estado,
            CEP = x.CEP,
            DataNascimento = x.DataNascimento,
            DataCadastro = x.DataCadastro,
            Ativo = x.Ativo,
            Mensalidade = x.Mensalidade,
            Cursos = x.Cursos.Select(x => x.Nome).ToList()
        }).ToListAsync();

        return alunos;
    }

    public async Task<AlunoSaidaDTO> GetById(Guid id)
    {
        var aluno = await _context.Alunos.Select(x => new AlunoSaidaDTO()
        {
            Id = x.Id,
            Nome = x.Nome,
            Email = x.Email,
            Telefone = x.Telefone,
            Rua = x.Rua,
            Numero = x.Numero,
            Cidade = x.Cidade,
            Estado = x.Estado,
            CEP = x.CEP,
            DataNascimento = x.DataNascimento,
            DataCadastro = x.DataCadastro,
            Ativo = x.Ativo,
            Mensalidade = x.Mensalidade,
            Cursos = x.Cursos.Select(x => x.Nome).ToList()
        }).FirstOrDefaultAsync(a => a.Id == id);

        if (aluno == null)
            throw new NullReferenceException("Nenhum aluno encontrado");

        return aluno;
    }

    public async Task<AlunoSaidaDTO> Create(AlunoEntradaDTO dto)
    {
        var aluno = new Aluno
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Telefone = dto.Telefone,
            Rua = dto.Rua,
            Numero = dto.Numero,
            Cidade = dto.Cidade,
            Estado = dto.Estado,
            CEP = dto.CEP,
            DataNascimento = dto.DataNascimento,
            Mensalidade = dto.Mensalidade,
            DataCadastro = DateTime.UtcNow,
            Ativo = true
        };

        await _context.Alunos.AddAsync(aluno);
        await _context.SaveChangesAsync();

        var alunoSaida = new AlunoSaidaDTO
        {
            Id = aluno.Id,
            Nome = aluno.Nome,
            Email = aluno.Email,
            Telefone = aluno.Telefone,
            Rua = aluno.Rua,
            Numero = aluno.Numero,
            Cidade = aluno.Cidade,
            Estado = aluno.Estado,
            CEP = aluno.CEP,
            DataNascimento = aluno.DataNascimento,
            DataCadastro = aluno.DataCadastro,
            Ativo = aluno.Ativo,
            Mensalidade = aluno.Mensalidade,
            Cursos = aluno.Cursos?.Select(c => c.Nome).ToList() ?? new List<string>()
        };

        return alunoSaida;
    }


    public async Task<AlunoSaidaDTO> Update(Guid id, AlunoEntradaDTO dto)
    {
        var alunoExistente = await _context.Alunos
            .Include(a => a.Cursos) // Inclui os cursos se precisar
            .FirstOrDefaultAsync(a => a.Id == id);

        if (alunoExistente == null)
            throw new NullReferenceException("Aluno não encontrado");

        alunoExistente.Nome = dto.Nome;
        alunoExistente.Email = dto.Email;
        alunoExistente.Telefone = dto.Telefone;
        alunoExistente.Rua = dto.Rua;
        alunoExistente.Numero = dto.Numero;
        alunoExistente.Cidade = dto.Cidade;
        alunoExistente.Estado = dto.Estado;
        alunoExistente.CEP = dto.CEP;
        alunoExistente.DataNascimento = dto.DataNascimento;
        alunoExistente.Mensalidade = dto.Mensalidade;

        _context.Alunos.Update(alunoExistente);
        await _context.SaveChangesAsync();

        var alunoSaida = new AlunoSaidaDTO
        {
            Id = alunoExistente.Id,
            Nome = alunoExistente.Nome,
            Email = alunoExistente.Email,
            Telefone = alunoExistente.Telefone,
            Rua = alunoExistente.Rua,
            Numero = alunoExistente.Numero,
            Cidade = alunoExistente.Cidade,
            Estado = alunoExistente.Estado,
            CEP = alunoExistente.CEP,
            DataNascimento = alunoExistente.DataNascimento,
            DataCadastro = alunoExistente.DataCadastro,
            Ativo = alunoExistente.Ativo,
            Mensalidade = alunoExistente.Mensalidade,
            Cursos = alunoExistente.Cursos?.Select(c => c.Nome).ToList() ?? new List<string>()
        };

        return alunoSaida;
    }

    public async Task Delete(Guid id)
    {
        var aluno = await _context.Alunos.FirstOrDefaultAsync(a => a.Id == id);
        _context.Alunos.Remove(aluno);
    }

    public async Task<List<AlunosNomes>> GetAlunoAsync(Expression<Func<Aluno, bool>> filter)
    {
        var alunos = await _context.Alunos.Where(filter).ToListAsync();

        return alunos.Select(a => new AlunosNomes
        {
            Nome = a.Nome,
        }).ToList();
    }

    public async Task<List<AlunoSaidaDTO>> CreateRange(List<AlunoEntradaDTO> dtos)
    {
        var alunos = dtos.Select(dto => new Aluno
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Telefone = dto.Telefone,
            Rua = dto.Rua,
            Numero = dto.Numero,
            Cidade = dto.Cidade,
            Estado = dto.Estado,
            CEP = dto.CEP,
            DataNascimento = dto.DataNascimento,
            Mensalidade = dto.Mensalidade,
            DataCadastro = DateTime.UtcNow,
            Ativo = true
        }).ToList();

        await _context.Alunos.AddRangeAsync(alunos);
        await _context.SaveChangesAsync();

        var alunoSaida = alunos.Select(a => new AlunoSaidaDTO
        {
            Id = a.Id,
            Nome = a.Nome,
            Email = a.Email,
            Telefone = a.Telefone,
            Rua = a.Rua,
            Numero = a.Numero,
            Cidade = a.Cidade,
            Estado = a.Estado,
            CEP = a.CEP,
            DataNascimento = a.DataNascimento,
            DataCadastro = a.DataCadastro,
            Ativo = a.Ativo,
            Mensalidade = a.Mensalidade,
            Cursos = a.Cursos?.Select(c => c.Nome).ToList() ?? new List<string>()
        }).ToList();

        return alunoSaida;
    }
}
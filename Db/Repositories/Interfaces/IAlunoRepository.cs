using System.Linq.Expressions;
using SeuProjeto.Models;
using SeuProjeto.Models.DTOs;

namespace ConsultasEF.Db.Repositories.Interfaces;

public interface IAlunoRepository
{
    Task<IEnumerable<AlunosIdsDTO>> GetId();
    Task<IEnumerable<AlunoSaidaDTO>> GetAll();
    Task<AlunoSaidaDTO> GetById(Guid id);
    Task<AlunoSaidaDTO> Create(AlunoEntradaDTO dto);
    Task<List<AlunoSaidaDTO>> CreateRange(List<AlunoEntradaDTO> dtos);
    Task<AlunoSaidaDTO> Update(Guid id, AlunoEntradaDTO alunoEntradaDTO);
    Task Delete(Guid id);
    Task<List<AlunosNomes>> GetAlunoAsync(Expression<Func<Aluno, bool>> filter);



}
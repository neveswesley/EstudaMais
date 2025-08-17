using System.Linq.Expressions;
using SeuProjeto.Models;
using SeuProjeto.Models.DTOs;

namespace ConsultasEF.Services.Interfaces;

public interface IAlunoService
{
    public Task<IEnumerable<AlunosIdsDTO>> GetId();
    public Task<IEnumerable<AlunoSaidaDTO>> GetAll();
    public Task<AlunoSaidaDTO> GetById(Guid id);
    public Task<AlunoSaidaDTO> Create(AlunoEntradaDTO dto);
    public Task<IEnumerable<AlunoSaidaDTO>> CreateRange(List<AlunoEntradaDTO> dto);
    public Task<AlunoSaidaDTO> Update(Guid id, AlunoEntradaDTO dto);
    public Task Delete(Guid id);
    public Task<IEnumerable<AlunosNomes>> GetAlunosPorCidade(string cidade);
    public Task<IEnumerable<AlunosNomes>> GetAlunosPorEstado(string estado);
    public Task<IEnumerable<AlunosNomes>> GetMatriculasAtivas();
    public Task<IEnumerable<AlunosNomes>> GetAlunosSemCurso();
    public Task<IEnumerable<AlunosNomes>> GetAlunosPorNome(string nome);
    public Task<IEnumerable<AlunosPorMensalidadesDTO>> GetAlunosPorMensalidade(decimal mensalidade);
}
using System.Linq.Expressions;
using AutoMapper;
using ConsultasEF.Db.Repositories;
using ConsultasEF.Db.Repositories.Interfaces;
using ConsultasEF.Services.Interfaces;
using SeuProjeto.Models;
using SeuProjeto.Models.DTOs;

namespace ConsultasEF.Services;

public class AlunoService : IAlunoService
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AlunoService(IAlunoRepository alunoRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _alunoRepository = alunoRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<AlunosIdsDTO>> GetId()
    {
        var alunosIds = await _alunoRepository.GetId();
        return alunosIds;
    }

    public async Task<IEnumerable<AlunoSaidaDTO>> GetAll()
    {
        var alunos = await _alunoRepository.GetAll();
        return alunos;
    }

    public async Task<AlunoSaidaDTO> GetById(Guid id)
    {
        var aluno = await _alunoRepository.GetById(id);
        return aluno;
    }

    public async Task<AlunoSaidaDTO> Create(AlunoEntradaDTO dto)
    {
        await _alunoRepository.Create(dto);
        var alunoSaida = _mapper.Map<AlunoSaidaDTO>(dto);
        await _unitOfWork.CommitAsync();
        return alunoSaida;
    }

    public async Task<IEnumerable<AlunoSaidaDTO>> CreateRange(List<AlunoEntradaDTO> dtos)
    {
        await _alunoRepository.CreateRange(dtos);
        var alunoSaidas = _mapper.Map<List<AlunoSaidaDTO>>(dtos);
        await _unitOfWork.CommitAsync();
        return alunoSaidas;
    }

    public async Task<AlunoSaidaDTO> Update(Guid id, AlunoEntradaDTO dto)
    {
        var alunoExistente = await _alunoRepository.GetById(id);
        var alunoSaida = _mapper.Map<AlunoSaidaDTO>(dto);
        _mapper.Map(dto, alunoExistente);
        await _alunoRepository.Update(id, dto);
        await _unitOfWork.CommitAsync();
        return alunoSaida;
    }

    public async Task Delete(Guid id)
    {
        await _alunoRepository.Delete(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<AlunosNomes>> GetAlunosPorCidade(string cidade)
    {
        return await _alunoRepository.GetAlunoAsync(a=>a.Cidade == cidade);
    }

    public async Task<IEnumerable<AlunosNomes>> GetAlunosPorEstado(string estado)
    {
        return await _alunoRepository.GetAlunoAsync(a=>a.Estado == estado);
    }

    public async Task<IEnumerable<AlunosNomes>> GetMatriculasAtivas()
    {
        return await _alunoRepository.GetAlunoAsync(a => a.Ativo == true);
    }

    public async Task<IEnumerable<AlunosNomes>> GetAlunosSemCurso()
    {
        return await _alunoRepository.GetAlunoAsync(a=>a.Cursos.Count == 0);
    }

    public async Task<IEnumerable<AlunosNomes>> GetAlunosPorNome(string nome)
    {
        return await _alunoRepository.GetAlunoAsync(a=>a.Nome == nome);
    }

    public async Task<IEnumerable<AlunosPorMensalidadesDTO>> GetAlunosPorMensalidade(decimal mensalidade)
    {
        var alunos = await _alunoRepository.GetAlunoAsync(a=>a.Mensalidade == mensalidade);
        var alunoDTO = _mapper.Map<IEnumerable<AlunosPorMensalidadesDTO>>(alunos);
        
        return alunoDTO;
    }

}
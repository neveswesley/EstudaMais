using AutoMapper;
using SeuProjeto.Models;
using SeuProjeto.Models.DTOs;

namespace ConsultasEF.Profiles;

public class ConsultasProfile : Profile
{
    public ConsultasProfile()
    {
        CreateMap<Aluno, AlunoEntradaDTO>().ReverseMap();
        CreateMap<Aluno, AlunoSaidaDTO>().ReverseMap();
        CreateMap<Curso, CursoEntradaDTO>().ReverseMap();
        CreateMap<Curso, CursoSaidaDTO>().ReverseMap();
        CreateMap<AlunoEntradaDTO, AlunoSaidaDTO>().ReverseMap();
        
    }
}
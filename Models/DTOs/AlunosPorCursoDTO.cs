namespace SeuProjeto.Models.DTOs;

public class AlunosPorCursoDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public List<AlunosNomes> Alunos { get; set; }
}
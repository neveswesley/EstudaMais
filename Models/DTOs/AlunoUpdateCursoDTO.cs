namespace SeuProjeto.Models.DTOs;

public class AlunoUpdateCursoDTO
{
    public Guid CursoId { get; set; }
    public List<string> Curso { get; set; }
}
namespace SeuProjeto.Models.DTOs;

public class CursoSaidaDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; }
    public int CargaHoraria { get; set; }
    public decimal Preco { get; set; }

    // Relacionamento inverso
    public IEnumerable<AlunoCursoSaidaDTO> Alunos { get; set; } = new List<AlunoCursoSaidaDTO>();
    public int TotalAlunos { get; set; }

}
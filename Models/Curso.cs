namespace SeuProjeto.Models;

public class Curso
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; }
    public int CargaHoraria { get; set; }
    public decimal Preco { get; set; }

    // Relacionamento inverso
    public ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();
}
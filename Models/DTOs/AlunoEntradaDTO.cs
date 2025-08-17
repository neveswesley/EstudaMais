namespace SeuProjeto.Models.DTOs;

public class AlunoEntradaDTO
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }

    
    public string Rua { get; set; }
    public string Numero { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string CEP { get; set; }
    
    public DateTime DataNascimento { get; set; }
    public decimal Mensalidade { get; set; }

}
using System.Data;
using FluentValidation;
using SeuProjeto.Models;

namespace ConsultasEF.Validations;

public class AlunoValidation : AbstractValidator<Aluno>
{
    public AlunoValidation()
    {
        RuleFor(a => a.Id).NotEmpty();
        
        RuleFor(a=>a.Nome).MinimumLength(2).WithMessage("O nome é muito curto (min: 2)").MaximumLength(100).WithMessage("O nome é muito longo (max: 100)").NotEmpty().WithMessage("O nome é obrigatório.");
        
        RuleFor(a=>a.Email).NotEmpty().WithMessage("O e-mail é obrigatório.").EmailAddress().WithMessage("E-mail inválido.");
        
        RuleFor(a => a.Telefone).NotEmpty().WithMessage("O número de telefone é obrigatório.").Length(8,15).WithMessage("O telefone deve ter entre 8 e 15 caracteres.");
        
        RuleFor(a => a.Rua).MaximumLength(100).WithMessage("O nome da rua é muito longo (max. 100)");
        
        RuleFor(a=>a.Numero).MaximumLength(10).WithMessage("O número da casa é muito longo (max. 10)");
        
        RuleFor(a=>a.Cidade).NotEmpty().WithMessage("A cidade é obrigatória.").MinimumLength(2).WithMessage("O nome da cidade é muito curta. (min. 2)").MaximumLength(50).WithMessage("O nome da cidade é muito longo (max. 50)");
        
        RuleFor(a=>a.Estado).NotEmpty().WithMessage("O estado é obrigatório").Length(2).WithMessage("Por favor, digite apenas os dois dígitos da UF. (ex: SP");
        
        RuleFor(a => a.DataNascimento).NotEmpty().WithMessage("A data de nascimento é obrigatória.").Must(data => data <= DateTime.Today)
            .WithMessage("Data não pode ser futura.")
            .Must(dataNascimento => dataNascimento <= DateTime.Today.AddYears(-18))
            .WithMessage("O aluno deve ter pelo menos 18 anos de idade");

        RuleFor(a => a.DataCadastro).NotEmpty().WithMessage("A data de cadastro é obrigatória.").Must(data => data <= DateTime.Today).WithMessage("A data não pode ser futura.");
        
        RuleFor(a => a.Ativo).NotEmpty().WithMessage("O status da matrícula é obrigatório.");
        
        RuleFor(a => a.Mensalidade).NotEmpty().WithMessage("O valor da mensalidade é obrigatória.").Must(mensalidade => mensalidade > 0).WithMessage("O valor da mensagem deve ser maior que R$ 0,00");
    }

}
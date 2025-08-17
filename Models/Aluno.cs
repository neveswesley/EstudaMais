using System;
using System.Collections.Generic;

namespace SeuProjeto.Models
{
    public class Aluno
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        // Dados básicos
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        // Endereço
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }

        // Informações acadêmicas
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Today;
        public bool Ativo { get; set; } = true;
        public decimal Mensalidade { get; set; }

        // Relacionamentos (pra treinar Includes)
        public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
    }
}
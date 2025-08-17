using Microsoft.EntityFrameworkCore;
using SeuProjeto.Models;

namespace ConsultasEF.Db;

public class ConsultasContext : DbContext
{
    public ConsultasContext(DbContextOptions<ConsultasContext> options) : base(options)
    {
    }

    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Curso> Cursos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        #region Relacionamentos

        modelBuilder.Entity<Aluno>().HasMany(a => a.Cursos).WithMany(c => c.Alunos);

        #endregion

        #region FluentAPI

        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Email).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Telefone).IsRequired().HasMaxLength(50);
            
            entity.Property(e => e.Rua).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Numero).IsRequired().HasMaxLength(50);
            entity.Property(e=>e.Cidade).IsRequired().HasMaxLength(50);
            entity.Property(e=>e.Estado).IsRequired().HasMaxLength(50);
            entity.Property(e=>e.CEP).IsRequired().HasMaxLength(50);
            
            entity.Property(e=>e.DataNascimento).IsRequired().HasColumnType("datetime");
            entity.Property(e=>e.DataCadastro).IsRequired().HasColumnType("datetime");
            entity.Property(e => e.Ativo).IsRequired();
            
            entity.Property(e=>e.Mensalidade).IsRequired().HasColumnType("decimal(18,2)");
            
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.Property(e=>e.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(e=>e.Nome).IsRequired().HasMaxLength(50);
            entity.Property(e=>e.CargaHoraria).IsRequired();
            entity.Property(e => e.Preco).IsRequired().HasColumnType("decimal(18,2)");
        });

        #endregion
    }
}
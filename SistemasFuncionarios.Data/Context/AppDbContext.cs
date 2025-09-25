using Microsoft.EntityFrameworkCore;
using SistemasFuncionarios.Domain.Entities;

namespace SistemasFuncionarios.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Profissao> Profissoes { get; set; }
        public DbSet<Ctps> Ctps { get; set; }
        public DbSet<Cnh> Cnh { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<FuncionarioCurso> FuncionarioCursos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração para Profissoes
            modelBuilder.Entity<Profissao>(entity =>
            {
                entity.ToTable("profissoes");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nome).HasColumnName("nome").IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descricao).HasColumnName("descricao").HasColumnType("text");
                entity.Property(e => e.Ativo).HasColumnName("ativo").HasDefaultValue(true);
                entity.Property(e => e.DtCriacao).HasColumnName("dtcriacao").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.DtAtualizacao).HasColumnName("dtatualizacao").HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasIndex(e => e.Nome).IsUnique();
            });

            // Configuração para Funcionarios
            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.ToTable("funcionarios");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nome).HasColumnName("nome").IsRequired().HasMaxLength(150);
                entity.Property(e => e.CPF).HasColumnName("cpf").IsRequired().HasMaxLength(11);
                entity.Property(e => e.RG).HasColumnName("rg").HasMaxLength(15);
                entity.Property(e => e.DtNasc).HasColumnName("dtnasc").HasColumnType("date");
                entity.Property(e => e.Telefone).HasColumnName("telefone").HasMaxLength(15);
                entity.Property(e => e.Celular).HasColumnName("celular").HasMaxLength(15);
                entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(150);
                entity.Property(e => e.ProfissaoId).HasColumnName("profissao_id");
                entity.Property(e => e.Ativo).HasColumnName("ativo").HasDefaultValue(true);
                entity.Property(e => e.DtCriacao).HasColumnName("dtcriacao").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.DtAtualizacao).HasColumnName("dtatualizacao").HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasIndex(e => e.CPF).IsUnique();
                
                // Relacionamento com Profissao
                entity.HasOne(e => e.Profissao)
                    .WithMany(p => p.Funcionarios)
                    .HasForeignKey(e => e.ProfissaoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuração para CTPS
            modelBuilder.Entity<Ctps>(entity =>
            {
                entity.ToTable("ctps");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.FuncionarioId).HasColumnName("funcionario_id");
                entity.Property(e => e.Numero).HasColumnName("numero").IsRequired().HasMaxLength(20);
                entity.Property(e => e.Serie).HasColumnName("serie").IsRequired().HasMaxLength(10);
                entity.Property(e => e.UfEmissao).HasColumnName("uf_emissao").IsRequired().HasMaxLength(2);
                entity.Property(e => e.DtEmissao).HasColumnName("dtemissao").HasColumnType("date");
                entity.Property(e => e.DtCriacao).HasColumnName("dtcriacao").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.DtAtualizacao).HasColumnName("dtatualizacao").HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasIndex(e => e.FuncionarioId).IsUnique();
                
                // Relacionamento com Funcionario (1:1)
                entity.HasOne(e => e.Funcionario)
                    .WithOne(f => f.Ctps)
                    .HasForeignKey<Ctps>(e => e.FuncionarioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuração para CNH
            modelBuilder.Entity<Cnh>(entity =>
            {
                entity.ToTable("cnh");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.FuncionarioId).HasColumnName("funcionario_id");
                entity.Property(e => e.Numero).HasColumnName("numero").IsRequired().HasMaxLength(15);
                entity.Property(e => e.Categoria).HasColumnName("categoria").IsRequired().HasMaxLength(5);
                entity.Property(e => e.UfEmissao).HasColumnName("uf_emissao").IsRequired().HasMaxLength(2);
                entity.Property(e => e.DtValidade).HasColumnName("dtvalidade").HasColumnType("date");
                entity.Property(e => e.DtCriacao).HasColumnName("dtcriacao").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.DtAtualizacao).HasColumnName("dtatualizacao").HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasIndex(e => e.FuncionarioId).IsUnique();
                entity.HasIndex(e => e.Numero).IsUnique();
                
                // Relacionamento com Funcionario (1:1)
                entity.HasOne(e => e.Funcionario)
                    .WithOne(f => f.Cnh)
                    .HasForeignKey<Cnh>(e => e.FuncionarioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuração para Cursos
            modelBuilder.Entity<Curso>(entity =>
            {
                entity.ToTable("cursos");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nome).HasColumnName("nome").IsRequired().HasMaxLength(150);
                entity.Property(e => e.Instituicao).HasColumnName("instituicao").HasMaxLength(150);
                entity.Property(e => e.CargaHoraria).HasColumnName("carga_horaria");
                entity.Property(e => e.Descricao).HasColumnName("descricao").HasColumnType("text");
                entity.Property(e => e.Ativo).HasColumnName("ativo").HasDefaultValue(true);
                entity.Property(e => e.DtCriacao).HasColumnName("dtcriacao").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.DtAtualizacao).HasColumnName("dtatualizacao").HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Configuração para FuncionarioCurso
            modelBuilder.Entity<FuncionarioCurso>(entity =>
            {
                entity.ToTable("funcionario_cursos");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.FuncionarioId).HasColumnName("funcionario_id");
                entity.Property(e => e.CursoId).HasColumnName("curso_id");
                entity.Property(e => e.DtConclusao).HasColumnName("dtconclusao").HasColumnType("date");
                entity.Property(e => e.CertificadoNumero).HasColumnName("certificado_numero").HasMaxLength(50);
                entity.Property(e => e.Observacoes).HasColumnName("observacoes").HasColumnType("text");
                entity.Property(e => e.DtCriacao).HasColumnName("dtcriacao").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.DtAtualizacao).HasColumnName("dtatualizacao").HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasIndex(e => new { e.FuncionarioId, e.CursoId }).IsUnique();
                
                // Relacionamentos
                entity.HasOne(e => e.Funcionario)
                    .WithMany(f => f.FuncionarioCurso)
                    .HasForeignKey(e => e.FuncionarioId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Curso)
                    .WithMany(c => c.FuncionarioCursos)
                    .HasForeignKey(e => e.CursoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuração para Enderecos
            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.ToTable("enderecos");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.FuncionarioId).HasColumnName("funcionario_id");
                entity.Property(e => e.TpEndereco).HasColumnName("tp_endereco").HasMaxLength(20).HasDefaultValue("RESIDENCIAL");
                entity.Property(e => e.Logradouro).HasColumnName("logradouro").IsRequired().HasMaxLength(200);
                entity.Property(e => e.Numero).HasColumnName("numero").HasMaxLength(10);
                entity.Property(e => e.Complemento).HasColumnName("complemento").HasMaxLength(100);
                entity.Property(e => e.Bairro).HasColumnName("bairro").IsRequired().HasMaxLength(100);
                entity.Property(e => e.Cidade).HasColumnName("cidade").IsRequired().HasMaxLength(100);
                entity.Property(e => e.UF).HasColumnName("uf").IsRequired().HasMaxLength(2);
                entity.Property(e => e.CEP).HasColumnName("cep").IsRequired().HasMaxLength(8);
                entity.Property(e => e.Principal).HasColumnName("principal").HasDefaultValue(false);
                entity.Property(e => e.Ativo).HasColumnName("ativo").HasDefaultValue(true);
                entity.Property(e => e.DtCriacao).HasColumnName("dtcriacao").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.DtAtualizacao).HasColumnName("dtatualizacao").HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Relacionamento com Funcionario
                entity.HasOne(e => e.Funcionario)
                    .WithMany(f => f.Enderecos)
                    .HasForeignKey(e => e.FuncionarioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.Entity.GetType().GetProperty("DtAtualizacao") != null)
                {
                    entry.Property("DtAtualizacao").CurrentValue = DateTime.UtcNow;
                }
            }
        }
    }
}
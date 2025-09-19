using System.ComponentModel.DataAnnotations;

namespace SistemasFuncionarios.Domain.Entities
{
    public class Funcionario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do funcionário é obrigatório.")]
        [StringLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter 11 dígitos.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter apenas números.")]
        public string CPF { get; set; } = string.Empty;

        [StringLength(15, ErrorMessage = "RG deve ter no máximo 15 caracteres.")]
        public string? RG { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [Display(Name = "Data de Nascimento")]
        public DateTime DtNasc { get; set; }

        [StringLength(15, ErrorMessage = "Telefone deve ter no máximo 15 caracteres.")]
        public string? Telefone { get; set; }

        [StringLength(15, ErrorMessage = "Celular deve ter no máximo 15 caracteres.")]
        public string? Celular { get; set; }

        [StringLength(150, ErrorMessage = "Email deve ter no máximo 150 caracteres.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Profissão é obrigatória.")]
        [Display(Name = "Profissão")]
        public int ProfissaoId { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DtCriacao { get; set; } = DateTime.UtcNow;

        public DateTime DtAtualizacao { get; set; } = DateTime.UtcNow;

        public virtual Profissao Profissao { get; set; } = null!;
        public virtual Ctps? Ctps { get; set; }
        public virtual Cnh? Cnh { get; set; }
        public virtual ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
        public virtual ICollection<FuncionarioCurso> FuncionarioCurso { get; set; } = new List<FuncionarioCurso>();
    }
}
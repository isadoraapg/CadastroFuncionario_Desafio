using System.ComponentModel.DataAnnotations;

namespace SistemaFuncionarios.Domain.Entities
{
    public class Cnh
    {
        public int Id { get; set; }
        
        [Required]
        public int FuncionarioId { get; set; }
        
        [Required(ErrorMessage = "Número da CNH é obrigatório")]
        [StringLength(15, ErrorMessage = "Número deve ter no máximo 15 caracteres")]
        public string Numero { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Categoria é obrigatória")]
        [StringLength(5, ErrorMessage = "Categoria deve ter no máximo 5 caracteres")]
        public string Categoria { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "UF de emissão é obrigatória")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "UF deve ter exatamente 2 caracteres")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "UF deve conter apenas letras maiúsculas")]
        [Display(Name = "UF de Emissão")]
        public string UfEmissao { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Data de validade é obrigatória")]
        [Display(Name = "Data de Validade")]
        public DateTime DtValidade { get; set; }
        
        public DateTime DtCriacao { get; set; } = DateTime.UtcNow;
        
        public DateTime DtAtualizacao { get; set; } = DateTime.UtcNow;
        
        public virtual Funcionario Funcionario { get; set; } = null!;
    }
}
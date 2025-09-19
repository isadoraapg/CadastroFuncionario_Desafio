using System.ComponentModel.DataAnnotations;

namespace SistemaFuncionarios.Domain.Entities
{
    public class Ctps
    {
        public int Id { get; set; }
        
        [Required]
        public int FuncionarioId { get; set; }
        
        [Required(ErrorMessage = "Número da CTPS é obrigatório")]
        [StringLength(20, ErrorMessage = "Número deve ter no máximo 20 caracteres")]
        public string Numero { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Série da CTPS é obrigatória")]
        [StringLength(10, ErrorMessage = "Série deve ter no máximo 10 caracteres")]
        public string Serie { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "UF de emissão é obrigatória")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "UF deve ter exatamente 2 caracteres")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "UF deve conter apenas letras maiúsculas")]
        [Display(Name = "UF de Emissão")]
        public string UfEmissao { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Data de emissão é obrigatória")]
        [Display(Name = "Data de Emissão")]
        public DateTime DtEmissao { get; set; }
        
        public DateTime DtCriacao { get; set; } = DateTime.UtcNow;
        
        public DateTime DtAtualizacao { get; set; } = DateTime.UtcNow;
        
        public virtual Funcionario Funcionario { get; set; } = null!;
    }
}
using System.ComponentModel.DataAnnotations;

namespace SistemasFuncionarios.Domain.Entities
{
    public class Endereco
    {
        public int Id { get; set; }
        
        [Required]
        public int FuncionarioId { get; set; }
        
        [StringLength(20, ErrorMessage = "Tipo de endereço deve ter no máximo 20 caracteres")]
        [Display(Name = "Tipo de Endereço")]
        public string TpEndereco { get; set; } = "RESIDENCIAL";
        
        [Required(ErrorMessage = "Logradouro é obrigatório")]
        [StringLength(200, ErrorMessage = "Logradouro deve ter no máximo 200 caracteres")]
        public string Logradouro { get; set; } = string.Empty;
        
        [StringLength(10, ErrorMessage = "Número deve ter no máximo 10 caracteres")]
        public string? Numero { get; set; }
        
        [StringLength(100, ErrorMessage = "Complemento deve ter no máximo 100 caracteres")]
        public string? Complemento { get; set; }
        
        [Required(ErrorMessage = "Bairro é obrigatório")]
        [StringLength(100, ErrorMessage = "Bairro deve ter no máximo 100 caracteres")]
        public string Bairro { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Cidade é obrigatória")]
        [StringLength(100, ErrorMessage = "Cidade deve ter no máximo 100 caracteres")]
        public string Cidade { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "UF é obrigatória")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "UF deve ter exatamente 2 caracteres")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "UF deve conter apenas letras maiúsculas")]
        public string UF { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "CEP é obrigatório")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "CEP deve ter exatamente 8 dígitos")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "CEP deve conter apenas números")]
        public string CEP { get; set; } = string.Empty;
        
        [Display(Name = "Endereço Principal")]
        public bool Principal { get; set; } = false;
        
        public bool Ativo { get; set; } = true;
        
        public DateTime DtCriacao { get; set; } = DateTime.UtcNow;
        
        public DateTime DtAtualizacao { get; set; } = DateTime.UtcNow;
        
        public virtual Funcionario Funcionario { get; set; } = null!;
    }
}
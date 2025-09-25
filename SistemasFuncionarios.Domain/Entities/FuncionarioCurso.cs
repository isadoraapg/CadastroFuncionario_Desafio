using System.ComponentModel.DataAnnotations;

namespace SistemasFuncionarios.Domain.Entities
{
    public class FuncionarioCurso
    {
        public int Id { get; set; }
        
        [Required]
        public int FuncionarioId { get; set; }
        
        [Required]
        public int CursoId { get; set; }
        
        [Display(Name = "Data de Conclusão")]
        public DateTime? DtConclusao { get; set; }
        
        [StringLength(50, ErrorMessage = "Número do certificado deve ter no máximo 50 caracteres")]
        [Display(Name = "Número do Certificado")]
        public string? CertificadoNumero { get; set; }
        
        [StringLength(500, ErrorMessage = "Observações devem ter no máximo 500 caracteres")]
        public string? Observacoes { get; set; }
        
        public DateTime DtCriacao { get; set; } = DateTime.UtcNow;
        
        public DateTime DtAtualizacao { get; set; } = DateTime.UtcNow;
        
        public virtual Funcionario Funcionario { get; set; } = null!;
        public virtual Curso Curso { get; set; } = null!;
    }
}
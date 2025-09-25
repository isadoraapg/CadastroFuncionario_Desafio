using System.ComponentModel.DataAnnotations;

namespace SistemasFuncionarios.Domain.Entities
{
    public class Curso
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nome do curso é obrigatório")]
        [StringLength(150, ErrorMessage = "Nome deve ter no máximo 150 caracteres")]
        public string Nome { get; set; } = string.Empty;
        
        [StringLength(150, ErrorMessage = "Instituição deve ter no máximo 150 caracteres")]
        public string? Instituicao { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Carga horária deve ser maior que zero")]
        [Display(Name = "Carga Horária")]
        public int? CargaHoraria { get; set; }
        
        [StringLength(500, ErrorMessage = "Descrição deve ter no máximo 500 caracteres")]
        public string? Descricao { get; set; }
        
        public bool Ativo { get; set; } = true;
        
        public DateTime DtCriacao { get; set; } = DateTime.UtcNow;
        
        public DateTime DtAtualizacao { get; set; } = DateTime.UtcNow;
        
        public virtual ICollection<FuncionarioCurso> FuncionarioCursos { get; set; } = new List<FuncionarioCurso>();
    }
}
using System.ComponentModel.DataAnnotations;

namespace SistemasFuncionarios.Domain.Entities
{
    public class Profissao
    {
        //autoimplement - por convenção do ef core Id do tipo int vira PK
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da profissão é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty; //indica que nome é obrigatório

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string? Descricao { get; set; }  //"string?" indica que pode ser nula

        public bool Ativo { get; set; } = true; 

        public DateTime DtCriacao { get; set; } = DateTime.UtcNow;

        public DateTime DtAtualizacao { get; set; } = DateTime.UtcNow; 

        //indica um relacionamento com outra entidade, no caso "funcionario"
        //representa uma coleção de funcionários que tem essa profissão
        public virtual ICollection<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
    }
}
using SistemasFuncionarios.Domain.Entities;

namespace SistemaFuncionarios.Domain.Interfaces
{
    public interface IFuncionarioRepository
    {
        Task<IEnumerable<Funcionario>> GetAllAsync();
        Task<IEnumerable<Funcionario>> GetAllActiveAsync();
        Task<Funcionario?> GetByIdAsync(int id);
        Task<Funcionario?> GetByIdWithDetailsAsync(int id);
        Task<Funcionario?> GetByCpfAsync(string cpf);
        Task<Funcionario?> GetByEmailAsync(string email);
        Task<Funcionario> AddAsync(Funcionario funcionario);
        Task<Funcionario> UpdateAsync(Funcionario funcionario);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> CpfExistsAsync(string cpf, int? excludeId = null);
        Task<bool> EmailExistsAsync(string email, int? excludeId = null);
        
        Task AddEnderecoAsync(Endereco endereco);
        Task UpdateEnderecoAsync(Endereco endereco);
        Task RemoveEnderecoAsync(int enderecoId);
        
        Task AddFuncionarioCursoAsync(FuncionarioCurso funcionarioCurso);
        Task RemoveFuncionarioCursoAsync(int funcionarioId, int cursoId);
    }
}
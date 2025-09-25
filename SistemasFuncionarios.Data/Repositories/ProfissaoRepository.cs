using SistemasFuncionarios.Domain.Entities;

namespace SistemasFuncionarios.Domain.Interfaces
{
    public interface IProfissaoRepository
    {
        Task<IEnumerable<Profissao>> GetAllAsync();
        Task<IEnumerable<Profissao>> GetAllActiveAsync();
        Task<Profissao?> GetByIdAsync(int id);
        Task<Profissao> AddAsync(Profissao profissao);
        Task<Profissao> UpdateAsync(Profissao profissao);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> NomeExistsAsync(string nome, int? excludeId = null);
    }
}
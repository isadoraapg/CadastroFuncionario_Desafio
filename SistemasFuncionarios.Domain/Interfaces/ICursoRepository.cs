using SistemaFuncionarios.Domain.Entities;

namespace SistemaFuncionarios.Domain.Interfaces
{
    public interface ICursoRepository
    {
        Task<IEnumerable<Curso>> GetAllAsync();
        Task<IEnumerable<Curso>> GetAllActiveAsync();
        Task<Curso?> GetByIdAsync(int id);
        Task<Curso> AddAsync(Curso curso);
        Task<Curso> UpdateAsync(Curso curso);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
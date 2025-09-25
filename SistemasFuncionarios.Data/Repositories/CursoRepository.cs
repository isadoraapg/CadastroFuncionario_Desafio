using Microsoft.EntityFrameworkCore;
using SistemasFuncionarios.Data.Context;
using SistemasFuncionarios.Domain.Entities;
using SistemasFuncionarios.Domain.Interfaces;

namespace SistemasFuncionarios.Data.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly AppDbContext _context;

        public CursoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Curso>> GetAllAsync()
        {
            return await _context.Cursos
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Curso>> GetAllActiveAsync()
        {
            return await _context.Cursos
                .Where(c => c.Ativo)
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<Curso?> GetByIdAsync(int id)
        {
            return await _context.Cursos
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Curso> AddAsync(Curso curso)
        {
            await _context.Cursos.AddAsync(curso);
            await _context.SaveChangesAsync();
            return curso;
        }

        public async Task<Curso> UpdateAsync(Curso curso)
        {
            _context.Cursos.Update(curso);
            await _context.SaveChangesAsync();
            return curso;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var curso = await GetByIdAsync(id);
            if (curso == null)
                return false;


            curso.Ativo = false;
            await UpdateAsync(curso);
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Cursos
                .AnyAsync(c => c.Id == id);
        }
    }
}
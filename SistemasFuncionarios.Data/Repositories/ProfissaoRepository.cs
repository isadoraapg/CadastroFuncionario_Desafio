using Microsoft.EntityFrameworkCore;
using SistemasFuncionarios.Data.Context;
using SistemasFuncionarios.Domain.Entities;
using SistemasFuncionarios.Domain.Interfaces;

namespace SistemasFuncionarios.Data.Repositories
{
    public class ProfissaoRepository : IProfissaoRepository
    {
        private readonly AppDbContext _context;

        public ProfissaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profissao>> GetAllAsync()
        {
            return await _context.Profissoes
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Profissao>> GetAllActiveAsync()
        {
            return await _context.Profissoes
                .Where(p => p.Ativo)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<Profissao?> GetByIdAsync(int id)
        {
            return await _context.Profissoes
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Profissao> AddAsync(Profissao profissao)
        {
            await _context.Profissoes.AddAsync(profissao);
            await _context.SaveChangesAsync();
            return profissao;
        }

        public async Task<Profissao> UpdateAsync(Profissao profissao)
        {
            _context.Profissoes.Update(profissao);
            await _context.SaveChangesAsync();
            return profissao;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var profissao = await GetByIdAsync(id);
            if (profissao == null)
                return false;

            
            profissao.Ativo = false;
            await UpdateAsync(profissao);
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Profissoes
                .AnyAsync(p => p.Id == id);
        }

        public async Task<bool> NomeExistsAsync(string nome, int? excludeId = null)
        {
            var query = _context.Profissoes.Where(p => p.Nome.ToLower() == nome.ToLower());
            
            if (excludeId.HasValue)
                query = query.Where(p => p.Id != excludeId.Value);
            
            return await query.AnyAsync();
        }
    }
}
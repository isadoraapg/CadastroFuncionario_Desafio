using Microsoft.EntityFrameworkCore;
using SistemasFuncionarios.Data.Context;
using SistemasFuncionarios.Domain.Entities;
using SistemasFuncionarios.Domain.Interfaces;

namespace SistemasFuncionarios.Data.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly AppDbContext _context;

        public FuncionarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Funcionario>> GetAllAsync()
        {
            return await _context.Funcionarios
                .Include(f => f.Profissao)
                .OrderBy(f => f.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Funcionario>> GetAllActiveAsync()
        {
            return await _context.Funcionarios
                .Include(f => f.Profissao)
                .Where(f => f.Ativo)
                .OrderBy(f => f.Nome)
                .ToListAsync();
        }

        public async Task<Funcionario?> GetByIdAsync(int id)
        {
            return await _context.Funcionarios
                .Include(f => f.Profissao)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Funcionario?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Funcionarios
                .Include(f => f.Profissao)
                .Include(f => f.Ctps)
                .Include(f => f.Cnh)
                .Include(f => f.Enderecos.Where(e => e.Ativo))
                .Include(f => f.FuncionarioCurso)
                    .ThenInclude(fc => fc.Curso)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Funcionario?> GetByCpfAsync(string cpf)
        {
            return await _context.Funcionarios
                .Include(f => f.Profissao)
                .FirstOrDefaultAsync(f => f.CPF == cpf);
        }

        public async Task<Funcionario?> GetByEmailAsync(string email)
        {
            return await _context.Funcionarios
                .Include(f => f.Profissao)
                .FirstOrDefaultAsync(f => f.Email == email);
        }

        public async Task<Funcionario> AddAsync(Funcionario funcionario)
        {
            await _context.Funcionarios.AddAsync(funcionario);
            await _context.SaveChangesAsync();
            return funcionario;
        }

        public async Task<Funcionario> UpdateAsync(Funcionario funcionario)
        {
            _context.Funcionarios.Update(funcionario);
            await _context.SaveChangesAsync();
            return funcionario;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var funcionario = await GetByIdAsync(id);
            if (funcionario == null)
                return false;

            funcionario.Ativo = false;
            await UpdateAsync(funcionario);
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Funcionarios
                .AnyAsync(f => f.Id == id);
        }

        public async Task<bool> CpfExistsAsync(string cpf, int? excludeId = null)
        {
            var query = _context.Funcionarios.Where(f => f.CPF == cpf);
            
            if (excludeId.HasValue)
                query = query.Where(f => f.Id != excludeId.Value);
            
            return await query.AnyAsync();
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            var query = _context.Funcionarios.Where(f => f.Email == email);
            
            if (excludeId.HasValue)
                query = query.Where(f => f.Id != excludeId.Value);
            
            return await query.AnyAsync();
        }

        public async Task AddEnderecoAsync(Endereco endereco)
        {
            await _context.Enderecos.AddAsync(endereco);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEnderecoAsync(Endereco endereco)
        {
            _context.Enderecos.Update(endereco);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveEnderecoAsync(int enderecoId)
        {
            var endereco = await _context.Enderecos.FindAsync(enderecoId);
            if (endereco != null)
            {
                endereco.Ativo = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddFuncionarioCursoAsync(FuncionarioCurso funcionarioCurso)
        {
            await _context.FuncionarioCursos.AddAsync(funcionarioCurso);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFuncionarioCursoAsync(int funcionarioId, int cursoId)
        {
            var funcionarioCurso = await _context.FuncionarioCursos
                .FirstOrDefaultAsync(fc => fc.FuncionarioId == funcionarioId && fc.CursoId == cursoId);
            
            if (funcionarioCurso != null)
            {
                _context.FuncionarioCursos.Remove(funcionarioCurso);
                await _context.SaveChangesAsync();
            }
        }
    }
}
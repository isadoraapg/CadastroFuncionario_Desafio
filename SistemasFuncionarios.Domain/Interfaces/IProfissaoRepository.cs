//Interface: declara métodos que uma classe obrigatoriamente precisa implementar

using SistemaFuncionarios.Domain.Entities;

namespace SistemaFuncionarios.Domain.Interfaces
{
    public interface IProfissaoRepository
    {
        //Task: assíncrono, nçao bloqueia a aplicação enquanto espera o bd responder
        //Busca todas as profissões - lista
        Task<IEnumerable<Profissao>> GetAllAsync();
        //Busca profissões ativas 
        Task<IEnumerable<Profissao>> GetAllActiveAsync();
        //Busca profissão pelo Id
        Task<Profissao?> GetByIdAsync(int id);
        //Adiciona profissão
        Task<Profissao> AddAsync(Profissao profissao);
        //Atualiza profissão
        Task<Profissao> UpdateAsync(Profissao profissao);
        //Deleta profissão
        Task<bool> DeleteAsync(int id);
        //verifica se existe
        Task<bool> ExistsAsync(int id);
        //Verifica se já existe o nome da profissão
        Task<bool> NomeExistsAsync(string nome, int? excludeId = null);

        //As tasks são implementadas no Repository
    }
}




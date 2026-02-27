using GerenciaTarefas.API.DTOs;
using GerenciaTarefas.API.Models;

namespace GerenciaTarefas.API.Repository
{
    public interface ITarefasRepository
    {
        public Task<List<TarefaResumidaDTO>> ListAll();
        public Task<Tarefa?> GetById(int id);
        public Task Create(Tarefa tarefa);
        public Task<bool> Update(int id, Tarefa tarefa);
        public Task<bool> Delete(int id);
    }
}

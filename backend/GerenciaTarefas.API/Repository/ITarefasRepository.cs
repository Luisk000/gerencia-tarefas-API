using GerenciaTarefas.API.DTOs;
using GerenciaTarefas.API.Models;

namespace GerenciaTarefas.API.Repository
{
    public interface ITarefasRepository
    {
        public Task<List<Tarefa>> Get();
        public Task<Tarefa?> GetById(int id);
        public Task Create(Tarefa tarefa);
        public Task Update(Tarefa tarefa);
        public Task Delete(Tarefa tarefa);
    }
}

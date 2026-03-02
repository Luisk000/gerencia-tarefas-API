using GerenciaTarefas.API.DTOs;
using GerenciaTarefas.API.Models;

namespace GerenciaTarefas.API.Services
{
    public interface ITarefasService
    {
        public Task<List<TarefaResumidaDTO>> ListAll();
        public Task<TarefaDetalhadaDTO?> GetById(int id);
        public Task Create(Tarefa tarefa);
        public Task Update(int id, Tarefa tarefa);
        public Task Delete(int id);
    }
}

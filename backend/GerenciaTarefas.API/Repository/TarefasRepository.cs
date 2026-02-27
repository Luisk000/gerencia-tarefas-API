using GerenciaTarefas.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using GerenciaTarefas.API.DTOs;

namespace GerenciaTarefas.API.Repository
{
    public class TarefasRepository: ITarefasRepository
    {
        private readonly AppDbContext _context;
        public TarefasRepository(AppDbContext context) 
        { 
            _context = context;
        }

        public async Task<List<TarefaResumidaDTO>> ListAll()
        {
            List<TarefaResumidaDTO> tarefas = await _context.Tarefas
                .Select(t => new TarefaResumidaDTO
                {
                    Id = t.Id,
                    Titulo = t.Titulo
                })
                .ToListAsync();

            return tarefas;
        }
        public async Task<Tarefa?> GetById(int id)
        {
            Tarefa? tarefa = await _context.Tarefas.FindAsync(id);
            return tarefa;
        }

        public async Task Create(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Update(int id, Tarefa tarefa)
        {
            Tarefa? tarefaBanco = await _context.Tarefas.FindAsync(id);

            if (tarefaBanco == null)
                return false;

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Prioridade = tarefa.Prioridade;
            tarefaBanco.Status = tarefa.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            Tarefa? tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa == null)
                return false;

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

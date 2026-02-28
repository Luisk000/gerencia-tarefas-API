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
            List<TarefaResumidaDTO> tarefas = await _context.tarefas
                .Select(t => new TarefaResumidaDTO
                {
                    Id = t.id,
                    Titulo = t.titulo
                })
                .ToListAsync();

            return tarefas;
        }
        public async Task<Tarefa?> GetById(int id)
        {
            Tarefa? tarefa = await _context.tarefas.FindAsync(id);
            return tarefa;
        }

        public async Task Create(Tarefa tarefa)
        {
            _context.tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Update(int id, Tarefa tarefa)
        {
            Tarefa? tarefaBanco = await _context.tarefas.FindAsync(id);

            if (tarefaBanco == null)
                return false;

            tarefaBanco.titulo = tarefa.titulo;
            tarefaBanco.descricao = tarefa.descricao;
            tarefaBanco.prioridade = tarefa.prioridade;
            tarefaBanco.status = tarefa.status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            Tarefa? tarefa = await _context.tarefas.FindAsync(id);

            if (tarefa == null)
                return false;

            _context.tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

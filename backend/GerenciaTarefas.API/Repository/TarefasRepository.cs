using GerenciaTarefas.API.DTOs;
using GerenciaTarefas.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace GerenciaTarefas.API.Repository
{
    public class TarefasRepository: ITarefasRepository
    {
        private readonly AppDbContext _context;
        public TarefasRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Tarefa tarefa)
        {
            _context.tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Tarefa tarefa)
        {
            _context.tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Tarefa tarefa)
        {
            _context.tarefas.Update(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Tarefa>> Get()
        {
            List<Tarefa> tarefas = await _context.tarefas.ToListAsync();
            return tarefas;
        }

        public async Task<Tarefa?> GetById(int id)
        {
            Tarefa? tarefa = await _context.tarefas.FindAsync(id);
            return tarefa;
        }
    }
}

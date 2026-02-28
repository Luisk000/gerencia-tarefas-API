using GerenciaTarefas.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciaTarefas.API
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<Tarefa> tarefas { get; set; }
    }
}

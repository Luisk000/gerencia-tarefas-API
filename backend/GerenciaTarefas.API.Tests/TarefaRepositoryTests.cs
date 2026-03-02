using GerenciaTarefas.API.Models;
using GerenciaTarefas.API.Repository;
using Microsoft.EntityFrameworkCore;

namespace GerenciaTarefas.API.Tests
{
    public class TarefaRepositoryTests
    {
        [Fact]
        public async Task Create_WhenValid_ShouldSucceed()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                 .UseInMemoryDatabase("TestDb")
                 .Options;

            using var context = new AppDbContext(options);
            var repository = new TarefasRepository(context);

            var tarefa = new Tarefa
            {
                titulo = "Teste",
                descricao = "Descricao Teste",
                prioridade = PrioridadeTipo.Alta
            };

            await repository.Create(tarefa);

            var tarefaBanco = await context.tarefas.FindAsync(tarefa.id);
            Assert.NotNull(tarefaBanco);
            Assert.Equal(tarefaBanco.titulo, tarefa.titulo);
            Assert.Equal(tarefaBanco.descricao, tarefa.descricao);
            Assert.Equal(tarefaBanco.prioridade, tarefa.prioridade);
        }
        
        [Fact]
        public async Task GetById_WhenExists_ShouldReturnTarefas()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            using var context = new AppDbContext(options);
            var repository = new TarefasRepository(context);

            var tarefa = new Tarefa
            {
                titulo = "Teste",
                descricao = "Descricao Teste",
                prioridade = PrioridadeTipo.Alta
            };

            context.Add(tarefa);
            await context.SaveChangesAsync();

            var tarefaBanco = await repository.GetById(tarefa.id);
            Assert.NotNull(tarefaBanco);
            Assert.Equal(tarefaBanco.titulo, tarefa.titulo);
            Assert.Equal(tarefaBanco.descricao, tarefa.descricao);
            Assert.Equal(tarefaBanco.prioridade, tarefa.prioridade);
        }

        [Fact]
        public async Task Update_WhenValid_ShouldSucceed()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                 .UseInMemoryDatabase("TestDb")
                 .Options;

            using var context = new AppDbContext(options);
            var repository = new TarefasRepository(context);

            var tarefa = new Tarefa
            {
                titulo = "Teste",
                descricao = "Descricao Teste",
                prioridade = PrioridadeTipo.Alta
            };

            context.Add(tarefa);
            await context.SaveChangesAsync();

            tarefa.titulo = "Teste 2";
            tarefa.descricao = "Segunda descricao";
            tarefa.prioridade = PrioridadeTipo.Media;
            tarefa.status = StatusTipo.EmAndamento;

            await repository.Update(tarefa);

            var tarefaAtualizada = await context.tarefas.FindAsync(tarefa.id);
            Assert.NotNull(tarefaAtualizada);
            Assert.Equal(tarefaAtualizada.titulo, tarefa.titulo);
            Assert.Equal(tarefaAtualizada.descricao, tarefa.descricao);
            Assert.Equal(tarefaAtualizada.prioridade, tarefa.prioridade);
            Assert.Equal(tarefaAtualizada.status, tarefa.status);
        }

        [Fact]
        public async Task Delete_WhenExisting_ShouldDelete()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                 .UseInMemoryDatabase("TestDb")
                 .Options;

            using var context = new AppDbContext(options);
            var repository = new TarefasRepository(context);

            var tarefa = new Tarefa
            {
                titulo = "Teste",
                descricao = "Descricao Teste",
                prioridade = PrioridadeTipo.Alta
            };

            context.Add(tarefa);
            await context.SaveChangesAsync();

            await repository.Delete(tarefa);

            var tarefaApagada = await context.tarefas.FindAsync(tarefa.id);
            Assert.Null(tarefaApagada);
        }
    }
}

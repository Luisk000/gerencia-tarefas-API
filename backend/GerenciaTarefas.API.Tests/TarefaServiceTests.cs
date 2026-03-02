using GerenciaTarefas.API.Models;
using GerenciaTarefas.API.Repository;
using GerenciaTarefas.API.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciaTarefas.API.Tests
{
    public class TarefaServiceTests
    {
        private readonly Mock<ITarefasRepository> _tarefasRepository;
        private readonly Mock<IMetadataService> _metadataRepository;
        private readonly TarefasService _service;

        public TarefaServiceTests()
        {
            _tarefasRepository = new Mock<ITarefasRepository>();
            _metadataRepository = new Mock<IMetadataService>();
            _service = new TarefasService(_tarefasRepository.Object, _metadataRepository.Object);
        }

        [Fact]
        public async Task ListAll_WhenFound_ShouldReturnCorrectly()
        {
            List<Tarefa> tarefas = new List<Tarefa>
            {
                new Tarefa
                {
                    id = 1,
                    titulo = "Teste",
                    descricao = "Descricao Teste",
                    prioridade = PrioridadeTipo.Alta,
                    status = StatusTipo.Pendente
                },
                new Tarefa
                {
                    id = 2,
                    titulo = "Teste 2",
                    descricao = "Descricao Teste 2",
                    prioridade = PrioridadeTipo.Media,
                    status = StatusTipo.EmAndamento
                }
            };

            _tarefasRepository.Setup(s => s.Get()).ReturnsAsync(tarefas);
            var tarefasList = await _service.ListAll();

            Assert.Equal(2, tarefasList.Count);
            Assert.Equal(tarefas[0].id, tarefasList[0].Id);
            Assert.Equal(tarefas[1].id, tarefasList[1].Id);
            Assert.Equal(tarefas[0].titulo, tarefasList[0].Titulo);
            Assert.Equal(tarefas[1].titulo, tarefasList[1].Titulo);
        }

        [Fact]
        public async Task GetById_WhenFound_ShouldSucceed()
        {
            var tarefa = new Tarefa
            {
                id = 1,
                titulo = "Teste",
                descricao = "Descricao Teste",
                prioridade = PrioridadeTipo.Alta,
                status = StatusTipo.Pendente,
                data_criacao = DateTime.Now
            };

            _tarefasRepository.Setup(s => s.GetById(1)).ReturnsAsync(tarefa);
            var returnedTarefa = await _service.GetById(1);

            Assert.NotNull(returnedTarefa);
            Assert.Equal(returnedTarefa.id, tarefa.id);
            Assert.Equal(returnedTarefa.titulo, tarefa.titulo);
            Assert.Equal(returnedTarefa.descricao, tarefa.descricao);
            Assert.Equal(returnedTarefa.prioridade, tarefa.prioridade.ToString());
            Assert.Equal(returnedTarefa.status, tarefa.status.ToString());
        }

        [Fact]
        public async Task Update_WhenValid_ShouldSucceed()
        {
            var tarefa = new Tarefa
            {
                id = 1,
                titulo = "Teste",
                descricao = "Descricao Teste",
                prioridade = PrioridadeTipo.Alta,
                status = StatusTipo.Pendente,
                data_criacao = DateTime.Now
            };

            _tarefasRepository.Setup(s => s.GetById(1)).ReturnsAsync(tarefa);

            var tarefaUpdated = new Tarefa
            {
                id = 1,
                titulo = "Teste Atualizado",
                descricao = "Descricao Teste Atualizada",
                prioridade = PrioridadeTipo.Media,
                status = StatusTipo.EmAndamento
            };

            await _service.Update(tarefaUpdated.id, tarefaUpdated);

            _tarefasRepository.Verify(r => r.Update(It.Is<Tarefa>(t =>
                t.id == tarefaUpdated.id &&
                t.titulo == tarefaUpdated.titulo &&
                t.descricao == tarefaUpdated.descricao &&
                t.prioridade == tarefaUpdated.prioridade &&
                t.status == tarefaUpdated.status
            )), Times.Once);
        }
    }
}

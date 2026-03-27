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
        public async Task ListAll_WhenTarefasAreFound_ShouldReturnTarefas()
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
            Assert.Collection(tarefasList, 
                t =>
                {
                    Assert.Equal(1, t.Id);
                    Assert.Equal("Teste", t.Titulo);
                },
                t =>
                {
                    Assert.Equal(2, t.Id);
                    Assert.Equal("Teste 2", t.Titulo);
                }
            );
        }

        [Fact]
        public async Task ListAll_WhenEmpty_ShouldReturnEmptyList()
        {
            _tarefasRepository.Setup(s => s.Get()).ReturnsAsync(new List<Tarefa>());
            var tarefasList = await _service.ListAll();

            Assert.Empty(tarefasList);
        }


        [Fact]
        public async Task GetById_WhenTarefaIsFound_ShouldReturnTarefa()
        {
            var tarefa = new Tarefa
            {
                id = 1,
                titulo = "Teste",
                descricao = "Descricao Teste",
                prioridade = PrioridadeTipo.Alta,
                status = StatusTipo.Pendente,
                data_criacao = new DateTime(2026, 1, 1)
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
        public async Task GetById_WhenNotFound_ShouldThrowKeyNotFoundException()
        {
            _tarefasRepository.Setup(s => s.GetById(1)).ReturnsAsync((Tarefa?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => _service.GetById(1)
            );
        }

        [Fact]
        public async Task Create_WhenValid_ShouldSucceed()
        {
            var tarefa = new Tarefa
            {
                id = 1,
                titulo = "Teste",
                descricao = "Descricao Teste",
                prioridade = PrioridadeTipo.Alta,
            };

            await _service.Create(tarefa);

            //Verifica se uma tarefa esteve no Create
            _tarefasRepository.Verify(
                r => r.Create(It.IsAny<Tarefa>()),
                Times.Once
            );
        }

        [Fact]
        public async Task Create_WhenTarefaIsNull_ShouldThrowArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => _service.Create(null)
            );
        }

        [Fact]
        public async Task Create_WhenTituloIsNullOrWhitespace_ShouldThrowArgumentException()
        {
            var tarefa = new Tarefa
            {
                id = 1,
                titulo = "",
                descricao = "Descricao Teste",
                prioridade = PrioridadeTipo.Alta,
            };

            await Assert.ThrowsAsync<ArgumentException>(
                () => _service.Create(tarefa)
            );
        }

        [Fact]
        public async Task Create_WhenDescricaoIsNullOrWhitespace_ShouldThrowArgumentException()
        {
            var tarefa = new Tarefa
            {
                id = 1,
                titulo = "Tarefa",
                descricao = "",
                prioridade = PrioridadeTipo.Alta,
            };

            await Assert.ThrowsAsync<ArgumentException>(
                () => _service.Create(tarefa)
            );
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

        [Fact]
        public async Task Update_WhenTarefaIsNull_ShouldThrowArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => _service.Update(1, null)
            );
        }

        [Fact]
        public async Task Update_WhenTituloIsNullOrWhitespace_ShouldThrowArgumentException()
        {
            var tarefa = new Tarefa
            {
                id = 1,
                titulo = "",
                descricao = "Descricao Teste",
                prioridade = PrioridadeTipo.Alta
            };

            await Assert.ThrowsAsync<ArgumentException>(
                () => _service.Update(1, tarefa)
            );
        }

        [Fact]
        public async Task Update_WhenDescricaoIsNullOrWhitespace_ShouldThrowArgumentException()
        {
            var tarefa = new Tarefa
            {
                id = 1,
                titulo = "Teste",
                descricao = "",
                prioridade = PrioridadeTipo.Alta
            };

            await Assert.ThrowsAsync<ArgumentException>(
                () => _service.Update(1, tarefa)
            );
        }

        [Fact]
        public async Task Update_WhenTarefaIsNotFound_ShouldThrowKeyNotFoundException()
        {
            var tarefa = new Tarefa
            {
                id = 1,
                titulo = "Teste Atualizado",
                descricao = "Descricao Teste Atualizada",
                prioridade = PrioridadeTipo.Media
            };

            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => _service.Update(1, tarefa)
            );
        }
    }
}

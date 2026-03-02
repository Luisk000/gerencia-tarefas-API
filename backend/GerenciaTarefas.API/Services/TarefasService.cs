using GerenciaTarefas.API.DTOs;
using GerenciaTarefas.API.Models;
using GerenciaTarefas.API.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace GerenciaTarefas.API.Services
{
    public class TarefasService: ITarefasService
    {
        private readonly ITarefasRepository _repository;
        private readonly IMetadataService _metadataService;
        public TarefasService(ITarefasRepository repository, IMetadataService metadataService)
        {
            _repository = repository;
            _metadataService = metadataService;
        }

        public async Task<List<TarefaResumidaDTO>> ListAll()
        {
            List<Tarefa> tarefas = await _repository.Get();

            List<TarefaResumidaDTO> tarefasResumidas = tarefas
                .Select(t => new TarefaResumidaDTO
                {
                    Id = t.id,
                    Titulo = t.titulo
                })
                .ToList();

            return tarefasResumidas;
        }
        public async Task<TarefaDetalhadaDTO?> GetById(int id)
        {
            Tarefa? tarefa = await _repository.GetById(id);

            if (tarefa == null)
                return null;

            TarefaDetalhadaDTO tarefaDetalhada = new TarefaDetalhadaDTO
            {
                id = tarefa.id,
                titulo = tarefa.titulo,
                descricao = tarefa.descricao,
                data_criacao = (DateTime)tarefa.data_criacao!,
                status = tarefa.status.ToString(),
                prioridade = tarefa.prioridade.ToString(),
                todos_status = _metadataService.GetTodosStatus(),
                todas_prioridades = _metadataService.GetTodasPrioridades()
            };

            return tarefaDetalhada;
        }

        public async Task Create(Tarefa tarefa)
        {
            await _repository.Create(tarefa);
        }

        public async Task<bool> Update(int id, Tarefa tarefa)
        {
            Tarefa? tarefaBanco = await _repository.GetById(id);

            if (tarefaBanco == null)
                return false;

            tarefaBanco.titulo = tarefa.titulo;
            tarefaBanco.descricao = tarefa.descricao;
            tarefaBanco.prioridade = tarefa.prioridade;
            tarefaBanco.status = tarefa.status;

            await _repository.Update(tarefaBanco);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            Tarefa? tarefa = await _repository.GetById(id);

            if (tarefa == null)
                return false;

            await _repository.Delete(tarefa);
            return true;
        }
    }
}

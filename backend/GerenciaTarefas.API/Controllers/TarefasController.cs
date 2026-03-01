using GerenciaTarefas.API.DTOs;
using GerenciaTarefas.API.Models;
using GerenciaTarefas.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GerenciaTarefas.API.Controllers
{
    [Route("api/[controller]")]
    public class TarefasController: Controller
    {
        private readonly ITarefasRepository _tarefasRepository;
        public TarefasController(ITarefasRepository tarefasRepository)
        {
            _tarefasRepository = tarefasRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            List<TarefaResumidaDTO> tarefas = await _tarefasRepository.ListAll();
            return Json(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            TarefaDetalhadaDTO? tarefa = await _tarefasRepository.GetById(id);
            if (tarefa == null)
                return NotFound();
            return Json(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tarefa tarefa)
        {
            await _tarefasRepository.Create(tarefa);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Tarefa tarefa)
        {
            bool encontrado = await _tarefasRepository.Update(id, tarefa);
            if (!encontrado)
                return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool encontrado = await _tarefasRepository.Delete(id);
            if (!encontrado)
                return NotFound();
            return Ok();
        }
    }
}

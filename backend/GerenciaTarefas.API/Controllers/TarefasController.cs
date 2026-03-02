using GerenciaTarefas.API.DTOs;
using GerenciaTarefas.API.Models;
using GerenciaTarefas.API.Repository;
using GerenciaTarefas.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciaTarefas.API.Controllers
{
    [Route("api/[controller]")]
    public class TarefasController: Controller
    {
        private readonly ITarefasService _service;
        public TarefasController(ITarefasService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            List<TarefaResumidaDTO> tarefas = await _service.ListAll();
            return Json(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            TarefaDetalhadaDTO? tarefa = await _service.GetById(id);
            if (tarefa == null)
                return NotFound();
            return Json(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tarefa tarefa)
        {
            await _service.Create(tarefa);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Tarefa tarefa)
        {
            bool encontrado = await _service.Update(id, tarefa);
            if (!encontrado)
                return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool encontrado = await _service.Delete(id);
            if (!encontrado)
                return NotFound();
            return Ok();
        }
    }
}

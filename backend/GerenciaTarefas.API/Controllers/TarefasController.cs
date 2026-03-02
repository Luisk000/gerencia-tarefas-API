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
            try
            {
                List<TarefaResumidaDTO> tarefas = await _service.ListAll();
                return Json(tarefas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                TarefaDetalhadaDTO? tarefa = await _service.GetById(id);
                return Json(tarefa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tarefa tarefa)
        {
            try
            {
                await _service.Create(tarefa);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Tarefa tarefa)
        {
            try
            {
                await _service.Update(id, tarefa);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

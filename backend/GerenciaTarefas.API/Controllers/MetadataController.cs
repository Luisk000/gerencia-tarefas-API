using GerenciaTarefas.API.DTOs;
using GerenciaTarefas.API.Repository;
using GerenciaTarefas.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciaTarefas.API.Controllers
{
    [Route("api/[controller]")]
    public class MetadataController: Controller
    {
        private readonly IMetadataService _metadataService;
        public MetadataController(IMetadataService metadataService) 
        {
            _metadataService = metadataService;
        }

        [HttpGet]
        [Route("status")]
        public async Task<IActionResult> GetTodosStatus()
        {
            IEnumerable<string> todosStatus = _metadataService.GetTodosStatus();
            return Json(todosStatus);
        }
    }
}

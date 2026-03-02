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
        [Route("prioridades")]
        public async Task<IActionResult> GetPrioridades()
        {
            IEnumerable<string> todosStatus = _metadataService.GetTodasPrioridades();
            return Json(todosStatus);
        }
    }
}

using GerenciaTarefas.API.Repository;
using GerenciaTarefas.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciaTarefas.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class OAuthController: Controller
    {
        private readonly IOauthService _service;
        public OAuthController(IOauthService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAcessToken()
        {
            string client_id = Request.Headers["client_id"]!;
            string client_secret = Request.Headers["client_secret"]!;

            string token = await _service.GetTokenAcess(client_id, client_secret);
            return Json(token);
        }
    }
}

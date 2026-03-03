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
        private static readonly IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

        private readonly IOauthService _service;
        public OAuthController(IOauthService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAcessToken()
        {
            try
            {
                string client_id = configuration["Authentication:client_id"]!;
                string client_secret = configuration["Authentication:client_secret"]!;

                string token = await _service.GetTokenAcess(client_id, client_secret);
                return Json(token);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

        }
    }
}

using GerenciaTarefas.API.Models;

namespace GerenciaTarefas.API.Services
{
    public interface IOauthService
    {
        public Task<string> GetTokenAcess(string client_id, string client_secret);
    }
}

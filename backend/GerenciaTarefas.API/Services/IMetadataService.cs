namespace GerenciaTarefas.API.Services
{
    public interface IMetadataService
    {
        public IEnumerable<string> GetTodosStatus();
    }
}

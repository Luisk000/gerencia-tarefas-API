using GerenciaTarefas.API.Models;

namespace GerenciaTarefas.API.Services
{
    public class MetadataService: IMetadataService
    {
        public MetadataService() { }

        public IEnumerable<string> GetTodosStatus()
        {
            IEnumerable<string> todosStatus = Enum.GetValues<StatusTipo>().Select(s => s.ToString());
            return todosStatus;
        }

        public IEnumerable<string> GetTodasPrioridades()
        {
            IEnumerable<string> todasPrioridades = Enum.GetValues<PrioridadeTipo>().Select(s => s.ToString());
            return todasPrioridades;
        }
    }
}

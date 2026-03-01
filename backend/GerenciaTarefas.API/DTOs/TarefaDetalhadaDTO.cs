using GerenciaTarefas.API.Models;

namespace GerenciaTarefas.API.DTOs
{
    public class TarefaDetalhadaDTO
    {
        public int id { get; set; }
        public required string titulo { get; set; }
        public required string descricao { get; set; }
        public required string status { get; set; }
        public required string prioridade { get; set; }
        public required DateTime data_criacao { get; set; }

        public required IEnumerable<string> todos_status { get; set; }
        public required IEnumerable<string> todas_prioridades { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace GerenciaTarefas.API.Models
{
    public class Tarefa
    {
        [Key]
        public int id { get; set; }
        public required string titulo { get; set; }
        public required string descricao { get; set; }
        public StatusTipo status { get; set; }
        public PrioridadeTipo prioridade { get; set; }
        public DateTime? data_criacao { get; set; }  
    }
}

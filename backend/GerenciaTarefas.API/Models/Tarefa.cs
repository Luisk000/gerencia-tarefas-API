using System.ComponentModel.DataAnnotations;

namespace GerenciaTarefas.API.Models
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public required string Descricao { get; set; }
        public StatusTipo Status { get; set; }
        public PrioridadeTipo Prioridade { get; set; }
        public DateTime Data_criacao { get; set; }  
    }
}

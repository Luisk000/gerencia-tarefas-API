using System.Runtime.Serialization;

namespace GerenciaTarefas.API.Models
{
    public enum StatusTipo
    {
        [EnumMember(Value = "pendente")]
        Pendente,

        [EnumMember(Value = "em andamento")]
        EmAndamento,

        [EnumMember(Value = "concluido")]
        Concluido
    }
}

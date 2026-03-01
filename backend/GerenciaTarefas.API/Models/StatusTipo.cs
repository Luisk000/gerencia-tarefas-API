using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GerenciaTarefas.API.Models
{
    public enum StatusTipo
    {
        [EnumMember(Value = "pendente")]
        Pendente,

        [EnumMember(Value = "em_andamento")]
        EmAndamento,

        [EnumMember(Value = "concluido")]
        Concluido
    }
}

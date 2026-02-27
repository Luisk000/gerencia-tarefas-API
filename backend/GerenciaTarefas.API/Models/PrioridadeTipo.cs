using System.Runtime.Serialization;

namespace GerenciaTarefas.API.Models
{
    public enum PrioridadeTipo
    {
        [EnumMember(Value = "baixa")]
        Baixa,

        [EnumMember(Value = "media")]
        Media,

        [EnumMember(Value = "alta")]
        Alta
    }
}

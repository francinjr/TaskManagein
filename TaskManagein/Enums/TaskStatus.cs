using System.ComponentModel;

namespace TaskManagein.Enums
{
    public enum TaskStatus
    {
        [Description("A fazer")]
        ToDo = 1,
        [Description("Em andamento")]
        InProgress = 2,
        [Description("Concluída")]
        Concluded = 3
    }
}

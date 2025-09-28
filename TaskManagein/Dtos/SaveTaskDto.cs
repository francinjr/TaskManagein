using System.ComponentModel.DataAnnotations;
using TaskManagein.Models;

namespace TaskManagein.Dtos
{
    public class SaveTaskDto
    {
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [MinLength(5, ErrorMessage = "O campo nome deve ter pelo menos 5 caracteres.")]
        [StringLength(100, ErrorMessage = "O campo nome deve ter no máximo 100 caracteres.")]
        public string? Name { get; set; }

        [StringLength(500, ErrorMessage = "O campo descrição deve ter no máximo 500 caracteres")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "O status da tarefa é obrigatório")]
        public TaskStatus Status { get; set; }

        public int? UserId { get; set; }
    }
}

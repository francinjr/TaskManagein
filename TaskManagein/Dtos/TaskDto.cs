using TaskManagein.Models;

namespace TaskManagein.Dtos
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public TaskStatus Status { get; set; }


        //public int? UserId { get; set; }

        //public virtual UserModel? User { get; set; }
    }
}

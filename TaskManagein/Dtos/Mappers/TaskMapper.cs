using TaskManagein.Models;

namespace TaskManagein.Dtos.Mappers
{
    public static class TaskMapper
    {
        public static TaskDto ToDto(TaskModel task)
        {
            return new TaskDto
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Status = task.Status
            };
        }

        public static TaskModel FromDto(TaskDto taskDto)
        {
            return new TaskModel
            {
                Id = taskDto.Id,
                Name = taskDto.Name,
                Description = taskDto.Description,
                Status = taskDto.Status
            };
        }

        public static TaskModel FromSaveDto(SaveTaskDto saveTaskDto)
        {
            return new TaskModel
            {
                Name = saveTaskDto.Name,
                Description = saveTaskDto.Description,
                Status = saveTaskDto.Status,
                UserId = saveTaskDto.UserId
            };
        }
    }
}

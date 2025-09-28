using Microsoft.AspNetCore.Mvc;
using TaskManagein.Dtos;
using TaskManagein.Dtos.Mappers;
using TaskManagein.Exceptions;
using TaskManagein.Exceptions.Handler;
using TaskManagein.Models;
using TaskManagein.Repositories.Interfaces;
using TaskManagein.Services.Interfaces;

namespace SistemaDeTarefas.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<TaskModel>>>> FindAll()
        {
            // Proxima coisa a ser feita criar uma excecao para recurso nao encontrado e tratar ela no exception handler
            List<TaskModel> tasks = await _taskService.FindAll();
            List<TaskDto> taskDtos = tasks.Select(TaskMapper.ToDto).ToList();

            var response = new ApiResponse<List<TaskDto>>("Tarefas buscadas com sucesso", taskDtos, null);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TaskDto>>> FindById(int id)
        {
            TaskModel searchedTask = await _taskService.FindById(id);
            TaskDto searchedTaskDto = TaskMapper.ToDto(searchedTask);

            var response = new ApiResponse<TaskDto>("Tarefa buscada com sucesso", searchedTaskDto, null);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<TaskDto>>> Create([FromBody] SaveTaskDto saveTaskDto)
        {
            TaskModel task = TaskMapper.FromSaveDto(saveTaskDto);
            TaskModel createdTask = await _taskService.Create(task);

            TaskDto createdTaskDto = TaskMapper.ToDto(createdTask);
            var response = new ApiResponse<TaskDto>("Tarefa criada com sucesso", createdTaskDto, null);

            return CreatedAtAction(nameof(FindById), new { id = createdTask.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<TaskDto>>> Update([FromBody] SaveTaskDto saveTaskDto, int id)
        {
            TaskModel task = TaskMapper.FromSaveDto(saveTaskDto);
            TaskModel updatedTask = await _taskService.Update(id,task);

            TaskDto updatedTaskDto = TaskMapper.ToDto(updatedTask);
            var response = new ApiResponse<TaskDto>("Tarefa atualizada com sucesso", updatedTaskDto, null);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _taskService.Delete(id);
            return NoContent();
        }
    }
}

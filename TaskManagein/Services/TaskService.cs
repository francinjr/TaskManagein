using TaskManagein.Data;
using TaskManagein.Exceptions;
using TaskManagein.Exceptions.Handler;
using TaskManagein.Models;
using TaskManagein.Repositories;
using TaskManagein.Repositories.Interfaces;
using TaskManagein.Services.Interfaces;

namespace TaskManagein.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }


        public async Task<List<TaskModel>> FindAll()
        {
            return await _taskRepository.FindAll();
        }

        public async Task<TaskModel> FindById(int id)
        {
            TaskModel searchedTask = await _taskRepository.FindById(id);

            if(searchedTask == null)
                throw new ResourceNotFoundException($"Tarefa com id: {id} não foi existe");

            return searchedTask;
        }

        public async Task<TaskModel> Create(TaskModel task)
        {

            List<ValidationField> fields = await ValidateUniqueTaskFieldValues(task);
            if(fields.Any())
                throw new InvalidFieldException("Não foi possível criar a tarefa. Há campos com valores que já estão sendo usados", fields);
            
            return await _taskRepository.Add(task);
        }



        public async Task<TaskModel> Update(int id, TaskModel task)
        {
            if (!await _taskRepository.ExistsById(id))
                throw new ResourceNotFoundException($"Não foi possível atualizar, pois a tarefa com id: {id} não foi existe");

            task.Id = id;

            List<ValidationField> fields = await ValidateUniqueTaskFieldValues(task);
            if (fields.Any())
                throw new InvalidFieldException("Não foi possível atualizar a tarefa. Há campos com valores que já estão sendo usados", fields);

            return await _taskRepository.Update(task);
        }

        public async Task<bool> Delete(int id)
        {
            if(!await _taskRepository.ExistsById(id))
                throw new ResourceNotFoundException($"Não foi possível deletar, pois a tarefa com id: {id} não foi existe");

            await _taskRepository.Delete(id);
            return true;
        }


        private async Task<List<ValidationField>> ValidateUniqueTaskFieldValues(TaskModel task)
        {
            List<ValidationField> fields = new List<ValidationField>();
            if (await _taskRepository.ExistsByName(task.Name))
                fields.Add(new ValidationField("name", "Já existe uma tarefa com o nome: " + task.Name));
            
            return fields;
        }
    }
}

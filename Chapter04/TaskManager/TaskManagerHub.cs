using Microsoft.AspNetCore.SignalR;
using TaskManager.Model;
using TaskManager.Service.Contract;

namespace TaskManager
{
    public class TaskManagerHub : Hub
    {
        private readonly ITaskRepository _taskRepository;

        public TaskManagerHub(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task CreateTask(TaskModel taskModel)
        {
            _taskRepository.Save(taskModel);

            await Clients.All.SendAsync(ClientConstants.NOTIFY_TASK_MANAGER_EVENT, taskModel);
        }

        public async Task CompleteTask(TaskModel taskModel)
        {
            taskModel.IsCompleted = true;
            await Clients.All.SendAsync(ClientConstants.NOTIFY_TASK_MANAGER_EVENT, taskModel);
        }
    }
}
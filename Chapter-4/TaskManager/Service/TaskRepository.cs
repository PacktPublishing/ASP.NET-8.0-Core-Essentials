using TaskManager.Model;
using TaskManager.Service.Contract;

namespace TaskManager.Service;

public class TaskRepository : ITaskRepository
{
    private readonly List<TaskModel> _tasks = new List<TaskModel>();

    public void Save(TaskModel taskModel)
    {
        _tasks.Add(taskModel);
    }
}
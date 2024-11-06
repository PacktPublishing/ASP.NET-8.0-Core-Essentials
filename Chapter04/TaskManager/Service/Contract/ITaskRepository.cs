using TaskManager.Model;

namespace TaskManager.Service.Contract;

public interface ITaskRepository
{
    void Save(TaskModel taskModel);
}
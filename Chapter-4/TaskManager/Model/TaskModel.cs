namespace TaskManager.Model;

public class TaskModel
{
    public Guid Id { get; set;} = Guid.NewGuid();
    public string Name { get; set; }
    public bool IsCompleted { get; set; }

    public TaskModel()
    {
        IsCompleted = false;
    }

    public TaskModel(string name) : this()
    {
        Name = name;
    }

    public TaskModel(string name, bool isCompleted)
    {
        Name = name;
        IsCompleted = isCompleted;
    }
}

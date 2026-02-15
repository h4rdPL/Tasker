namespace Tasker.Domain.Entities;

public class Tag 
{
    public Guid Id { get; private set; }
    public string Name {get; private set;}

    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();


    private Tag() { } 

    public Tag(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Tag name cannot be empty");
        }
        Id = Guid.NewGuid();
        Name = name.Trim();
    }
}
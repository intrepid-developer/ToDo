namespace ToDo.Api.Entities;

public record TodoItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }

    public TodoItem()
    {
    }

    public TodoItem(string title)
    {
        Title = title;
        IsCompleted = false;
        CreatedAt = DateTime.UtcNow;
    }
}
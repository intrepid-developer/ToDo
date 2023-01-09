namespace ToDo.Web;

public class ToDoApiClient(HttpClient httpClient)
{
    public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
    {
        var response = await httpClient.GetAsync("/todo");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<IEnumerable<TodoItem>>() ?? [];
    }

    public async Task CreateTodoItemAsync(TodoItem item)
    {
        var response = await httpClient.PostAsJsonAsync("/todo", item);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateTodoItemAsync(int id, TodoItem item)
    {
        var response = await httpClient.PutAsJsonAsync($"/todo/{id}", item);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteTodoItemAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"/todo/{id}");
        response.EnsureSuccessStatusCode();
    }
}

public record TodoItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
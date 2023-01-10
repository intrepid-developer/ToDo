using ToDoApp.Application.Models;

namespace ToDoApp.Application.Services;

public interface IToDoService
{
    Task AddToDoAsync(ToDo todo, CancellationToken cancellationToken = default);
    Task RemoveToDoAsync(ToDo todo, CancellationToken cancellationToken = default);
    Task UpdateToDoAsync(ToDo todo, CancellationToken cancellationToken = default);
    Task<IList<ToDo>> GetToDosAsync();
}
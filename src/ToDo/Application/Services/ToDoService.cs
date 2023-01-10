using Blazored.LocalStorage;
using ToDoApp.Application.Models;

namespace ToDoApp.Application.Services;

public class ToDoService : IToDoService
{
    private IList<ToDo> _toDos;
    private readonly ILocalStorageService _localStorage;

    public ToDoService(ILocalStorageService localStorage)
    {
        _toDos = new List<ToDo>();
        _localStorage = localStorage;
    }

    public async Task AddToDoAsync(ToDo todo, CancellationToken cancellationToken = new CancellationToken())
    {
        ArgumentNullException.ThrowIfNull(todo, "ToDo Cannot be null");

        await LoadToDosAsync(cancellationToken);

        _toDos.Add(todo);

        await SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveToDoAsync(ToDo todo, CancellationToken cancellationToken = new CancellationToken())
    {
        ArgumentNullException.ThrowIfNull(todo, "ToDo Cannot be null");

        await LoadToDosAsync(cancellationToken);

        _toDos.Remove(todo);

        await SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateToDoAsync(ToDo todo, CancellationToken cancellationToken = new CancellationToken())
    {
        ArgumentNullException.ThrowIfNull(todo, "ToDo Cannot be null");

        await LoadToDosAsync(cancellationToken);

        var existingTodo = _toDos.First(_ => _.Id == todo.Id);
        existingTodo.IsComplete = todo.IsComplete;
        existingTodo.Name = todo.Name;

        await SaveChangesAsync(cancellationToken);
    }

    public async Task<IList<ToDo>> GetToDosAsync()
    {
        await LoadToDosAsync(new CancellationToken());
        return _toDos;
    }


    private async Task LoadToDosAsync(CancellationToken cancellationToken)
    {
        _toDos = await _localStorage.GetItemAsync<IList<ToDo>>("todos", cancellationToken);
        if (_toDos == null) _toDos = new List<ToDo>();
    }

    private async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _localStorage.SetItemAsync<IList<ToDo>>("todos", _toDos, cancellationToken);
    }

}


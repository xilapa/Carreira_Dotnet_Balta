using TodoApp.Domain.Entities;

namespace Domain.Repositories;

public interface ITodoRepository
{
    Task<TodoItem?> Get(Guid todoId, Guid userId);
    void Create(TodoItem todo);
    void Update(TodoItem todo);
    Task<TodoItem[]> GetAll(Guid user);
    Task<TodoItem[]> GetAllDone(Guid user);
    Task<TodoItem[]> GetAllUndone(Guid user);
    Task<TodoItem[]> GetByDate(Guid user, DateTime date, bool done);
}
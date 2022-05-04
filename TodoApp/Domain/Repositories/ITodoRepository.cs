using TodoApp.Domain.Entities;

namespace Domain.Repositories;

public interface ITodoRepository
{
    Task<TodoItem?> Get(Guid todoId, Guid userId);
    void Create(TodoItem todo);
    void Update(TodoItem todo);
}
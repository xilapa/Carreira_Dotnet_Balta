using TodoApp.Domain.Entities;

namespace Domain.Repositories;

public interface ITodoRepository
{
    void Create(TodoItem todo);
    void Update(TodoItem todo);
}
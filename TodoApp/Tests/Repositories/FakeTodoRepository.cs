using Domain.Repositories;
using TodoApp.Domain.Entities;

namespace Tests.Repositories;

public class FakeTodoRepository : ITodoRepository
{
    public void Create(TodoItem todo)
    { }

    public void Update(TodoItem todo)
    { }
}
using System;
using System.Threading.Tasks;
using Domain.Repositories;
using TodoApp.Domain.Entities;

namespace Tests.Repositories;

public class FakeTodoRepository : ITodoRepository
{
    public Task<TodoItem?> Get(Guid todoId, Guid userId)
    {
        return Task.FromResult<TodoItem?>(null);
    }

    public void Create(TodoItem todo)
    { }

    public void Update(TodoItem todo)
    { }
}
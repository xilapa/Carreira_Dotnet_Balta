using System;
using System.Threading.Tasks;
using Domain.Repositories;
using TodoApp.Domain.Entities;

namespace TodoApp.Tests.Repositories;

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

    public Task<TodoItem[]> GetAll(Guid user)
    {
        throw new NotImplementedException();
    }

    public Task<TodoItem[]> GetAllDone(Guid user)
    {
        throw new NotImplementedException();
    }

    public Task<TodoItem[]> GetAllUndone(Guid user)
    {
        throw new NotImplementedException();
    }

    public Task<TodoItem[]> GetByDate(Guid user, DateTime date, bool done)
    {
        throw new NotImplementedException();
    }
}
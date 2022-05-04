using Domain.Queries;
using Domain.Repositories;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;

namespace Infra.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly TodoDataContext _context;

    public TodoRepository(TodoDataContext context)
    {
        _context = context;
    }

    public Task<TodoItem?> Get(Guid todoId, Guid userId) =>
        _context.Todos
            .FirstOrDefaultAsync(t => t.Id == todoId && t.UserId == userId);

    public void Create(TodoItem todo) =>
        _context.Todos.Add(todo);

    public void Update(TodoItem todo) =>
        _context.Entry(todo).State = EntityState.Modified;

    public async Task<TodoItem[]> GetAll(Guid user) =>
        await _context.Todos
            .AsNoTracking()
            .Where(TodoQueries.GetAll(user))
            .OrderBy(t => t.Date)
            .ToArrayAsync();

    public async Task<TodoItem[]> GetAllDone(Guid user) =>
        await _context.Todos
            .AsNoTracking()
            .Where(TodoQueries.GetAllDone(user))
            .OrderBy(t => t.Date)
            .ToArrayAsync();

    public async Task<TodoItem[]> GetAllUndone(Guid user) =>
        await _context.Todos
            .AsNoTracking()
            .Where(TodoQueries.GetAllUndone(user))
            .OrderBy(t => t.Date)
            .ToArrayAsync();

    public async Task<TodoItem[]> GetByDate(Guid user, DateTime date, bool done) =>
        await _context.Todos
            .AsNoTracking()
            .Where(TodoQueries.GetByDate(user,date, done))
            .OrderBy(t => t.Date)
            .ToArrayAsync();
}
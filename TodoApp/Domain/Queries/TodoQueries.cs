using System.Linq.Expressions;
using TodoApp.Domain.Entities;

namespace Domain.Queries;

public static class TodoQueries
{
    public static Expression<Func<TodoItem, bool>> GetAll(Guid userId) =>
        x => x.UserId == userId;

    public static Expression<Func<TodoItem, bool>> GetAllDone(Guid userId) =>
        x => x.UserId == userId && x.Done;

    public static Expression<Func<TodoItem, bool>> GetAllUndone(Guid userId) =>
        x => x.UserId == userId && !x.Done;

    public static Expression<Func<TodoItem, bool>> GetByDate(Guid userId, DateTime date, bool done) =>
        x => x.UserId == userId && x.Done == done && x.Date.Date == date.Date;
}
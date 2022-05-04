using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Queries;
using FluentAssertions;
using TodoApp.Domain.Entities;
using Xunit;

namespace TodoApp.Tests.QueryTests;

public class TodoQueryTests
{
    private readonly List<TodoItem> _todos;
    private static readonly Guid _userAId = Guid.Parse("EF3457EF-15C9-40B4-9D21-CF051825399D");
    private static readonly Guid _userBId = Guid.Parse("81F553B2-BD21-46D2-AB63-EA0663CABF0E");

    public TodoQueryTests()
    {
        _todos = new List<TodoItem>
        {
            new TodoItem("titulo1", DateTime.Today, _userAId),
            new TodoItem("titulo2", DateTime.Today, _userAId),
            new TodoItem("titulo1", DateTime.Today, _userBId),
            new TodoItem("titulo2", DateTime.Today, _userBId),
        };
    }

    public static IEnumerable<object[]> GetTodosByUserIdData()
    {
        yield return new object[] { _userAId, 2 };
        yield return new object[] { _userBId, 2 };
    }

    [Theory]
    [MemberData(nameof(GetTodosByUserIdData))]
    public void RetornaTarefasApenasDoUsuario(Guid userId, int expectedCount)
    {
        // Arrange feito no ctor

        // Act
        var todos = _todos.AsQueryable().Where(TodoQueries.GetAll(userId)).ToArray();

        // Assert
        todos.Should().OnlyContain(t => t.UserId == userId);
        todos.Length.Should().Be(expectedCount);
    }
}
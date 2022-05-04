using System;
using FluentAssertions;
using TodoApp.Domain.Entities;
using Xunit;

namespace TodoApp.Tests.EntityTests;

public class TodoItemTests
{
    [Fact]
    public void NovoTodoDeveSerUndone()
    {
        // Arrange e Act
        var todo = new TodoItem("titulo", DateTime.Now, Guid.NewGuid());

        // Assert
        todo.Done.Should().BeFalse();
    }
}
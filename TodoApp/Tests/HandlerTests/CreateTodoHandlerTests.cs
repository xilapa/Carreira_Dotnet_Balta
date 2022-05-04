using System;
using System.Threading.Tasks;
using Domain.Commands;
using FluentAssertions;
using TodoApp.Domain.Commands.CreateTodo;
using TodoApp.Tests.Repositories;
using Xunit;

namespace TodoApp.Tests.HandlerTests;

public class CreateTodoHandlerTestsBase
{
    public readonly TodoHandler TodoHandler;

    public CreateTodoHandlerTestsBase()
    {
        TodoHandler = new TodoHandler(new FakeTodoRepository(), new FakeUnitOfWork());
    }
}

public class CreateTodoHandlerTests : IClassFixture<CreateTodoHandlerTestsBase>
{
    private readonly CreateTodoHandlerTestsBase _base;

    public CreateTodoHandlerTests(CreateTodoHandlerTestsBase fixture)
    {
        _base = fixture;
    }

    [Fact]
    public async Task DadoUmComandoInvalido()
    {
        // Arrange
        var command = new CreateTodoCommand("", Guid.Empty, DateTime.Now);

        // Act
        var result = await _base.TodoHandler.Handle(command);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public async Task DadoUmComandoValido()
    {
        // Arrange
        var command = new CreateTodoCommand("titulo", Guid.NewGuid(), DateTime.Now);

        // Act
        var result = await _base.TodoHandler.Handle(command);

        // Assert
        result.Success.Should().BeTrue();
    }
}
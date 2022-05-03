using System;
using System.Threading.Tasks;
using Domain.Commands;
using Domain.Repositories;
using FluentAssertions;
using Tests.Repositories;
using TodoApp.Domain.Commands.CreateTodo;
using Xunit;

namespace Tests.HandlerTests;

public class CreateTodoHandlerTestsBase
{
    public readonly Handler Handler;

    public CreateTodoHandlerTestsBase()
    {
        Handler = new Handler(new FakeTodoRepository(), new FakeUnitOfWork());
    }
}

public class CreateTodoHandlerTests : IClassFixture<CreateTodoHandlerTestsBase>
{
    private readonly CreateTodoHandlerTestsBase @base;

    public CreateTodoHandlerTests(CreateTodoHandlerTestsBase fixture)
    {
        @base = fixture;
    }

    [Fact]
    public async Task DadoUmComandoInvalido()
    {
        // Arrange
        var command = new CreateTodoCommand("", Guid.Empty, DateTime.Now);

        // Act
        var result = await @base.Handler.Handle(command);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public async Task DadoUmComandoValido()
    {
        // Arrange
        var command = new CreateTodoCommand("titulo", Guid.NewGuid(), DateTime.Now);

        // Act
        var result = await @base.Handler.Handle(command);

        // Assert
        result.Success.Should().BeTrue();
    }
}
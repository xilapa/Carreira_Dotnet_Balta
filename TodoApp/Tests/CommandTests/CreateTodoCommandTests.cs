using System;
using FluentAssertions;
using TodoApp.Domain.Commands.CreateTodo;
using Xunit;

namespace TodoApp.Tests.CommandTests;

public class UnitTest1
{
    [Fact]
    public void DadoUmComandoInvalido()
    {
        // Arrange
        var command = new CreateTodoCommand("", Guid.Empty, DateTime.Now);

        // Act
        command.Validate();

        // Assert
        command.Valid.Should().BeFalse();
    }

    [Fact]
    public void DadoUmComandoValido()
    {
        // Arrange
        var command = new CreateTodoCommand("titulo", Guid.NewGuid(), DateTime.Now);

        // Act
        command.Validate();

        // Assert
        command.Valid.Should().BeTrue();
    }
}
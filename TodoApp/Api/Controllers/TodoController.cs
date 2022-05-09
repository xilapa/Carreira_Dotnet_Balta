using Domain.Commands;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Domain.Commands.Contracts;
using TodoApp.Domain.Commands.CreateTodo;
using TodoApp.Domain.Entities;

namespace Api.Controllers;

[ApiController]
[Route("v1/todos")]
public class TodoController : ControllerBase
{
    private readonly TodoHandler _handler;
    private readonly ITodoRepository _repository;

    public TodoController(TodoHandler handler, ITodoRepository repository)
    {
        _handler = handler;
        _repository = repository;
    }

    [HttpPost]
    [Route("")]
    public async Task<ICommandResult> Create([FromBody] CreateTodoCommand command) =>
        await _handler.Handle(command);

    [HttpGet]
    [Route("")]
    public async Task<IEnumerable<TodoItem>> GetAll() =>
        await _repository.GetAll(Guid.NewGuid());

    [Route("undone/today")]
    [HttpGet]
    public async Task<IEnumerable<TodoItem>> GetInactiveForToday() =>
        await _repository.GetByDate(
            Guid.NewGuid(),
            DateTime.Now.Date,
            false
        );
}
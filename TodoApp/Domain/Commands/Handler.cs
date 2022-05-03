using Domain.Repositories;
using TodoApp.Domain.Commands.Base;
using TodoApp.Domain.Commands.Contracts;
using TodoApp.Domain.Commands.CreateTodo;
using TodoApp.Domain.Entities;

namespace Domain.Commands;

public class Handler : BaseHandler, IHandler<CreateTodoCommand>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IUnitOfWork _uow;

    public Handler(ITodoRepository todoRepository, IUnitOfWork uow)
    {
        _todoRepository = todoRepository;
        _uow = uow;
    }

    public async Task<ICommandResult> Handle(CreateTodoCommand command)
    {
        // Validar command
        if(command.IsInvalid())
            return new CommandResult(false, "A tarefa está errada", command.Errors);

        // Criar todo
        var todo = new TodoItem(command.Title, command.Date, command.UserId);

        // Salvar todo no banco
        _todoRepository.Create(todo);
        await _uow.SaveChangesAsync();

        // Retornar resultado
        return new CommandResult(true, "Tarefa criada com sucesso", todo);
    }
}
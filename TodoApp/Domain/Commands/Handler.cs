using Domain.Repositories;
using TodoApp.Domain.Commands.Base;
using TodoApp.Domain.Commands.Contracts;
using TodoApp.Domain.Commands.CreateTodo;
using TodoApp.Domain.Commands.MarkTodoAsDone;
using TodoApp.Domain.Commands.MarkTodoAsUndone;
using TodoApp.Domain.Commands.UpdateTodo;
using TodoApp.Domain.Entities;

namespace Domain.Commands;

public class Handler : BaseHandler, IHandler<CreateTodoCommand>, IHandler<UpdateTodoCommand>, IHandler<MarkTodoAsDoneCommand>, IHandler<MarkTodoAsUndoneCommand>
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

    public async Task<ICommandResult> Handle(UpdateTodoCommand command)
    {
        // Validar command
        if(command.IsInvalid())
            return new CommandResult(false, "A tarefa está errada", command.Errors);

        // Obter todo do banco
        var todo = await _todoRepository.Get(command.Id, command.UserId);

        // Verificar se todo existe
        if(todo is null)
            return new CommandResult(false, "Tarefa não encontrada", null);

        // Atualizar todo
        todo.UpdateTitle(command.Title);

        // Salvar todo no banco
        await _uow.SaveChangesAsync();
        return new CommandResult(true, "Tarefa atualizada com sucesso", todo);
    }

    public async Task<ICommandResult> Handle(MarkTodoAsDoneCommand command)
    {
        // Validar command
        if(command.IsInvalid())
            return new CommandResult(false, "A tarefa está errada", command.Errors);

        // Obter todo do banco
        var todo = await _todoRepository.Get(command.Id, command.UserId);

        // Verificar se todo existe
        if(todo is null)
            return new CommandResult(false, "Tarefa não encontrada", null);

        // Atualizar todo
        todo.MarkAsDone();

        // Salvar todo no banco
        await _uow.SaveChangesAsync();
        return new CommandResult(true, "Tarefa marcada como concluída", todo);
    }

    public async Task<ICommandResult> Handle(MarkTodoAsUndoneCommand command)
    {
        // Validar command
        if(command.IsInvalid())
            return new CommandResult(false, "A tarefa está errada", command.Errors);

        // Obter todo do banco
        var todo = await _todoRepository.Get(command.Id, command.UserId);

        // Verificar se todo existe
        if(todo is null)
            return new CommandResult(false, "Tarefa não encontrada", null);

        // Atualizar todo
        todo.MarkAsUndone();

        // Salvar todo no banco
        await _uow.SaveChangesAsync();
        return new CommandResult(true, "Tarefa marcada como concluída", todo);
    }
}
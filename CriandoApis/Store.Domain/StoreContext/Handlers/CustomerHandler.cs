using Store.Domain.StoreContext.Commands.CustomerCommand.Inputs;
using Store.Domain.StoreContext.Commands.CustomerCommand.Outputs;
using Store.Shared.Command;

namespace Store.Domain.StoreContext.Handlers;

public class CustomerHandler : ICommandHandler<CreateCustomerCommand>, ICommandHandler<AddAddressCommand>
{
    public ICommandResult Handle(CreateCustomerCommand command)
    {
        // cria cliente
        
        return new CreateCustomerCommandResult();
    }

    public ICommandResult Handle(AddAddressCommand command)
    {
        throw new NotImplementedException();
    }
}
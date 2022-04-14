using Store.Shared.Command;

namespace Store.Domain.StoreContext.Commands.CustomerCommand.Inputs;

public class CreateCustomerCommand : ICommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    
    public bool Valid()
    {
        // validar command
        return true;
    }
}
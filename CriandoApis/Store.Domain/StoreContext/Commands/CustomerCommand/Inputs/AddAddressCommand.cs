using Store.Shared.Command;

namespace Store.Domain.StoreContext.Commands.CustomerCommand.Inputs;

public class AddAddressCommand : ICommand
{
    public Guid Id { get; set; }
    public string Street { get; set; }
    public string Number { get;  set; }
    public string Complement { get; set; }
    public string District { get;  set; }
    public string City { get;  set; }
    public string State { get;  set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
    
    public bool Valid()
    {
        //validar command
        return true;
    }
}
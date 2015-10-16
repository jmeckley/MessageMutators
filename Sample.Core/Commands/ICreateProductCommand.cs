namespace Sample.Core.Commands
{
    public class ICreateProductCommand
    {
        string ProductId { get; set; }

        string ProductName { get; set; }

        decimal ListPrice { get; set; }

        byte[] Image { get; set; }
    }
}

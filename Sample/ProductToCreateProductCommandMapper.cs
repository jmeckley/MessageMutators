namespace Sample
{
    using Sample.Core;
    using Sample.Core.Commands;

    public class ProductToCreateProductCommandMapper
        : IMapper<Product, CreateProductCommand>
    {
        public CreateProductCommand Map(Product item)
        {
            return new CreateProductCommand
            {
                Image = item.Image,
                ListPrice = item.ListPrice,
                ProductId = item.ProductId,
                ProductName = item.ProductName
            };
        }
    }
}
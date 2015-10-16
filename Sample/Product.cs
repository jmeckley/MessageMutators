namespace Sample
{
    public class Product
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ListPrice { get; set; }

        public byte[] Image { get; set; }

        public override string ToString()
        {
            return string.Format("Product: ProductId={0}, ProductName={1}, ListPrice={2} Image (length)={3}", ProductId, ProductName, ListPrice, (Image == null ? 0 : Image.Length));
        }
    }
}
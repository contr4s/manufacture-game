namespace Domain.Items
{
    public class Product : Item<ProductType>
    {
        public Product(ProductType type, int count = 0) : base(type, count) { }
    }
}
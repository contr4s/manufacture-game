namespace Domain.Items
{
    public class Resource : Item<ResourceType>
    {
        public Resource(ResourceType type, int count = 0) : base(type, count) { }
    }
}
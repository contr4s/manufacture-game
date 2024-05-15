namespace Domain.Items
{
    public class Resource : Item<ResourceType>
    {
        public Resource(ResourceType type, int count = 1) : base(type, count) { }
    }
}
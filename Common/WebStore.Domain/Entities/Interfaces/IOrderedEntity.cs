namespace WebStore.Domain.Entities.Interfaces
{
    public interface IOrderedEntity : IEntity
    {
        int Order { get; set; }
    }
}

namespace WebStore.Domain.Entities.Interfaces
{
    public interface INamedEntity : IEntity
    {
        string Name { get; set; }
    }
}

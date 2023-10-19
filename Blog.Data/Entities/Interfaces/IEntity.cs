namespace Blog.Data.Entities.Interfaces
{
    public interface IEntity
    {
        string Id { get; set; }
        string NormalizedTag { get; set; }
        string CreatorId { get; set; }
        DateTime CreationDate { get; set; }
        string LastModifierId { get; set; }
        DateTime LastModifiedOn { get; set; }
        bool Deleted { get; set; }
    }
}

namespace Blog.Data.Common.Entities.Interfaces
{
    public interface ISoftDeletableEntity
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}

namespace Blog.Data.Common.Entities
{
    using Interfaces;

    public abstract class BaseSoftDeletableEntity<TKey> : BaseEntity<TKey>, ISoftDeletableEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}

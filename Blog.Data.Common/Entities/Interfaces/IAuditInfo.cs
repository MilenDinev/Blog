namespace Blog.Data.Common.Entities.Interfaces
{
    public interface IAuditInfo<TKey>
    {
        string NormalizedTag { get; set; }
        TKey CreatorId { get; set; }
        DateTime? CreatedOn { get; set; }
        TKey LastModifierId { get; set; }
        DateTime? LastModifiedOn { get; set; }
    }
}
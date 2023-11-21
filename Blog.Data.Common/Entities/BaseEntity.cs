namespace Blog.Data.Common.Entities
{
    using System.ComponentModel.DataAnnotations;
    using Interfaces;

    public abstract class BaseEntity<TKey> : IAuditInfo<TKey>
    {
        [Key]
        public TKey Id { get; set; }
        public string NormalizedTag { get; set; }
        public TKey CreatorId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public TKey LastModifierId { get; set; }
        public DateTime? LastModifiedOn { get; set; }      
    }
}

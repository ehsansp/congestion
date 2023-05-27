namespace CodePractice.Framework.Common.AuditableEntities;

public interface IAuditableEntity
{
    int? CreatedById { get; set; }
    DateTime? CreatedOn { get; set; }
    int? LastModifiedById { get; set; }
    DateTime? LastModifiedOn { get; set; }

}

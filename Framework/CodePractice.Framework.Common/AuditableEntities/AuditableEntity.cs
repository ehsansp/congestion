namespace CodePractice.Framework.Common.AuditableEntities;
using Swashbuckle.AspNetCore.Annotations;

public class AuditableEntity : IAuditableEntity
{

    [SwaggerSchema(ReadOnly = true)]
    public int? CreatedById { get; set; }

    [SwaggerSchema(ReadOnly = true)]
    public DateTime? CreatedOn { get; set; }

    [SwaggerSchema(ReadOnly = true)]
    public int? LastModifiedById { get; set; }

    [SwaggerSchema(ReadOnly = true)]
    public DateTime? LastModifiedOn { get; set; }

}

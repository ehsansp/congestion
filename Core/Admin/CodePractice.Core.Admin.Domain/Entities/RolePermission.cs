using CodePractice.Framework.Domain.Entities;

namespace CodePractice.Core.Admin.Domain.Entities;

public class RolePermission: AggregateRoot
{
    public int? RoleId { get; set; }
    public int? MenuId { get; set; }

    public virtual Menu? Menu { get; set; }
    public virtual Role? Role { get; set; }
}
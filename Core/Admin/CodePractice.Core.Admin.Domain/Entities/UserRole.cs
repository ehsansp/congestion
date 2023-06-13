using CodePractice.Framework.Domain.Entities;

namespace CodePractice.Core.Admin.Domain.Entities;

public class UserRole: AggregateRoot
{
    public int? UserId { get; set; }
    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }
    public virtual User? User { get; set; }
}

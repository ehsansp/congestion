using CodePractice.Framework.Domain.Entities;

namespace CodePractice.Core.Admin.Domain.Entities;

public class Role: AggregateRoot
{
    public Role()
    {
        RolePermissions = new HashSet<RolePermission>();
        UserRoles = new HashSet<UserRole>();
    }

    public string? Title { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
}

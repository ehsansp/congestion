namespace CodePractice.Core.Admin.Domain.Entities;

public partial class Role
{
    public Role()
    {
        RolePermissions = new HashSet<RolePermission>();
        UserRoles = new HashSet<UserRole>();
    }

    public int Id { get; set; }
    public string? Title { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
}

namespace CodePractice.Core.Admin.Domain.Entities;

public partial class RolePermission
{
    public int Id { get; set; }
    public int? RoleId { get; set; }
    public int? MenuId { get; set; }

    public virtual Menu? Menu { get; set; }
    public virtual Role? Role { get; set; }
}
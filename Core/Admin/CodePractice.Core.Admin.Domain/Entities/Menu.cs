namespace CodePractice.Core.Admin.Domain.Entities;

public partial class Menu
{
    public Menu()
    {
        InverseParent = new HashSet<Menu>();
        RolePermissions = new HashSet<RolePermission>();
        UserMenus = new HashSet<UserMenu>();
    }

    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string? Title { get; set; }
    public string? Url { get; set; }
    public bool? IsMenu { get; set; }
    public string? IconId { get; set; }
    public int? Order { get; set; }

    public virtual Menu? Parent { get; set; }
    public virtual ICollection<Menu> InverseParent { get; set; }
    public virtual ICollection<RolePermission> RolePermissions { get; set; }
    public virtual ICollection<UserMenu> UserMenus { get; set; }
}

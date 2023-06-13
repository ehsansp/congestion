using CodePractice.Framework.Domain.Entities;

namespace CodePractice.Core.Admin.Domain.Entities;

public class User: AggregateRoot
{
    public User()
    {
        UserMenus = new HashSet<UserMenu>();
        UserRoles = new HashSet<UserRole>();
    }

    public string? UserName { get; set; }
    public string? Password { get; set; }
    public bool? Active { get; set; }
    public string? Token { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public virtual ICollection<UserMenu> UserMenus { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
}


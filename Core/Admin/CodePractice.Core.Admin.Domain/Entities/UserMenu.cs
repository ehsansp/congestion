using CodePractice.Framework.Domain.Entities;

namespace CodePractice.Core.Admin.Domain.Entities;

public class UserMenu: AggregateRoot
{
    public int? UserId { get; set; }
    public int MenuId { get; set; }

    public virtual Menu Menu { get; set; } = null!;
    public virtual User? User { get; set; }
}

namespace CodePractice.Core.Admin.Domain.Entities;

public partial class UserMenu
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public int MenuId { get; set; }

    public virtual Menu Menu { get; set; } = null!;
    public virtual User? User { get; set; }
}

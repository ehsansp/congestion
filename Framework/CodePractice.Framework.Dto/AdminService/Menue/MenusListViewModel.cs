namespace CodePractice.Framework.Dto.AdminService.Menue;

public class MenusListViewModel
{
    public string Title { get; set; }
    public string Url { get; set; }
    public string? IconId { get; set; }
    public ICollection<MenusListViewModel> Children { get; set; }
}

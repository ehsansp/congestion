namespace CodePractice.Framework.Dto;

public interface IPageable
{
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public string Sort { get; set; }
    public string SortDir { get; set; }
}

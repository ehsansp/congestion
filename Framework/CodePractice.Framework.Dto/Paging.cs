using System;

namespace CodePractice.Framework.Dto;

public class Paging : IPageable
{
    public string? Title { get; set; }
    const int _maxSize = 1000000;

    private int pageSize = 20;
    public int PageSize
    {
        get => pageSize;
        set
        {
            if (value > _maxSize)
                pageSize = _maxSize;
            else
                pageSize = value;
        }
    }
    public int CurrentPage { get; set; } = 1;
    public string? Sort { get; set; }
    public string? SortDir { get; set; }
}

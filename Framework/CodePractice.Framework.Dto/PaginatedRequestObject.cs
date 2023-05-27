using System.Linq.Expressions;

namespace CodePractice.Framework.Dto;

public class PaginatedRequestObject<Entity, SelectTo, TKeySelector> where SelectTo : class, new()
{
    //DO NOT ADD PUBLIC DEFAULT CONSTRUCTOR
    public PaginatedRequestObject(
        Expression<Func<Entity, TKeySelector>> orderBy,
        Expression<Func<Entity, SelectTo>> select,
        int page = 1,
        int pageSize = 10,
        CancellationToken token = default(CancellationToken))
    {
        OrderBy = orderBy;
        Select = select;
        Page = page;
        PageSize = pageSize;
        CancellationToken = token;
    }
    public Expression<Func<Entity, TKeySelector>> OrderBy { get; private set; }
    public Expression<Func<Entity, SelectTo>> Select { get; private set; }
    public int Page { get; private set; } = 1;
    public int PageSize { get; private set; } = 10;
    public CancellationToken CancellationToken { get; private set; } = default(CancellationToken);
}

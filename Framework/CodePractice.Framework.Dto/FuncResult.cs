using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CodePractice.Framework.Dto;

public class FuncResult<T> : FuncResult
{
    public T Entity { get; set; }
    public FuncResult() : base()
    {

    }
}
public class FuncResult
{
    public Status Status { set; get; }
    public object ID { set; get; }
    public Error Error { set; get; }

    public object Entity { set; get; }
    public long? Total { set; get; }


    public FuncResult()
    {
        Status = Status.Fail;
        Error = new Error() { Code = "0", Message = string.Empty };
        ID = 0;
    }

    public static FuncResult CreateValidationError(ModelStateDictionary ModelState)
    {
        var fr = new FuncResult();
        fr.Status = Status.Fail;

        fr.Error = new Error()
        {
            Code = "Validation",
            Message = ModelState
                .ToDictionary(x => x.Key, y => y.Value.Errors)
                .Select(z => z.Value.FirstOrDefault().ErrorMessage).FirstOrDefault()
        };
        return fr;
    }

    public static FuncResult CreateOkResult(long id = 0, object? entity = null)
    {
        var fr = new FuncResult();
        fr.Status = Status.Ok;
        fr.ID = id;

        if (entity != null)
        {
            fr.Entity = entity;
        }

        return fr;
    }

    public static FuncResult CreateGetResult(object paginatedResult, long total = 1)
    {
        var fr = new FuncResult();
        fr.Status = Status.Ok;
        fr.Total = total;
        fr.Entity = paginatedResult;
        return fr;
    }
    public static FuncResult CreateValidationError(Error error)
    {
        var fr = new FuncResult();
        fr.Status = Status.Fail;
        fr.Error = error;
        return fr;
    }
}

using CodePractice.Framework.Dto;
using User = CodePractice.Core.Admin.Domain.Entities.User;

namespace CodePractice.Core.Admin.Service;

public interface IUserService : IServiceBase<User>
{
    Task<FuncResult> Atourize(Login login);
    Task<FuncResult> ChangePassword(ChangePassword changePassword);
    Task<FuncResult> GetMenues(long userId);
}
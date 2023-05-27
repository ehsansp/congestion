using CodePractice.Core.Admin.Domain.Entities;
using CodePractice.Core.Admin.Repository;
using CodePractice.Framework.Dto;
using CodePractice.Framework.Dto.AdminService.Menue;
using CodePractice.Framework.Tools;
using CodePractice.Core.Admin.Domain.Entities;
using CodePractice.Framework.Security;

namespace CodePractice.Core.Admin.Service;

public class UserService : ServiceBase<Domain.Entities.User>, IUserService
{
    public async Task<FuncResult> ChangePassword(ChangePassword changePassword)
    {
        UnitWork<Domain.Entities.User> unitWork = new UnitWork<Domain.Entities.User>();

        var user = await unitWork.EntityRepository
            .FirstOrDefault(x => x.Id == changePassword.UserId);
        if (user != null)
        {
            if (user.Password != PasswordHelper.EncodePasswordMd5(changePassword.OldPassword))
            {


                throw new Exception("تغییر رمز عبور با خطا مواجه شد.");
            }
            else
            {

                user.Password = PasswordHelper.EncodePasswordMd5(changePassword.Password);
                await unitWork.EntityRepository.Save();
                return new FuncResult
                {
                    Status = Status.Ok
                    //Error = "رمز عبور قبلی اشتباه است.",
                };
            }

        }
        user.Password = PasswordHelper.EncodePasswordMd5(changePassword.Password);
        await unitWork.EntityRepository.Save();

        return new FuncResult
        {
            Status = Status.Ok

        };
    }

    public async Task<FuncResult> GetMenues(long userId)
    {
        UnitWork<Domain.Entities.User> unitWork = new UnitWork<Domain.Entities.User>();

        var user = await unitWork.EntityRepository
            .FirstOrDefault(x => x.Id == userId, "UserMenus.Menu");

        var menues = user.UserMenus.Select(x => x.Menu)
            .Where(x => x.IsMenu.HasValue && x.IsMenu.Value)
            .OrderBy(x => x.Order)
            .ToList();

        var result = GenerateMenusTreeModel(null, menues);

        return new FuncResult
        {
            Status = Status.Ok,
            Entity = result,
        };

    }
    public async Task<FuncResult> Atourize(Login login)
    {
        if (login.UserName == null)
        {
            return new FuncResult() { Status = Status.Fail, ID = 0, Error = new Error() { Code = "u101", Message = "UserName not valid" } };
        }
        if (login.Password == null)
        {
            return new FuncResult() { Status = Status.Fail, ID = 0, Error = new Error() { Code = "u101", Message = "Password not valid" } };
        }

        UnitWork<Domain.Entities.User> unitWork = new UnitWork<Domain.Entities.User>();
        FuncResult funcResult = new FuncResult();

        var user = await unitWork.EntityRepository
            .FirstOrDefault(x => x.UserName == login.UserName && x.Password == PasswordHelper.EncodePasswordMd5(login.Password), "UserRoles.Role.RolePermissions");

        if (user == null)
        {
            return new FuncResult() { Status = Status.Fail, ID = 0, Error = new Error() { Code = "u101", Message = "Invalid username or password." } };
        }

        var enty = user.MapProperties<CodePractice.Framework.Dto.User>();
        var permissions = user.UserRoles.SelectMany(x => x.Role.RolePermissions);
        enty.Roles = permissions?.Select(x => x.MenuId.ToString())?.ToList();

        if (user != null)
        {
            if (string.IsNullOrEmpty(user.Token))
            {
                user.Token = Tool.RandomString(128);

                enty.Token = user.Token;
                await unitWork.EntityRepository.Save();
            }

            funcResult = new FuncResult()
            {
                ID = user.Id,
                Status = Status.Ok,
                Error = null,
                Entity = enty
            };
        }
        else
        {
            return new FuncResult() { Status = Status.Fail, ID = 0, Error = new Error() { Code = "u101", Message = "user not valid" } };
        }
        return funcResult;
    }

    public List<MenusListViewModel> GenerateMenusTreeModel(long? parentId, List<Menu> nodes)
    {
        List<MenusListViewModel> menues = new List<MenusListViewModel>();
        foreach (var item in nodes.Where(x => x.ParentId == parentId))
        {
            menues.Add(new MenusListViewModel
            {
                Title = item.Title,
                Url = item.Url,
                IconId = item.IconId,
                Children = GenerateMenusTreeModel(item.Id, nodes)
            });
        }
        return menues;
    }
}

using CodePractice.Core.Admin.Service;
using CodePractice.Framework.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePractice.Service.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<ActionResult<FuncResult>> Menues(long UserId)
        {
            FuncResult funcResult = new FuncResult();
            funcResult = await _userService.GetMenues(UserId);
            return funcResult;
        }

        [HttpPost]
        public async Task<ActionResult<FuncResult>> Authenticate([FromBody] Login login)
        {
            FuncResult funcResult = new FuncResult();
            funcResult = await _userService.Atourize(login);
            return funcResult;
        }

        [HttpPost]
        public async Task<ActionResult<FuncResult>> ChangePassword([FromBody] ChangePassword changePassword)
        {
            FuncResult funcResult = new FuncResult();
            funcResult = await _userService.ChangePassword(changePassword);
            return funcResult;
        }

    }
}

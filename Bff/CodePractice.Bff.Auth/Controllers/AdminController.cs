using CodePractice.Framework.Dto;
using CodePractice.Framework.Functions;
using CodePractice.Framework.Security;
using CodePractice.Framework.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CodePractice.Bff.Auth.Controllers
{
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly HttpClientWrapper _httpClientWrapper;
        protected readonly IWebHostEnvironment HostEnvironment;
        private readonly IToken Token;
        public AdminController(IConfiguration iConfig, HttpClientWrapper httpClientWrapper, IWebHostEnvironment hostEnvironment, IToken token)
        {
            configuration = iConfig;
            _httpClientWrapper = httpClientWrapper;
            HostEnvironment = hostEnvironment;
            Token = token;
        }

        [HttpPost]
        [Route("api/Admin/Authenticate")]
        public async Task<FuncResult> Authenticate([FromBody] Login login)
        {
            FuncResult response;
            Login lg = new Login()
            {
                UserName = login.UserName.PersianNumbersToEnglish(),
                Password = login.Password.PersianNumbersToEnglish()
            };
            response = await _httpClientWrapper
                   .PostAsync(StaticParams.AutorizeBaseUrl, "/api/User/Authenticate", lg);

            if (response.Status == Framework.Dto.Status.Ok)
            {
                var d = JsonConvert.DeserializeObject<Framework.Dto.User>(
                    JsonConvert.SerializeObject(response.Entity));
                var res = Token.addToken(d);
                d.Token = res.Token;
                response.Entity = d;
            }

            return response;
        }


        [HttpGet]
        [Route("api/Admin/Autorize")]
        public async Task<Framework.Dto.FuncResult> Autorize([FromHeader] string token)
        {
            Framework.Dto.FuncResult funcResult = new Framework.Dto.FuncResult();
            var User = Token.getUserModel(token);
            if (User == null)
            {
                //Response.StatusCode = StatusCodes.Status401Unauthorized;
                return new Framework.Dto.FuncResult() { ID = 0, Status = Framework.Dto.Status.Fail, Error = new Framework.Dto.Error() { Code = "Z01", Message = "UnAutorizetion" } };
            }

            funcResult.ID = User.UserID;
            funcResult.Status = Framework.Dto.Status.Ok;
            funcResult.Error = null;
            funcResult.Entity = User;
            return funcResult;
        }

        [HttpGet]
        [Route("api/Admin/UpdateToken")]
        public async Task<Framework.Dto.FuncResult> UpdateToken([FromHeader] string token)
        {
            Framework.Dto.FuncResult funcResult = new Framework.Dto.FuncResult();
            var UserID = Token.getUser(token);
            if (UserID == null)
            {
                //Response.StatusCode = StatusCodes.Status401Unauthorized;
                return new Framework.Dto.FuncResult() { ID = 0, Status = Framework.Dto.Status.Fail, Error = new Framework.Dto.Error() { Code = "1", Message = "UnAutorizetion" } };
            }
            //TODO : update token
            //Token.updateToken(token);
            funcResult.ID = UserID;
            funcResult.Status = Framework.Dto.Status.Ok;
            funcResult.Error = null;
            funcResult.Entity = null;
            return funcResult;
        }

        [HttpGet]
        [Route("api/Admin/GetAllToken")]
        public async Task<Framework.Dto.FuncResult> GetAllToken()
        {
            Framework.Dto.FuncResult funcResult = new Framework.Dto.FuncResult();
            var All = Token.getAll();


            funcResult.ID = All.Count;
            funcResult.Status = Framework.Dto.Status.Ok;
            funcResult.Error = null;
            funcResult.Entity = All;
            return funcResult;
        }

        [HttpGet]
        [Route("api/Admin/Expire")]
        public async Task<FuncResult> ExpireToken([FromHeader] string token)
        {
            Framework.Dto.FuncResult funcResult = new Framework.Dto.FuncResult();
            Token.Expire(token);
            funcResult.Status = Framework.Dto.Status.Ok;
            funcResult.Error = null;
            return funcResult;
        }
    }
}

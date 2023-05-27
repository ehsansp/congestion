using System.Collections.Concurrent;
using Azure.Core;
using CodePractice.Framework.Dto;
using CodePractice.Framework.Functions;
using CodePractice.Framework.Security;
using CodePractice.Framework.Tools;
using CodePractice.Product.ApplicationService.Features.Person.Commands.CreatePerson;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace CodePractice.Bff.Admin.Controllers
{
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly HttpClientWrapper _httpClientWrapper;
        protected readonly IWebHostEnvironment HostEnvironment;
        private readonly IToken Token;

        public AdminController(IConfiguration iConfig, HttpClientWrapper httpClientWrapper,
            IWebHostEnvironment hostEnvironment, IToken token)
        {
            configuration = iConfig;
            _httpClientWrapper = httpClientWrapper;
            HostEnvironment = hostEnvironment;
            Token = token;
        }

        [HttpPost]
        [Common.Autorize]
        public async Task<IActionResult> Add([FromForm] CreateProductCommand command)
        {
            FuncResult response;
            var ids = new ConcurrentBag<long>();

            
            response = await _httpClientWrapper.PostAsync(Framework.Functions.StaticParams.CrmServiceURL, "api/product/add", command);
            return Ok(response);

        }
    }
}
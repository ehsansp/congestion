using CodePractice.Framework.Dto;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;

namespace CodePractice.Framework.Tools;

public class HttpClientWrapper
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public HttpClientWrapper(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<FuncResult> GetAsync(Uri uri)
    {
        FuncResult funcResult = new Framework.Dto.FuncResult();

        var httpClient = _httpClientFactory.CreateClient();
        if (_httpContextAccessor.HttpContext.Items.ContainsKey("USERID"))
        {
            var userId = (long)(_httpContextAccessor?.HttpContext?.Items?.FirstOrDefault(x => x.Key == "USERID")
                .Value ?? 0);
            if (userId > 0)
            {
                httpClient.DefaultRequestHeaders.Add("USERID", userId.ToString());
            }
        }
        var httpResponseMessage = await httpClient.GetAsync(uri);

        //if (httpResponseMessage.IsSuccessStatusCode)
        //{
        var content = await httpResponseMessage.Content.ReadAsStringAsync();

        if (httpResponseMessage.StatusCode > HttpStatusCode.BadRequest)
        {
            return funcResult = new FuncResult()
            {
                Status = Status.Fail,
                Error = new Error()
                {
                    Message = content
                }
            };
        }

        if (this.IsValidJson(content))
        {
            funcResult = System.Text.Json.JsonSerializer.Deserialize<Framework.Dto.FuncResult>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        else
        {
            funcResult.Entity = content;
        }
        //}

        return funcResult;
    }

    public async Task<FuncResult> PostAsync<T>(string baseUrl, string path, T data)
    {
        FuncResult funcResult = new FuncResult();

        var httpClient = _httpClientFactory.CreateClient();
        if (_httpContextAccessor.HttpContext.Items.ContainsKey("USERID"))
        {
            var userId = (long)(_httpContextAccessor?.HttpContext?.Items?.FirstOrDefault(x => x.Key == "USERID")
                .Value ?? 0);
            if (userId > 0)
            {
                httpClient.DefaultRequestHeaders.Add("USERID", userId.ToString());
            }
        }

        var httpResponseMessage = await httpClient.PostAsJsonAsync<T>(baseUrl + path, data);

        //if (httpResponseMessage.IsSuccessStatusCode)  
        //{
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        if (httpResponseMessage.StatusCode > HttpStatusCode.BadRequest)
        {
            return funcResult = new FuncResult()
            {
                Status = Status.Fail,
                Error = new Error()
                {
                    Message = content
                }
            };
        }
        funcResult = JsonConvert.DeserializeObject<FuncResult>(content);
        //}
        return funcResult;
    }

    public async Task<FuncResult> PostWithResponceAsync<T>(string baseUrl, string path, T data)
    {
        FuncResult funcResult = new FuncResult();

        var httpClient = _httpClientFactory.CreateClient();
        if (_httpContextAccessor.HttpContext.Items.ContainsKey("USERID"))
        {
            var userId = (long)(_httpContextAccessor?.HttpContext?.Items?.FirstOrDefault(x => x.Key == "USERID")
                .Value ?? 0);
            if (userId > 0)
            {
                httpClient.DefaultRequestHeaders.Add("USERID", userId.ToString());
            }
        }
        var httpResponseMessage = await httpClient.PostAsJsonAsync<T>(baseUrl + path, data);

        //if (httpResponseMessage.IsSuccessStatusCode)
        //{
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        if (httpResponseMessage.StatusCode > HttpStatusCode.BadRequest)
        {
            return funcResult = new FuncResult()
            {
                Status = Status.Fail,
                Error = new Error()
                {
                    Message = content
                }
            };
        }
        funcResult = System.Text.Json.JsonSerializer.Deserialize<Framework.Dto.FuncResult>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        //}
        return funcResult;
    }

    public async Task<FuncResult> PutAsync<T>(string baseUrl, string path, T data)
    {
        FuncResult funcResult = new FuncResult();

        var httpClient = _httpClientFactory.CreateClient();
        if (_httpContextAccessor.HttpContext.Items.ContainsKey("USERID"))
        {
            var userId = (long)(_httpContextAccessor?.HttpContext?.Items?.FirstOrDefault(x => x.Key == "USERID")
                .Value ?? 0);
            if (userId > 0)
            {
                httpClient.DefaultRequestHeaders.Add("USERID", userId.ToString());
            }
        }

        var httpResponseMessage = await httpClient.PutAsJsonAsync<T>(baseUrl + path, data);

        //if (httpResponseMessage.IsSuccessStatusCode)
        //{
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        if (httpResponseMessage.StatusCode > HttpStatusCode.BadRequest)
        {
            return funcResult = new FuncResult()
            {
                Status = Status.Fail,
                Error = new Error()
                {
                    Message = content
                }
            };
        }
        funcResult = JsonConvert.DeserializeObject<FuncResult>(content);

        //}

        return funcResult;
    }

    public async Task<FuncResult> DeleteAsync(string baseUrl, string path)
    {
        FuncResult funcResult = new FuncResult();

        var httpClient = _httpClientFactory.CreateClient();
        if (_httpContextAccessor.HttpContext.Items.ContainsKey("USERID"))
        {
            var userId = (long)(_httpContextAccessor?.HttpContext?.Items?.FirstOrDefault(x => x.Key == "USERID")
                .Value ?? 0);
            if (userId > 0)
            {
                httpClient.DefaultRequestHeaders.Add("USERID", userId.ToString());
            }
        }
        var httpResponseMessage = await httpClient.DeleteAsync(baseUrl + path);

        //if (httpResponseMessage.IsSuccessStatusCode)
        //{
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        if (httpResponseMessage.StatusCode > HttpStatusCode.BadRequest)
        {
            return funcResult = new FuncResult()
            {
                Status = Status.Fail,
                Error = new Error()
                {
                    Message = content
                }
            };
        }
        funcResult = JsonConvert.DeserializeObject<FuncResult>(content);

        //}

        return funcResult;
    }

    public async Task<FuncResult> DeleteAsync<T>(string baseUrl, string path, T value)
    {
        FuncResult funcResult = new FuncResult();

        var httpClient = _httpClientFactory.CreateClient();
        if (_httpContextAccessor.HttpContext.Items.ContainsKey("USERID"))
        {
            var userId = (long)(_httpContextAccessor?.HttpContext?.Items?.FirstOrDefault(x => x.Key == "USERID")
                .Value ?? 0);
            if (userId > 0)
            {
                httpClient.DefaultRequestHeaders.Add("USERID", userId.ToString());
            }
        }

        HttpRequestMessage request = new HttpRequestMessage
        {
            Content = JsonContent.Create(value),
            Method = HttpMethod.Delete,
            RequestUri = new Uri(baseUrl + path, UriKind.Absolute)
        };
        var httpResponseMessage = await httpClient.SendAsync(request);

        //if (httpResponseMessage.IsSuccessStatusCode)
        //{
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        if (httpResponseMessage.StatusCode > HttpStatusCode.BadRequest)
        {
            return funcResult = new FuncResult()
            {
                Status = Status.Fail,
                Error = new Error()
                {
                    Message = content
                }
            };
        }
        funcResult = JsonConvert.DeserializeObject<FuncResult>(content);

        //}

        return funcResult;
    }


    private bool IsValidJson(string strInput)
    {
        if (string.IsNullOrWhiteSpace(strInput)) { return false; }
        strInput = strInput.Trim();
        if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
            (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
        {
            try
            {
                var obj = JToken.Parse(strInput);
                return true;
            }
            catch (JsonReaderException jex)
            {
                //Exception in parsing json
                Console.WriteLine(jex.Message);
                return false;
            }
            catch (System.Exception ex) //some other exception
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}

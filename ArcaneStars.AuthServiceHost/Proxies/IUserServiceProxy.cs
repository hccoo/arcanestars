using ArcaneStars.AuthServiceHost.Configurations;
using ArcaneStars.AuthServiceHost.Proxies.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ArcaneStars.AuthServiceHost.Proxies
{
    public interface IUserServiceProxy
    {
        Task<UserDto> CheckUser(string userName, string password);

        Task<string> AddUser(string userName, string password, string mobile, string nickName, string email);

        Task<UserDto> GetUser(string userName);
    }

    public class UserServiceProxy : IUserServiceProxy
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IServiceConfigurationAgent _config;

        public UserServiceProxy(IHttpClientFactory clientFactory, IServiceConfigurationAgent config)
        {
            _config = config;
            _clientFactory = clientFactory;
        }

        public async Task<UserDto> CheckUser(string userName, string password)
        {
            UserDto result = null;

            var client = _clientFactory.CreateClient("arcanestars");
            var jsonStr = JsonConvert.SerializeObject(new UserDto { UserName = userName, Password = password });
            var content = new StringContent(jsonStr);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");//x-www-form-urlencoded
            content.Headers.Add(_config.UserServiceApiKey, _config.UserServiceApiValue);

            var response = await client.PostAsync($"{_config.UserServiceRootUrl}/api/user/check/v1", content);
            var contentJson = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                result = JsonConvert.DeserializeObject<UserDto>(contentJson);
            else
                result = null;

            return result;
        }

        public async Task<string> AddUser(string userName, string password, string mobile, string nickName, string email)
        {
            var result = string.Empty;
            var client = _clientFactory.CreateClient("arcanestars");
            var jsonStr = JsonConvert.SerializeObject(new UserDto { UserName = userName, Password = password, Mobile = mobile, NickName = nickName, Email = email });
            var content = new StringContent(jsonStr);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");//x-www-form-urlencoded
            content.Headers.Add(_config.UserServiceApiKey, _config.UserServiceApiValue);

            var response = await client.PostAsync($"{_config.UserServiceRootUrl}/api/user/v1", content);

            if (response.IsSuccessStatusCode) return "";

            var responseContent = await response.Content.ReadAsStringAsync();
            result = responseContent;
            return result;
        }

        public async Task<UserDto> GetUser(string userName)
        {
            UserDto result = null;

            var client = _clientFactory.CreateClient("arcanestars");
            client.DefaultRequestHeaders.Add(_config.UserServiceApiKey, _config.UserServiceApiValue);
            var response = await client.GetAsync($"{_config.UserServiceRootUrl}/api/user/v1/{userName}");

            var contentJson = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                result = JsonConvert.DeserializeObject<UserDto>(contentJson);
            else
                result = null;

            return result;
        }
    }
}

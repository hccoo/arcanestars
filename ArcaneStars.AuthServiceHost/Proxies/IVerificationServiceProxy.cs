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
    public interface IVerificationServiceProxy
    {
        Task<VerificationDto> GetVerification(BizCode bizCode, string to);

        Task SetVerificationUsed(BizCode bizCode, string code, string to);

        Task<VerificationDto> AddVerification(BizCode bizCode, string to);
    }

    public class VerificationServiceProxy : IVerificationServiceProxy
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IServiceConfigurationAgent _config;

        public VerificationServiceProxy(IHttpClientFactory clientFactory, IServiceConfigurationAgent config)
        {
            _config = config;
            _clientFactory = clientFactory;
        }

        public async Task<VerificationDto> AddVerification(BizCode bizCode, string to)
        {
            VerificationDto result = null;
            var client = _clientFactory.CreateClient("arcanestars");
            var jsonStr = JsonConvert.SerializeObject(new VerificationDto { BizCode=bizCode, To = to });
            var content = new StringContent(jsonStr);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");//x-www-form-urlencoded
            content.Headers.Add(_config.VerificationServiceApiKey, _config.VerificationServiceApiValue);

            var response = await client.PostAsync($"{_config.VerificationServiceRootUrl}api/verification/v1", content);

            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                result = JsonConvert.DeserializeObject<VerificationDto>(responseContent);
            else
                result = null;
            
            return result;
        }

        public async Task<VerificationDto> GetVerification(BizCode bizCode, string to)
        {
            VerificationDto result = null;

            var client = _clientFactory.CreateClient("arcanestars");
            client.DefaultRequestHeaders.Add(_config.VerificationServiceApiKey, _config.VerificationServiceApiValue);
            var response = await client.GetAsync($"{_config.VerificationServiceRootUrl}api/verification/v1/{(int)bizCode}/{to}");

            var contentJson = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                result = JsonConvert.DeserializeObject<VerificationDto>(contentJson);
            else
                result = null;

            return result;
        }

        public async Task SetVerificationUsed(BizCode bizCode, string code, string to)
        {
            VerificationDto result = null;
            var client = _clientFactory.CreateClient("arcanestars");
            var jsonStr = JsonConvert.SerializeObject(new VerificationDto { BizCode = bizCode, To = to, Code = code });
            var content = new StringContent(jsonStr);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");//x-www-form-urlencoded
            content.Headers.Add(_config.VerificationServiceApiKey, _config.VerificationServiceApiValue);

            var response = await client.PatchAsync($"{_config.VerificationServiceRootUrl}api/verification/v1", content);

            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                result = JsonConvert.DeserializeObject<VerificationDto>(responseContent);
            else
                result = null;
        }
    }
}

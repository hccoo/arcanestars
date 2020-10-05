using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArcaneStars.Service.Application.Dtos;
using ArcaneStars.Service.Applications.Services;
using ArcaneStars.Service.Domain.Aggregates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace ArcaneStars.VerificationServiceHost.Controllers
{
    [Route("api/[controller]")]
    public class VerificationController : Controller
    {
        readonly IVerificationAppService _verificationAppService;

        public VerificationController(IVerificationAppService verificationAppService)
        {
            _verificationAppService = verificationAppService;
        }

        // GET api/values/5
        [HttpGet]
        [Route("v1/{bizcode}/{to}")]
        public VerificationDTO Get(BizCode bizCode, string to)
        {
            return _verificationAppService.GetAvailableVerification(bizCode, to);
        }

        [HttpPatch]
        [Route("v1")]
        public void Patch([FromBody]VerificationDTO model)
        {
            _verificationAppService.SetVerificationUsed(model);
        }

        // POST api/values
        [HttpPost]
        [Route("v1")]
        public void Post([FromBody]VerificationDTO model)
        {
            _verificationAppService.AddVerification(model);
        }
    }
}

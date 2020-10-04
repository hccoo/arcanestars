using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArcaneStars.UserService.Applications.Dtos;
using ArcaneStars.UserService.Applications.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ArcaneStars.UserServiceHost.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        readonly IUserAppService _userAppService;
        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        // GET: api/<controller>/v1/username
        [HttpGet("{username}")]
        [Route("v1/{username}")]
        public async Task<UserDto> Get(string userName) => _userAppService.Get(userName);

        // POST api/<controller>
        [HttpPost]
        [Route("v1")]
        public async Task Post([FromBody]UserDto model) => _userAppService.AddUser(model, model.UserName);

        [HttpPost]
        [Route("check/v1")]
        public async Task<UserDto> CheckUser([FromBody]UserDto model) => _userAppService.CheckUser(model.UserName, model.Password);
    }
}

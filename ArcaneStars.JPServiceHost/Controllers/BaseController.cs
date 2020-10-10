using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArcaneStars.JPServiceHost.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected string UserName => Request.Headers["user_name"].FirstOrDefault();

    }
}
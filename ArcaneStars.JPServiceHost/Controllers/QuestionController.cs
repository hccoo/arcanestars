using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArcaneStars.JPService.Applications.Dtos;
using ArcaneStars.JPService.Applications.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArcaneStars.JPServiceHost.Controllers
{
    [ApiController]
    public class QuestionController : BaseController
    {
        readonly IQuestionAppService _questionAppService;

        public QuestionController(IQuestionAppService questionAppService)
        {
            _questionAppService = questionAppService;
        }

        [Route("api/question/v1")]
        [HttpPost]
        public void AddQuestion(QuestionDto model) => _questionAppService.AddQuestion(model, UserName);

        [Route("api/question/v1")]
        [HttpPut]
        public void UpdateQuestion(QuestionDto model) => _questionAppService.UpdateQuestion(model, UserName);

        [Route("api/questions/v1")]
        [HttpGet]
        public PagingResultDto<QuestionDto> Get(string key, string tag, int pageIndex, int pageSize) => _questionAppService.Get(key, tag, pageIndex, pageSize);

        [Route("api/question/v1")]
        [HttpGet]
        public QuestionDto Get(long id) => _questionAppService.Get(id);
    }
}
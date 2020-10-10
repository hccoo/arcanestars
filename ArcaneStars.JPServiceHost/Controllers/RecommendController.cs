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
    public class RecommendController : BaseController
    {
        readonly IRecommendAppService _recommendAppService;

        public RecommendController(IRecommendAppService recommendAppService)
        {
            _recommendAppService = recommendAppService;
        }

        [HttpPost]
        [Route("api/recommend/v1")]
        public void AddRecommend(RecommendDto model) => _recommendAppService.AddRecommend(model, UserName);

        [HttpPut]
        [Route("api/recommend/v1")]
        public void UpdateRecommend(RecommendDto model) => _recommendAppService.UpdateRecommend(model, UserName);

        [HttpGet]
        [Route("api/recommend/v1")]
        public RecommendDto Get(long id) => _recommendAppService.Get(id);

        [HttpGet]
        [Route("api/recommends/v1")]
        public PagingResultDto<RecommendDto> Get(long questionId, int pageIndex, int pageSize) => _recommendAppService.Get(pageIndex, pageSize, questionId);
    }
}
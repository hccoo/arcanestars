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
    public class CommentController : BaseController
    {
        readonly ICommentAppService _commentAppService;
        public CommentController(ICommentAppService commentAppService)
        {
            _commentAppService = commentAppService;
        }

        [Route("api/comment/v1")]
        [HttpPost]
        public void AddComment(CommentDto model) => _commentAppService.AddComment(model, UserName);

        [Route("api/comment/v1")]
        [HttpPut]
        public void UpdateComment(CommentDto model) => _commentAppService.UpdateComment(model, UserName);

        [Route("api/comment/v1")]
        [HttpGet]
        public CommentDto Get(long id) => _commentAppService.Get(id);

        [Route("api/comments/v1")]
        [HttpGet]
        public PagingResultDto<CommentDto> Get(long recommendId, int pageIndex, int pageSize, string mark) => _commentAppService.Get(recommendId, pageIndex, pageSize, mark);
    }
}
using ArcaneStars.JPService.Applications.Dtos;
using ArcaneStars.JPService.Domains.Aggregates;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.JPService.Configurations
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CommentDto, Comment>();
            CreateMap<Comment, CommentDto>();

            CreateMap<QuestionDto, Question>();
            CreateMap<Question, QuestionDto>();

            CreateMap<QuestionTagDto, QuestionTag>();
            CreateMap<QuestionTag, QuestionTagDto>();

            CreateMap<RecommendDto, Recommend>();
            CreateMap<Recommend, RecommendDto>();

            CreateMap<RecommendMediaDto, RecommendMedia>();
            CreateMap<RecommendMedia, RecommendMediaDto>();

            CreateMap<RecommendSpecDto, RecommendSpec>();
            CreateMap<RecommendSpec, RecommendSpecDto>();
        }
    }
}

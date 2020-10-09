using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.JPService.Applications.Dtos
{
    public class ResultDto<T>
    {
        public BusinessCode Code { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }
    }
    public class ResultDto
    {
        public BusinessCode Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }

    public class PagingResultDto<T>
    {
        public BusinessCode Code { get; set; }

        public IEnumerable<T> Data { get; set; }

        public string Message { get; set; }

        public long Total { get; set; }
    }

    public enum BusinessCode
    {
        Successed = 200,
        Failed = 400,
        Error = 500
    }
}

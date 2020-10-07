using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.JPService.Domains
{
    public static class IDGenerator
    {
        public static long GenerateId()
        {
            return IDProvider.IDWorker.NextId();
        }
    }
}

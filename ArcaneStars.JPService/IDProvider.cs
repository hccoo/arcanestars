using Snowflake.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.JPService
{
    public class IDProvider
    {
        public static IdWorker IDWorker { get; private set; }

        public static void InitIDWorker()
        {
            IDWorker = new IdWorker(1, 1);
        }
    }
}

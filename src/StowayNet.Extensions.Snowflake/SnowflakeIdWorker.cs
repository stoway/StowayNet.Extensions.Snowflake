using System;
using System.Collections.Generic;
using System.Text;

namespace StowayNet.Extensions.Snowflake
{
    [StowayDependency(StowayDependencyType.Singleton)]
    internal class SnowflakeIdWorker : ISnowflakeIdWorker
    {
        private readonly IdWorker _idWorker;
        public SnowflakeIdWorker()
        {
            _idWorker = new IdWorker(1, 1);
        }

        public Int64 NewId()
        {
            return _idWorker.NextId();
        }
    }
}

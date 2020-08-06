using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace StowayNet.Extensions.Snowflake
{
    [StowayDependency(StowayDependencyType.Singleton)]
    internal class SnowflakeIdWorker : ISnowflakeIdWorker
    {
        private readonly IdWorker _idWorker;
        private readonly IOptions<SnowflakeIdOptions> _options;

        public int ServerId
        {
            get
            {
                return _options.Value.ServerId ?? 1;
            }
        }
        public SnowflakeIdWorker(IOptions<SnowflakeIdOptions> options)
        {
            _idWorker = new IdWorker(options.Value.ServerId ?? 1, options.Value.DataCenterId ?? 1);
            _options = options;
        }

        public Int64 NewId()
        {
            return _idWorker.NextId();
        }
    }
}

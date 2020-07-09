using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace StowayNet.Extensions.Snowflake
{
    class SnowflakeRegister : IStowayServiceRegister
    {
        public void Register(IServiceCollection services, List<Type> types, IConfiguration configuration)
        {
            services.AddOptions<SnowflakeIdOptions>();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StowayNet.Extensions.Snowflake.Tests
{
    public class SnowflakeIdWorkerTest
    {
        [Fact]
        public void NewIdTest()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddStowayNet();
            var provider = services.BuildServiceProvider();

            var idWorker = provider.GetService<ISnowflakeIdWorker>();

            var id = new long[2];
            Parallel.For(0, 1, (i) =>
            {
                id[i] = idWorker.NewId();
            });
            Assert.NotEqual(id[0], id[1]);
        }
        [Fact]
        public void OptionTest()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddStowayNet();
            var random = new Random(1);
            var serverId = random.Next(1, 50);
            services.Configure<SnowflakeIdOptions>(options =>
            {
                options.ServerId = serverId;
            });

            var provider = services.BuildServiceProvider();

            var idWorker = provider.GetService<ISnowflakeIdWorker>();
            Assert.Equal(serverId, idWorker.ServerId);
        }
    }
}

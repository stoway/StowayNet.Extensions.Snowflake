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

            var id1 = idWorker.NewId();
            var id2 = idWorker.NewId();
            Assert.NotEqual(id1, id2);

        }
    }
}

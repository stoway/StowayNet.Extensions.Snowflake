<p align="center">
  <img height="80" src="https://s.gravatar.com/avatar/6275a0046443d6fb33421b52e503bc3e?s=140">
</p>

# StowayNet.Extensions.Snowflake
StowayNet.Extensions.Snowflake is an extension of twitter's snowflake algorithm, which can support injection dependencies.

## Get Started
### NuGet 

You can run the following command to install the `StowayNet.Extensions.Snowflake` in your project.

```
PM> Install-Package StowayNet.Extensions.Snowflake
```

### Configuration

First,You need to config `StowayNet.Extensions.Snowflake` in your `Startup.cs`:
```c#
......
using StowayNet;
......

public void ConfigureServices(IServiceCollection services)
{
    ......

    services.AddStowayNet();

    ......
}

```

### Sample


```c#
    using StowayNet.Extensions.Snowflake;
    
    class TestJob
    {
        private readonly ILogger<TestJob> _logger;
        private readonly ISnowflakeIdWorker _idWorker;

        public TestJob(ILogger<TestJob> logger, ISnowflakeIdWorker idWorker)
        {
            _logger = logger;
            _idWorker = idWorker;
        }

        public void Execute()
        {
            _logger.LogDebug($"newid:{_idWorker.NewId()}");

        }
    }
```

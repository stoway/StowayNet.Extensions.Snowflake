﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StowayNet.Extensions.Snowflake
{
    public interface ISnowflakeIdWorker
    {
        int ServerId { get; }
        Int64 NewId();
    }
}

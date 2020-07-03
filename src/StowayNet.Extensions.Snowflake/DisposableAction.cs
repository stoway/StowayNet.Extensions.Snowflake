using System;
using System.Collections.Generic;
using System.Text;

namespace StowayNet.Extensions.Snowflake
{
    internal class DisposableAction : IDisposable
    {
        readonly Action _action;

        public DisposableAction(Action action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Dispose()
        {
            _action();
        }
    }
}

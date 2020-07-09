using System;
using System.Collections.Generic;
using System.Text;

namespace StowayNet.Extensions.Snowflake
{
    internal class IdWorker
    {
        public const long Twepoch = 1288834974657L;
        const int WorkerIdBits = 5;
        const int DatacenterIdBits = 5;
        const int SequenceBits = 12;
        const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits);
        const long MaxDatacenterId = -1L ^ (-1L << DatacenterIdBits);
        private const long SequenceMask = -1L ^ (-1L << SequenceBits);
        private const int WorkerIdShift = SequenceBits;
        private const int DatacenterIdShift = SequenceBits + WorkerIdBits;
        public const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;

        private long _sequence = 0L;

        private long _lastTimestamp = -1L;

        public long WorkerId { get; protected set; }

        public long DatacenterId { get; protected set; }

        public long Sequence
        {
            get => _sequence;
            internal set => _sequence = value;
        }

        public IdWorker(long workerId, long datacenterId, long sequence = 0L)
        {
            if (workerId > MaxWorkerId || workerId < 0)
            {
                throw new ArgumentException($"worker Id must be greater than 0 and cannot be greater than MaxWorkerId: {MaxWorkerId}");
            }

            if (datacenterId > MaxDatacenterId || datacenterId < 0)
            {
                throw new ArgumentException($"region Id must be greater than 0 and cannot be greater than MaxWorkerId: {MaxDatacenterId}");
            }

            WorkerId = workerId;
            DatacenterId = datacenterId;
            _sequence = sequence;
        }

        readonly object _lock = new Object();

        public virtual long NextId()
        {
            lock (_lock)
            {
                var timestamp = TimeGen();
                if (timestamp < _lastTimestamp)
                {
                    throw new Exception($"Timestamp must be greater than the timestamp of the last ID generation. Refuse to generate id for {_lastTimestamp - timestamp} milliseconds");
                }

                if (_lastTimestamp == timestamp)
                {
                    _sequence = (_sequence + 1) & SequenceMask;
                    if (_sequence == 0)
                    {
                        timestamp = TilNextMillis(_lastTimestamp);
                    }
                }
                else
                {
                    _sequence = 0;
                }

                _lastTimestamp = timestamp;
                return ((timestamp - Twepoch) << TimestampLeftShift) | (DatacenterId << DatacenterIdShift) | (WorkerId << WorkerIdShift) | _sequence;
            }
        }

        protected virtual long TilNextMillis(long lastTimestamp)
        {
            var timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }
            return timestamp;
        }

        protected virtual long TimeGen()
        {
            return TimeExtensions.CurrentTimeMillis();
        }
    }

}

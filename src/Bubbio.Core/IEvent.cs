﻿using System;

namespace Bubbio.Core
{
    public interface IEvent
    {
        Guid EventId { get; set; }
        Guid BabyId { get; set; }
        DateTimeOffset Timestamp { get; set; }
        EventType EventType { get; set; }
    }
}
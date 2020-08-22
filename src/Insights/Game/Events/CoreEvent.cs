using System;
using UnityEngine;

namespace Insights.Game.Events
{
    public abstract class CoreEvent
    {
        public DateTime Timestamp { get; set; }

        public string Module { get; set; }

        public string Subroutine { get; set; }

        public EventType EventType { get; set; }
    }
}

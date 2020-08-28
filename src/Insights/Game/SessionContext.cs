using System;
using ICities;

namespace Insights.Game
{
    /// <summary>
    /// Provides access to game session properties for creating game events.
    /// </summary>
    /// <remarks>
    /// See reference for singleton design pattern:
    /// 
    /// https://csharpindepth.com/Articles/Singleton
    /// 
    /// This class implements pattern four.
    /// </remarks>
    public class SessionContext
    {
        static SessionContext()
        {
        }

        private SessionContext()
        {
        }

        public static SessionContext Instance { get; } = new SessionContext();

        public Guid SessionId { get; set; }

        public AppMode SessionType { get; set; }

        public SimulationManager.UpdateMode SessionSubtype { get; set; }
    }
}

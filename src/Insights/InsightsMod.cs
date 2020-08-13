using System;
using System.Reflection;
using ICities;
using UnityEngine;
using Insights.Diagnostics;

namespace Insights
{
    public class InsightsMod : IUserMod
    {
        public string Name => "Insights";

        public string Description => "Gain insights from gameplay";

        public InsightsMod()
        {
        }

        public void OnEnabled()
        {
            Tracer.Trace("OnEnabled");

            Tracer.Trace($"Data Path: {Application.dataPath}");
        }

        public void OnDisabled()
        {
            Tracer.Trace("OnDisabled");
        }
    }
}

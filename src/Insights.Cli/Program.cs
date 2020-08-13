using System;
using Insights;

namespace Insights.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var insights = new InsightsMod();
            insights.OnEnabled();
            insights.OnDisabled();
        }
    }
}

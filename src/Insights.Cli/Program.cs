using System;
using System.IO;
using System.Reflection;
using Insights;

namespace Insights.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var assemblyPath = Assembly
                .GetExecutingAssembly()
                .Location;

            var assemblyDirectory = Path.GetDirectoryName(assemblyPath);

            Console.WriteLine(assemblyPath);
        }
    }
}

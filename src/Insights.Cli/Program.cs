using System;
using System.IO;
using System.Reflection;

namespace Insights.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var assemblyName = Assembly
                .GetExecutingAssembly()
                .GetName();

            Console.WriteLine(assemblyName.Version);
        }
    }
}

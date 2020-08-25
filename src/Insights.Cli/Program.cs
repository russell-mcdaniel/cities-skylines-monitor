using System;
using System.Reflection;

namespace Insights.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check out-of-range enum value.
            PrintEnum(City.NewYork);
            PrintEnum((City)9);

            // Check DateTimeOffset ISO format string output.
            var timeText = DateTimeOffset.Now.ToString("O");

            Console.WriteLine(timeText);

            // Check assembly version value.
            var assemblyName = Assembly
                .GetExecutingAssembly()
                .GetName();

            Console.WriteLine(assemblyName.Version);
        }

        static void PrintEnum(City city)
        {
            Console.WriteLine($"The city is {city}.");
        }
    }

    enum City
    {
        LosAngeles,
        NewYork,
        Seattle
    }
}

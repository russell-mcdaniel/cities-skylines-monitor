using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Insights.Game.Events;
using Insights.Utilities;

namespace Insights.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check deserialization of XML.
            DeserializeXml();

            // Check JSON serialization via DataContract.
            PrintJson(new Person() { Name = "Bob Smith", Age = 47 });

            // Check out-of-range enum value.
            PrintEnum(City.NewYork);
            PrintEnum((City)9);

            // Check DateTimeOffset ISO format string output.
            PrintTimestamp();

            // Check assembly version value.
            PrintAssemblyVersion();
        }

        static void DeserializeXml()
        {
            // With namespaces.
            var xml = "<GameEvent xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xsi:type=\"SessionBeginEvent\" Timestamp=\"08/28/2020 00:38:10 -07:00\" EventType=\"SessionBegin\" SessionId=\"d75a6be8-fcbf-4521-94b0-efb6b439bb08\" Type=\"Game\" Subtype=\"NewGameFromMap\" InstanceId=\"888e7d96-3584-40e3-b1dd-3e053d50c284\" CityName=\"Thisisanothercitytown\" />";
            var gex = XmlHelper.FromXml<GameEvent>(xml);

            // Determine which case gets selected for an object deserialized to its base type.
            // Result: The derived class case is selected.
            switch (gex)
            {
                case SessionStartedEvent sbe:
                    Console.WriteLine($"SessionBeginEvent > Session ID: {sbe.SessionId} | Event Type: {sbe.EventType} | Instance ID: {sbe.InstanceId}");
                    break;

                case GameEvent ge:
                    Console.WriteLine($"SessionBeginEvent > Session ID: {ge.SessionId} | Event Type: {ge.EventType}");
                    break;

                default:
                    Console.WriteLine("Impossible?");
                    break;
            }

            // Determine which overload gets called for an object is deserialized to its base type.
            // Result: The base class overload is called.
            // Comment: Use dynamic dispatch to change this. See:
            // https://stackoverflow.com/questions/13095544/overloaded-method-why-is-base-class-given-precedence
            // Mono does support the dynamic keyword, but it is unknown if this will work in the
            // Cities runtime environment due to the CSharp library requirement.
            DeserializeXmlForEvent(gex);
            //DeserializeXmlForEvent((dynamic)gex);
        }

        static void DeserializeXmlForEvent(GameEvent ge)
        {
            Console.WriteLine("GameEvent");
        }

        static void DeserializeXmlForEvent(SessionStartedEvent sbe)
        {
            Console.WriteLine("SessionBeginEvent");
        }

        static void PrintAssemblyVersion()
        {
            var assemblyName = Assembly
                .GetExecutingAssembly()
                .GetName();

            Console.WriteLine(assemblyName.Version);
        }

        static void PrintEnum(City city)
        {
            Console.WriteLine($"The city is {city}.");
        }

        static void PrintJson<T>(T entity)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));

                serializer.WriteObject(stream, entity);

                stream.Position = 0;

                using (var reader = new StreamReader(stream))
                {
                    var jsonText = reader.ReadToEnd();

                    Console.WriteLine(jsonText);
                }
            }
        }

        static void PrintTimestamp()
        {
            var timeText = DateTimeOffset.Now.ToString("O");

            Console.WriteLine(timeText);
        }
    }

    [DataContract]
    class Person
    {
        public Person()
        {
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Age { get; set; }
    }

    enum City
    {
        LosAngeles,
        NewYork,
        Seattle
    }
}

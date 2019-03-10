using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace TestPlans
{
    class Program
    {
        static void Main(string[] args) {
            var path = @"C:\D\code\khalid\SelfService\SelfService\DB\plans.json";
            string json = File.ReadAllText(path);
            JObject obj = JObject.Parse(json);
            var panels = obj;
            Console.Write(panels);
            Console.ReadLine();
        }
    }
}

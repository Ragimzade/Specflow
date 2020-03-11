using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Bdd.JsonParse
{
    [TestClass]
    public class ParseTest
    {
        private const string AddressesJsonFilePath = "JsonParse/addresses.json";

        public static T ParseJsonFile<T>(string jsonText)
        {
            return JsonConvert.DeserializeObject<T>(jsonText);
        }

        [TestMethod]
        public void DoTest()
        {
            var location = ParseJsonFile<Location>(File.ReadAllText(AddressesJsonFilePath));
        }
    }
}
using System.Collections.Generic;

namespace Bdd.JsonParse
{
    public class Location
    {
        public string id { get; set; }
        public List<AddressData> addresses { get; set; }
        public List<string> images { get; set; }
        public string timezone { get; set; }

        public override string ToString()
        {
            return $"id:{id}, addresses{addresses}, images{images}, timezone{timezone}";
        }

        public class AddressData
        {
            public string line1 { get; set; }
            public City city { get; set; }
            public State state { get; set; }
            public Country country { get; set; }
            public GPS gps { get; set; }
            public string id { get; set; }
            public bool active { get; set; }

            public class State
            {
                public string name { get; set; }
            }

            public class City
            {
                public string name { get; set; }
            }

            public class Country
            {
                public string name { get; set; }
            }

            public class GPS
            {
                public List<double> coordinates { get; set; }
            }
        }
    }
}
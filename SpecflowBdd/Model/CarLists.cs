using System.Collections.Generic;

namespace Bdd.Model
{
    public class CarLists
    {
        public List<CarData> CarsOnPage { get; set; }
        public List<CarData> SortedByPrice { get; set; }
        public List<CarData> SortedByDate { get; set; }
        public List<CarData> SortedByYear { get; set; }
    }
}
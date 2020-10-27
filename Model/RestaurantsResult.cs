using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantsCatalog.Model
{
    public class RestaurantsResult : Result
    {
        public int Page { get; set; }
        public int TotalPage { get; set; }
        public IEnumerable<RestaurantDTO> Restaurants { get; set; }
    }
}

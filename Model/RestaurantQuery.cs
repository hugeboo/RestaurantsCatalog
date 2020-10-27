using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantsCatalog.Model
{
    public class RestaurantQuery
    {
        public int CityId { get; set; }
        public int Page { get; set; }
    }
}

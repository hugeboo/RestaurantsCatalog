using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantsCatalog.Model
{
    public class CitiesResult : Result
    {
        public IEnumerable<CityDTO> Cities { get; set; }
    }
}

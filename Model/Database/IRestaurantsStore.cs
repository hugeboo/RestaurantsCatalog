using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantsCatalog.Model.Database
{
    public interface IRestaurantsStore
    {
        Task<IEnumerable<CityDTO>> GetCities();
        Task<IEnumerable<RestaurantDTO>> GetRestaurants(int cityId, int limit, int offset);
        Task<int> GetRestaurantCount(int cityId);
        Task AddCity(CityDTO city);
        Task AddRestaurant(RestaurantDTO restaurant);
    }
}

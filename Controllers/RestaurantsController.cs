using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestaurantsCatalog.Model;
using RestaurantsCatalog.Model.Database;

namespace RestaurantsCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantsStore _store;
        private readonly IConfiguration _config;

        public RestaurantsController(IRestaurantsStore store, IConfiguration config)
        {
            _store = store;
            _config = config;
        }

        // GET: api/<RestaurantsController>?cityId=1&page=1
        [HttpGet()]
        public async Task<RestaurantsResult> Get([FromQuery] RestaurantQuery query)
        {
            try
            {
                if (query.CityId < 1)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return new RestaurantsResult { Status = "Error", Message = "CityId is unspecified" };
                }

                int page = Math.Max(1, query.Page);

                var totalCount = await _store.GetRestaurantCount(query.CityId);
                var pageSize = int.Parse(_config["RestaurantsCatalog:RestaurantsPageSize"]);
                var totalPage = (int)Math.Ceiling(totalCount / (double)pageSize);

                var restaurants = page <= totalPage ?
                    await _store.GetRestaurants(query.CityId, pageSize, pageSize * (page - 1)) :
                    null;

                return new RestaurantsResult
                {
                    Status = "OK",
                    Page = page,
                    TotalPage = totalPage,
                    Restaurants = restaurants
                };
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new RestaurantsResult { Status = "Error", Message = ex.Message };
            }
        }

        // POST api/<RestaurantsController>
        [HttpPost]
        public async Task<Result> Post([FromBody] RestaurantDTO restaurant)
        {
            try
            {
                if (restaurant == null || string.IsNullOrWhiteSpace(restaurant.Name) || restaurant.CityId < 1)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Result.Error("Restaurant Name or CityId is unspecified");
                }
                await _store.AddRestaurant(restaurant);
                return Result.OK();
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var message = ex.Message.Contains("UNIQUE constraint failed") ?
                    $"A restaurant named '{restaurant.Name}' with CityId '{restaurant.CityId}' already exists" : ex.Message;
                return Result.Error(message);
            }
        }
    }
}

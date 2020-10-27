using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestaurantsCatalog.Model;
using RestaurantsCatalog.Model.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantsCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IRestaurantsStore _store;
        private readonly IConfiguration _config;

        public CitiesController(IRestaurantsStore store, IConfiguration config)
        {
            _store = store;
            _config = config;
        }

        // GET: api/<Cities>
        [HttpGet]
        public async Task<CitiesResult> Get()
        {
            try
            {
                return new CitiesResult { Status = "OK", Cities = await _store.GetCities() };
            }
            catch(Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new CitiesResult { Status = "Error", Message = ex.Message };
            }
        }

        // POST api/<Cities>
        [HttpPost]
        public async Task<Result> Post([FromBody] CityDTO city)
        {
            try
            {
                if (city == null || string.IsNullOrWhiteSpace(city.Name))
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Result.Error("City Name is unspecified");
                }
                await _store.AddCity(city);
                return Result.OK();
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var message = ex.Message.Contains("UNIQUE constraint failed") ?
                    $"A city named '{city.Name}' already exists" : ex.Message;
                return Result.Error(message);
            }
        }
    }
}

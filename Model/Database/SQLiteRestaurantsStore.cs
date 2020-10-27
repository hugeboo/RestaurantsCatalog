using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantsCatalog.Model.Database
{
    sealed class SQLiteRestaurantsStore : IRestaurantsStore
    {
        private readonly string _connectionString;

        private const string _sqlSelectCities = "SELECT Id,Name FROM Cities";
        private const string _sqlSelectRestaurants = "SELECT Id,Name,CityId FROM Restaurants WHERE CityId = @cityId LIMIT @limit OFFSET @offset";
        private const string _sqlSelectRestaurantCount = "SELECT COUNT(*) FROM Restaurants WHERE CityId = @cityId";
        private const string _sqlInsertCity = "INSERT INTO Cities (Name) VALUES (@name)";
        private const string _sqlInsertRestaurant = "INSERT INTO Restaurants (Name,CityId) VALUES (@name,@cityId)";

        public SQLiteRestaurantsStore(IConfiguration con)
        {
            _connectionString = con["RestaurantsCatalog:ConnectionString"];
        }

        public async Task AddCity(CityDTO city)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var com = connection.CreateCommand();
                com.CommandText = _sqlInsertCity;
                com.Parameters.AddRange(new[]
                {
                    new SQLiteParameter("@name", city.Name),
                });
                await com.ExecuteNonQueryAsync();
            }
        }

        public async Task AddRestaurant(RestaurantDTO restaurant)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var com = connection.CreateCommand();
                com.CommandText = _sqlInsertRestaurant;
                com.Parameters.AddRange(new[]
                {
                    new SQLiteParameter("@name", restaurant.Name),
                    new SQLiteParameter("@cityId", restaurant.CityId),
                });
                await com.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<CityDTO>> GetCities()
        {
            var lstCity = new List<CityDTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var com = connection.CreateCommand();
                com.CommandText = _sqlSelectCities;
                using (var r = com.ExecuteReader())
                {
                    while (await r.ReadAsync())
                    {
                        var city = new CityDTO
                        {
                            Id = r.GetInt32(0),
                            Name = r.GetString(1),
                        };
                        lstCity.Add(city);
                    }
                }
            }
            return lstCity;
        }

        public async Task<IEnumerable<RestaurantDTO>> GetRestaurants(int cityId, int limit, int offset)
        {
            var lstRestaurant = new List<RestaurantDTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var com = connection.CreateCommand();
                com.CommandText = _sqlSelectRestaurants;
                com.Parameters.AddRange(new[]
                {
                    new SQLiteParameter("@cityId", cityId),
                    new SQLiteParameter("@limit", limit),
                    new SQLiteParameter("@offset", offset),
                });
                using (var r = await com.ExecuteReaderAsync())
                {
                    while (r.Read())
                    {
                        var restaurant = new RestaurantDTO
                        {
                            Id = r.GetInt32(0),
                            Name = r.GetString(1),
                            CityId = r.GetInt32(2)
                        };
                        lstRestaurant.Add(restaurant);
                    }
                }
            }
            return lstRestaurant;
        }

        public async Task<int> GetRestaurantCount(int cityId)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var com = connection.CreateCommand();
                com.CommandText = _sqlSelectRestaurantCount;
                com.Parameters.AddRange(new[]
                {
                    new SQLiteParameter("@cityId", cityId),
                });
                return (int)(Int64)(await com.ExecuteScalarAsync());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantsCatalog.Model
{
    public class Result
    {
        public string Status { get; set; }
        public string Message { get; set; }

        public static Result OK()
        {
            return new Result { Status = "OK" };
        }

        public static Result Error(string message)
        {
            return new Result { Status = "Error", Message = message };
        }
    }
}

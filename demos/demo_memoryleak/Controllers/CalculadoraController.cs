// Generate a class that will be used to calculate the sum of two numbers
// The class will be a controller
// the class will implement a dictionary to store the results of the sum of two numbers
// the method to store results will contain a memory leak

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace demo_memoryleak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculadoraController : ControllerBase
    {
        private static Dictionary<object, int> _results = new Dictionary<object, int>();

        [HttpGet("sum")]
        public int Sum(int a, int b)
        {
            string key = $"{a}+{b}";

            if (_results.ContainsKey(key))
            {
                return _results[key];
            }

            int result = a + b;
            _results.Add((object)key, result);
            return result;
        }
    }
}
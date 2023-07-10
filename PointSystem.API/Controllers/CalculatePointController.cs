using Microsoft.AspNetCore.Mvc;
using PointSystem.API.Model;
using PointSystem.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PointSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatePointController : ControllerBase
    {
        private readonly ICalcuatePointSystemService _calcuatePointSystem;

        public CalculatePointController(ICalcuatePointSystemService calcuatePointSystem)
        {
            _calcuatePointSystem = calcuatePointSystem;
        }
        // GET: api/<CalculatePointController>
        [HttpGet]
        public async Task<CalculateSystemResponse> CalculatePointSystem()
        {
           return await _calcuatePointSystem.CalculatePointSystemAsync();
        }

        // GET api/<CalculatePointController>/5
       
    }
}

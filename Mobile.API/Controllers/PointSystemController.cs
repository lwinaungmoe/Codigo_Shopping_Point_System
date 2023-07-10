using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobile.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mobile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController,Authorize]
    public class PointSystemController : ControllerBase
    {
        private readonly IPointSystemService _pointSystemService;

        public PointSystemController(IPointSystemService pointSystemService)
        {
            _pointSystemService = pointSystemService;
        }

        // GET api/<PointSystemController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var pointData = _pointSystemService.GetPointData(id);
            if (pointData == null)
            {
                await Task.CompletedTask;
                return BadRequest();
            }
            else
            {
                return Ok(pointData);
            }
        }
    }
}
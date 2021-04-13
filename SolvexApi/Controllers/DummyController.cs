using DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SolvexApi.Controllers
{
    [ApiController]
    [Route("api/test/db")]
    public class DummyController : ControllerBase
    {
        private readonly CityInfoContext _ctx;

        public DummyController(CityInfoContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        [HttpGet]
        public IActionResult TestDb()
        {
            return Ok();
        }
    }
}

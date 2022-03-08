using Microsoft.AspNetCore.Mvc;

namespace Bnaya.Samples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IKeyed<IFunctionality> _selector;
        private readonly ILogger<TestController> _logger;

        public TestController(
            IKeyed<IFunctionality> selector,
            ILogger<TestController> logger)
        {
            _selector = selector;
            _logger = logger;
        }

        [HttpGet("a")]
        public string GetA()
        {
            return _selector["A"].Id;
        }

        [HttpGet("b")]
        public string GetB()
        {
            return _selector["B"].Id;
        }
    }
}
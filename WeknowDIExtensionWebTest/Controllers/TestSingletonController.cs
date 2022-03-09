using Microsoft.AspNetCore.Mvc;

namespace Bnaya.Samples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestSingletonController : ControllerBase
    {
        private readonly IKeyed<IFunctionality> _selector;
        private readonly Component<IFunctionality> _component;
        private readonly ILogger<TestSingletonController> _logger;

        public TestSingletonController(
            IKeyed<IFunctionality> selector,
            Component<IFunctionality> component,
            ILogger<TestSingletonController> logger)
        {
            _selector = selector;
            _component = component;
            _logger = logger;
        }

        [HttpGet("a")]
        public string GetA()
        {
            return $"{_selector["A"].Id} / {_component.GetText("A")}";
        }

        [HttpGet("b")]
        public string GetB()
        {
            return $"{_selector["B"].Id} / {_component.GetText("B")}";
        }
    }
}
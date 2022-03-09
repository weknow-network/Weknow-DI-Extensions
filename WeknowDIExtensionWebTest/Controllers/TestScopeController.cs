using Microsoft.AspNetCore.Mvc;

namespace Bnaya.Samples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestScopeController : ControllerBase
    {
        private readonly IKeyed<IFunctionalityScoped> _selector;
        private readonly Component<IFunctionalityScoped> _component;
        private readonly ILogger<TestScopeController> _logger;

        public TestScopeController(
            IKeyed<IFunctionalityScoped> selector,
            Component<IFunctionalityScoped> component,
            ILogger<TestScopeController> logger)
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
using Microsoft.AspNetCore.Mvc;
using SocialSearchTool.Services;

namespace SocialSearchTool.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class DummyApiController : Controller
    {
        private readonly DummyDataService _dummyDataService;

        public DummyApiController(DummyDataService dummyDataService)
        {
            _dummyDataService = dummyDataService;
        }

        public IActionResult Index(string query)
        {
            var searchResults = _dummyDataService.Search(query);
            return View("Index", searchResults);
        }
    }
}

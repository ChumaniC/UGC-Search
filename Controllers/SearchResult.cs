using Microsoft.AspNetCore.Mvc;
using SocialSearchTool.Models;
using SocialSearchTool.Services;

namespace SocialSearchTool.Controllers
{
    public class SearchResultController : Controller
    {
        private readonly DummyDataService _dummyDataService;

        public SearchResultController(DummyDataService dummyDataService)
        {
            _dummyDataService = dummyDataService;
        }

        public IActionResult Index(string query)
        {
            // Check if the query is empty
            if (string.IsNullOrWhiteSpace(query))
            {
                ViewBag.NoResultsMessage = "Please enter a search query.";
                return PartialView("_SearchResultsPartial", new List<SearchResult>());
            }

            // Call the Dummy data service to perform the search
            var searchResults = _dummyDataService.Search(query);

            if (searchResults.Count == 0)
            {
                // If there are no results, return a message
                ViewBag.NoResultsMessage = $"No results found for '{query}'.";
            }

            return View(searchResults);
        }

        public IActionResult Search(string query)
        {
            // Call the Dummy data service to perform the search
            var searchResults = _dummyDataService.Search(query);

            if (searchResults.Count == 0)
            {
                // If there are no results, return a message
                ViewBag.NoResultsMessage = $"No results found for '{query}'.";
            }

            return PartialView("_SearchResultsPartial", searchResults);
        }

        [HttpPost]
        public IActionResult SaveBookmark(int postId)
        {
            // Retrieve the search result details for the specified postId
            var searchResult = _dummyDataService.GetSearchResultDetails(postId);

            // Retrieve the searchId for the current search
            int searchId = _dummyDataService.GetLastSearchId();

            if (searchResult == null)
            {
                return NotFound();
            }

            // Save the bookmark
            _dummyDataService.SaveBookmark(searchResult, searchId);

            return Ok("Saved successfully!");
        }
    }
}
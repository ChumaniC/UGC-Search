using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SocialSearchTool.Data;
using SocialSearchTool.Models;

namespace SocialSearchTool.Services
{
    public class DummyDataService
    {
        private readonly AppDbContext _dbContext;

        public DummyDataService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<SearchResult> Search(string query)
        {
            var results = _dbContext.SocialMediaPosts
                .Where(post => post.Caption.Contains(query) || post.Username.Contains(query))
                .Select(post => new SearchResult
                {
                    ImageUrl = post.ImageUrl,
                    Caption = post.Caption,
                    Username = post.Username,
                    PostId = post.PostId,
                    SearchDate = DateTime.Now,
                })
                .ToList();

            // Log the search results
            LogSearchResults(query, results);

            return results;
        }

        // Log search results method
        private void LogSearchResults(string query, List<SearchResult> results)
        {
            // Create a log entry for each result
            foreach (var result in results)
            {
                var searchLog = new SearchResult
                {
                    Caption = query,
                    PostId = result.PostId, 
                    ImageUrl = result.ImageUrl, 
                    Username = result.Username,
                    SearchDate = DateTime.Now,
                };

                _dbContext.SearchResults.Add(searchLog);
            }

            // Save changes to the database
            _dbContext.SaveChanges();
        }

        public void SaveBookmark(SearchResult searchResult, int searchId)
        {
            var bookmark = new BookmarkPost
            {
                SearchId = searchId,
                PostId = searchResult.PostId,
                ImageUrl = searchResult.ImageUrl,
                Caption = searchResult.Caption,
                Username = searchResult.Username,
                SearchDate = DateTime.Now,
            };

            _dbContext.Bookmarks.Add(bookmark);
            _dbContext.SaveChanges();
        }

        // Retrieve search result details
        public SearchResult? GetSearchResultDetails(int postId)
        {
            var post = _dbContext.SocialMediaPosts
                .Where(post => post.PostId == postId)
                .FirstOrDefault();

            // Check if the post is null
            if (post == null)
            {
                return null; // Return null if no matching post is found
            }

            return new SearchResult
            {
                ImageUrl = post.ImageUrl,
                Caption = post.Caption,
                Username = post.Username,
                PostId = post.PostId,
                SearchDate = DateTime.Now,
            };
        }

        public int GetLastSearchId()
        {
            // Order the SearchResults by SearchDate in descending order and take the first one
            var lastSearchResult = _dbContext.SearchResults
                .OrderByDescending(sr => sr.SearchDate)
                .FirstOrDefault();

            // Check if a SearchResult was found
            if (lastSearchResult != null)
            {
                return lastSearchResult.SearchId;
            }

            // Return a default value
            return 0;
        }
    }
}

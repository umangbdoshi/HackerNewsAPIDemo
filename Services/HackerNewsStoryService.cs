using HackerNewsAPIDemo.Client;
using HackerNewsAPIDemo.Models;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNewsAPIDemo.Services
{
    public class HackerNewsStoryService : IHackerNewsStoryService
    {
        private readonly IHackerNewsClient _hackerNewsClient;
        private readonly IMemoryCache _cache;

        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(15);

        public HackerNewsStoryService(IHackerNewsClient hackerNewsClient, IMemoryCache cache)
        {
            _hackerNewsClient = hackerNewsClient;
            _cache = cache;
        }

        public async Task<IEnumerable<StoryDto>> GetTopStoriesAsync(int count)
        {
            var bestStoryIds = await _hackerNewsClient.GetBestStoryIdsAsync();
            var topStoryIds = bestStoryIds.Take(count);

            var tasks = topStoryIds.Select(x => GetStoryWithCachingAsync(x));
            var stories = await Task.WhenAll(tasks);

            return stories.OrderByDescending(s => s.Score);
        }

        private async Task<StoryDto> GetStoryWithCachingAsync(int storyId)
        {
            string cacheKey = $"Story_{storyId}";

            if (_cache.TryGetValue(cacheKey, out StoryDto cachedStory))
            {
                return cachedStory;
            }

            var story = await _hackerNewsClient.GetStoryByIdAsync(storyId);

            if (story != null)
            {
                _cache.Set(cacheKey, story, CacheDuration);
            }

            return story;
        }
    }
}

using HackerNewsAPIDemo.Models;
using System.Text.Json;

namespace HackerNewsAPIDemo.Client
{
    public class HackerNewsClient : IHackerNewsClient
    {
        private readonly HttpClient _httpClient;
        private const string TopStoriesUrl = "https://hacker-news.firebaseio.com/v0/beststories.json";
        private const string StoryUrlTemplate = "https://hacker-news.firebaseio.com/v0/item/{0}.json";

        public HackerNewsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<int>> GetBestStoryIdsAsync()
        {
            var response = await _httpClient.GetStringAsync(TopStoriesUrl);
            return JsonSerializer.Deserialize<IEnumerable<int>>(response) ?? new List<int>();
        }

        public async Task<StoryDto> GetStoryByIdAsync(int id)
        {
            var response = await _httpClient.GetStringAsync(string.Format(StoryUrlTemplate, id));
            var story = JsonSerializer.Deserialize<HackerNewsStory>(response);

            return new StoryDto
            {
                Title = story?.Title,
                Uri = story?.Url,
                PostedBy = story?.By,
                Time = DateTimeOffset.FromUnixTimeSeconds(story?.Time ?? 0).UtcDateTime,
                Score = story?.Score ?? 0,
                CommentCount = story?.Descendants ?? 0
            };
        }

        private class HackerNewsStory
        {
            public long Id { get; set; }
            public string Title { get; set; }
            public string Url { get; set; }
            public string By { get; set; }
            public long[] Kids { get; set; }
            public long Time { get; set; }
            public int Score { get; set; }
            public int Descendants { get; set; }
        }
    }
}

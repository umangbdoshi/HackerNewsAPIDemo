using HackerNewsAPIDemo.Models;

namespace HackerNewsAPIDemo.Services
{
    public interface IHackerNewsStoryService
    {
        Task<IEnumerable<StoryDto>> GetTopStoriesAsync(int count);
    }
}

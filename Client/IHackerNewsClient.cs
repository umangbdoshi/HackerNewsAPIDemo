using HackerNewsAPIDemo.Models;

namespace HackerNewsAPIDemo.Client
{
    public interface IHackerNewsClient
    {
        Task<IEnumerable<int>> GetBestStoryIdsAsync();
        Task<StoryDto> GetStoryByIdAsync(int id);
    }
}

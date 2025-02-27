using HackerNewsAPIDemo.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HackerNewsController : ControllerBase
{
    private readonly IHackerNewsStoryService _hackerNewsService;

    public HackerNewsController(IHackerNewsStoryService hackerNewsService)
    {
        _hackerNewsService = hackerNewsService;
    }

    [HttpGet("best-stories")]
    public async Task<IActionResult> GetBestStories([FromQuery] int count = 10)
    {
        if (count < 1 || count > 100)
        {
            return BadRequest("Count must be between 1 and 100.");
        }

        var stories = await _hackerNewsService.GetTopStoriesAsync(count);
        return Ok(stories);
    }
}

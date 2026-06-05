using System.Text;
using System.Text.Json;
using lofi_backend.Data_Models;
using lofi_backend.Data_Models.Enums;
using lofi_backend.Database;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace lofi_backend.Repository
{
    public interface IYoutubeRepository
    {
        Task<List<Music>> SearchYoutubeAsync(string search);
        
    }


    public class YoutubeRepository : IYoutubeRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public YoutubeRepository(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

    

    public async Task<List<Music>> SearchYoutubeAsync(string search)
        {
            var apiKey = _configuration["YouTube:ApiKey"];

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("YouTube API key is missing.");
            }

            var client = _httpClientFactory.CreateClient();

            var url =
                "https://youtube.googleapis.com/youtube/v3/search" +
                "?part=snippet" +
                $"&q={Uri.EscapeDataString(search + " lofi music")}" +
                "&maxResults=20" +
                "&order=viewCount" +
                $"&key={apiKey}";

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(json);

            var musicList = new List<Music>();

            foreach (var item in document.RootElement.GetProperty("items").EnumerateArray())
            {
                var snippet = item.GetProperty("snippet");
                var title = System.Net.WebUtility.HtmlDecode(
                    System.Text.RegularExpressions.Regex.Unescape(
                        snippet.GetProperty("title").GetString() ?? "Unknown Title"
                    ));
                var channel = snippet.GetProperty("channelTitle").GetString() ?? "Unknown Channel";
                var videoId = item.GetProperty("id").GetProperty("videoId").GetString() ?? string.Empty;
                var thumbnail = snippet.GetProperty("thumbnails").GetProperty("high").GetProperty("url").ToString();

                Console.WriteLine(thumbnail);
                
                musicList.Add(new Music
                {
                    Title = title ?? "",
                    Artist = channel ?? "",
                    Channel = channel ?? "",
                    Mood = Mood.Chill,
                    Genre = Genre.LoFi,
                    URL = $"https://www.youtube.com/watch?v={videoId}",
                    Thumbnail = thumbnail
                });
            }
            return musicList;
        }
    }
}

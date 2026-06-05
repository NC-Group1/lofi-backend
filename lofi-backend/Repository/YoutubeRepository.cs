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
            //    var apiKey = _configuration["YouTube:ApiKey"];

            //    if (string.IsNullOrEmpty(apiKey))
            //    {
            //        throw new Exception("YouTube API key is missing.");
            //    }

            //    var client = _httpClientFactory.CreateClient();

            //    var url =
            //        "https://youtube.googleapis.com/youtube/v3/search" +
            //        "?part=snippet" +
            //        $"&q={Uri.EscapeDataString(search + " lofi music")}" +
            //        "&maxResults=20" +
            //        "&order=viewCount" +
            //        $"&key={apiKey}";

            //    var response = await client.GetAsync(url);

            //    response.EnsureSuccessStatusCode();

            //    var json = await response.Content.ReadAsStringAsync();

            //    using var document = JsonDocument.Parse(json);

            //    var musicList = new List<Music>();

            //    foreach (var item in document.RootElement.GetProperty("items").EnumerateArray())
            //    {
            //        var snippet = item.GetProperty("snippet");
            //        var title = snippet.GetProperty("title").GetString() ?? "Unknown Title";
            //        var channel = snippet.GetProperty("channelTitle").GetString() ?? "Unknown Channel";
            //        var videoId = item.GetProperty("id").GetProperty("videoId").GetString() ?? string.Empty;


            //        musicList.Add(new Music
            //        {
            //            Title = title ?? "",
            //            Artist = channel ?? "",
            //            Channel = channel ?? "",
            //            Mood = Mood.Chill,
            //            Genre = Genre.LoFi,
            //            URL = $"https://www.youtube.com/watch?v={videoId}"
            //        });
            //    }
            //    return musicList;

            var mockVideos = new List<Music>
            {
                    new Music { Id = 1, Title = "Best of lofi hip hop 2021", Artist = "Various Artists", Channel = "Lofi Girl", Mood = Mood.Study, Genre = Genre.LoFi, URL = "https://www.youtube.com/watch?v=n61ULEU7CO0" },
                    new Music { Id = 2, Title = "1 A.M Study Session", Artist = "Various Artists", Channel = "Lofi Girl", Mood = Mood.Study, Genre = Genre.LoFi, URL = "https://www.youtube.com/watch?v=lTRiuFIWV54" },
                    new Music { Id = 3, Title = "Winter lofi mix", Artist = "Various Artists", Channel = "Lofi Girl", Mood = Mood.Relax, Genre = Genre.LoFi, URL = "https://www.youtube.com/watch?v=S-4hwfyK-XQ" },
                    new Music { Id = 4, Title = "Lofi beats to do absolutely nothing to", Artist = "Various Artists", Channel = "Lofi Girl", Mood = Mood.Chill, Genre = Genre.LoFi, URL = "https://www.youtube.com/watch?v=M8J9zHyyUYc" },
                    new Music { Id = 5, Title = "Study with me Pomodoro lofi focus music", Artist = "Various Artists", Channel = "Lofi Girl", Mood = Mood.Focus, Genre = Genre.LoFi, URL = "https://www.youtube.com/watch?v=53gNFOqDFcE" },

                    new Music { Id = 6, Title = "Chillhop Essentials Spring 2018", Artist = "Various Artists", Channel = "Chillhop Music", Mood = Mood.Happy, Genre = Genre.LoFi, URL = "https://www.youtube.com/watch?v=HRNcojzOJVk" },
                    new Music { Id = 7, Title = "Chillhop Essentials Summer 2018", Artist = "Various Artists", Channel = "Chillhop Music", Mood = Mood.Chill, Genre = Genre.LoFi, URL = "https://www.youtube.com/watch?v=TTXFKD7fMlE" },
                    new Music { Id = 8, Title = "Chillhop Essentials Fall 2018", Artist = "Various Artists", Channel = "Chillhop Music", Mood = Mood.Calm, Genre = Genre.LoFi, URL = "https://www.youtube.com/watch?v=M8HDvTuctOU" },
                    new Music { Id = 9, Title = "Chillhop Essentials Winter 2018", Artist = "Various Artists", Channel = "Chillhop Music", Mood = Mood.Relax, Genre = Genre.LoFi, URL = "https://www.youtube.com/watch?v=Rhomn5Um9dg" },
                    new Music { Id = 10, Title = "Chillhop Essentials Fall 2016", Artist = "Various Artists", Channel = "Chillhop Music", Mood = Mood.Chill, Genre = Genre.LoFi, URL = "https://www.youtube.com/watch?v=5jZyM6-k50o" },

                    new Music { Id = 11, Title = "Chillhop Essentials Winter 2020", Artist = "Various Artists", Channel = "Chillhop Music", Mood = Mood.Calm, Genre = Genre.LoFi, URL = "https://www.youtube.com/watch?v=i_cV8B2pjqk" },
                    new Music { Id = 12, Title = "Chillhop Daydreams", Artist = "Various Artists", Channel = "Chillhop Music", Mood = Mood.Relax, Genre = Genre.LoFi, URL = "https://www.youtube.com/watch?v=kEPakJDkTOk" },
                    new Music { Id = 13, Title = "5 Hours Chill Lofi Hip-Hop Mix 2018", Artist = "Various Artists", Channel = "Lofi Mix", Mood = Mood.Study, Genre = Genre.LoFi, URL = "https://www.youtube.com/watch?v=kNZjFeqw_28" },
                    new Music { Id = 14, Title = "Chill music for work", Artist = "Various Artists", Channel = "Lofi Work", Mood = Mood.Focus, Genre = Genre.LoFi, URL = "https://www.youtube.com/watch?v=JCKBaJDRMw4" },
                    new Music { Id = 15, Title = "Cozy spring lofi chill music", Artist = "Various Artists", Channel = "Lofi Girl", Mood = Mood.Relax, Genre = Genre.LoFi, URL = "https://www.youtube.com/watch?v=fsPRybb-xXg" },

                    new Music { Id = 16, Title = "Best of lofi 2018", Artist = "Various Artists", Channel = "Lofi Girl", Mood = Mood.Study, Genre = Genre.LoFi, URL = "https://www.youtube.com/watch?v=-FlxM_0S2lA" },
                    new Music { Id = 17, Title = "Tomorrow", Artist = "Various Artists", Channel = "Lofi Girl", Mood = Mood.Calm, Genre = Genre.LoFi, URL = "https://www.youtube.com/watch?v=J8nTKJ-dP00" },
                    new Music { Id = 18, Title = "Chill Study Music Playlist", Artist = "Various Artists", Channel = "Lofi Beats", Mood = Mood.Focus, Genre = Genre.LoFi, URL = "https://www.youtube.com/watch?v=2tr6iYIvL3k" },
                    new Music
                    {
                        Id = 19,
                        Title = "Relaxing LoFi Study Mix",
                        Artist = "LoFi Beats",
                        Channel = "LoFi Beats",
                        Mood = Mood.Study,
                        Genre = Genre.LoFi,
                        URL = "https://www.youtube.com/watch?v=JdqL89ZZwFw"
                    },
                    new Music
                    {
                        Id = 20,
                        Title = "Late Night LoFi Coding",
                        Artist = "Chill Programmer",
                        Channel = "Coding Beats",
                        Mood = Mood.Focus,
                        Genre = Genre.LoFi,
                        URL = "https://www.youtube.com/watch?v=dQi-ofZmrPw"
                    }
            };

                     return mockVideos.Where(x =>
                        string.IsNullOrWhiteSpace(search) ||
                        x.Title.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

        }
    }
}

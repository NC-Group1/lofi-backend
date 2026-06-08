using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using lofi_backend.Data_Models;
using lofi_backend.Data_Models.Enums;
using lofi_backend.Database;
using lofi_backend.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

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
            Console.WriteLine("Repository Layer");
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

            var mockVideos = MusicResults.Results;

            if (search.IsNullOrEmpty())
            {
                Console.WriteLine("Returning full video list");
               mockVideos.Shuffle();
                return mockVideos;
            }
            else
            {
                Console.WriteLine("Returning filtered search");
                return mockVideos.Where(x => x.Title.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

        }
    }
    public static class ListExtensions
    {
        private static Random _rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _rng.Next(n + 1); // Select a random index
                T value = list[k];        // Swap elements
                list[k] = list[n];
                list[n] = value;
            }
        }
    }


    public static class MusicResults
    {
        public static List<Music> Results = new List<Music>
        {
            new Music
            {
                Id = 1,
                Title = "1 A.M Study Session 📚 [lofi hip hop]",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Study,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=lTRiuFIWV54",
                Thumbnail = "https://i.ytimg.com/vi/lTRiuFIWV54/hqdefault.jpg"
            },
            new Music
            {
                Id = 2,
                Title = "C H I L L V I B E S | Chill & aesthetic music playlist",
                Artist = "EYM",
                Channel = "EYM",
                Mood = Mood.Chill,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=oJnF5VxTO5g",
                Thumbnail = "https://i.ytimg.com/vi/oJnF5VxTO5g/hqdefault.jpg"
            },
            new Music
            {
                Id = 3,
                Title = "Best of lofi 2018 🎶 beats to chill/study to",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Study,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=-FlxM_0S2lA",
                Thumbnail = "https://i.ytimg.com/vi/-FlxM_0S2lA/hqdefault.jpg"
            },
            new Music
            {
                Id = 4,
                Title = "old songs but it's lofi remix",
                Artist = "Various Artists",
                Channel = "Lo-fi Music",
                Mood = Mood.Chill,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=BrnDlRmW5hs",
                Thumbnail = "https://i.ytimg.com/vi/BrnDlRmW5hs/hqdefault.jpg"
            },
            new Music
            {
                Id = 5,
                Title = "Best of lofi hip hop 2021 ✨ [beats to relax/study to]",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Relax,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=n61ULEU7CO0",
                Thumbnail = "https://i.ytimg.com/vi/n61ULEU7CO0/hqdefault.jpg"
            },
            new Music
            {
                Id = 6,
                Title = "Chill Study Beats 2 • Instrumental & Jazz Hip Hop Music [2016]",
                Artist = "Chillhop Music",
                Channel = "Chillhop Music",
                Mood = Mood.Study,
                Genre = Genre.Jazz,
                URL = "https://www.youtube.com/watch?v=gwDoRPcPxtc",
                Thumbnail = "https://i.ytimg.com/vi/gwDoRPcPxtc/hqdefault.jpg"
            },
            new Music
            {
                Id = 7,
                Title = "Chill Study Beats 4 • jazz & lofi hiphop Mix [2017]",
                Artist = "Chillhop Music",
                Channel = "Chillhop Music",
                Mood = Mood.Study,
                Genre = Genre.Jazz,
                URL = "https://www.youtube.com/watch?v=8iU8LPEa4o0",
                Thumbnail = "https://i.ytimg.com/vi/8iU8LPEa4o0/hqdefault.jpg"
            },
            new Music
            {
                Id = 8,
                Title = "Chill Lofi Mix [chill lo-fi hip hop beats]",
                Artist = "Settle",
                Channel = "Settle",
                Mood = Mood.Chill,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=CLeZyIID9Bo",
                Thumbnail = "https://i.ytimg.com/vi/CLeZyIID9Bo/hqdefault.jpg"
            },
            new Music
            {
                Id = 9,
                Title = "3:30 a.m. ~ lofi hip hop / jazzhop / chillhop mix [study/sleep/homework music]",
                Artist = "Feardog",
                Channel = "Feardog",
                Mood = Mood.Sleep,
                Genre = Genre.Jazz,
                URL = "https://www.youtube.com/watch?v=dR17U5-VKtw",
                Thumbnail = "https://i.ytimg.com/vi/dR17U5-VKtw/hqdefault.jpg"
            },
            new Music
            {
                Id = 10,
                Title = "90's Chill Lofi ☕️ Study Music Lofi Rain Chillhop Beats ☔️ Lofi Rain Playlist",
                Artist = "The Japanese Town",
                Channel = "The Japanese Town",
                Mood = Mood.Study,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=sF80I-TQiW0",
                Thumbnail = "https://i.ytimg.com/vi/sF80I-TQiW0/hqdefault.jpg"
            },
            new Music
            {
                Id = 11,
                Title = "2 A.M Study Session 📚 [lofi hip hop]",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Study,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=wAPCSnAhhC8",
                Thumbnail = "https://i.ytimg.com/vi/wAPCSnAhhC8/hqdefault.jpg"
            },
            new Music
            {
                Id = 12,
                Title = "ＳＭＯＫＥ ＆ ＣＨＩＬＬ",
                Artist = "the bootleg boy 2",
                Channel = "the bootleg boy 2",
                Mood = Mood.Chill,
                Genre = Genre.Jazz,
                URL = "https://www.youtube.com/watch?v=N8VHBJooRwg",
                Thumbnail = "https://i.ytimg.com/vi/N8VHBJooRwg/hqdefault.jpg"
            },
            new Music
            {
                Id = 13,
                Title = "Best of lofi hip hop 2022 🎆 - beats to relax/study to",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Relax,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=i43tkaTXtwI",
                Thumbnail = "https://i.ytimg.com/vi/i43tkaTXtwI/hqdefault.jpg"
            },
            new Music
            {
                Id = 14,
                Title = "Chill Drive - Aesthetic Music ~ Lofi hip hop mix",
                Artist = "Chill Beats Records",
                Channel = "chilli music",
                Mood = Mood.Chill,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=iicfmXFALM8",
                Thumbnail = "https://i.ytimg.com/vi/iicfmXFALM8/hqdefault.jpg"
            },
            new Music
            {
                Id = 15,
                Title = "you need to sleep.",
                Artist = "she's gone",
                Channel = "she's gone",
                Mood = Mood.Sleep,
                Genre = Genre.Ambient,
                URL = "https://www.youtube.com/watch?v=2AH5t_o7lmg",
                Thumbnail = "https://i.ytimg.com/vi/2AH5t_o7lmg/hqdefault.jpg"
            },
            new Music
            {
                Id = 16,
                Title = "Morning Coffee ☕️ [lofi hip hop]",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Focus,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=1fueZCTYkpA",
                Thumbnail = "https://i.ytimg.com/vi/1fueZCTYkpA/hqdefault.jpg"
            },
            new Music
            {
                Id = 17,
                Title = "Alone with myself / lofi hip hop mix",
                Artist = "Dreamy",
                Channel = "Dreamy",
                Mood = Mood.Sad,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=ldUT4FLxql4",
                Thumbnail = "https://i.ytimg.com/vi/ldUT4FLxql4/hqdefault.jpg"
            },
            new Music
            {
                Id = 18,
                Title = "Lazy Sunday 💤 [lofi hip hop]",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Relax,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=zFhfksjf_mY",
                Thumbnail = "https://i.ytimg.com/vi/zFhfksjf_mY/hqdefault.jpg"
            },
            new Music
            {
                Id = 19,
                Title = "ＳＬＥＥＰＹ 💤 Lofi hip hop mix - Beats to sleep/chill to | Deep Sleeping Music",
                Artist = "Dreamhop Music",
                Channel = "Music chill",
                Mood = Mood.Sleep,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=ff5lO8TkVX8",
                Thumbnail = "https://i.ytimg.com/vi/ff5lO8TkVX8/hqdefault.jpg"
            },
            new Music
            {
                Id = 20,
                Title = "Make you feel positive and peaceful 🍀 Lofi Coffee ☕ ~ Lofi Hip Hop - Lofi Music [ Study/ Relax ]",
                Artist = "Lofi Coffee",
                Channel = "Lofi Coffee",
                Mood = Mood.Focus,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=OO2kPK5-qno",
                Thumbnail = "https://i.ytimg.com/vi/OO2kPK5-qno/hqdefault.jpg"
            },
            new Music
            {
                Id = 21,
                Title = "Chill Summer Lofi [chill lo-fi hip hop beats]",
                Artist = "Settle",
                Channel = "Settle",
                Mood = Mood.Chill,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=kyqpSycLASY",
                Thumbnail = "https://i.ytimg.com/vi/kyqpSycLASY/hqdefault.jpg"
            },
            new Music
            {
                Id = 22,
                Title = "Less talk....  more action. / Lo-fi for study, work ( with Rain sounds)",
                Artist = "Chill Chill Journal",
                Channel = "chill chill journal",
                Mood = Mood.Study,
                Genre = Genre.Jazz,
                URL = "https://www.youtube.com/watch?v=9kzE8isXlQY",
                Thumbnail = "https://i.ytimg.com/vi/9kzE8isXlQY/hqdefault.jpg"
            },
            new Music
            {
                Id = 23,
                Title = "Ghibli Coffee Shop ☕️ Music to put you in a better mood 🌿 lofi hip hop - lofi songs | study / relax",
                Artist = "Lofi Coffee",
                Channel = "Lofi Coffee",
                Mood = Mood.Happy,
                Genre = Genre.Ambient,
                URL = "https://www.youtube.com/watch?v=zhDwjnYZiCo",
                Thumbnail = "https://i.ytimg.com/vi/zhDwjnYZiCo/hqdefault.jpg"
            },
            new Music
            {
                Id = 24,
                Title = "C H I L L V I B E S | Simpson Lofi Mix 2022 | Chill & Aesthetic Music Playlist",
                Artist = "Lofi Fan",
                Channel = "lofi fan",
                Mood = Mood.Chill,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=MtT5_PgLJlY",
                Thumbnail = "https://i.ytimg.com/vi/MtT5_PgLJlY/hqdefault.jpg"
            },
            new Music
            {
                Id = 25,
                Title = "Quiet 🌤️ Lofi Keep You Safe 🍃 Serenity to Deep focus work, relax [ Lofi hip hop - Lofi Summer ]",
                Artist = "Lofi Keep You Safe",
                Channel = "LOFI KEEP YOU SAFE",
                Mood = Mood.Focus,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=JdqL89ZZwFw",
                Thumbnail = "https://i.ytimg.com/vi/JdqL89ZZwFw/hqdefault.jpg"
            },
            new Music
            {
                Id = 26,
                Title = "Lofi music playlist『2 hour』sleep/study/aesthetic/work/relax",
                Artist = "Xuanlofi",
                Channel = "xuanlofi",
                Mood = Mood.Relax,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=q0ff3e-A7DY",
                Thumbnail = "https://i.ytimg.com/vi/q0ff3e-A7DY/hqdefault.jpg"
            },
            new Music
            {
                Id = 27,
                Title = "Ｓｍｏｋｅ Ａｎｄ Ｃｈｉｌｌ 🚬 Lofi Hip Hop 🎵 [ Beats To Smoke / Chill / Relax / Stress Relief ]",
                Artist = "Chill Melodies",
                Channel = "Chill Melodies",
                Mood = Mood.Chill,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=_gVrQa_bvm8",
                Thumbnail = "https://i.ytimg.com/vi/_gVrQa_bvm8/hqdefault.jpg"
            },
            new Music
            {
                Id = 28,
                Title = "12 A.M Study Session 📚 [lofi hip hop]",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Study,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=l98w9OSKVNA",
                Thumbnail = "https://i.ytimg.com/vi/l98w9OSKVNA/hqdefault.jpg"
            },
            new Music
            {
                Id = 29,
                Title = "code-fi / lofi beats to code/relax to",
                Artist = "The AMP Channel",
                Channel = "The AMP Channel",
                Mood = Mood.Focus,
                Genre = Genre.Electronic,
                URL = "https://www.youtube.com/watch?v=f02mOEt11OQ",
                Thumbnail = "https://i.ytimg.com/vi/f02mOEt11OQ/hqdefault.jpg"
            },
            new Music
            {
                Id = 30,
                Title = "𝙻𝚘𝚏𝚒 𝚁𝚘𝚘𝚖 / 𝙶𝚞𝚒𝚝𝚊𝚛 𝙻𝚘𝚏𝚒 / 𝙲𝚊𝚏𝚎 𝙼𝚞𝚜𝚒𝚌 / 𝙴𝚊𝚜𝚢 𝚕𝚒𝚜𝚝𝚎𝚗𝚒𝚗𝚐 / 𝙱𝙶𝙼 / 𝙻𝚘𝚏𝚒 𝙼𝚞𝚜𝚒𝚌 / 𝚅𝚎𝚛.𝟾",
                Artist = "Myour Music",
                Channel = "Myour Music",
                Mood = Mood.Chill,
                Genre = Genre.Acoustic,
                URL = "https://www.youtube.com/watch?v=VUQfT3gNT3g",
                Thumbnail = "https://i.ytimg.com/vi/VUQfT3gNT3g/hqdefault.jpg"
            },
            new Music
            {
                Id = 31,
                Title = "Coffee Lofi ☕1 Hour Cafe Song 🎵 Stream cafe ✨cute & relaxing music 🍪 Make Your Day Better",
                Artist = "Lofi Kitty",
                Channel = "Lofi Kitty",
                Mood = Mood.Happy,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=01dn67QubYQ",
                Thumbnail = "https://i.ytimg.com/vi/01dn67QubYQ/hqdefault.jpg"
            },            new Music
            {
                Id = 33,
                Title = "lofi hip hop mix 📚 beats to relax/study to (Part 1)",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Study,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=CFGLoQIhmow",
                Thumbnail = "https://i.ytimg.com/vi/CFGLoQIhmow/hqdefault.jpg"
            },
            new Music
            {
                Id = 34,
                Title = "Quiet Solitude - Lofi Song ~ Lofi hip hop mix ~ Stress Relief / Relaxing Music / Smoke & Chill",
                Artist = "Chilli High",
                Channel = "Chilli High",
                Mood = Mood.Relax,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=82ujdQBjpDQ",
                Thumbnail = "https://i.ytimg.com/vi/82ujdQBjpDQ/hqdefault.jpg"
            },
            new Music
            {
                Id = 35,
                Title = "ＳＴＵＤＹ ＴＩＭＥ ✍ Lofi Hip Hop | Study Music ✍ Lofi study, Relaxing Music",
                Artist = "Mimi Lofi Chill",
                Channel = "Mimi Lofi Chill",
                Mood = Mood.Study,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=32dGIHCzbfE",
                Thumbnail = "https://i.ytimg.com/vi/32dGIHCzbfE/hqdefault.jpg"
            },
            new Music
            {
                Id = 36,
                Title = "Lofi Relax 🍃 Lofi Hip Hop | Calming Music 🎶 Deep Focus, Relaxing Music",
                Artist = "Chill Melodies",
                Channel = "Chill Melodies",
                Mood = Mood.Relax,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=UOJ4V3DAAx8",
                Thumbnail = "https://i.ytimg.com/vi/UOJ4V3DAAx8/hqdefault.jpg"
            },
            new Music
            {
                Id = 37,
                Title = "Ｎｉｇｈｔ Ｄｒｉｖｅ ~ lofi hip hop mix ~ beats to chill / drive to",
                Artist = "Mimi Lofi Chill",
                Channel = "Mimi Lofi Chill",
                Mood = Mood.Chill,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=zW5wpJY1rgQ",
                Thumbnail = "https://i.ytimg.com/vi/zW5wpJY1rgQ/hqdefault.jpg"
            },
            new Music
            {
                Id = 38,
                Title = "Chill Lofi Beats Mix [chill lo-fi hip hop beats/Study & Relax Music] Vol. 32",
                Artist = "Art Is Sound",
                Channel = "Art Is Sound",
                Mood = Mood.Relax,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=BCxTQq0UiFs",
                Thumbnail = "https://i.ytimg.com/vi/BCxTQq0UiFs/hqdefault.jpg"
            },
            new Music
            {
                Id = 39,
                Title = "Musica Para Trabajar 📚 Música Para Estudiar 📚 Lofi hip hop mix | Musica Relajante",
                Artist = "Music For Life",
                Channel = "music for life ",
                Mood = Mood.Study,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=aQZHAl_eV1c",
                Thumbnail = "https://i.ytimg.com/vi/aQZHAl_eV1c/hqdefault.jpg"
            },
            new Music
            {
                Id = 40,
                Title = "The Abyss 🌿 Deep Lofi Beats",
                Artist = "Ani R",
                Channel = "Dreamhop Music",
                Mood = Mood.Calm,
                Genre = Genre.Ambient,
                URL = "https://www.youtube.com/watch?v=TWTV4T3yxzs",
                Thumbnail = "https://i.ytimg.com/vi/TWTV4T3yxzs/hqdefault.jpg"
            },
            new Music
            {
                Id = 41,
                Title = "Zelda & Chill 2",
                Artist = "GameChops",
                Channel = "GameChops",
                Mood = Mood.Chill,
                Genre = Genre.Electronic,
                URL = "https://www.youtube.com/watch?v=rJlY1uKL87k",
                Thumbnail = "https://i.ytimg.com/vi/rJlY1uKL87k/hqdefault.jpg"
            },
            new Music
            {
                Id = 42,
                Title = "lofi sleep, lo-fi rain 💤 8 hours mix 😴 beats to sleep/chill/relax to - music for insomnia & anxiety",
                Artist = "Sleep Tales",
                Channel = "Sleep Tales",
                Mood = Mood.Sleep,
                Genre = Genre.Ambient,
                URL = "https://www.youtube.com/watch?v=gWp8xxB2PxM",
                Thumbnail = "https://i.ytimg.com/vi/gWp8xxB2PxM/hqdefault.jpg"
            },
            new Music
            {
                Id = 43,
                Title = "Just relax 🍀 stop overthinking, calm your anxiety - lofi hip hop mix - aesthetic lofi",
                Artist = "Purrple Cat",
                Channel = "Aesthetic Lofi",
                Mood = Mood.Calm,
                Genre = Genre.Ambient,
                URL = "https://www.youtube.com/watch?v=zuCRSwWssVk",
                Thumbnail = "https://i.ytimg.com/vi/zuCRSwWssVk/hqdefault.jpg"
            },
            new Music
            {
                Id = 44,
                Title = "Sleepless Night 🌙 [lofi hip hop]",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Sleep,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=Nyx6SBixRE8",
                Thumbnail = "https://i.ytimg.com/vi/Nyx6SBixRE8/hqdefault.jpg"
            },
            new Music
            {
                Id = 45,
                Title = "Steven Universe Theme (Lofi Remix) (1 hour)",
                Artist = "L.Dre",
                Channel = "wistful fox",
                Mood = Mood.Chill,
                Genre = Genre.Pop,
                URL = "https://www.youtube.com/watch?v=r05jnglMeyU",
                Thumbnail = "https://i.ytimg.com/vi/r05jnglMeyU/hqdefault.jpg"
            },
            new Music
            {
                Id = 46,
                Title = "Study with Bluey // Cozy chill lofi music",
                Artist = "Hartistley",
                Channel = "Hartistley",
                Mood = Mood.Study,
                Genre = Genre.Ambient,
                URL = "https://www.youtube.com/watch?v=CV96jTya1_Y",
                Thumbnail = "https://i.ytimg.com/vi/CV96jTya1_Y/hqdefault.jpg"
            },
            new Music
            {
                Id = 47,
                Title = "Bedtime Lofi 💤 8 hours of relaxing beats to sleep to",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Sleep,
                Genre = Genre.Ambient,
                URL = "https://www.youtube.com/watch?v=UJs6__K7gSY",
                Thumbnail = "https://i.ytimg.com/vi/UJs6__K7gSY/hqdefault.jpg"
            },
            new Music
            {
                Id = 48,
                Title = "Lofi Work Space 📂 Deep Focus Study/Work Concentration [chill lo-fi hip hop beats]",
                Artist = "Chill Village",
                Channel = "𝗖𝗛𝗜𝗟𝗟 𝗩𝗜𝗟𝗟𝗔𝗚Ｅ",
                Mood = Mood.Focus,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=Q89Dzox4jAE",
                Thumbnail = "https://i.ytimg.com/vi/Q89Dzox4jAE/hqdefault.jpg"
            },
            new Music
            {
                Id = 49,
                Title = "Chill Vibes 🎧😌💨 Lofi hip hop ~ Healing Music, Relaxing, Study To",
                Artist = "Chilli Music",
                Channel = "chilli music",
                Mood = Mood.Chill,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=IYLDF2-PvFg",
                Thumbnail = "https://i.ytimg.com/vi/IYLDF2-PvFg/hqdefault.jpg"
            },
            new Music
            {
                Id = 50,
                Title = "ＳＬＥＥＰＹ Lofi Cat 💤 Listen to it to escape from a hard day with my cat 💤 Beats To Sleep / Chill To",
                Artist = "Lofi Ailurophile",
                Channel = "Lofi Ailurophile",
                Mood = Mood.Sleep,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=vvThzcBfnyc",
                Thumbnail = "https://i.ytimg.com/vi/vvThzcBfnyc/hqdefault.jpg"
            },
            new Music
            {
                Id = 51,
                Title = "1 A.M Study Session 📚 [lofi hip hop]",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Study,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=lTRiuFIWV54",
                Thumbnail = "https://i.ytimg.com/vi/lTRiuFIWV54/hqdefault.jpg"
            },
            new Music
            {
                Id = 52,
                Title = "C H I L L V I B E S | Chill & aesthetic music playlist",
                Artist = "EYM",
                Channel = "EYM",
                Mood = Mood.Chill,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=oJnF5VxTO5g",
                Thumbnail = "https://i.ytimg.com/vi/oJnF5VxTO5g/hqdefault.jpg"
            }, 
            new Music
            {
                Id = 54,
                Title = "Best of lofi 2018 🎶 beats to chill/study to",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Study,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=-FlxM_0S2lA",
                Thumbnail = "https://i.ytimg.com/vi/-FlxM_0S2lA/hqdefault.jpg"
            },
            new Music
            {
                Id = 55,
                Title = "old songs but it's lofi remix",
                Artist = "Various Artists",
                Channel = "Lo-fi Music",
                Mood = Mood.Chill,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=BrnDlRmW5hs",
                Thumbnail = "https://i.ytimg.com/vi/BrnDlRmW5hs/hqdefault.jpg"
            },
            new Music
            {
                Id = 56,
                Title = "Best of lofi hip hop 2021 ✨ [beats to relax/study to]",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Relax,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=n61ULEU7CO0",
                Thumbnail = "https://i.ytimg.com/vi/n61ULEU7CO0/hqdefault.jpg"
            },
            new Music
            {
                Id = 57,
                Title = "Rainy Jazz Cafe - Slow Jazz Music in Coffee Shop Ambience for Work, Study and Relaxation",
                Artist = "Coffee Shop Vibes",
                Channel = "Coffee Shop Vibes",
                Mood = Mood.Focus,
                Genre = Genre.Jazz,
                URL = "https://www.youtube.com/watch?v=NJuSStkIZBg",
                Thumbnail = "https://i.ytimg.com/vi/NJuSStkIZBg/hqdefault.jpg"
            },
            new Music
            {
                Id = 58,
                Title = "90's Chill Lofi ☕️ Study Music Lofi Rain Chillhop Beats ☔️ Lofi Rain Playlist",
                Artist = "The Japanese Town",
                Channel = "The Japanese Town",
                Mood = Mood.Study,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=sF80I-TQiW0",
                Thumbnail = "https://i.ytimg.com/vi/sF80I-TQiW0/hqdefault.jpg"
            },
            new Music
            {
                Id = 59,
                Title = "2 A.M Study Session 📚 [lofi hip hop]",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Study,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=wAPCSnAhhC8",
                Thumbnail = "https://i.ytimg.com/vi/wAPCSnAhhC8/hqdefault.jpg"
            },
            new Music
            {
                Id = 61,
                Title = "Best of lofi hip hop 2022 🎆 - beats to relax/study to",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Relax,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=i43tkaTXtwI",
                Thumbnail = "https://i.ytimg.com/vi/i43tkaTXtwI/hqdefault.jpg"
            },
            new Music
            {
                Id = 62,
                Title = "Chill Drive - Aesthetic Music ~ Lofi hip hop mix",
                Artist = "Chill Beats Records",
                Channel = "chilli music",
                Mood = Mood.Chill,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=iicfmXFALM8",
                Thumbnail = "https://i.ytimg.com/vi/iicfmXFALM8/hqdefault.jpg"
            },
            new Music
            {
                Id = 63,
                Title = "you need to sleep.",
                Artist = "she's gone",
                Channel = "she's gone",
                Mood = Mood.Sleep,
                Genre = Genre.Ambient,
                URL = "https://www.youtube.com/watch?v=2AH5t_o7lmg",
                Thumbnail = "https://i.ytimg.com/vi/2AH5t_o7lmg/hqdefault.jpg"
            },
            new Music
            {
                Id = 64,
                Title = "Morning Coffee ☕️ [lofi hip hop]",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Focus,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=1fueZCTYkpA",
                Thumbnail = "https://i.ytimg.com/vi/1fueZCTYkpA/hqdefault.jpg"
            },
            new Music
            {
                Id = 67,
                Title = "🎧2027 Ultimate Mind Relaxing Lofi Beats | Study, Sleep & Chill 😍✨",
                Artist = "77 Bird",
                Channel = "77 Bird ",
                Mood = Mood.Calm,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=yQDGJ7L3rxQ",
                Thumbnail = "https://i.ytimg.com/vi/yQDGJ7L3rxQ/hqdefault.jpg"
            },
            new Music
            {
                Id = 69,
                Title = "Make you feel positive and peaceful 🍀 Lofi Coffee ☕ ~ Lofi Hip Hop - Lofi Music [ Study/ Relax ]",
                Artist = "Lofi Coffee",
                Channel = "Lofi Coffee",
                Mood = Mood.Focus,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=OO2kPK5-qno",
                Thumbnail = "https://i.ytimg.com/vi/OO2kPK5-qno/hqdefault.jpg"
            },
            new Music
            {
                Id = 70,
                Title = "Chill Summer Lofi [chill lo-fi hip hop beats]",
                Artist = "Settle",
                Channel = "Settle",
                Mood = Mood.Chill,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=kyqpSycLASY",
                Thumbnail = "https://i.ytimg.com/vi/kyqpSycLASY/hqdefault.jpg"
            },
            new Music
            {
                Id = 72,
                Title = "It's 3am. Why so sad ? ~ lofi hip hop mix",
                Artist = "Dreamy",
                Channel = "Dreamy",
                Mood = Mood.Sad,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=hzpt3fQjY9U",
                Thumbnail = "https://i.ytimg.com/vi/hzpt3fQjY9U/hqdefault.jpg"
            },
            new Music
            {
                Id = 73,
                Title = "Less talk.... more action. / Lo-fi for study, work ( with Rain sounds)",
                Artist = "Chill Chill Journal",
                Channel = "chill chill journal",
                Mood = Mood.Study,
                Genre = Genre.Jazz,
                URL = "https://www.youtube.com/watch?v=9kzE8isXlQY",
                Thumbnail = "https://i.ytimg.com/vi/9kzE8isXlQY/hqdefault.jpg"
            },
            new Music
            {
                Id = 75,
                Title = "Ghibli Coffee Shop ☕️ Music to put you in a better mood 🌿 lofi hip hop - lofi songs | study / relax",
                Artist = "Lofi Coffee",
                Channel = "Lofi Coffee",
                Mood = Mood.Happy,
                Genre = Genre.Ambient,
                URL = "https://www.youtube.com/watch?v=zhDwjnYZiCo",
                Thumbnail = "https://i.ytimg.com/vi/zhDwjnYZiCo/hqdefault.jpg"
            },
            new Music
            {
                Id = 76,
                Title = "Quiet 🌤️ Lofi Keep You Safe 🍃 Serenity to Deep focus work, relax [ Lofi hip hop - Lofi Summer ]",
                Artist = "Lofi Keep You Safe",
                Channel = "LOFI KEEP YOU SAFE",
                Mood = Mood.Focus,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=JdqL89ZZwFw",
                Thumbnail = "https://i.ytimg.com/vi/JdqL89ZZwFw/hqdefault.jpg"
            },
            new Music
            {
                Id = 77,
                Title = "Lofi music playlist『2 hour』sleep/study/aesthetic/work/relax",
                Artist = "Xuanlofi",
                Channel = "xuanlofi",
                Mood = Mood.Relax,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=q0ff3e-A7DY",
                Thumbnail = "https://i.ytimg.com/vi/q0ff3e-A7DY/hqdefault.jpg"
            },
            new Music
            {
                Id = 78,
                Title = "12 A.M Study Session 📚 [lofi hip hop]",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Study,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=l98w9OSKVNA",
                Thumbnail = "https://i.ytimg.com/vi/l98w9OSKVNA/hqdefault.jpg"
            },
            new Music
            {
                Id = 79,
                Title = "𝙻𝚘𝚏𝚒 𝚁𝚘𝚘𝚖 / 𝙶𝚞𝚒𝚝𝚊𝚛 𝙻𝚘𝚏𝚒 / 𝙲𝚊𝚏𝚎 𝙼𝚞𝚜𝚒𝚌 / 𝙴𝚊𝚜𝚢 𝚕𝚒𝚜𝚝𝚎𝚗𝚒敷 / 𝙱𝙶𝙼 / 𝙻𝚘𝚏𝚒 𝙼𝚞𝚜𝚒𝚌 / 𝚅𝚎𝚛.𝟾",
                Artist = "Myour Music",
                Channel = "Myour Music",
                Mood = Mood.Chill,
                Genre = Genre.Acoustic,
                URL = "https://www.youtube.com/watch?v=VUQfT3gNT3g",
                Thumbnail = "https://i.ytimg.com/vi/VUQfT3gNT3g/hqdefault.jpg"
            },
            new Music
            {
                Id = 80,
                Title = "Breathe 🍀 Lofi Deep Focus 🌳 Study/Calm/Heal [ Lofi Hip Hop - Lofi Chill ]",
                Artist = "Lofi Keep You Safe",
                Channel = "LOFI KEEP YOU SAFE",
                Mood = Mood.Focus,
                Genre = Genre.Ambient,
                URL = "https://www.youtube.com/watch?v=6H-PLF2CR18",
                Thumbnail = "https://i.ytimg.com/vi/6H-PLF2CR18/hqdefault.jpg"
            },
            new Music
            {
                Id = 82,
                Title = "Coffee Lofi ☕1 Hour Cafe Song 🎵 Stream cafe ✨cute & relaxing music 🍪 Make Your Day Better",
                Artist = "Lofi Kitty",
                Channel = "Lofi Kitty",
                Mood = Mood.Happy,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=01dn67QubYQ",
                Thumbnail = "https://i.ytimg.com/vi/01dn67QubYQ/hqdefault.jpg"
            },
            new Music
            {
                Id = 83,
                Title = "lofi hip hop mix 📚 beats to relax/study to (Part 1)",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Study,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=CFGLoQIhmow",
                Thumbnail = "https://i.ytimg.com/vi/CFGLoQIhmow/hqdefault.jpg"
            },
            new Music
            {
                Id = 84,
                Title = "Quiet Solitude - Lofi Song ~ Lofi hip hop mix ~ Stress Relief / Relaxing Music / Smoke & Chill",
                Artist = "Chilli High",
                Channel = "Chilli High",
                Mood = Mood.Relax,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=82ujdQBjpDQ",
                Thumbnail = "https://i.ytimg.com/vi/82ujdQBjpDQ/hqdefault.jpg"
            },
            new Music
            {
                Id = 85,
                Title = "ＳＴＵＤＹ ＴＩＭＥ ✍ Lofi Hip Hop | Study Music ✍ Lofi study, Relaxing Music",
                Artist = "Mimi Lofi Chill",
                Channel = "Mimi Lofi Chill",
                Mood = Mood.Study,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=32dGIHCzbfE",
                Thumbnail = "https://i.ytimg.com/vi/32dGIHCzbfE/hqdefault.jpg"
            },
            new Music
            {
                Id = 86,
                Title = "Broken & Alone 😅🚶‍♂️ | Heartfelt Sad Mashup | Hindi Lofi 2026",
                Artist = "LOFI BOY",
                Channel = "LOFI BOY",
                Mood = Mood.Sad,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=wj27s9XpxRQ",
                Thumbnail = "https://i.ytimg.com/vi/wj27s9XpxRQ/hqdefault.jpg"
            },
            new Music
            {
                Id = 87,
                Title = "Soothing Breeze 🍃 [asian lofi]",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Calm,
                Genre = Genre.World,
                URL = "https://www.youtube.com/watch?v=gnZImHvA0ME",
                Thumbnail = "https://i.ytimg.com/vi/gnZImHvA0ME/hqdefault.jpg"
            },            new Music
            {
                Id = 90,
                Title = "Lofi Relax 🍃 Lofi Hip Hop | Calming Music 🎶 Deep Focus, Relaxing Music",
                Artist = "Chill Melodies",
                Channel = "Chill Melodies",
                Mood = Mood.Relax,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=UOJ4V3DAAx8",
                Thumbnail = "https://i.ytimg.com/vi/UOJ4V3DAAx8/hqdefault.jpg"
            },
            new Music
            {
                Id = 91,
                Title = "夜間飛行 ~ lofi hip hop mix ~ beats to chill / drive to",
                Artist = "Mimi Lofi Chill",
                Channel = "Mimi Lofi Chill",
                Mood = Mood.Chill,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=zW5wpJY1rgQ",
                Thumbnail = "https://i.ytimg.com/vi/zW5wpJY1rgQ/hqdefault.jpg"
            },
            new Music
            {
                Id = 92,
                Title = "Chill Lofi Beats Mix [chill lo-fi hip hop beats/Study & Relax Music] Vol. 32",
                Artist = "Art Is Sound",
                Channel = "Art Is Sound",
                Mood = Mood.Relax,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=BCxTQq0UiFs",
                Thumbnail = "https://i.ytimg.com/vi/BCxTQq0UiFs/hqdefault.jpg"
            },
            new Music
            {
                Id = 93,
                Title = "LO-FI 2307 - NON STOP INSTAGRAM TRENDING LOVE MASHUP - Part 15",
                Artist = "Lo-fi 2307",
                Channel = "Lo-fi 2307",
                Mood = Mood.Romantic,
                Genre = Genre.Pop,
                URL = "https://www.youtube.com/watch?v=yBG72loq_iI",
                Thumbnail = "https://i.ytimg.com/vi/yBG72loq_iI/hqdefault.jpg"
            },
            new Music
            {
                Id = 94,
                Title = "Avatar: The Last Airbender☁️ ~ 1 HOUR OF LOFI CHILLOUT MUSIC | Vol.1",
                Artist = "Simon Groß",
                Channel = "Simon Groß",
                Mood = Mood.Chill,
                Genre = Genre.Ambient,
                URL = "https://www.youtube.com/watch?v=D-ya6U-pbWo",
                Thumbnail = "https://i.ytimg.com/vi/D-ya6U-pbWo/hqdefault.jpg"
            },
            new Music
            {
                Id = 95,
                Title = "Musica Para Trabajar 📚 Música Para Estudiar 📚 Lofi hip hop mix | Musica Relajante",
                Artist = "Music For Life",
                Channel = "music for life ",
                Mood = Mood.Study,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=aQZHAl_eV1c",
                Thumbnail = "https://i.ytimg.com/vi/aQZHAl_eV1c/hqdefault.jpg"
            },
            new Music
            {
                Id = 96,
                Title = "lofi sleep, lo-fi rain 💤 8 hours mix 😴 beats to sleep/chill/relax to - music for insomnia & anxiety",
                Artist = "Sleep Tales",
                Channel = "Sleep Tales",
                Mood = Mood.Sleep,
                Genre = Genre.Ambient,
                URL = "https://www.youtube.com/watch?v=gWp8xxB2PxM",
                Thumbnail = "https://i.ytimg.com/vi/gWp8xxB2PxM/hqdefault.jpg"
            },
            new Music
            {
                Id = 97,
                Title = "2026 Ultimate Mind Relaxing Lofi Beats| Study, Sleep & Chill lofi 💫 part 6",
                Artist = "ManiacsVS Music",
                Channel = "ManiacsVS music ",
                Mood = Mood.Calm,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=bhYubx9A3TQ",
                Thumbnail = "https://i.ytimg.com/vi/bhYubx9A3TQ/hqdefault.jpg"
            },
            new Music
            {
                Id = 98,
                Title = "Study with Bluey // Cozy chill lofi music",
                Artist = "Hartistley",
                Channel = "Hartistley",
                Mood = Mood.Study,
                Genre = Genre.Ambient,
                URL = "https://www.youtube.com/watch?v=CV96jTya1_Y",
                Thumbnail = "https://i.ytimg.com/vi/CV96jTya1_Y/hqdefault.jpg"
            },
            new Music
            {
                Id = 99,
                Title = "Bedtime Lofi 💤 8 hours of relaxing beats to sleep to",
                Artist = "Lofi Girl",
                Channel = "Lofi Girl",
                Mood = Mood.Sleep,
                Genre = Genre.Ambient,
                URL = "https://www.youtube.com/watch?v=UJs6__K7gSY",
                Thumbnail = "https://i.ytimg.com/vi/UJs6__K7gSY/hqdefault.jpg"
            },
            new Music
            {
                Id = 100,
                Title = "Lofi Work Space 📂 Deep Focus Study/Work Concentration [chill lo-fi hip hop beats]",
                Artist = "Chill Village",
                Channel = "𝗖𝗛𝗜𝗟𝗟 𝗩𝗜𝗟𝗟𝗔𝗚Ｅ",
                Mood = Mood.Focus,
                Genre = Genre.HipHop,
                URL = "https://www.youtube.com/watch?v=Q89Dzox4jAE",
                Thumbnail = "https://i.ytimg.com/vi/Q89Dzox4jAE/hqdefault.jpg"
            },
                new Music
                {
                    Id = 101,
                    Title = "C H I L L V I B E S | Chill & aesthetic music playlist",
                    Artist = "EYM",
                    Channel = "EYM",
                    Mood = Mood.Chill,
                    Genre = Genre.Chill,
                    URL = "https://www.youtube.com/watch?v=oJnF5VxTO5g",
                    Thumbnail = "https://i.ytimg.com/vi/oJnF5VxTO5g/hqdefault.jpg"
                },
                new Music
                {
                    Id = 102,
                    Title = "Chill Lofi Mix [chill lo-fi hip hop beats]",
                    Artist = "Settle",
                    Channel = "Settle",
                    Mood = Mood.Chill,
                    Genre = Genre.LoFi,
                    URL = "https://www.youtube.com/watch?v=CLeZyIID9Bo",
                    Thumbnail = "https://i.ytimg.com/vi/CLeZyIID9Bo/hqdefault.jpg"
                },
                new Music
                {
                    Id = 103,
                    Title = "Zelda & Chill",
                    Artist = "GameChops",
                    Channel = "GameChops",
                    Mood = Mood.Chill,
                    Genre = Genre.LoFi,
                    URL = "https://www.youtube.com/watch?v=GdzrrWA8e7A",
                    Thumbnail = "https://i.ytimg.com/vi/GdzrrWA8e7A/hqdefault.jpg"
                },
                new Music
                {
                    Id = 104,
                    Title = "Peaceful Piano & Soft Rain - Relaxing Sleep Music, A Bitter Rain",
                    Artist = "The Soul of Wind",
                    Channel = "The Soul of Wind",
                    Mood = Mood.Sleep,
                    Genre = Genre.Ambient,
                    URL = "https://www.youtube.com/watch?v=hj83cwfOF3Y",
                    Thumbnail = "https://i.ytimg.com/vi/hj83cwfOF3Y/hqdefault.jpg"
                },
                new Music
                {
                    Id = 105,
                    Title = "Epic Chillstep Collection 2015 [2 Hours]",
                    Artist = "Arctic Empire",
                    Channel = "Arctic Empire",
                    Mood = Mood.Chill,
                    Genre = Genre.Electronic,
                    URL = "https://www.youtube.com/watch?v=fWRISvgAygU",
                    Thumbnail = "https://i.ytimg.com/vi/fWRISvgAygU/hqdefault.jpg"
                },
                new Music
                {
                    Id = 106,
                    Title = "90's Chill Lofi ☕️ Study Music Lofi Rain Chillhop Beats ☔️ Lofi Rain Playlist",
                    Artist = "The Japanese Town",
                    Channel = "The Japanese Town",
                    Mood = Mood.Study,
                    Genre = Genre.LoFi,
                    URL = "https://www.youtube.com/watch?v=sF80I-TQiW0",
                    Thumbnail = "https://i.ytimg.com/vi/sF80I-TQiW0/hqdefault.jpg"
                },
                new Music
                {
                    Id = 107,
                    Title = "all of a sudden, everything becomes alright...",
                    Artist = "Ambient Crafts",
                    Channel = "Ambient Crafts",
                    Mood = Mood.Calm,
                    Genre = Genre.Ambient,
                    URL = "https://www.youtube.com/watch?v=ANkxRGvl1VY",
                    Thumbnail = "https://i.ytimg.com/vi/ANkxRGvl1VY/hqdefault.jpg"
                },
                new Music
                {
                    Id = 108,
                    Title = "🎧 Thunder and Rain with Halo 3: ODST Piano 8 Hours | Sleep and Relaxation",
                    Artist = "Dukino",
                    Channel = "Dukino",
                    Mood = Mood.Sleep,
                    Genre = Genre.Ambient,
                    URL = "https://www.youtube.com/watch?v=MzJjzEEphfM",
                    Thumbnail = "https://i.ytimg.com/vi/MzJjzEEphfM/hqdefault.jpg"
                },
                new Music
                {
                    Id = 109,
                    Title = "Hogwarts Classroom | Harry Potter Music & Ambience - 5 Scenes for Studying, Focusing, & Sleep",
                    Artist = "Ambient Worlds",
                    Channel = "Ambient Worlds",
                    Mood = Mood.Focus,
                    Genre = Genre.Ambient,
                    URL = "https://www.youtube.com/watch?v=BQrxsyGTztM",
                    Thumbnail = "https://i.ytimg.com/vi/BQrxsyGTztM/hqdefault.jpg"
                },
                new Music
                {
                    Id = 110,
                    Title = "Chill Drive - Aesthetic Music ~ Lofi hip hop mix",
                    Artist = "chilli music",
                    Channel = "chilli music",
                    Mood = Mood.Chill,
                    Genre = Genre.LoFi,
                    URL = "https://www.youtube.com/watch?v=iicfmXFALM8",
                    Thumbnail = "https://i.ytimg.com/vi/iicfmXFALM8/hqdefault.jpg"
                },
                new Music
                {
                    Id = 111,
                    Title = "City of Gamers - Chill/Gaming/Studying Lofi Hip Hop Mix - (1 hour)",
                    Artist = "Deepspot Lofi",
                    Channel = "Deepspot Lofi",
                    Mood = Mood.Study,
                    Genre = Genre.LoFi,
                    URL = "https://www.youtube.com/watch?v=FFfdyV8gnWk",
                    Thumbnail = "https://i.ytimg.com/vi/FFfdyV8gnWk/hqdefault.jpg"
                },
                new Music
                {
                    Id = 112,
                    Title = "Party music mix ~ Best songs that make you dance",
                    Artist = "A.C Vibes",
                    Channel = "A.C Vibes",
                    Mood = Mood.Party,
                    Genre = Genre.Dance,
                    URL = "https://www.youtube.com/watch?v=7J653nwumcw",
                    Thumbnail = "https://i.ytimg.com/vi/7J653nwumcw/hqdefault.jpg"
                },
            new Music
                {
                    Id = 114,
                    Title = "The Witcher 3: One hour of Emotional and Relaxing Music",
                    Artist = "Prestigious_Gaming",
                    Channel = "Prestigious_Gaming",
                    Mood = Mood.Relax,
                    Genre = Genre.Ambient,
                    URL = "https://www.youtube.com/watch?v=8GYL6c_GTE0",
                    Thumbnail = "https://i.ytimg.com/vi/8GYL6c_GTE0/hqdefault.jpg"
                },
                new Music
                {
                    Id = 115,
                    Title = "no more thinking tonight... (minecraft music w/ soft rain)",
                    Artist = "Drift Away Ambience",
                    Channel = "Drift Away Ambience",
                    Mood = Mood.Sleep,
                    Genre = Genre.Ambient,
                    URL = "https://www.youtube.com/watch?v=yJ6Lbsmb1lY",
                    Thumbnail = "https://i.ytimg.com/vi/yJ6Lbsmb1lY/hqdefault.jpg"
                },
                new Music
                {
                    Id = 116,
                    Title = "ＳＬＥＥＰＹ 💤 Lofi hip hop mix - Beats to sleep/chill to | Deep Sleeping Music",
                    Artist = "Music chill",
                    Channel = "Music chill",
                    Mood = Mood.Sleep,
                    Genre = Genre.LoFi,
                    URL = "https://www.youtube.com/watch?v=ff5lO8TkVX8",
                    Thumbnail = "https://i.ytimg.com/vi/ff5lO8TkVX8/hqdefault.jpg"
                },
                new Music
                {
                    Id = 117,
                    Title = "Game of Thrones Music & North Ambience | Winterfell - House Stark Theme",
                    Artist = "Apollo",
                    Channel = "Apollo",
                    Mood = Mood.Calm,
                    Genre = Genre.Ambient,
                    URL = "https://www.youtube.com/watch?v=OYlzcXA3LxI",
                    Thumbnail = "https://i.ytimg.com/vi/OYlzcXA3LxI/hqdefault.jpg"
                },
                new Music
                {
                    Id = 118,
                    Title = "Relaxing Animal Crossing music + rain sounds ♡",
                    Artist = "miffynoa",
                    Channel = "miffynoa",
                    Mood = Mood.Relax,
                    Genre = Genre.Ambient,
                    URL = "https://www.youtube.com/watch?v=1wOAhRAqb40",
                    Thumbnail = "https://i.ytimg.com/vi/1wOAhRAqb40/hqdefault.jpg"
                },
                new Music
                {
                    Id = 119,
                    Title = "best anime openings but it's lofi remix extended edition (2 hours)",
                    Artist = "LlamaLoops",
                    Channel = "LlamaLoops",
                    Mood = Mood.Chill,
                    Genre = Genre.LoFi,
                    URL = "https://www.youtube.com/watch?v=GNWLILeztaI",
                    Thumbnail = "https://i.ytimg.com/vi/GNWLILeztaI/hqdefault.jpg"
                },
                new Music
                {
                    Id = 120,
                    Title = "Ghibli Coffee Shop ☕️ Music to put you in a better mood 🌿 lofi hip hop - lofi songs | study / relax",
                    Artist = "Lofi Coffee",
                    Channel = "Lofi Coffee",
                    Mood = Mood.Relax,
                    Genre = Genre.LoFi,
                    URL = "https://www.youtube.com/watch?v=zhDwjnYZiCo",
                    Thumbnail = "https://i.ytimg.com/vi/zhDwjnYZiCo/hqdefault.jpg"
                },
                new Music
                {
                    Id = 121,
                    Title = "The Last of Us - Relaxing Music Compilation",
                    Artist = "Eduardo Lima",
                    Channel = "Eduardo Lima",
                    Mood = Mood.Relax,
                    Genre = Genre.Acoustic,
                    URL = "https://www.youtube.com/watch?v=zm3IBGxHL3w",
                    Thumbnail = "https://i.ytimg.com/vi/zm3IBGxHL3w/hqdefault.jpg"
                },
                new Music
                {
                    Id = 122,
                    Title = "Productive Music For Work | Chill Playlist",
                    Artist = "BLUME",
                    Channel = "BLUME",
                    Mood = Mood.Focus,
                    Genre = Genre.Chill,
                    URL = "https://www.youtube.com/watch?v=ZVb_yKMivqo",
                    Thumbnail = "https://i.ytimg.com/vi/ZVb_yKMivqo/hqdefault.jpg"
                },
                new Music
                {
                    Id = 123,
                    Title = "best slowed down/chill music",
                    Artist = "Mecry",
                    Channel = "Mecry",
                    Mood = Mood.Chill,
                    Genre = Genre.Chill,
                    URL = "https://www.youtube.com/watch?v=bDaswBd-_ck",
                    Thumbnail = "https://i.ytimg.com/vi/bDaswBd-_ck/hqdefault.jpg"
                },
                new Music
                {
                    Id = 124,
                    Title = "Chill Work Music — Calm Focus Mix",
                    Artist = "Chill Music Lab",
                    Channel = "Chill Music Lab",
                    Mood = Mood.Focus,
                    Genre = Genre.Chill,
                    URL = "https://www.youtube.com/watch?v=BYl7v0YsX9g",
                    Thumbnail = "https://i.ytimg.com/vi/BYl7v0YsX9g/hqdefault.jpg"
                },
                new Music
                {
                    Id = 125,
                    Title = "ＳＴＵＤＹ ＴＩＭＥ ✍ Lofi Hip Hop | Study Music ✍ Lofi study, Relaxing Music",
                    Artist = "Mimi Lofi Chill",
                    Channel = "Mimi Lofi Chill",
                    Mood = Mood.Study,
                    Genre = Genre.LoFi,
                    URL = "https://www.youtube.com/watch?v=32dGIHCzbfE",
                    Thumbnail = "https://i.ytimg.com/vi/32dGIHCzbfE/hqdefault.jpg"
                },
            new Music
                {
                    Id = 127,
                    Title = "peaceful solitude",
                    Artist = "Eternal Warriors",
                    Channel = "Eternal Warriors",
                    Mood = Mood.Calm,
                    Genre = Genre.Ambient,
                    URL = "https://www.youtube.com/watch?v=F02iMCEEQWs",
                    Thumbnail = "https://i.ytimg.com/vi/F02iMCEEQWs/hqdefault.jpg"
                },
                new Music
                {
                    Id = 128,
                    Title = "Skyrim Ambience - Study & Relaxation Music - 3 hours",
                    Artist = "Aaronmn7",
                    Channel = "Aaronmn7",
                    Mood = Mood.Study,
                    Genre = Genre.Ambient,
                    URL = "https://www.youtube.com/watch?v=_Z1VzsE1GVg",
                    Thumbnail = "https://i.ytimg.com/vi/_Z1VzsE1GVg/hqdefault.jpg"
                },
                new Music
                {
                    Id = 129,
                    Title = "DnD Calm Fantasy Music for Adventure and Exploration | 3 Hour Mix for Dungeons & Dragons",
                    Artist = "Everrune",
                    Channel = "Everrune",
                    Mood = Mood.Calm,
                    Genre = Genre.Ambient,
                    URL = "https://www.youtube.com/watch?v=sHA_4wfQhE8",
                    Thumbnail = "https://i.ytimg.com/vi/sHA_4wfQhE8/hqdefault.jpg"
                },
                new Music
                {
                    Id = 130,
                    Title = "Ｎｉｇｈｔ Ｄｒｉｖｅ ~ lofi hip hop mix ~ beats to chill / drive to",
                    Artist = "Mimi Lofi Chill",
                    Channel = "Mimi Lofi Chill",
                    Mood = Mood.Chill,
                    Genre = Genre.LoFi,
                    URL = "https://www.youtube.com/watch?v=zW5wpJY1rgQ",
                    Thumbnail = "https://i.ytimg.com/vi/zW5wpJY1rgQ/hqdefault.jpg"
                },
                new Music
                {
                    Id = 131,
                    Title = "Fullmetal Alchemist Beautiful Music | Best Anime OST",
                    Artist = "LO-FI SENPAI",
                    Channel = "LO-FI SENPAI",
                    Mood = Mood.Relax,
                    Genre = Genre.Classical,
                    URL = "https://www.youtube.com/watch?v=CZPul4k9bUU",
                    Thumbnail = "https://i.ytimg.com/vi/CZPul4k9bUU/hqdefault.jpg"
                },
                new Music
                {
                    Id = 132,
                    Title = "Café Leblanc | Coffee Shop Ambience: Smooth Jazz Persona Music & Rain to Study, Relax, & Sleep",
                    Artist = "Ambience Academy",
                    Channel = "Ambience Academy",
                    Mood = Mood.Study,
                    Genre = Genre.Jazz,
                    URL = "https://www.youtube.com/watch?v=ZXni9_91ORs",
                    Thumbnail = "https://i.ytimg.com/vi/ZXni9_91ORs/hqdefault.jpg"
                },
                new Music
                {
                    Id = 133,
                    Title = "Hollow Knight • Relaxing Music with Ambiance (Rain, Fire, Night, Snow) 🎧 #tenpers",
                    Artist = "Tenpers Universe",
                    Channel = "Tenpers Universe",
                    Mood = Mood.Relax,
                    Genre = Genre.Ambient,
                    URL = "https://www.youtube.com/watch?v=mYEA5A0Bjyo",
                    Thumbnail = "https://i.ytimg.com/vi/mYEA5A0Bjyo/hqdefault.jpg"
                },
                new Music
                {
                    Id = 134,
                    Title = "Zelda & Chill 2",
                    Artist = "GameChops",
                    Channel = "GameChops",
                    Mood = Mood.Focus,
                    Genre = Genre.LoFi,
                    URL = "https://www.youtube.com/watch?v=rJlY1uKL87k",
                    Thumbnail = "https://i.ytimg.com/vi/rJlY1uKL87k/hqdefault.jpg"
                },
                new Music
                {
                    Id = 135,
                    Title = "Animal Crossing New Horizons Music To Study/Chill/Sleep",
                    Artist = "RemDaBom",
                    Channel = "RemDaBom",
                    Mood = Mood.Study,
                    Genre = Genre.Chill,
                    URL = "https://www.youtube.com/watch?v=V6GUhCxMDLg",
                    Thumbnail = "https://i.ytimg.com/vi/V6GUhCxMDLg/hqdefault.jpg"
                },
                new Music
                {
                    Id = 136,
                    Title = "lofi sleep, lo-fi rain 💤 8 hours mix 😴 beats to sleep/chill/relax to - music for insomnia & anxiety",
                    Artist = "Sleep Tales",
                    Channel = "Sleep Tales",
                    Mood = Mood.Sleep,
                    Genre = Genre.LoFi,
                    URL = "https://www.youtube.com/watch?v=gWp8xxB2PxM",
                    Thumbnail = "https://i.ytimg.com/vi/gWp8xxB2PxM/hqdefault.jpg"
                },
                new Music
                {
                    Id = 137,
                    Title = "2 hours of chill video game music 🍹",
                    Artist = "alf 🌙",
                    Channel = "alf 🌙",
                    Mood = Mood.Chill,
                    Genre = Genre.Chill,
                    URL = "https://www.youtube.com/watch?v=JJis0sld2cM",
                    Thumbnail = "https://i.ytimg.com/vi/JJis0sld2cM/hqdefault.jpg"
                },
                new Music
                {
                    Id = 138,
                    Title = "Music for when you are stressed🍀",
                    Artist = "Sunshine",
                    Channel = "Sunshine",
                    Mood = Mood.Calm,
                    Genre = Genre.Ambient,
                    URL = "https://www.youtube.com/watch?v=_BtXPQimVhg",
                    Thumbnail = "https://i.ytimg.com/vi/_BtXPQimVhg/hqdefault.jpg"
                },
                new Music
                {
                    Id = 139,
                    Title = "Mario Kart Music to Study /Work FAST | Tenpers",
                    Artist = "Moki",
                    Channel = "Moki",
                    Mood = Mood.Workout,
                    Genre = Genre.Electronic,
                    URL = "https://www.youtube.com/watch?v=ctL1r742ETI",
                    Thumbnail = "https://i.ytimg.com/vi/ctL1r742ETI/hqdefault.jpg"
                },
                new Music
                {
                    Id = 140,
                    Title = "Chill Vibes 🎧😌💨 Lofi hip hop ~ Healing Music, Relaxing, Study To",
                    Artist = "chilli music",
                    Channel = "chilli music",
                    Mood = Mood.Chill,
                    Genre = Genre.LoFi,
                    URL = "https://www.youtube.com/watch?v=IYLDF2-PvFg",
                    Thumbnail = "https://i.ytimg.com/vi/IYLDF2-PvFg/hqdefault.jpg"
                },
                new Music
                {
                    Id = 141,
                    Title = "1 Hour of Relaxing and Beautiful Zelda Music",
                    Artist = "Ralph L. Tanaka",
                    Channel = "Ralph L. Tanaka",
                    Mood = Mood.Relax,
                    Genre = Genre.Classical,
                    URL = "https://www.youtube.com/watch?v=wb_E3HnLwG4",
                    Thumbnail = "https://i.ytimg.com/vi/wb_E3HnLwG4/hqdefault.jpg"
                },
                new Music
                {
                    Id = 142,
                    Title = "1 A.M Chill Session 🌌 [synthwave]",
                    Artist = "Lofi Girl",
                    Channel = "Lofi Girl",
                    Mood = Mood.Chill,
                    Genre = Genre.Electronic,
                    URL = "https://www.youtube.com/watch?v=TlWYgGyNnJo",
                    Thumbnail = "https://i.ytimg.com/vi/TlWYgGyNnJo/hqdefault.jpg"
                },
                new Music
                {
                    Id = 143,
                    Title = "ＦＯＲＥＶＥＲ ＩＮ ２００６ サイレントヒル (4 Hour Silent Hill Ambient - Zerofuturism REUPLOAD )",
                    Artist = "Renna",
                    Channel = "Renna",
                    Mood = Mood.Sad,
                    Genre = Genre.Ambient,
                    URL = "https://www.youtube.com/watch?v=CNEW2udsaTc",
                    Thumbnail = "https://i.ytimg.com/vi/CNEW2udsaTc/hqdefault.jpg"
                },
                new Music
                {
                    Id = 144,
                    Title = "Meditate Like A WITCHER 🎵 10 HOURS Relaxing Music (SLEEP | STUDY | FOCUS)",
                    Artist = "z3n Pnk",
                    Channel = "z3n Pnk",
                    Mood = Mood.Focus,
                    Genre = Genre.Ambient,
                    URL = "https://www.youtube.com/watch?v=-MJi7T4lX80",
                    Thumbnail = "https://i.ytimg.com/vi/-MJi7T4lX80/hqdefault.jpg"
                },
                new Music
                {
                    Id = 145,
                    Title = "miss the good old days...",
                    Artist = "Ambient Crafts",
                    Channel = "Ambient Crafts",
                    Mood = Mood.Calm,
                    Genre = Genre.Ambient,
                    URL = "https://www.youtube.com/watch?v=_5jELltfi9U",
                    Thumbnail = "https://i.ytimg.com/vi/_5jELltfi9U/hqdefault.jpg"
                },
                new Music
                {
                    Id = 146,
                    Title = "Super Mario 🍄 Lofi HipHop |best calm and relaxing Mix | Super Mario Bros - Art: @pixeljeff_design",
                    Artist = "Lofi Culture",
                    Channel = "Lofi Culture",
                    Mood = Mood.Chill,
                    Genre = Genre.LoFi,
                    URL = "https://www.youtube.com/watch?v=FDUk0Kcte9A",
                    Thumbnail = "https://i.ytimg.com/vi/FDUk0Kcte9A/hqdefault.jpg"
                },
                new Music
                {
                    Id = 147,
                    Title = "Night lofi playlist • lofi music | chill beats to relax/study to",
                    Artist = "HITO",
                    Channel = "HITO",
                    Mood = Mood.Chill,
                    Genre = Genre.LoFi,
                    URL = "https://www.youtube.com/watch?v=cIZhlFIyJ_Y",
                    Thumbnail = "https://i.ytimg.com/vi/cIZhlFIyJ_Y/hqdefault.jpg"
                },
                new Music
                {
                    Id = 148,
                    Title = "Relaxing The Legend Of Zelda: Twilight Princess Music",
                    Artist = "Lou Says",
                    Channel = "Lou Says",
                    Mood = Mood.Relax,
                    Genre = Genre.Classical,
                    URL = "https://www.youtube.com/watch?v=3oypmjuiM0E",
                    Thumbnail = "https://i.ytimg.com/vi/3oypmjuiM0E/hqdefault.jpg"
                },
                new Music
                {
                    Id = 149,
                    Title = "some peace for hard nights... (minecraft music, soft rain & water)",
                    Artist = "Drift Away Ambience",
                    Channel = "Drift Away Ambience",
                    Mood = Mood.Sleep,
                    Genre = Genre.Ambient,
                    URL = "https://www.youtube.com/watch?v=OkNo_N85em0",
                    Thumbnail = "https://i.ytimg.com/vi/OkNo_N85em0/hqdefault.jpg"
                },
                new Music
                {
                    Id = 150,
                    Title = "One Piece Ambient: Music mix & ambience",
                    Artist = "Anime Ambient アニメアンビエント",
                    Channel = "Anime Ambient アニメアンビエント",
                    Mood = Mood.Chill,
                    Genre = Genre.Ambient,
                    URL = "https://www.youtube.com/watch?v=7_-ePcoRgHs",
                    Thumbnail = "https://i.ytimg.com/vi/7_-ePcoRgHs/hqdefault.jpg"
                }
        };
    }
}

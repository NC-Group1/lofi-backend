using System.Text;
using System.Text.Json;
using lofi_backend.Data_Models;
using lofi_backend.Data_Models.Enums;
using lofi_backend.Database;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
                return mockVideos;
            }
            else
            {
                Console.WriteLine("Returning filtered search");
                return mockVideos.Where(x => x.Title.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
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
            },
            new Music
            {
                Id = 32,
                Title = "Mind Relax Lofi Song | Mind Relax Lofi Mashup | Mind Fresh Lofi Songs | Slowed and Reverb",
                Artist = "RP Crazy Creator",
                Channel = "RP CRAZY CREATOR ",
                Mood = Mood.Relax,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=JqnBMOywBBw",
                Thumbnail = "https://i.ytimg.com/vi/JqnBMOywBBw/hqdefault.jpg"
            },
            new Music
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
                Id = 53,
                Title = "SAD LOFI SONGS | HEART BROKEN LOFI MASHUP | SLOW + REVERB LOFI MIX",
                Artist = "Loferian Rahul",
                Channel = "ʟᴏꜰᴇʀɪᴀɴ ʀᴀʜᴜʟꜱ\n",
                Mood = Mood.Sad,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=v-Pizmo0lcw",
                Thumbnail = "https://i.ytimg.com/vi/v-Pizmo0lcw/hqdefault.jpg"
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
                Id = 60,
                Title = "Mind Relax Lofi Mashup | Mind Relaxing Songs | Mind Relax Lofi Song | Slowed And Reverb",
                Artist = "Nitin Tomu Payal",
                Channel = "ɴɪᴛɪɴ_ᴛᴏᴍᴜ_ᴘᴀʏᴀʟ",
                Mood = Mood.Calm,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=zoFLbJ_09aM",
                Thumbnail = "https://i.ytimg.com/vi/zoFLbJ_09aM/hqdefault.jpg"
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
                Id = 65,
                Title = "1 Hour of Night Hindi Lofi Songs To Chill Relax Refreshing",
                Artist = "Vicky Bhai",
                Channel = "viral vicky vlogs ",
                Mood = Mood.Chill,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=t8yVk0bm684",
                Thumbnail = "https://i.ytimg.com/vi/t8yVk0bm684/hqdefault.jpg"
            },
            new Music
            {
                Id = 66,
                Title = "Best of Bollywood Hindi lofi / chill mix playlist | 1 hour non-stop to relax, drive, study, sleep 💙🎵",
                Artist = "aMeth Music",
                Channel = "aMeth Music",
                Mood = Mood.Relax,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=KRA26LhuTP4",
                Thumbnail = "https://i.ytimg.com/vi/KRA26LhuTP4/hqdefault.jpg"
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
                Id = 68,
                Title = "Lofi mashup || non stop + love songs || use headphones And feel Songs",
                Artist = "Music Club",
                Channel = "music club",
                Mood = Mood.Romantic,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=vOxZJ0wKaGc",
                Thumbnail = "https://i.ytimg.com/vi/vOxZJ0wKaGc/hqdefault.jpg"
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
                Id = 71,
                Title = "love - mashup (slowed+ reverb) Lo-fi songs 🎧🎧",
                Artist = "Shubankar",
                Channel = "shubankar 007",
                Mood = Mood.Romantic,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=HM_OGtwR2jM",
                Thumbnail = "https://i.ytimg.com/vi/HM_OGtwR2jM/hqdefault.jpg"
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
                Id = 74,
                Title = "NON STOP INSTAGRAM TRENDING LOVE MASHUP - Part 17 | PLAYLIST BY @lofi2307",
                Artist = "Lo-fi 2307",
                Channel = "Lo-fi 2307",
                Mood = Mood.Romantic,
                Genre = Genre.Pop,
                URL = "https://www.youtube.com/watch?v=Ez54QnP0Ais",
                Thumbnail = "https://i.ytimg.com/vi/Ez54QnP0Ais/hqdefault.jpg"
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
                Id = 81,
                Title = "Krishna Lofi Songs 4.0 | Slow & Reverb | The Sound Of Inner Peace | Relaxing Lofi Song",
                Artist = "Krishna for Life",
                Channel = "Krishna for Life",
                Mood = Mood.Calm,
                Genre = Genre.World,
                URL = "https://www.youtube.com/watch?v=wBbdNd682A4",
                Thumbnail = "https://i.ytimg.com/vi/wBbdNd682A4/hqdefault.jpg"
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
            },
            new Music
            {
                Id = 88,
                Title = "1 Hour Of Night Hindi Lofi Songs To Study \\Chill \\Relax \\Refreshing",
                Artist = "Indian Musical Videos",
                Channel = "indianmusicalvideos",
                Mood = Mood.Chill,
                Genre = Genre.LoFi,
                URL = "https://www.youtube.com/watch?v=qG8hzNAxrhY",
                Thumbnail = "https://i.ytimg.com/vi/qG8hzNAxrhY/hqdefault.jpg"
            },
            new Music
            {
                Id = 89,
                Title = "K.K X Emraan Hashmi Mashup (Non-Stop Jukebox) Part - 2 | Lo-fi 2307",
                Artist = "KK & Emraan Hashmi",
                Channel = "Lo-fi 2307",
                Mood = Mood.Chill,
                Genre = Genre.Pop,
                URL = "https://www.youtube.com/watch?v=EWZqulvXnZQ",
                Thumbnail = "https://i.ytimg.com/vi/EWZqulvXnZQ/hqdefault.jpg"
            },
            new Music
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
            }
        };
    }
}

using lofi_backend.Data_Models;
using lofi_backend.Repository;

namespace lofi_backend.Service
{

    public interface IYoutubeService
    {
        Task<List<Music>> SearchYoutubeAsync(string search);
    }
    public class YoutubeService : IYoutubeService
    {
        private readonly IYoutubeRepository _youtubeRepository;
        public YoutubeService(IYoutubeRepository youtubeRepository)
        {
            _youtubeRepository = youtubeRepository;
        }
        public async Task<List<Music>> SearchYoutubeAsync(string search)
        {
            if(string.IsNullOrEmpty(search))
            {
                throw new ArgumentException("Search query cannot be null or empty.");
            }


            return await _youtubeRepository.SearchYoutubeAsync(search);
        }
    }
}

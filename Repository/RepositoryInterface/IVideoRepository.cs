using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface
{
    public interface IVideoRepository
    {
        IEnumerable<Video> GetAllVideos();
        Video GetVideoById(int videoId);
        void AddVideo(Video video);
        void UpdateVideo(Video video);
        void DeleteVideo(int videoId);
    }
}

using BusinessObject;
using DataAccessLayer;
using Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class VideoRepository : IVideoRepository
    {
        public void AddVideo(Video video) => VideoDAO.AddVideo(video);

        public void DeleteVideo(int videoId) => VideoDAO.DeleteVideo(videoId);

        public Video GetVideoById(int videoId) => VideoDAO.GetVideoById(videoId);

        public IEnumerable<Video> GetAllVideos() => VideoDAO.GetAllVideos();

        public void UpdateVideo(Video video) => VideoDAO.UpdateVideo(video);
    }
}

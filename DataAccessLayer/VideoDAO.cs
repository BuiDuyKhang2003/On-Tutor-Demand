using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class VideoDAO
    {
        private static AppDbContext db = new();

        public static List<Video> GetAllVideos()
        {
            return db.Videos.ToList();
        }

        public static Video GetVideoById(int videoId)
        {
            return db.Videos.Find(videoId) ?? new Video();
        }

        public static void AddVideo(Video video)
        {
            db.Videos.Add(video);
            db.SaveChanges();
        }

        public static void UpdateVideo(Video video)
        {
            db.Entry(video).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void DeleteVideo(int videoId)
        {
            var video = db.Videos.Find(videoId);
            if (video != null)
            {
                db.Videos.Remove(video);
                db.SaveChanges();
            }
        }
    }

}

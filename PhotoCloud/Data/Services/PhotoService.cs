using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using PhotoCloud.Data;
using PhotoCloud.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCloud.Services
{
    public class PhotoService : IPhotoService
    {

        private ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private string _photoUploadsFolder;

        public PhotoService(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            _photoUploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
        }

        public async Task AddPhoto(string userId, byte[] photo)
        {
            var filename = Guid.NewGuid() + ".jpg";
            var filePath = Path.Combine(_photoUploadsFolder, filename);
            await File.WriteAllBytesAsync(filePath, photo);

            await SaveImageToDatabase(userId, filename);

        }

        private async Task SaveImageToDatabase(string userId, string photoName)
        {
            var user = _context.Users.Where(x => x.Id == userId).Include(x => x.Photos).FirstOrDefault();

            var photo = new PhotoModel
            {
                Id = photoName,
                UploadDate = DateTime.Now
            };

            user.Photos.Add(photo);

            await _context.SaveChangesAsync();
        }


        public async Task<ICollection<PhotoModel>> GetUsersPhotos(string userId)
        {
            var user = await _context.Users.Where(x => x.Id == userId).Include(x => x.Photos).FirstOrDefaultAsync();

            var photos = user.Photos
                .OrderByDescending(x => x.UploadDate)
                .ToList();

            return photos;
        }

        public async Task<ICollection<AlbumModel>> GetUserAlbums(string userId)
        {
            var user = _context.Users.Include(x => x.Albums).ThenInclude(x => x.Photos).FirstOrDefault(x => x.Id == userId);


            var albums = user.Albums.ToList();

            return albums;
        }

        public async Task CreateNewAlbum(string userId, string albumName, List<string> photoIds)
        {
            var newAlbum = new AlbumModel
            {
                Id = Guid.NewGuid().ToString(),
                AlbumName = albumName

            };

            if (photoIds.Count != 0)
            {
                var photos = new List<PhotoModel>();

                foreach (var photoId in photoIds)
                {
                    var photo = _context.Photos.FirstOrDefault(x => x.Id == photoId);
                    photos.Add(photo);
                }

                newAlbum.Photos = photos;
            }

            var user = _context.Users.FirstOrDefault(x => x.Id == userId);

            user.Albums.Add(newAlbum);

            _context.SaveChanges();

        }


        public async Task<string> GetFirstPhotoFromAlbum(string albumId)
        {
            var album = _context.Albums.FirstOrDefault(x => x.Id == albumId);
            var path = album.Photos.FirstOrDefault();
            if (path != null)
                return path.Id;

            return null;

        }


        public async Task<ICollection<PhotoModel>> GetAlbumPhotos(string albumId)
        {
            var album = _context.Albums.FirstOrDefault(x => x.Id == albumId);

            var photos = album.Photos.ToList();

            return photos;
        }

        public async Task<string> GetAlbumName(string albumId)
        {
            var album = _context.Albums.FirstOrDefault(x => x.Id == albumId);
            return album.AlbumName;
        }


        public async Task AddPhotosToAlbum(string albumId, List<string> photoIds)
        {
            var album = _context.Albums.FirstOrDefault(x => x.Id == albumId);
            var photos = _context.Photos.Where(x => photoIds.Contains(x.Id)).ToList();

            foreach (var photo in photos)
            {
                album.Photos.Add(photo);

            }

            _context.SaveChanges();
        }

    }
}

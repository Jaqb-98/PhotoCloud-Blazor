using ImageMagick;
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
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
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

        #region Photos
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

        public async Task AddPhoto(string userId, byte[] photo)
        {
            var filename = Guid.NewGuid() + ".jpg";
            var filePath = Path.Combine(_photoUploadsFolder, filename);
            await File.WriteAllBytesAsync(filePath, photo);

            await SaveImageToDatabase(userId, filename);

        }

        public async Task DeletePhotos(List<string> photoIds)
        {
            var photos = _context.Photos.Where(x => photoIds.Contains(x.Id)).ToList();

            foreach (var photo in photos)
            {
                _context.Remove(photo);
            }

            await _context.SaveChangesAsync();
        }


        public async Task<int> NumberOfPages(string userId, int photosPerPage = 20)
        {
            var user = await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            var photoCount = user.Photos.Count();
            var pagesCount = (photoCount + photosPerPage - 1) / photosPerPage;
            return pagesCount;
        }

        public async Task<ICollection<PhotoModel>> GetUsersPhotos(string userId, int page = 0, int photosPerPage = 20)
        {
            var user = await _context.Users.Where(x => x.Id == userId).Include(x => x.Photos).FirstOrDefaultAsync();
            var user1 = await _context.Users.SingleAsync(x => x.Id == userId);

            //var photos = user.Photos
            //    .OrderByDescending(x => x.UploadDate)
            //    .Skip(page * photosPerPage)
            //    .Take(photosPerPage)
            //    .ToList();

            var photos =  user.Photos.ToList();
            //.OrderByDescending(x => x.UploadDate)


            return photos;
        }

        #endregion

        #region Albums

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
                if (!album.Photos.Contains(photo))
                    album.Photos.Add(photo);

            }

            _context.SaveChanges();
        }

        public async Task DeleteAlbum(string albumId)
        {
            var album = _context.Albums.FirstOrDefault(x => x.Id == albumId);
            _context.Remove(album);
            _context.SaveChanges();
        }

        public async Task DeletePhotosFromAlbum(string albumId, List<string> photoIds)
        {
            var album = _context.Albums.Include(x => x.Photos).FirstOrDefault(x => x.Id == albumId);
            var albumPhotos = album.Photos;

            foreach (var id in photoIds)
            {
                var toDelete = albumPhotos.FirstOrDefault(x => x.Id == id);
                album.Photos.Remove(toDelete);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<string>> GetAlbumCover(string albumId)
        {
            var album = _context.Albums.FirstOrDefault(x => x.Id == albumId);
            var photos = album.Photos.Take(4).Select(x => x.Id).ToList();
            return photos;

        }


        public async Task<string> MakeAlbumCover(string albumId)
        {
            var ids = await GetAlbumCover(albumId);
            if (ids.Count > 0)
            {

                using (var images = new MagickImageCollection())
                {
                    var size = new MagickGeometry(200, 200);
                    size.IgnoreAspectRatio = true;

                    for (int i = 0; i < ids.Count; i++)

                    {
                        var filePath = Path.Combine(_photoUploadsFolder, ids[i]);
                        FileInfo fileInfo = new FileInfo(filePath);
                        byte[] data = new byte[fileInfo.Length];

                        using (FileStream fs = fileInfo.OpenRead())
                        {
                            fs.Read(data, 0, data.Length);
                        }


                        var img = new MagickImage(data);

                        var posX = (img.Page.Width) * (i % 2);
                        var posY = (img.Page.Height) * (i / 2);
                        img.Page = new MagickGeometry(posX, posY, new Percentage(100), new Percentage(100));
                        images.Add(img);
                    }

                    foreach (var image in images)
                    {
                        image.Resize(size);
                    }

                    using (var result = images.Mosaic())
                    {
                        var data = result.ToByteArray();
                        var base64 = result.ToBase64();

                        return base64;

                    }
                }
            }
            else
                return null;

        }

        #endregion





























    }
}

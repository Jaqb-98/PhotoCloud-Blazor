using PhotoCloud.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoCloud.Services
{
    public interface IPhotoService
    {
        Task AddPhoto(string userId, byte[] photo);
        Task<ICollection<PhotoModel>> GetUsersPhotos(string userId, int page = 0);

        Task<ICollection<AlbumModel>> GetUserAlbums(string userId);

        Task CreateNewAlbum(string userId, string albumName, List<string> photoIds);
        Task<string> GetFirstPhotoFromAlbum(string albumId);
        Task<AlbumModel> GetAlbumPhotos(string albumId);
    }
}
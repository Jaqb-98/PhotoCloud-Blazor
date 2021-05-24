using PhotoCloud.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoCloud.Services
{
    public interface IPhotoService
    {
        Task AddPhoto(string userId, byte[] photo);
        Task<ICollection<PhotoModel>> GetUsersPhotos(string userId);

        Task<ICollection<AlbumModel>> GetUserAlbums(string userId);

        Task CreateNewAlbum(string userId, string albumName, List<string> photoIds);
        Task<string> GetFirstPhotoFromAlbum(string albumId);
        Task<ICollection<PhotoModel>> GetAlbumPhotos(string albumId);
        Task<string> GetAlbumName(string albumId);

        Task AddPhotosToAlbum(string albumId, List<string> photoIds);
    }
}
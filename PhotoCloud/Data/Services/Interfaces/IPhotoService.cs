using PhotoCloud.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PhotoCloud.Services
{
    public interface IPhotoService
    {
        /// <summary>
        /// Uploads photo to server
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        Task<bool> AddPhoto(string userId, byte[] photo);

        /// <summary>
        /// Returns one page of users photos
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page"></param>
        /// <param name="photosPerPage"></param>
        /// <returns></returns>
        Task<ICollection<PhotoModel>> GetUsersPhotos(string userId, int page = 0, int photosPerPage = 20);


        /// <summary>
        /// Returns all albums of user with given id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ICollection<AlbumModel>> GetUserAlbums(string userId);


        /// <summary>
        /// Creates new album assigned to user with given id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="albumName"></param>
        /// <param name="photoIds"></param>
        /// <returns></returns>
        Task CreateNewAlbum(string userId, string albumName, List<string> photoIds);

        /// <summary>
        /// Returns all photos from album with given id
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        Task<ICollection<PhotoModel>> GetAlbumPhotos(string albumId);

        /// <summary>
        /// Returns name of an album with given id
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        Task<string> GetAlbumName(string albumId);


        /// <summary>
        /// Adds photos to album with given id
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="photoIds"></param>
        /// <returns></returns>
        Task AddPhotosToAlbum(string albumId, List<string> photoIds);


        /// <summary>
        /// Deletes album with given id
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        Task DeleteAlbum(string albumId);


        /// <summary>
        /// Deletes photos with given ids
        /// </summary>
        /// <param name="photoIds"></param>
        /// <returns></returns>
        Task DeletePhotos(List<string> photoIds);


        /// <summary>
        /// Deletes photos from album with given id
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="photoIds"></param>
        /// <returns></returns>
        Task DeletePhotosFromAlbum(string albumId, List<string> photoIds);


        /// <summary>
        /// Returns 4 photos form album cover
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        Task<List<string>> GetAlbumCover(string albumId);


        /// <summary>
        /// Returns album cover in base64 format
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        Task<string> MakeAlbumCover(string albumId);


        /// <summary>
        /// Returns number of pages needed to display all photos assigned to user with given id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="photosPerPage"></param>
        /// <returns></returns>
        Task<int> NumberOfPages(string userId, int photosPerPage = 20);



        string DownloadAlbum(string albumId);
    }
}
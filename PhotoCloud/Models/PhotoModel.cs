using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCloud.Models
{
    public class PhotoModel
    {
        public string Id { get; set; }
        public DateTime UploadDate { get; set; }

        public virtual ICollection<AlbumModel> Albums { get; set; }

        public PhotoModel()
        {
            Albums = new HashSet<AlbumModel>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCloud.Models
{
    public class AlbumModel
    {
        public string Id { get; set; }
        public string AlbumName { get; set; }
        public virtual ICollection<PhotoModel> Photos { get; set; }

        public AlbumModel()
        {
            Photos = new HashSet<PhotoModel>();
        }
    }
}

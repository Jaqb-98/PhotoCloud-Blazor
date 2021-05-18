using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCloud.Models
{
    public class ApplicationUser: IdentityUser
    {
        public virtual ICollection<PhotoModel> Photos { get; set; }
        public virtual ICollection<AlbumModel> Albums { get; set; }

        public ApplicationUser()
        {
            Photos = new HashSet<PhotoModel>();
            Albums = new HashSet<AlbumModel>();
        }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoCloud.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoCloud.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PhotoModel> Photos { get; set; }
        public DbSet<AlbumModel> Albums { get; set; }

    }
}

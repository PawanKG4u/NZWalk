using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NZWeb2.Api.Models.Domain;

namespace NZWeb2.Api.Data
{
    public class NZWalksDBContext:DbContext
    {
        public NZWalksDBContext(DbContextOptions<NZWalksDBContext> Options) : base(Options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.Role)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.UserId);


            
        }


        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficultys { get; set; }


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> User_Roles { get; set; }


    }
}

using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PassionProject_YejunSon.Models
{
    // ApplicationUser 클래스에 더 많은 속성을 추가하여 사용자의 프로필 데이터를 추가할 수 있습니다. 자세한 내용은 https://go.microsoft.com/fwlink/?LinkID=317594를 참조하세요.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // authenticationType은 CookieAuthenticationOptions.AuthenticationType에 정의된 항목과 일치해야 합니다.
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 여기에 사용자 지정 사용자 클레임 추가
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        
        public DbSet<User> Users { get; set; }

        public DbSet<RestaurantsFolder> RestaurantsFolders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This line needs to be here to allow Identity models to be built

            modelBuilder.Entity<Restaurant>()
                .HasRequired(r => r.Users)
                .WithMany(u => u.Restaurants)
                .HasForeignKey(r => r.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RestaurantsFolder>()
                .HasRequired(f => f.Users)
                .WithMany(u => u.RestaurantsFolders)
                .HasForeignKey(f => f.UserId)
                .WillCascadeOnDelete(false);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
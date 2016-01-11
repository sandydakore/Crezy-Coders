using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SDStudentPortal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and roles. Also initializes sample data.
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<SDStudentPortal.Models.UserModel> UserModels { get; set; }

        public System.Data.Entity.DbSet<SDStudentPortal.Models.Uploads> Uploads { get; set; }

        public System.Data.Entity.DbSet<SDStudentPortal.Models.Blog> blog { get; set; }
        public System.Data.Entity.DbSet<SDStudentPortal.Models.BlogComment> blogcomment { get; set; }

        public System.Data.Entity.DbSet<SDStudentPortal.Models.Project> project { get; set; }
       
      //  public System.Data.Entity.DbSet<SDStudentPortal.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}
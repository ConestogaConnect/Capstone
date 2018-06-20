using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ConestogaConnect.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Program { get; set; }
        public int Semester { get;  set; }

        public int StudentId { get;  set; }
        public bool ConfirmedEmail { get; set; }

        


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

        //public static object AspNetUserRole { get; internal set; }

        // public static object AspNetUserRoles { get; internal set; }

        //public object AspNetUserRoles { get; internal set; }

        //public object AspNetUserRoles { get; internal set; }
        //public DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        //public DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }

        // public object UserRoleModel { get; internal set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
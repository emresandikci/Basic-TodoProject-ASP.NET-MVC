using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections;
using TodoListProject.Domain;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TodoListProject.Models
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

        public ApplicationUser()
        {
            //Todos = new HashSet<Todo>(Todos);
        }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string imageUrl { get; set; }

        //Mapping
        public virtual ICollection<Todo> Todos { get; set; }
        public virtual ICollection<Domain.Task> Tasks { get; set; }
        public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<SharingTodoWithFriend> SharingTodoWithFriends { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("TodoCon", throwIfV1Schema: false)
        {
        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Domain.Task> Tasks { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<SharingTodoWithFriend> SharingTodoWithFriends { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Settings
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");
            modelBuilder.Entity<IdentityRole>().ToTable("ApplicationUserRole");
            modelBuilder.Entity<IdentityUserRole>().ToTable("ApplicationUserInRole");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("ApplicationUserLogin");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("ApplicationUserClaim");

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
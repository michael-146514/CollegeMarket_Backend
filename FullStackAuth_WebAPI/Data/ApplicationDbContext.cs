using FullStackAuth_WebAPI.Configuration;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FullStackAuth_WebAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public DbSet<Product> Products { get; set; }

        public DbSet<Messages> Messages { get; set; }

        public DbSet<Conversation> Conversations { get; set; }

        public DbSet<UserConversation> UserConversations { get; set; }

        public DbSet<Image> Image { get; set; }




        public ApplicationDbContext(DbContextOptions options)
    : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RolesConfiguration());

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ImageUrls)
                .WithOne()
                .HasForeignKey(i => i.Id);
        }

       
    }
}

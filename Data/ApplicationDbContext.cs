using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Salto.Models;
using System.IO.Compression;

namespace Salto.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Prices> Prices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Detta ska vara det första anropet

            modelBuilder.Entity<Article>().ToTable("Article");
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<Prices>().ToTable("Prices");
            //modelBuilder.Entity<LearnArticle>().ToTable("LearnArticle");
            //modelBuilder.Entity<LearnTag>().ToTable("LearnTag");
        }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<LearnArticle> LearnArticle { get; set; }
        public DbSet<LearnTag> LearnTag { get; set; }
 

    }
}
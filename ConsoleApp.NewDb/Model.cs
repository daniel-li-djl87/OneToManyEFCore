using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ConsoleApp.NewDb
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Author> Authors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.ConsoleApp.NewDb;Trusted_Connection=True;");
        }

    }

    //Parent to Post
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }

    //Child to Blog
    //Parent to Author
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public Author Author { get; set; }
    }

    //Child to Post
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
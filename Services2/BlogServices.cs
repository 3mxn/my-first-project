using System.Reflection.Metadata;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace Services
{

    public class BlogService
    {

        public async Task<List<Blog>> GetAll()
        {
            using var db = new BloggingContext();
            return await db.Blogs.ToListAsync();
        }
        public async Task AddBlog(BlogDto blogDto)
        {
            using var db = new BloggingContext();


            Console.WriteLine($"Database Path: {db.DbPath}");
            Console.WriteLine("Inserting a new blog");

            db.Add(new Blog { Url = blogDto.Url });
            await db.SaveChangesAsync();
            Console.WriteLine("Quering for a blog");
            var blog = await db.Blogs.OrderBy(b => b.BlogId).FirstAsync();
            Console.WriteLine("update");
            // blog.Url = blogDto.Url;
            Console.WriteLine($"Posts count: {blogDto.Posts.Count}");
            foreach (var item in blogDto.Posts)
            {
                blog.Posts.Add(new Post { Title = item.Title, Content = item.Content });

            }
            db.SaveChanges();

        }
    }

}

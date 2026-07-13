using System.Reflection.Metadata;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace Services
{

    public class BlogService
    {
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
            foreach (var item in blogDto.Posts)
            {
                blog.Posts.Add(new Post { Title = item.Title, Content = item.Content });

            }
            db.SaveChanges();

        }
    }

}

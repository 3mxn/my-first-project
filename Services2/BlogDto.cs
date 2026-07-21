using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class BlogDto
    {
        public int BlogId { get; set; }
        public string? Url { get; set; }
        public List<PostDto> Posts { get; set; }
    }
    public class PostDto
    {
        public int? PostId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }

        public int BlogId { get; set; }
        public BlogDto? Blog { get; set; }
    }
}
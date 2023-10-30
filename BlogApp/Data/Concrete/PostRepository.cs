﻿using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entities;

namespace BlogApp.Data.Concrete
{
    public class PostRepository : IPostRepository
    {
        private BlogContext _context;

        public PostRepository(BlogContext context)
        {
            _context = context;
        }

        public IQueryable<Post> Posts => _context.Posts;

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
    }
}
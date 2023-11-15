﻿using BlogApp.Entities;

namespace BlogApp.Data.Abstract
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }
        void CreatePost ( Post post );
        void EditPost ( Post post );
        void DeletePost ( Post post );
    }
}

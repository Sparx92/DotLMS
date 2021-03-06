﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DotLms.Data.Models;

namespace DotLms.Data.Contracts
{
    public interface IDotLmsEfDbContext
    {
        IDbSet<Page> Pages { get; set; }

        IDbSet<MediaItem> MediaItems { get; set; }

        IDbSet<CourseCategory> CourseCategories { get; set; }

        IDbSet<Course> Courses { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        int SaveChanges();
    }
}
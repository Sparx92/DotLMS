using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper.QueryableExtensions;
using DotLms.Data.Contracts;
using DotLms.Data.Models;

namespace DotLms.Data.Repositories
{
    public class CourseEfRepository : EntityFrameworkRepository<Course>
    {
        public CourseEfRepository(IDotLmsEfDbContext context) : base(context)
        {
        }

        public Course GetCourse(string name)
        {
            return base.Context.Courses.Where(x => x.UglyName == name)
                .Include(x => x.Category)
                .Include(x => x.MainImage)
                .FirstOrDefault();
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return base.Context.Courses
                .Include(x => x.Category)
                .Include(x => x.MainImage)
                .ToList();
        }
    }
}
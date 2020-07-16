using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

using Zemoga.Domain.Models;

namespace Zemoga.Data.Context
{
    public class BlogDbContext: DbContext
    {
        public BlogDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}

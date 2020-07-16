using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

using Zemoga.App.Interfaces;
using Zemoga.App.Services;
using Zemoga.Domain.Interfaces;
using Zemoga.Data.Repositories;

namespace Zemoga.IoC
{
   public  class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostRepository, PostRepository>();

            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddScoped<IApiPostService, ApiPostService>();
        }
    }
}

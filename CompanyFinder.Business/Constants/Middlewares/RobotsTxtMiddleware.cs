﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Constants.Middlewares
{
    public class RobotsTxtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public RobotsTxtMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/robots.txt"))
            {
                var robotsTxtPath = Path.Combine(_env.ContentRootPath, "robots.txt");
                string output = "User-agent: * \nAllow: /";

                if (File.Exists(robotsTxtPath))
                {
                    output = await File.ReadAllTextAsync(robotsTxtPath);
                }

                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(output);
                return;
            }
            await _next(context);
        }
    }
}

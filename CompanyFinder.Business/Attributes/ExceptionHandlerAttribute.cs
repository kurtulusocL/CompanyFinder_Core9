﻿using CompanyFinder.Core.Helpers;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyFinder.Business.Attributes
{
    public  class ExceptionHandlerAttribute:Attribute,IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            using (var scope = ServiceProviderHelper.ServiceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (!filterContext.ExceptionHandled)
                {
                    ExceptionLogger logger = new ExceptionLogger()
                    {
                        ExceptionMessage = filterContext.Exception.Message,
                        ExceptionStackTrace = filterContext.Exception.StackTrace,
                        ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                        CreatedDate = DateTime.Now.ToLocalTime()
                    };
                    dbContext.ExceptionLoggers.Add(logger);
                    dbContext.SaveChanges();
                    filterContext.ExceptionHandled = true;
                }
            }
        }
    }
}

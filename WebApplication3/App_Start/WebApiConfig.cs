﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApplication3.Models;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Microsoft.Owin.Security.OAuth;

namespace WebApplication3
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services   
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            // Web API routes
            config.MapHttpAttributeRoutes();

            ODataModelBuilder builder = new ODataConventionModelBuilder();
            config.Filter().Expand().Select().OrderBy().MaxTop(null).Count();
            builder.EntitySet<companies>("CompInf");
            config.MapODataServiceRoute(
                routeName: "odata",
                routePrefix: "odata",
                model: builder.GetEdmModel());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}

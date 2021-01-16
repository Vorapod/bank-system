using AutoMapper;
using BankSystem.BLL;
using BankSystem.BLL.Interface;
using BankSystem.BLL.Model;
using BankSystem.DAL;
using BankSystem.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Lifetime;

namespace BankSystem.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

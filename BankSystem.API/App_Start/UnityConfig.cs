using AutoMapper;
using BankSystem.BLL;
using BankSystem.BLL.Interface;
using BankSystem.BLL.Model;
using BankSystem.DAL;
using BankSystem.DAL.Interface;
using System.Data.Entity;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace BankSystem.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.CreateMap<AccountModel, Account>().ReverseMap();
                c.CreateMap<TransactionModel, Transaction>().ReverseMap();
            });
            IMapper mapper = mapperConfig.CreateMapper();

            var container = new UnityContainer();
            container.RegisterType<DbContext, BankSystemDbContext>(new HierarchicalLifetimeManager())
                     .RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager())
                     .RegisterType<IBankSystemBLL, BankSystemBLL>(new HierarchicalLifetimeManager())
                     .RegisterInstance(mapper);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
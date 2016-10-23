using System.Data.Entity;
using Microsoft.Practices.Unity;
using System.Web.Http;
using API.Controllers;
using API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Model;
using Model.Infrastructure;
using Model.Repository;
using Service;
using Unity.WebApi;

namespace API
{
  public static class UnityConfig
  {
    public static void RegisterComponents()
    {
      var container = new UnityContainer();
      container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
      container.RegisterType<UserManager<ApplicationUser>>();

      container.RegisterType<DbContext, ApplicationDbContext>();
      container.RegisterType<ApplicationUserManager>();
      container.RegisterType<AccountController>(new InjectionConstructor());
      container.RegisterType<RoleController>(new InjectionConstructor());

      container.RegisterType<IAspNetRoleService, AspNetRoleService>();
      container.RegisterType<IAspNetUserService, AspNetUserService>();
      container.RegisterType<IAspNetRoleRepository, AspNetRoleRepository>();
      container.RegisterType<IAspNetUserRepository, AspNetUserRepository>();
      container.RegisterType<IUnitOfWork, UnitOfWork>();
      container.RegisterType<IDatabaseFactory, DatabaseFactory>(new HierarchicalLifetimeManager());

      GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
    }
  }
}
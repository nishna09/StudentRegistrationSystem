using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using SystemLibrary.Repository;
using SystemLibrary.Repository.Database;
using SystemLibrary.Services;

namespace StudentRegistrationSystem
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IUserServices, UserServices>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IDatabaseCommand, DatabaseCommand>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
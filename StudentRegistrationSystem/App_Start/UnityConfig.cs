using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using SystemLibrary.Repository;
using SystemLibrary.Services;
using SystemLibrary.Helper;

namespace StudentRegistrationSystem
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IUserServices, UserServices>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IDatabaseConnect, DatabaseConnect>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
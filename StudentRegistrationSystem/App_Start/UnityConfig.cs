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
            container.RegisterType<IStudentServices, StudentServices>();
            container.RegisterType<IResultServices, ResultServices>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IStudentRepository, StudentRepository>();
            container.RegisterType<IResultRepository, ResultRepository>();
            container.RegisterType<IDatabaseCommand, DatabaseCommand>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
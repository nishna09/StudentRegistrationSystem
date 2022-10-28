using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using RepositoryLibrary.Repository;
using RepositoryLibrary.Repository.Database;
using ServicesLibrary.Services;

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
            container.RegisterType<IValidation, Validation>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
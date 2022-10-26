using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using SystemLibrary.DAL;
using SystemLibrary.DAL.Database;
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
            container.RegisterType<IUserDAL, UserRepository>();
            container.RegisterType<IStudentDAL, StudentRepository>();
            container.RegisterType<IResultDAL, ResultDAL>();
            container.RegisterType<IDatabaseCommand, DatabaseCommand>();
            container.RegisterType<IValidation, Validation>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
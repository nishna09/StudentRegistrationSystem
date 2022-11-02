using RepositoryLibrary.Entities;

namespace ServicesLibrary.Services
{
    public interface IUserServices
    {
        Response Authenticate(string emailAddress, string password);
    }

    
}

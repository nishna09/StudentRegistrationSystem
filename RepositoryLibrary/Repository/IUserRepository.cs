using RepositoryLibrary.Entities;
using RepositoryLibrary.Repository.Database;
namespace RepositoryLibrary.Repository
{
    public interface IUserRepository
    {
        int AddUser(User user, IDatabaseCommand db);
        User GetUser(string queryParameter, object queryValue);
    }

    
}

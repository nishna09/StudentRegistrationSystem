using RepositoryLibrary.Entities;
using RepositoryLibrary.Repository.Database;
using System.Collections.Generic;

namespace RepositoryLibrary.Repository
{
    public interface IRoleRepository
    {
        int AddRole(Role role, int UserId, IDatabaseCommand db);
        List<Role> GetRoles(int userId);
    }
}

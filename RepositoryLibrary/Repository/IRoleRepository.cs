using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLibrary.Entities;
using RepositoryLibrary.Repository.Database;

namespace RepositoryLibrary.Repository
{
    public interface IRoleRepository
    {
        int AddRole(Role role, int UserId, IDatabaseCommand db);
        List<Role> GetRoles(int userId)
    }
}

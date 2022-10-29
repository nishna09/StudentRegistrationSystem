using RepositoryLibrary.Entities;
using RepositoryLibrary.Repository.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IDatabaseCommand _dBContext;
        public RoleRepository(IDatabaseCommand dBContext)
        {
            _dBContext = dBContext;
        }
        public int AddRole(Role role, int userId, IDatabaseCommand db)
        {
            bool setDb = false;
            if (db == null)
            {
                setDb = true;
                db = _dBContext;
                db.OpenDbConnection();
            }
            string query = SQLQueries.AddUserRoleQuery;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@RoleId", (int)role));
            parameters.Add(new SqlParameter("@UserId", userId));
            int insert = db.InsertUpdateDelete(query, parameters);
            if (setDb)
                db.CloseDbConnection();
            return insert;
        }
        public List<Role> GetRoles(int userId)
        {
            List<Role> roles = new List<Role>();
            string query = SQLQueries.GetUserRoles;
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", userId));
            _dBContext.OpenDbConnection();
            DataTable results = _dBContext.QueryWithConditions(query, parameters);
            if (results.Rows.Count > 0)
            {
                foreach (DataRow row in results.Rows)
                {
                    int roleId = (int)row["RoleId"];
                    roles.Add((Role)roleId);
                }
            }
            _dBContext.CloseDbConnection();
            return roles;
        }
    }
}

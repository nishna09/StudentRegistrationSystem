using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLibrary.Entities;

namespace ServicesLibrary.Services
{
    public interface IUserServices
    {
        Response Authenticate(string emailAddress, string password);
        bool IsEmailAvailable(string emailName);
    }

    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.Entities;

namespace SystemLibrary.Services
{
    public interface IUserServices
    {
        Response Authenticate(User model);
        bool EmailAvailable(string emailName);


    }

    
}

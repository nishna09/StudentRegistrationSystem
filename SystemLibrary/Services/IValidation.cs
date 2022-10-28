using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLibrary.Entities;

namespace ServicesLibrary.Services
{
    public interface IValidation
    {
        Response ValidateEmail(string email);
        Response ValidatePassword(string password);
        Response ValidatePhoneNumber(string phoneNumber);
    }
}

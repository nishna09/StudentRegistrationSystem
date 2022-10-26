using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.Entities;

namespace SystemLibrary.Services
{
    public interface IValidation
    {
        Response ValidateEmail(string email);
    }
}

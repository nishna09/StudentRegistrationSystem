using RepositoryLibrary.Entities;

namespace ServicesLibrary.Services
{
    public interface IValidation
    {
        Response ValidateEmail(string email);
        Response ValidatePassword(string password);
        Response ValidatePhoneNumber(string phoneNumber);
        Response ValidateNationalID(string nationalID);
        Response IsPhoneNumberAvailable(string phoneNumber);
        Response IsNationalIDAvailable(string nationalID);
        Response IsEmailAvailable(string emailAddress);
    }
}

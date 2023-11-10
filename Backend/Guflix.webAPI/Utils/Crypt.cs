using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Guflix.webAPI.Utils
{
    public static class Crypt
    {
        public static string GenerateHash(string password) 
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static bool Compare(string FormPassword, string DatabasePassword)
        {
            bool A = BCrypt.Net.BCrypt.Verify(FormPassword, DatabasePassword);return A;
        }
        public static bool Validate(string DatabasePassword)
        {
            if (DatabasePassword.Length >= 32 && DatabasePassword.Substring(0, 1) == "$")
            {
                return true;
            }
            else return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Utilities
{
    public class BcryptHash
    {
        public static string HashPassword(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        } 
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_GYMAPP.Domain.Encryption
{
    public class Encryption
    {
        public static string EncrPassowrd(string pass)
        {
            using (var encrypter = SHA256.Create())
            {
                var encrBytes = encrypter.ComputeHash(Encoding.UTF8.GetBytes(pass));
                var encrypts = BitConverter.ToString(encrBytes).Replace("-", "").ToLower();

                return encrypts;
            }
        }
    }
}

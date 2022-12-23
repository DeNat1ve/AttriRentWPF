using System;
using System.Security.Cryptography;
using System.Text;

namespace AttriRent.Services
{
    public class HashServise
    {
        public string HashPassword(string password)
        {
            byte[] hashedPassword = new byte[49];

            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 16, 1000))
            {
                Buffer.BlockCopy(rfc2898DeriveBytes.Salt, 0, hashedPassword, 1, 16);
                Buffer.BlockCopy(rfc2898DeriveBytes.GetBytes(32), 0, hashedPassword, 17, 32);
            }

            return Convert.ToBase64String(hashedPassword);
        }
        public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);
            
            byte[] salt = new byte[16];
            Buffer.BlockCopy(decodedHashedPassword, 1, salt, 0, 16);

            byte[] hashPassword = new byte[32];
            Buffer.BlockCopy(decodedHashedPassword, 17, hashPassword, 0, 32);

            byte[] providePassword;


            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(providedPassword, salt, 1000))
            {
                providePassword = rfc2898DeriveBytes.GetBytes(32);
            }

            if(providePassword == null || hashPassword == null || providePassword.Length != hashPassword.Length)
                return false;

            for(int i = 0; i < hashPassword.Length; i++)
            {
                if (hashPassword[i] != providePassword[i])
                    return false;
            }

            return true;
        }
    }
}

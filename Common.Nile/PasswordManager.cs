using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core_Nile.Common
{
    public class PasswordManager
    {
        const int KEYSIZE = 64;
        const int ITERATIONS = 350000;


        readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

       public (string hash,string salt) HashPasword(string password)
        {
             
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("password cannot be empty");
            }
            string allowed = "ABCDEFGHIJKLMONOPQRSTUVWXYZabcdefghijklmonopqrstuvwxyz0123456789";
            int strlen = 50; 
            var salt = RandomNumberGenerator.GetString(allowed,strlen);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                Encoding.ASCII.GetBytes(salt),
                ITERATIONS,
                hashAlgorithm,
                KEYSIZE);

            return (Convert.ToHexString(hash), salt);
        }

       public bool VerifyPassword(string password, string hash, string salt)
        {
            if (string.IsNullOrEmpty(password)|| string.IsNullOrEmpty(hash)||string.IsNullOrEmpty(salt))
            {
                throw new ArgumentException("password/salt/hash cannot be empty");
            }
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, Encoding.ASCII.GetBytes(salt), ITERATIONS, hashAlgorithm, KEYSIZE);
            
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }

    }
}

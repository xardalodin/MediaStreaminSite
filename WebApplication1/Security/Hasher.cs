using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace kuroneko.Security
{
    public class Hasher
    {

        // retuns hashed password and salt for storage in database 64 and 10
        public string GenSaltSHA256(string password,out string saltpassword)
        {
            string salt = CreateSalt(10);
            saltpassword = salt;
            string hashedpassword = GenerateSHA256Hash(password, salt);
            return hashedpassword;
        }

        // checks if stored hash has matches the hash(password+salt) 
        public bool TestPassword(string password,string storedSalt,string storedPassword)
        {
            string hashedpassword = GenerateSHA256Hash(password, storedSalt);
            if (hashedpassword == storedPassword) return true;
            else return false;
        }

        private string CreateSalt(int size)
        {
            var rng =new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        private String GenerateSHA256Hash(String input, String salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256hasstring = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hasstring.ComputeHash(bytes);
            return ByteArrayToHexString (hash);
        
        }

        private static string ByteArrayToHexString(byte[] Bytes)
        {
            StringBuilder Result = new StringBuilder(Bytes.Length * 2);
            string HexAlphabet = "0123456789ABCDEF";

            foreach (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }

            return Result.ToString();
        }

        private static byte[] HexStringToByteArray(string Hex)
        {
            byte[] Bytes = new byte[Hex.Length / 2];
            int[] HexValue = new int[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 
       0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
       0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

            for (int x = 0, i = 0; i < Hex.Length; i += 2, x += 1)
            {
                Bytes[x] = (byte)(HexValue[Char.ToUpper(Hex[i + 0]) - '0'] << 4 |
                                  HexValue[Char.ToUpper(Hex[i + 1]) - '0']);
            }

            return Bytes;
        }

    }
}
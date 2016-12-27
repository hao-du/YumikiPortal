using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Yumiki.Commons.Security
{
    public class Crypto
    {
        private const string Key = "ValueNeedToBeEncryptedWithAnAES.";
        private const string IV = "AesCryptoService";
        private const string Salt = "00Very5ecureP@ssw0rd00";

        public static string Encrypt(string value, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                key = Key;
            }

            byte[] plaintextbytes = System.Text.ASCIIEncoding.ASCII.GetBytes(value + Salt);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(Key);
            aes.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            ICryptoTransform crypto = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] encrypted = crypto.TransformFinalBlock(plaintextbytes, 0, plaintextbytes.Length);
            return Convert.ToBase64String(encrypted);
        }

        public static string Encrypt(string value)
        {
            return Encrypt(value, null);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Yumiki.Common.Helper
{
    public class SecurityHelper
    {
        private const string Key = "ValueNeedToBeEncryptedWithAnAES.";
        private const string IV = "AesCryptoService";

        public static string Encrypt(string value, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                key = Key;
            }

            byte[] plaintextbytes = System.Text.ASCIIEncoding.ASCII.GetBytes(value);
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

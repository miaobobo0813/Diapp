using Microsoft.UI.Xaml.Controls;
using System;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace Diapp
{
    public static class Encryption
    {
        public static string ShareUserName { get; set; } = "";
        public static string SharePassword { get; set; } = "";
        public static string ShareUUID { get; set; } = "";
        public static void ClearKey()
        {
            SharePassword = "";
            SharePassword = "";
            ShareUUID = "";
        }

        public static string MakeUUID(string userName)
        {
            byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(userName));
            byte[] guidBytes = new byte[16];
            Array.Copy(hash, 0, guidBytes, 0, 16);

            return new Guid(guidBytes).ToString();
        }

        public static string MakeKey(string userName, string uuid, string date, string password)
        {
            string plainText = userName + uuid + date;
            string keyBook = "", key = "";
            foreach (char k in plainText)
            {
                int unicodeK = (int)k;
                while (unicodeK > 0)
                {
                    keyBook += unicodeK % 10 + '0';
                    unicodeK /= 10;
                }
            }
            foreach (char k in password)
            {
                int unicodeK = (int)k;
                unicodeK %= keyBook.Length;
                key += keyBook[unicodeK];
            }
            return key;
        }

        // 加密
        public static string Encrypt(string plainText, string key)
        {
            string cipherText = "";
            int i = 0;
            foreach (char k in plainText)
            {
                if (k == '\n')
                {
                    cipherText += k;
                    continue;
                }
                if (k == '\r')
                {
                    cipherText += k;
                    continue;
                }
                int unicodeK = (int)k;
                unicodeK += key[i++] - '0';
                i %= key.Length;
                cipherText += (char)unicodeK;
            }
            return cipherText;
        }

        // 解密
        public static string Decrypt(string cipherText, string key)
        {
            string plainText = "";
            int i = 0;
            foreach (char k in cipherText)
            {
                if (k == '\n')
                {
                    plainText += '\n';
                    continue;
                }
                if (k == '\r')
                {
                    plainText += '\r';
                    continue;
                }
                int unicodeK = (int)k;
                unicodeK -= key[i++] - '0';
                i %= key.Length;
                plainText += (char)unicodeK;
            }
            return plainText;
        }
    }
}

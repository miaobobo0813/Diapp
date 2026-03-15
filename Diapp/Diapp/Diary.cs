using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diapp
{
    [Serializable]
    public class Diary
    {
        public string Name { get; set; } = "";
        public string CreateDate { get; set; } = "";
        public string CipherContent { get; set; } = "";

        public Diary(string name, string plainText, string userName, string uuid, string password)
        {
            CreateDate = DateTime.Now.ToString("yyyyMMdd") ?? "20260310";
            string key = Encryption.MakeKey(userName, uuid, CreateDate, password);
            Name = Encryption.Encrypt(name, key);
            CipherContent = Encryption.Encrypt(plainText, key);
        }
        public Diary() { }
    }
}

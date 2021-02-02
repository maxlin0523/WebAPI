﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Models
{
    public class Tools
    {
        private Tools()
        {
        }

        private static Tools _instance;

        public static Tools Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Tools();
                }

                return _instance;
            }
        }

        /// <summary>
        /// aes解密
        /// </summary>
        /// <param name="SourceStr"></param>
        /// <param name="CryptoKey"></param>
        /// <returns></returns>
        public string aesDecryptBase64(string SourceStr, string CryptoKey)
        {
            string decrypt = "";
            try
            {
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));
                byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));
                aes.Key = key;
                aes.IV = iv;

                byte[] dataByteArray = Convert.FromBase64String(SourceStr);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(dataByteArray, 0, dataByteArray.Length);
                        cs.FlushFinalBlock();
                        decrypt = Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
            }
            return decrypt;
        }
    }
}
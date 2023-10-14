using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Conexion.Security
{
    public class Encrypter
    {

        public static string EncryptString(string text, string key)
        {
            /*
            int Temp, j, n;
            int[] UserKeyASCIIS;
            int[] TextASCIIS;
            string rtn = String.Empty;
            n = key.Length;
            j = 0;

            //Get UserKey characters
            UserKeyASCIIS = new int[key.Length];
            for (int i = 0; i < key.Length; i++)
            {
                UserKeyASCIIS[i] = (int)key[i];
            }
            //Get Text characters
            TextASCIIS = new int[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                TextASCIIS[i] = (int)text[i];
            }
            //Encrypt
            for (int i = 0; i < text.Length; i++)
            {
                j = (int)j + 1 >= n ? 1 : j + 1;
                Temp = TextASCIIS[i] + UserKeyASCIIS[j - 1];
                if (Temp > 255) Temp -= -255;
                rtn += System.Convert.ToChar(Temp);
            }
            return rtn;
            */
            return text;
        }

        public static string DecryptString(string text, string key)
        {
            /*+
            int Temp, j, n;
            int[] UserKeyASCIIS;
            int[] TextASCIIS;
            string rtn = String.Empty;
            n = key.Length;
            j = 0;

            //Get UserKey characters
            UserKeyASCIIS = new int[key.Length];
            for (int i = 0; i < key.Length; i++)
            {
                UserKeyASCIIS[i] = (int)key[i];
            }
            //Get Text characters
            TextASCIIS = new int[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                TextASCIIS[i] = (int)text[i];
            }

            for (int i = 0; i < text.Length; i++)
            {
                j = (int)j + 1 >= n ? 1 : j + 1;
                Temp = TextASCIIS[i] - UserKeyASCIIS[j - 1];

                if (Temp < 0)
                {
                    Temp = Temp + 255;
                }
                rtn += System.Convert.ToChar(Temp);
            }
            return rtn;
            */
            return text;
        }

    }
}

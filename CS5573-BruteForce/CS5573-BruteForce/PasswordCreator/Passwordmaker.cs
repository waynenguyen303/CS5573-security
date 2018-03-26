using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.IO;

namespace _CS5573_BruteForce.PasswordCreator
{
    class PasswordMaker
    {
        public static void MakeBruteForcePasswords()
        {
            String attempt = "";
            int first = 0; int second = 0; int third = 0; int fourth = 0; int cracks = 0;

            string[] array = new string[63];
            array[0] = "";
            array[1] = "a"; array[37] = "A";
            array[2] = "b"; array[38] = "B";
            array[3] = "c"; array[39] = "C";
            array[4] = "d"; array[40] = "D";
            array[5] = "e"; array[41] = "E";
            array[6] = "f"; array[42] = "F";
            array[7] = "g"; array[43] = "G";
            array[8] = "h"; array[44] = "H";
            array[9] = "i"; array[45] = "I";
            array[10] = "j"; array[46] = "J";
            array[11] = "k"; array[47] = "K";
            array[12] = "l"; array[48] = "L";
            array[13] = "m"; array[49] = "M";
            array[14] = "n"; array[50] = "N";
            array[15] = "o"; array[51] = "O";
            array[16] = "p"; array[52] = "P";
            array[17] = "q"; array[53] = "Q";
            array[18] = "r"; array[54] = "R";
            array[19] = "s"; array[55] = "S";
            array[20] = "t"; array[56] = "T";
            array[21] = "u"; array[57] = "U";
            array[22] = "v"; array[58] = "V";
            array[23] = "w"; array[59] = "W";
            array[24] = "x"; array[60] = "X";
            array[25] = "y"; array[61] = "Y";
            array[26] = "z"; array[62] = "Z";
            array[27] = "1"; 
            array[28] = "2";
            array[29] = "3";
            array[30] = "4";
            array[31] = "5";
            array[32] = "6";
            array[33] = "7";
            array[34] = "8";
            array[35] = "9";
            array[36] = "0";

            int i = 0;
            Int64 k = Convert.ToInt64(Math.Pow(63,4));
            List<string> pwlist = new List<string> { };
            
            while (i < k)
            {
                if (first == array.Length)
                {
                    second++;
                    first = 0;
                }
                if (second == array.Length)
                {
                    third++;
                    second = 0;
                }
                if (third == array.Length)
                {
                    fourth++;
                    third = 0;
                }
                if (fourth == array.Length)
                {
                    break;
                }

                attempt = array[fourth] + array[third] + array[second] + array[first];
                pwlist.Add(attempt);

                first++;
                i++;
                cracks++;
            }

            string path = string.Concat(Environment.CurrentDirectory, @"\input\BruteForcePassword.txt");
            File.WriteAllLines(path, pwlist);
            Console.WriteLine("With this character array, there are "+ cracks + " possible passwords. \n");
        }


        public static string GetRandomAlphanumericString(int length)
        {
            const string alphanumericCharacters =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "abcdefghijklmnopqrstuvwxyz" +
                "0123456789";
            return GetRandomString(length, alphanumericCharacters);
        }

        public static string GetRandomString(int length, IEnumerable<char> characterSet)
        {
            if (length < 0)
                throw new ArgumentException("length must not be negative", "length");
            if (length > int.MaxValue / 8) // 250 million chars ought to be enough for anybody
                throw new ArgumentException("length is too big", "length");
            if (characterSet == null)
                throw new ArgumentNullException("characterSet");
            var characterArray = characterSet.Distinct().ToArray();
            if (characterArray.Length == 0)
                throw new ArgumentException("characterSet must not be empty", "characterSet");

            var bytes = new byte[length * 8];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string(result);
        }

    }
}

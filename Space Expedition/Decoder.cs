using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Expedition
{
    internal class Decoder
    {
        private static char[] OriginalArray = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private static char[] MappedArray = { 'H', 'Z', 'A', 'U', 'Y', 'E', 'K', 'G', 'O', 'T', 'I', 'R', 'J', 'V', 'W', 'N', 'M', 'F', 'Q', 'S', 'D', 'B', 'X', 'L', 'C', 'P' };
        
        private static char MapForward(char c)
        {
            c = char.ToUpper(c);
            for (int i = 0; i < OriginalArray.Length; i++)
            {
                if (OriginalArray[i] == c)
                {
                    return MappedArray[i];
                }
            }
            return c;
        }

        private static char Mirror(char c)
        {
            c = char.ToUpper(c);
            for (int i = 0; i < OriginalArray.Length; i++)
            {
                if (OriginalArray[i] == c)
                {
                    int mirrorIndex = OriginalArray.Length - 1 - i;
                    return OriginalArray[mirrorIndex];
                }
            }
            return c;
        }

        private static char DecodeRecursive(char c, int level)
        {
            c = char.ToUpper(c);
            if (level == 0)
            {
                return Mirror(c);
            }

            c = MapForward(c);
            return DecodeRecursive(c, level - 1);
        }

        public static string DecodeName(string encodedName)
        {
            string result = "";
            int i = 0;

            while (i < encodedName.Length)
            {
                if (encodedName[i] == ' ' || encodedName[i] == ',' || encodedName[i] == '|' || encodedName[i] == '"')
                {
                    i++;
                    continue;
                }

                char letter = char.ToUpper(encodedName[i]);
                if (letter < 'A' || letter > 'Z')
                {
                    i++;
                    continue;
                }

                i++;

                string numText = "";
                while (i < encodedName.Length && char.IsDigit(encodedName[i]))
                {
                    numText += encodedName[i];
                    i++;
                }

                int level = 0;
                if (numText != "")
                {
                    level = int.Parse(numText);
                }

                char decodedChar = DecodeRecursive(letter, level);
                result += decodedChar;
            }

            return result;
        }
    }
}

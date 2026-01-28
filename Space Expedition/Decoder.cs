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
        public static char[] OriginalArray = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        public static char[] MappedArray = { 'H', 'Z', 'A', 'U', 'Y', 'E', 'K', 'G', 'O', 'T', 'I', 'R', 'J', 'V', 'W', 'N', 'M', 'F', 'Q', 'S', 'D', 'B', 'X', 'L', 'C', 'P' };
        
        public static char MapForward(char c)
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

        public static char Mirror(char c)
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

        public static char DecodeRecursive(char c, int level)
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
                char ch = encodedName[i];

                if (ch == '|' || ch == '"' || ch == ' ')
                {
                    i++;
                    continue;
                }

                if (ch == ',')
                {
                    break;
                }

                char letter = char.ToUpper(ch);
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

                result += DecodeRecursive(letter, level);
            }
            return result;
        }

    }
}

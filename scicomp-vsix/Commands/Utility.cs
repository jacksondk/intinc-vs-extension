using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable 
namespace intinc_vsix.Commands
{
    internal class Utility
    {
        internal static TextRange? GetDigits(string txt, int column)
        {
            if (!IsDigit(txt[column]))
                return null;

            var start = column;
            while (start != 0)
            {
                if (IsDigit(txt[start]))
                {
                    start--;
                    continue;
                }
                else
                {
                    break;
                }
            }
            start++;
            var length = 0;
            while (start+length < txt.Length)
            {
                if (IsDigit(txt[start+length]))
                    length++;
                else
                    break;
            }

            return new TextRange(start, length);
        }

        private static bool IsDigit(char v)
        {
            return v > '0' && v < '9';
        }
    }

    public record TextRange(int start, int length);
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContosoUniversity.Web
{
    public class Utility
    {
        // SqlServer Version
        public static string GetLastChars(byte[] token)
        {
            return token[7].ToString();
        }

        // SqlLite Version 
        public static string GetLastChars(Guid token)
        {
            return token.ToString().Substring(
                token.ToString().Length - 3);
        }
    }
}

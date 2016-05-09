using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication4
{
    public class PublicToolBox
    {
        public static  string  RemoveInhibitChar(string newfilename)
        {
            char[] invalidChars = System.IO.Path.GetInvalidFileNameChars();
            if (newfilename.IndexOfAny(invalidChars) < 0)
            {
               // Console.WriteLine("ファイル名に使用できない文字は使われていません。");
            }
            else
            {
                foreach (char c in invalidChars)
                {
                    newfilename = newfilename.Replace(c.ToString(), "");
                }
            }
            return newfilename;
        }

        public SpetialPageName spn;
        public List<string> ListSpecialStroyNames = new List<string> ( );
        public   PublicToolBox() { 
            ListSpecialStroyNames.Add (".Settings");
            ListSpecialStroyNames.Add ( ".Persons");
            ListSpecialStroyNames.Add ( ".KeyWords");
        }
        public enum SpetialPageName {
            Settings,
            Persons,
            KeyWords
        };
        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        
    }

}

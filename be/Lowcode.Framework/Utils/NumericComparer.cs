using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lowcode.Framework.Utils
{
    public class NumericComparer : IComparer
    {
        public NumericComparer()
        { }

        public int Compare(object x, object y)
        {
            if ((x is string) && (y is string))
            {
                return StringLogicalComparer.Compare((string)x, (string)y);
            }
            return -1;
        }
    }//EOC

    public class NumericStringComparer : IComparer<string>
    {
        public NumericStringComparer()
        { }

        public int Compare(string x, string y)
        {
         
                return StringLogicalComparer.Compare((string)x, (string)y);

        }
    }//EOC
}

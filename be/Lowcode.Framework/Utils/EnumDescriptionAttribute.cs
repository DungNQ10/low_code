using System;
using System.Collections.Generic;
using System.Text;

namespace Lowcode.Framework.Utils
{
    /// <summary>
    /// Description plain text for enum type
    /// </summary>
    public class EnumDescriptionAttribute : Attribute
    {
        private string _description;

        public EnumDescriptionAttribute(string description)
        {
            _description = description;
        }

        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Lowcode.Framework
{
    public class FrameworkException : Exception
    {
		/// <summary>
		/// Default constructor
		/// </summary>
		public FrameworkException() : base()
		{
			
		}

		/// <summary>
		/// Construct exception with a message
		/// </summary>
		public FrameworkException(string message) : base(message)
		{
			
		}
				
		/// <summary>
		/// Construct exception with a message and inner exception
		/// </summary>
        public FrameworkException(string message, Exception innerException)
            : base(message, innerException)
		{
			
		}
    }
}

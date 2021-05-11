using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.PMKT.Framework.Data
{
    public class AppContextFactory<T> : IAppContextFactory<T> where T : DbContext
    {
        private T _context;
        public AppContextFactory(T context)
        {
            _context = context;
        }

        public T GetContext()
        {
            return _context;
        }
    }
}

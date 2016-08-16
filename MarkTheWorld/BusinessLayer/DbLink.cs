using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Database;

namespace BusinessLayer
{
    public static class DbLink
    {
        public static void DocumentRep()
        {
            DocumentDBRepository<Dot>.Initialize();
            DocumentDBRepository<User>.Initialize();
        }
    }
}

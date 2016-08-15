using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class DbLink
    {
        public static void DocumentRep()
        {
            DocumentDBRepository<Data.DeleteLater.Item>.Initialize();
            DocumentDBRepository<Data.Database.Dot>.Initialize();
            DocumentDBRepository<Data.Database.User>.Initialize();
            DocumentDBRepository<Data.Database.Events>.Initialize();
        }
    }
}

using SmartWeb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartWeb.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<TblApplicationUser>
    {
        void Delete(TblApplicationUser tblApplicationUser);
    }
}

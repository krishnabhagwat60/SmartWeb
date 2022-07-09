using SmartWeb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartWeb.Repository.IRepository
{
    public interface ITeacherAlertRepository : IRepository<TblAlert>
    {
        void Update(TblAlert tblAlert);
        void Delete(int id);
    }
}

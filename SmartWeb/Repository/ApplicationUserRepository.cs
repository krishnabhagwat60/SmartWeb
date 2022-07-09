using SmartWeb.Data;
using SmartWeb.Models;
using SmartWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartWeb.Repository
{
    public class ApplicationUserRepository : Repository<TblApplicationUser>, IApplicationUserRepository
    {
        private readonly SmartTeacherDBContext _db;
        public ApplicationUserRepository(SmartTeacherDBContext db) : base(db)
        {
            _db = db;
        }

        public void Delete(TblApplicationUser tblApplicationUser)
        {
            var objFromDb = _db.TblApplicationUser.Find(tblApplicationUser.Id);
            if (objFromDb != null)
            {
                objFromDb.PhoneNumber = tblApplicationUser.UserName;
                objFromDb.UserVisible = "no";
            }
        }
    }
}

using SmartWeb.Data;
using SmartWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartWeb.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SmartTeacherDBContext _db;

        public UnitOfWork(SmartTeacherDBContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);           
            TeacherAlert = new TeacherAlertRepository(_db);
            SP_Call = new SP_Call(_db);
        }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public ITeacherAlertRepository TeacherAlert { get; private set; }
        public ISP_Call SP_Call { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

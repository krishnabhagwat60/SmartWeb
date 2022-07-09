using System;
using System.Collections.Generic;
using System.Text;

namespace SmartWeb.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationUserRepository ApplicationUser { get; }
        ITeacherAlertRepository TeacherAlert { get; }
        ISP_Call SP_Call { get; }
        void Save();
    }
}

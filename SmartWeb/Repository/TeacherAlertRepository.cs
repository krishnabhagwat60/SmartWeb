using SmartWeb.Data;
using SmartWeb.Models;
using SmartWeb.Repository.IRepository;
using System.Linq;

namespace SmartWeb.Repository
{
    public class TeacherAlertRepository : Repository<TblAlert>, ITeacherAlertRepository
    {
        private readonly SmartTeacherDBContext _db;
        public TeacherAlertRepository(SmartTeacherDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(TblAlert tblAlert)
        {
            var objFromDb = _db.TblAlert.Find(tblAlert.AlertID);
            if (objFromDb != null)
            {
                objFromDb.StageID = tblAlert.StageID;
                objFromDb.AlertName = tblAlert.AlertName.Trim();
            }
        }

        public void Delete(int id)
        {
            var objFromDb = _db.TblAlert.Find(id);
            if (objFromDb != null)
            {
                objFromDb.AlertVisible = "no";
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace SmartWeb.Models
{
    public class TblApplicationUser : IdentityUser
    {
        public string Category { get; set; }
        public DateTime DateCreated { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string ImageUrl { get; set; }

        public string RoleID { get; set; }
        //[ForeignKey("RoleID")]
        //public TblTeacherRoles TblTeacherRoles { get; set; }

        public string RoleChange { get; set; }

        public int GovernorateID { get; set; }
        //[ForeignKey("GovernorateID")]
        //public TblGovernorate TblGovernorate { get; set; }

        public string JobTitle { get; set; }
        public string Password { get; set; }
        //public string Token { get; set; }
        public string UserVisible { get; set; }



        public int StageID { get; set; }
        //[ForeignKey("StageID")]
        //public TblStage TblStage { get; set; }

        public float MonthCost { get; set; }
        public string FatherPhoneNumber { get; set; }
        public string StudentWebLogged { get; set; }
        public string StudentAppLogged { get; set; }
        public int StudentLogged { get; set; }
        public string StudentType { get; set; }
        public string StudentOnline { get; set; }
        public string StudentCenter { get; set; }
        public string UserActive { get; set; }
        public string MobileType { get; set; }
        public string AppVersion { get; set; }




        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> GovernoratList { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> RolesList { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> StageList { get; set; }
        [NotMapped]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [NotMapped]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
        [NotMapped]
        public string SettingStudentEdit { get; set; }
        [NotMapped]
        public string SettingStudentOpen { get; set; }
        [NotMapped]
        public string Coming { get; set; }
        [NotMapped]
        public int Mark { get; set; }
    }
}

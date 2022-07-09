using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartWeb.Models
{
    public class TblAlert
    {
        [Key]
        public int AlertID { get; set; }
        public int StageID { get; set; }
        public string AlertName { get; set; }
        public string AlertVideo { get; set; }
        public string AlertVisible { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> StageList { get; set; }
    }
}

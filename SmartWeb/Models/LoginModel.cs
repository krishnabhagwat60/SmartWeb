using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartWeb.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "تأكد من تسجيل رقم الهاتف"), StringLength(11, ErrorMessage = "تأكد من تسجيل رقم التليفون بشكل صحيح", MinimumLength = 11)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "تأكد من تسجيل كلمة المرور"), MinLength(4, ErrorMessage = "كلمة المرور يجب الا تقل عن أربعة حروف")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}

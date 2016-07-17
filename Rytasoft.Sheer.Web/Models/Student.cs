using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rytasoft.Sheer.Web.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Display(Name = "Label For First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Label For Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Label For Score")]
        public int Score { get; set; }
    }
}
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
        [Display(Name = "labelForName")]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int Score { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyVarsenoTask.Models
{
    public class Registration
    {
        [Key]
        [Column(Order = 0)]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please Enter The Email Address")]
        [Display(Name = "Email Id")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter The First Name")]
        [Display(Name = "FirstName")]

        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter The Last Name ")]
        [Display(Name = "LastName")]

        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter The Date Of Birth")]
        [Display(Name = "DOB")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString = "{mm/dd/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Please Enter The Gender")]
        [Display(Name = "Gender")]

        public char Gender { get; set; }
        [Required(ErrorMessage = "Please Enter The Education")]
        [Display(Name = "Education")]

        public string Education { get; set; }
        [Required(ErrorMessage = "Please Enter The Address")]
        [Display(Name = "Address")]

        public string Address { get; set; }
        [Required(ErrorMessage = "Please Enter The Password")]
        [Display(Name = "Password")]

        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter The ConfirmPassword")]
        [Display(Name = "ConfirmPassword")]

        public string ConfirmPassword { get; set; }
    }
}
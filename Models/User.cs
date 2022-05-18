using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RemedyAcknowledgement.Models
{
    [Table("User")]
    public class User
    {
        [Required(ErrorMessage = "First Name cannot not be empty")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Last Name cannot not be empty")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Designation cannot not be empty")]
        public string Designation { get; set; }
        [Required(ErrorMessage = "Contact Number cannot not be empty")]
        [RegularExpression(@"^[7-9]{1}[0-9]{9}$", ErrorMessage = "Please Enter Valid number")]
        public string ContactNumber { get; set; }
        [Key]
        [RegularExpression("^User[0-9]{4}$", ErrorMessage = "Invalid userId")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Password cannot not be empty")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters are required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{6,}$", ErrorMessage = "password should be in following format eg:Abcd@1")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Date of birth cannot not be empty")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [MinimumAge(18)]
        public DateTime Dob { get; set; }
        [Required(ErrorMessage = "Gender cannot not be empty")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Secret Question cannot not be empty")]
        public string Secretquestion { get; set; }
        [Required(ErrorMessage = "Secret Answer cannot not be empty")]
        public string Secretanswer { get; set; }

        [Required(ErrorMessage = "must not be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Id")]
        public string Mailid { get; set; }
        public ICollection<Logins> Logins { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<RemedyDetails> RemedyDetails { get; set; }
        public ICollection<RemedyList> RemedyLists { get; set; }
    }
}
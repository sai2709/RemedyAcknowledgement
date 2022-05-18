using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RemedyAcknowledgement.Models
{
    [Table("Logins")]
    public class Logins
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "userid should not be Empty")]
        
        public string UserId { get; set; }
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters are required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{6,}$", ErrorMessage = "password should be in following format eg:Abcd@1")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Role should not be Empty")]
        public string Role { get; set; }
        public string ContactNumber { get; set; }
        public string Secretquestion { get; set; }

        public string Secretans { get; set; }
        public string Mailid { get; set; }

        public User User { get; set; }

        public SupportAnalyst SupportAnalyst { get; set; }

        public Admin Admin { get; set; }
    }
}
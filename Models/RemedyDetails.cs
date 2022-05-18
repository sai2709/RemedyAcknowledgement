using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RemedyAcknowledgement.Models
{
    [Table("RemedyDetails")]
   
    public class RemedyDetails
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Category cannot not be empty")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Description cannot not be empty")]
        public string Description { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Remedyid { get; set; }
        [Required(ErrorMessage = "Contact Number cannot not be empty")]
        [RegularExpression(@"^[7-9]{1}[0-9]{9}$", ErrorMessage = "Please Enter Valid number")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Contact Number cannot not be empty")]
        [RegularExpression(@"^[7-9]{1}[0-9]{9}$", ErrorMessage = "Please Enter Valid number")]
        public string AlternateNumber { get; set; }
        [Required(ErrorMessage = "Seat number cannot not be empty")]
        public string SeatNo { get; set; }
        [Required(ErrorMessage = "PC number cannot not be empty")]
        public string PCNumber { get; set; }
        [Required(ErrorMessage = "IP Address cannot not be empty")]

        public string IPAddress { get; set; }
        public string RemedyStatus { get; set; }
        public ICollection<RemedyList> RemedyLists { get; set; }
        public User User { get; set; }

    }

}
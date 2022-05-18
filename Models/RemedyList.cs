using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RemedyAcknowledgement.Models
{
    [Table("RemedyList")]
    public class RemedyList
    {
        [Key]
        public int SerialNo { get; set; }
        public string UserId { get; set; }

        public string Description { get; set; }

        public string AssignedUserId { get; set; }

        public int Remedyid { get; set; }
        [Required(ErrorMessage = "Status cannot be empty")]
        public string Status { get; set; }
        public User User { get; set; }
        public Admin Admin { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public SupportAnalyst SupportAnalyst { get; set; }
        public RemedyDetails RemedyDetail { get; set; }

    }
}
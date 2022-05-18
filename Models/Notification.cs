using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RemedyAcknowledgement.Models
{
    [Table("Notification")]
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotifId { get; set; }
        public DateTime CreationDate { get; set; }
        public string  AlertText { get; set; }
        public string  Details { get; set; }
        public string ReceiverId { get; set; }
        public string SenderId { get; set; }
        public DateTime NotifDate { get; set; }
        public Admin Admin { get; set; }
        public SupportAnalyst SupportAnalyst { get; set; }
        public User User { get; set; }


    }
}
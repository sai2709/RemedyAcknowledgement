using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RemedyAcknowledgement.Models
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public string Userid { get; set; }
        public string Password { get; set; }
       public ICollection<Logins> logins { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<RemedyList> RemedyLists { get; set; }
        public ICollection<Questionnaire> Questionnaires { get; set; }
    }
}
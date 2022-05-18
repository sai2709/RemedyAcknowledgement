using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace RemedyAcknowledgement.Models
{
    public class RemedyContext:DbContext
    {
        public RemedyContext():base("Con")
        { 
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<SupportAnalyst> Supports { get; set; }
        public virtual DbSet<RemedyDetails> Remedies { get; set; }
        public virtual DbSet<RemedyList> RemedyLists { get; set; }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Logins> Logins { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Questionnaire> Questionnaires { get; set; }

    }
}
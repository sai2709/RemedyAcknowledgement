using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RemedyAcknowledgement.Models
{
    [Table("Feedback")]
    public class Feedback
    {
        [Key]
        [Column(Order = 0)]

        public string FeedbackId { get; set; }
        [Key]
        [Column(Order = 1)]

        public string QuestionId { get; set; }
        public string Answer { get; set; }
        public string UserId { get; set; }

        public Questionnaire Questionnaires { get; set; }

    }
}
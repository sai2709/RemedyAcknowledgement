using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RemedyAcknowledgement.Models
{
    [Table("Questionnaire")]
    public class Questionnaire
    {
        [Key]
        public string QuestionId { get; set; }


        [Required(ErrorMessage = "Question must not be empty")]
        public string QuestionText { get; set; }


        [Required(ErrorMessage = "User Id must not be empty")]
        public string UserId { get; set; }

        public Admin Admin { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
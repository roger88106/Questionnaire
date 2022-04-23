namespace Questionnaire.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Question
    {
        public Guid QuestionID { get; set; }

        public int QuestionnairesID { get; set; }

        public int QuestionType { get; set; }

        [Required]
        [StringLength(50)]
        public string QuestionContent { get; set; }

        public int QuestionOrder { get; set; }

        public bool Required { get; set; }

        [StringLength(500)]
        public string QuestionOptions { get; set; }
    }
}

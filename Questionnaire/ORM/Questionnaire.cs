namespace Questionnaire.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Questionnaire
    {
        public int QuestionnaireID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [Required]
        [StringLength(50)]
        public string QuestionnaireTital { get; set; }

        [Required]
        [StringLength(1000)]
        public string QuestionnaireContent { get; set; }

        public int QuestionnaireState { get; set; }
    }
}

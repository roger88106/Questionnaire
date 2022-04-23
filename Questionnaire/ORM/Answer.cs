namespace Questionnaire.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Answer
    {
        public Guid AnswerID { get; set; }

        public Guid? RespondentID { get; set; }

        public int? QuestionnaireID { get; set; }

        public Guid? QuestionID { get; set; }

        [Column("Answer")]
        [StringLength(500)]
        public string Answer1 { get; set; }
    }
}

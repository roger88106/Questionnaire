namespace Questionnaire.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Questionnaire
    {
        [Key]
        [Column(Order = 0)]
        public Guid QuestionnaireID { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime StartTime { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime EndTime { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string QuestionnaireTital { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(1000)]
        public string QuestionnaireContent { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionnaireState { get; set; }
    }
}

namespace Questionnaire.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Question
    {
        [Key]
        [Column(Order = 0)]
        public Guid QuestionID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid QuestionnairesID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionType { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string QuestionContent { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionOrder { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool Required { get; set; }

        [StringLength(500)]
        public string QuestionOptions { get; set; }
    }
}

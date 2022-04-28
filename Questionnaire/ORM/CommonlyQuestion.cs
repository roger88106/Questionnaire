namespace Questionnaire.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CommonlyQuestion
    {
        [Key]
        [Column(Order = 0)]
        public Guid CommonlyID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string QuestionContent { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionType { get; set; }

        [StringLength(500)]
        public string QuestionOptions { get; set; }
    }
}

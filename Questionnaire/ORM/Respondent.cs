namespace Questionnaire.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Respondent
    {
        [Key]
        [Column(Order = 0)]
        public Guid RespondentID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid QuestionnairesID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string Email { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Age { get; set; }

        [Key]
        [Column(Order = 6)]
        public DateTime FillTime { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Questionnaire.ORM
{
    public partial class ContextModel : DbContext
    {
        public ContextModel()
            : base("name=ContextModel")
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Questionnaire> Questionnaires { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Respondent> Respondents { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<CommonlyQuestion> CommonlyQuestions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>()
                .Property(e => e.Answer1)
                .IsUnicode(false);

            modelBuilder.Entity<Questionnaire>()
                .Property(e => e.QuestionnaireTital)
                .IsUnicode(false);

            modelBuilder.Entity<Questionnaire>()
                .Property(e => e.QuestionnaireContent)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.QuestionContent)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.QuestionOptions)
                .IsUnicode(false);

            modelBuilder.Entity<Respondent>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Respondent>()
                .Property(e => e.PhoneNumber)
                .IsFixedLength();

            modelBuilder.Entity<Respondent>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<CommonlyQuestion>()
                .Property(e => e.QuestionContent)
                .IsUnicode(false);

            modelBuilder.Entity<CommonlyQuestion>()
                .Property(e => e.QuestionOptions)
                .IsUnicode(false);
        }
    }
}

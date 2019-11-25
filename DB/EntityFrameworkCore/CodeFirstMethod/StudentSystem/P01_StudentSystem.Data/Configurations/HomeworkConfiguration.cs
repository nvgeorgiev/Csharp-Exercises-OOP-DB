namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;

    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> entity)
        {
            entity.HasKey(h => h.HomeworkId);

            entity
                .Property(h => h.Content)
                .IsRequired(true)
                .IsUnicode(false);

            entity
                .Property(h => h.ContentType)
                .IsRequired(true);

            entity
                .Property(h => h.SubmissionTime)
                .IsRequired(true);

            entity
                .HasOne(h => h.Student)
                .WithMany(s => s.HomeworkSubmissions)
                .HasForeignKey(h => h.StudentId)
                .OnDelete(DeleteBehavior.Restrict);


            entity
                .HasOne(h => h.Course)
                .WithMany(c => c.HomeworkSubmissions)
                .HasForeignKey(h => h.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

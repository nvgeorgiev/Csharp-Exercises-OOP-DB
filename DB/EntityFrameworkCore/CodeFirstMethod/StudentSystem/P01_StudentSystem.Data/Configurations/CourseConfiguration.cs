namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> entity)
        {
            entity.HasKey(c => c.CourseId);

            entity
                .Property(c => c.Name)
                .HasMaxLength(80)
                .IsRequired(true)
                .IsUnicode(true);

            entity
                .Property(c => c.Description)
                .IsRequired(false)
                .IsUnicode(true);

            entity
                .Property(c => c.StartDate)
                .IsRequired(true);

            entity
                .Property(c => c.EndDate)
                .IsRequired(true);

            entity
                .Property(c => c.Price)
                .IsRequired(true);
        }
    }
}

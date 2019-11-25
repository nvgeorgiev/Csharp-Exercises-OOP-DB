namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;

    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> entity)
        {
            entity.HasKey(s => s.StudentId);

            entity
                .Property(s => s.Name)
                .HasMaxLength(100)
                .IsRequired(true)
                .IsUnicode(true);

            entity
                .Property(s => s.PhoneNumber)
                .HasMaxLength(10)
                .IsFixedLength(true)
                .IsRequired(false)
                .IsUnicode(false);

            entity
                .Property(s => s.RegisteredOn)
                .HasColumnType("DATETIME2")
                .IsRequired(true);

            entity
                .Property(s => s.Birthday)
                .IsRequired(false)
                .HasColumnType("DATE");
        }
    }
}

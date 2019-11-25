namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;

    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> entity)
        {
            entity.HasKey(r => r.ResourceId);

            entity
                .Property(r => r.Name)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(true);

            entity
                .Property(r => r.Url)
                .IsRequired(true)
                .IsUnicode(false);

            entity
                .Property(r => r.ResourceType)
                .IsRequired(true);

            entity
                .HasOne(r => r.Course)
                .WithMany(c => c.Resources)
                .HasForeignKey(r => r.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

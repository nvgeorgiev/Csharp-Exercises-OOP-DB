namespace P03_FootballBetting.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> entity)
        {
            entity.HasKey(t => t.TeamId);

            entity
                .Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(true);

            entity
                .Property(t => t.LogoUrl)
                .HasMaxLength(250)
                .IsRequired(false)
                .IsUnicode(false);

            entity
                .Property(t => t.Initials)
                .HasMaxLength(3)
                .IsRequired(true)
                .IsUnicode(true);

            entity
                .Property(t => t.Budget)
                .IsRequired(true);

            entity
                .HasOne(t => t.PrimaryKitColor)
                .WithMany(c => c.PrimaryKitTeams)
                .HasForeignKey(t => t.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(t => t.SecondaryKitColor)
                .WithMany(c => c.SecondaryKitTeams)
                .HasForeignKey(t => t.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(t => t.Town)
                .WithMany(tw => tw.Teams)
                .HasForeignKey(t => t.TownId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

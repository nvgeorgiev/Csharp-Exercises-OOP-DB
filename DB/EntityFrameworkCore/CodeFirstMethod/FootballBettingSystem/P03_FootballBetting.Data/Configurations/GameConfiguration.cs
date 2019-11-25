namespace P03_FootballBetting.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> entity)
        {
            entity.HasKey(g => g.GameId);

            entity
                .Property(g => g.HomeTeamGoals)
                .IsRequired(true);

            entity
                .Property(g => g.AwayTeamGoals)
                .IsRequired(true);

            entity
                .Property(g => g.DateTime)
                .IsRequired(true);

            entity
                .Property(g => g.HomeTeamBetRate)
                .IsRequired(true);

            entity
                .Property(g => g.AwayTeamBetRate)
                .IsRequired(true);

            entity
                .Property(g => g.DrawBetRate)
                .IsRequired(true);

            entity
                .Property(g => g.Result)
                .HasMaxLength(7)
                .IsRequired(true)
                .IsUnicode(false);

            entity
                .HasOne(g => g.HomeTeam)
                .WithMany(t => t.HomeGames)
                .HasForeignKey(g => g.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(g => g.AwayTeam)
                .WithMany(t => t.AwayGames)
                .HasForeignKey(g => g.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

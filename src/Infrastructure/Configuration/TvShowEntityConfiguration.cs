using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class TvShowEntityConfiguration : IEntityTypeConfiguration<TvShow>
    {
        public void Configure(EntityTypeBuilder<TvShow> builder)
        {
            builder.OwnsOne(tvShow => tvShow.Rating,
                navigationBuilder =>
                {
                    navigationBuilder.Property(rating => rating.Average);
                });

            builder.HasMany(tvShow => tvShow.Genres);

        }
    }
}

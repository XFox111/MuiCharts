using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MuiCharts.Domain.Models;

/// <summary>
/// Represents the entity type configuration for the <see cref="Track"/> entity.
/// </summary>
public class TrackEntityTypeConfiguration : IEntityTypeConfiguration<Track>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<Track> builder)
	{
		builder.HasKey(t => new { t.FirstId, t.SecondId });
		builder.Property(t => t.Distance).IsRequired();
		builder.Property(t => t.Surface).IsRequired();
		builder.Property(t => t.MaxSpeed).IsRequired();
	}
}

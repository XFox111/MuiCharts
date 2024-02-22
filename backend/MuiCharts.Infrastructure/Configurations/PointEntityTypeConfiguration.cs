using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MuiCharts.Domain.Models;

namespace MuiCharts.Infrastructure.Configurations;

/// <summary>
/// Represents the entity type configuration for the <see cref="Point"/> entity.
/// </summary>
public class PointEntityTypeConfiguration : IEntityTypeConfiguration<Point>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<Point> builder)
	{
		builder.HasKey(p => p.Id);
		builder.Property(p => p.Name).IsRequired();
		builder.Property(p => p.Height).IsRequired();
	}
}

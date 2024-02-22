﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace MuiCharts.Infrastructure.Migrations
{
	[DbContext(typeof(DataContext))]
	partial class DataContextModelSnapshot : ModelSnapshot
	{
		protected override void BuildModel(ModelBuilder modelBuilder)
		{
#pragma warning disable 612, 618
			modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

			modelBuilder.Entity("MuiCharts.Domain.Models.Point", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("INTEGER");

					b.Property<int>("Height")
						.HasColumnType("INTEGER");

					b.Property<string>("Name")
						.IsRequired()
						.HasColumnType("TEXT");

					b.HasKey("Id");

					b.ToTable("Points");
				});

			modelBuilder.Entity("MuiCharts.Domain.Models.Track", b =>
				{
					b.Property<int>("FirstId")
						.HasColumnType("INTEGER");

					b.Property<int>("SecondId")
						.HasColumnType("INTEGER");

					b.Property<int>("Distance")
						.HasColumnType("INTEGER");

					b.Property<int>("MaxSpeed")
						.HasColumnType("INTEGER");

					b.Property<int>("Surface")
						.HasColumnType("INTEGER");

					b.HasKey("FirstId", "SecondId");

					b.ToTable("Tracks");
				});
#pragma warning restore 612, 618
		}
	}
}
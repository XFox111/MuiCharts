using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuiCharts.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class InitialCreate : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Points",
				columns: table => new
				{
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Name = table.Column<string>(type: "TEXT", nullable: false),
					Height = table.Column<int>(type: "INTEGER", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Points", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Tracks",
				columns: table => new
				{
					FirstId = table.Column<int>(type: "INTEGER", nullable: false),
					SecondId = table.Column<int>(type: "INTEGER", nullable: false),
					Distance = table.Column<int>(type: "INTEGER", nullable: false),
					Surface = table.Column<int>(type: "INTEGER", nullable: false),
					MaxSpeed = table.Column<int>(type: "INTEGER", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Tracks", x => new { x.FirstId, x.SecondId });
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Points");

			migrationBuilder.DropTable(
				name: "Tracks");
		}
	}
}

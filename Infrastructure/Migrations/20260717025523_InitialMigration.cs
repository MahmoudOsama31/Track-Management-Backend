using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DSPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DSPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    ISRC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tracks_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrackDistributions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackId = table.Column<int>(type: "int", nullable: false),
                    DSPId = table.Column<int>(type: "int", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackDistributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackDistributions_DSPs_DSPId",
                        column: x => x.DSPId,
                        principalTable: "DSPs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrackDistributions_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "Country", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "USA", "ava.stone@example.com", "Ava Stone" },
                    { 2, "UK", "liam.brooks@example.com", "Liam Brooks" },
                    { 3, "Italy", "sofia.rossi@example.com", "Sofia Rossi" }
                });

            migrationBuilder.InsertData(
                table: "DSPs",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Spotify" },
                    { 2, "Apple Music" },
                    { 3, "Amazon Music" }
                });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "ArtistId", "Genre", "ISRC", "ReleaseDate", "Status", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Pop", "USRC17600001", new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Distributed", "Midnight Drive" },
                    { 2, 1, "Pop", "USRC17600002", new DateTime(2025, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Submitted", "Golden Hour" },
                    { 3, 1, "Electronic", "USRC17600003", new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Draft", "Neon Skyline" },
                    { 4, 2, "Rock", "GBRC17600004", new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Distributed", "Silver Rain" },
                    { 5, 2, "Rock", "GBRC17600005", new DateTime(2025, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Draft", "Broken Compass" },
                    { 6, 2, "Indie", "GBRC17600006", new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Submitted", "Paper Boats" },
                    { 7, 3, "Jazz", "ITRC17600007", new DateTime(2025, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Draft", "Luna Nera" },
                    { 8, 3, "Jazz", "ITRC17600008", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Distributed", "Vento del Sud" }
                });

            migrationBuilder.InsertData(
                table: "TrackDistributions",
                columns: new[] { "Id", "DSPId", "Status", "SubmittedAt", "TrackId" },
                values: new object[,]
                {
                    { 1, 1, "Live", new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 2, "Live", new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, 1, "Pending", new DateTime(2025, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 4, 1, "Live", new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 5, 3, "Live", new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 6, 2, "Pending", new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 7, 1, "Live", new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 8, 2, "Live", new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 9, 3, "Rejected", new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artists_Email",
                table: "Artists",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrackDistributions_DSPId",
                table: "TrackDistributions",
                column: "DSPId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackDistributions_TrackId_DSPId",
                table: "TrackDistributions",
                columns: new[] { "TrackId", "DSPId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_ArtistId",
                table: "Tracks",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_ISRC",
                table: "Tracks",
                column: "ISRC",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackDistributions");

            migrationBuilder.DropTable(
                name: "DSPs");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Artists");
        }
    }
}

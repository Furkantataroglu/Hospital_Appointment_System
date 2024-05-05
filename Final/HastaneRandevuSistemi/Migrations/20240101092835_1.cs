using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HastaneRandevuSistemi.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BolumTablosu",
                columns: table => new
                {
                    BolumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BolumAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BolumTablosu", x => x.BolumId);
                });

            migrationBuilder.CreateTable(
                name: "doktorCalismaSaatlariModeliTablosu",
                columns: table => new
                {
                    DCSMId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoktorId = table.Column<int>(type: "int", nullable: false),
                    DCSMBaslangicSaati = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DCSMBitisSaati = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DCSMcalismaTarihi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doktorCalismaSaatlariModeliTablosu", x => x.DCSMId);
                });

            migrationBuilder.CreateTable(
                name: "KullaniciTablosu",
                columns: table => new
                {
                    KullaniciId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciAd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KullaniciSoyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KullaniciTcNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    KullaniciDogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KullaniciTelefon = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    KullaniciMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciCinsiyet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciSifre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciTablosu", x => x.KullaniciId);
                });

            migrationBuilder.CreateTable(
                name: "RandevuTablosu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HastaTcNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HastaAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HastaSoyadi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoktorNO = table.Column<int>(type: "int", nullable: false),
                    DoktorAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoktorSoyadi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gun = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Saat = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RandevuTablosu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SehirTablosu",
                columns: table => new
                {
                    SehirId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SehirAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SehirTablosu", x => x.SehirId);
                });

            migrationBuilder.CreateTable(
                name: "IlceTablosu",
                columns: table => new
                {
                    ilceid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sehirid = table.Column<int>(type: "int", nullable: false),
                    IlceAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IlceTablosu", x => x.ilceid);
                    table.ForeignKey(
                        name: "FK_IlceTablosu_SehirTablosu_Sehirid",
                        column: x => x.Sehirid,
                        principalTable: "SehirTablosu",
                        principalColumn: "SehirId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HastaneTablosu",
                columns: table => new
                {
                    HastaneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ilceid = table.Column<int>(type: "int", nullable: false),
                    HastaneAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HastaneTablosu", x => x.HastaneId);
                    table.ForeignKey(
                        name: "FK_HastaneTablosu_IlceTablosu_ilceid",
                        column: x => x.ilceid,
                        principalTable: "IlceTablosu",
                        principalColumn: "ilceid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoktorTablosu",
                columns: table => new
                {
                    DoktorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BolumId = table.Column<int>(type: "int", nullable: false),
                    hastanelerId = table.Column<int>(type: "int", nullable: false),
                    DoktorAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DoktorSoyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DoktorTcNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    DoktorTelefon = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DoktorMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoktorSifre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoktorTablosu", x => x.DoktorId);
                    table.ForeignKey(
                        name: "FK_DoktorTablosu_BolumTablosu_BolumId",
                        column: x => x.BolumId,
                        principalTable: "BolumTablosu",
                        principalColumn: "BolumId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoktorTablosu_HastaneTablosu_hastanelerId",
                        column: x => x.hastanelerId,
                        principalTable: "HastaneTablosu",
                        principalColumn: "HastaneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoktorTablosu_BolumId",
                table: "DoktorTablosu",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_DoktorTablosu_hastanelerId",
                table: "DoktorTablosu",
                column: "hastanelerId");

            migrationBuilder.CreateIndex(
                name: "IX_HastaneTablosu_ilceid",
                table: "HastaneTablosu",
                column: "ilceid");

            migrationBuilder.CreateIndex(
                name: "IX_IlceTablosu_Sehirid",
                table: "IlceTablosu",
                column: "Sehirid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "doktorCalismaSaatlariModeliTablosu");

            migrationBuilder.DropTable(
                name: "DoktorTablosu");

            migrationBuilder.DropTable(
                name: "KullaniciTablosu");

            migrationBuilder.DropTable(
                name: "RandevuTablosu");

            migrationBuilder.DropTable(
                name: "BolumTablosu");

            migrationBuilder.DropTable(
                name: "HastaneTablosu");

            migrationBuilder.DropTable(
                name: "IlceTablosu");

            migrationBuilder.DropTable(
                name: "SehirTablosu");
        }
    }
}

﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAPI.Migrations
{
    public partial class AddingCinemaAndAddressRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Cinemas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cinemas_AddressId",
                table: "Cinemas",
                column: "AddressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_Addresses_AddressId",
                table: "Cinemas",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_Addresses_AddressId",
                table: "Cinemas");

            migrationBuilder.DropIndex(
                name: "IX_Cinemas_AddressId",
                table: "Cinemas");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Cinemas");
        }
    }
}

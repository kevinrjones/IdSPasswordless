﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rsk.AspNetCore.Fido.EntityFramework;

#nullable disable

namespace PasswordlessIdentityServer.Migrations.Fido
{
    [DbContext(typeof(FidoDbContext))]
    [Migration("20240815202850_FidoMigration")]
    partial class FidoMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("Rsk.AspNetCore.Fido.EntityFramework.Entities.FidoKey", b =>
                {
                    b.Property<string>("CredentialId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Algorithm")
                        .HasColumnType("TEXT");

                    b.Property<int>("AttestationType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("AuthenticatorId")
                        .HasColumnType("TEXT");

                    b.Property<int?>("AuthenticatorIdType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Counter")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CredentialAsJson")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayFriendlyName")
                        .HasColumnType("TEXT");

                    b.Property<string>("KeyType")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastUsed")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserHandle")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("CredentialId");

                    b.HasIndex("UserId");

                    b.ToTable("FidoKeys", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}

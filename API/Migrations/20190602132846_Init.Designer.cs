﻿// <auto-generated />
using System;
using API;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(OLXDataContext))]
    [Migration("20190602132846_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API.Models.TableAd", b =>
                {
                    b.Property<int>("AdId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand");

                    b.Property<string>("Fuel");

                    b.Property<int>("HorsePower");

                    b.Property<bool>("IsDamaged");

                    b.Property<string>("Location");

                    b.Property<int>("Mileage");

                    b.Property<string>("Model");

                    b.Property<int>("ProductionYear");

                    b.Property<int?>("TableUserId");

                    b.HasKey("AdId");

                    b.HasIndex("TableUserId");

                    b.ToTable("Ads");
                });

            modelBuilder.Entity("API.Models.TableUser", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("DateOfCreation");

                    b.Property<string>("PhoneNumber");

                    b.Property<decimal>("Price");

                    b.Property<int?>("TableAdId");

                    b.HasKey("UserId");

                    b.HasIndex("TableAdId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Models.TableAd", b =>
                {
                    b.HasOne("API.Models.TableUser", "TableUser")
                        .WithMany()
                        .HasForeignKey("TableUserId");
                });

            modelBuilder.Entity("API.Models.TableUser", b =>
                {
                    b.HasOne("API.Models.TableAd", "TableAd")
                        .WithMany()
                        .HasForeignKey("TableAdId");
                });
#pragma warning restore 612, 618
        }
    }
}

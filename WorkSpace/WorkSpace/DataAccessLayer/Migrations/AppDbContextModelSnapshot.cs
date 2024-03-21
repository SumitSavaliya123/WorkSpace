﻿// <auto-generated />
using System;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.DataModels.Gender", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint")
                        .HasColumnName("id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("PK_Gender_Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("gender", (string)null);

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            Title = "Male"
                        },
                        new
                        {
                            Id = (byte)2,
                            Title = "Female"
                        });
                });

            modelBuilder.Entity("Entities.DataModels.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .HasMaxLength(512)
                        .HasColumnType("varchar")
                        .HasColumnName("address");

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("avatar");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<DateTimeOffset?>("DOB")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("dob");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar")
                        .HasColumnName("email");

                    b.Property<DateTimeOffset?>("ExpiryTime")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("expiry_time");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar")
                        .HasColumnName("first_name");

                    b.Property<byte?>("Gender")
                        .HasColumnType("tinyint")
                        .HasColumnName("gender");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar")
                        .HasColumnName("last_name");

                    b.Property<string>("OTP")
                        .HasMaxLength(6)
                        .HasColumnType("varchar")
                        .HasColumnName("otp");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar")
                        .HasColumnName("password");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(13)
                        .HasColumnType("varchar")
                        .HasColumnName("phone_number");

                    b.Property<byte?>("Role")
                        .HasColumnType("tinyint")
                        .HasColumnName("role");

                    b.Property<byte>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)1)
                        .HasColumnName("status");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("updated_by");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("updated_on");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Gender");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("Entities.DataModels.UserRefreshTokens", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("created_on");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("is_active");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("refresh_token");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("updated_by");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("updated_on");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Id");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("userRefreshToken", (string)null);
                });

            modelBuilder.Entity("Entities.DataModels.User", b =>
                {
                    b.HasOne("Entities.DataModels.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Entities.DataModels.Gender", "Genders")
                        .WithMany()
                        .HasForeignKey("Gender");

                    b.HasOne("Entities.DataModels.User", "UpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdatedBy")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("CreatedByUser");

                    b.Navigation("Genders");

                    b.Navigation("UpdatedByUser");
                });

            modelBuilder.Entity("Entities.DataModels.UserRefreshTokens", b =>
                {
                    b.HasOne("Entities.DataModels.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("Entities.DataModels.User", "UpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.Navigation("CreatedByUser");

                    b.Navigation("UpdatedByUser");
                });
#pragma warning restore 612, 618
        }
    }
}

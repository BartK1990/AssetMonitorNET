﻿// <auto-generated />
using System;
using AssetMonitorDataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AssetMonitorDataAccess.Migrations
{
    [DbContext(typeof(AssetMonitorContext))]
    partial class AssetMonitorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AssetMonitorDataAccess.Models.AgentDataType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("AgentDataType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DataType = "PerformanceCounter"
                        },
                        new
                        {
                            Id = 2,
                            DataType = "WMI"
                        },
                        new
                        {
                            Id = 3,
                            DataType = "ServiceState"
                        });
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.AgentTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AgentDataTypeId")
                        .HasColumnType("int");

                    b.Property<int>("AgentTagSetId")
                        .HasColumnType("int");

                    b.Property<string>("PerformanceCounter")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("ServiceName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Tagname")
                        .IsRequired()
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.Property<int>("ValueDataTypeId")
                        .HasColumnType("int");

                    b.Property<string>("WmiManagementObject")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("AgentDataTypeId");

                    b.HasIndex("AgentTagSetId");

                    b.HasIndex("ValueDataTypeId");

                    b.ToTable("AgentTag");

                    b.HasCheckConstraint("CK_AgentTag_NotNullTagInfo", "([PerformanceCounter] IS NOT NULL) OR ([WmiManagementObject] IS NOT NULL) OR ([ServiceName] IS NOT NULL)");
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.AgentTagSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.ToTable("AgentTagSet");
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.Asset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AgentTagSetId")
                        .HasColumnType("int");

                    b.Property<int>("AssetTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("HttpNodeRedTagSetId")
                        .HasColumnType("int");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("SnmpTagSetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AgentTagSetId");

                    b.HasIndex("AssetTypeId");

                    b.HasIndex("HttpNodeRedTagSetId");

                    b.HasIndex("SnmpTagSetId");

                    b.ToTable("Asset");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AssetTypeId = 1,
                            IpAddress = "127.0.0.1",
                            Name = "AssetMonitorNET Server"
                        });
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.AssetType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("AssetType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "Windows"
                        },
                        new
                        {
                            Id = 2,
                            Type = "SNMP"
                        });
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.HttpNodeRedTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HttpNodeRedTagSetId")
                        .HasColumnType("int");

                    b.Property<string>("HttpTag")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Tagname")
                        .IsRequired()
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.Property<int>("ValueDataTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HttpNodeRedTagSetId");

                    b.HasIndex("ValueDataTypeId");

                    b.ToTable("HttpNodeRedTag");
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.HttpNodeRedTagSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.ToTable("HttpNodeRedTagSet");
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.SnmpOperation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Operation")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("SnmpOperation");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Operation = "Get"
                        });
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.SnmpTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("OID")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("OperationId")
                        .HasColumnType("int");

                    b.Property<int>("SnmpTagSetId")
                        .HasColumnType("int");

                    b.Property<string>("Tagname")
                        .IsRequired()
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.Property<int>("ValueDataTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OperationId");

                    b.HasIndex("SnmpTagSetId");

                    b.HasIndex("ValueDataTypeId");

                    b.ToTable("SnmpTag");
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.SnmpTagSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.ToTable("SnmpTagSet");
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.TagDataType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("TagDataType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "Boolean"
                        },
                        new
                        {
                            Id = 2,
                            Type = "Integer"
                        },
                        new
                        {
                            Id = 3,
                            Type = "Float"
                        },
                        new
                        {
                            Id = 4,
                            Type = "Double"
                        },
                        new
                        {
                            Id = 5,
                            Type = "String"
                        });
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.AgentTag", b =>
                {
                    b.HasOne("AssetMonitorDataAccess.Models.AgentDataType", "AgentDataType")
                        .WithMany()
                        .HasForeignKey("AgentDataTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssetMonitorDataAccess.Models.AgentTagSet", "AgentTagSet")
                        .WithMany()
                        .HasForeignKey("AgentTagSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssetMonitorDataAccess.Models.TagDataType", "ValueDataType")
                        .WithMany()
                        .HasForeignKey("ValueDataTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.Asset", b =>
                {
                    b.HasOne("AssetMonitorDataAccess.Models.AgentTagSet", "AgentTagSet")
                        .WithMany()
                        .HasForeignKey("AgentTagSetId");

                    b.HasOne("AssetMonitorDataAccess.Models.AssetType", "AssetType")
                        .WithMany()
                        .HasForeignKey("AssetTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssetMonitorDataAccess.Models.HttpNodeRedTagSet", "HttpNodeRedTagSet")
                        .WithMany()
                        .HasForeignKey("HttpNodeRedTagSetId");

                    b.HasOne("AssetMonitorDataAccess.Models.SnmpTagSet", "SnmpTagSet")
                        .WithMany()
                        .HasForeignKey("SnmpTagSetId");
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.HttpNodeRedTag", b =>
                {
                    b.HasOne("AssetMonitorDataAccess.Models.HttpNodeRedTagSet", "HttpNodeRedTagSet")
                        .WithMany()
                        .HasForeignKey("HttpNodeRedTagSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssetMonitorDataAccess.Models.TagDataType", "ValueDataType")
                        .WithMany()
                        .HasForeignKey("ValueDataTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.SnmpTag", b =>
                {
                    b.HasOne("AssetMonitorDataAccess.Models.SnmpOperation", "Operation")
                        .WithMany()
                        .HasForeignKey("OperationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssetMonitorDataAccess.Models.SnmpTagSet", "SnmpTagSet")
                        .WithMany()
                        .HasForeignKey("SnmpTagSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssetMonitorDataAccess.Models.TagDataType", "ValueDataType")
                        .WithMany()
                        .HasForeignKey("ValueDataTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

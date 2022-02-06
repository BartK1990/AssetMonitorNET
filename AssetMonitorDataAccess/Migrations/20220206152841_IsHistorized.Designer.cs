﻿// <auto-generated />
using System;
using AssetMonitorDataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AssetMonitorDataAccess.Migrations
{
    [DbContext(typeof(AssetMonitorContext))]
    [Migration("20220206152841_IsHistorized")]
    partial class IsHistorized
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<bool>("IsHistorized")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("PerformanceCounter")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<double>("ScaleFactor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(1.0);

                    b.Property<double>("ScaleOffset")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(0.0);

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

                    b.HasIndex("Id", "Tagname")
                        .IsUnique();

                    b.ToTable("AgentTag");

                    b.HasCheckConstraint("CK_AgentTag_NotNullTagInfo", "[PerformanceCounter] IS NOT NULL OR [WmiManagementObject] IS NOT NULL OR [ServiceName] IS NOT NULL");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AgentDataTypeId = 1,
                            AgentTagSetId = 1,
                            IsHistorized = true,
                            PerformanceCounter = "Processor;% Processor Time;_Total",
                            ScaleFactor = 0.0,
                            ScaleOffset = 0.0,
                            Tagname = "CpuUsage",
                            ValueDataTypeId = 3
                        },
                        new
                        {
                            Id = 2,
                            AgentDataTypeId = 1,
                            AgentTagSetId = 1,
                            IsHistorized = true,
                            PerformanceCounter = "Memory;Available MBytes",
                            ScaleFactor = 0.0,
                            ScaleOffset = 0.0,
                            Tagname = "MemoryAvailable",
                            ValueDataTypeId = 3
                        },
                        new
                        {
                            Id = 3,
                            AgentDataTypeId = 2,
                            AgentTagSetId = 1,
                            IsHistorized = true,
                            ScaleFactor = 0.0,
                            ScaleOffset = 0.0,
                            Tagname = "MemoryTotal",
                            ValueDataTypeId = 4,
                            WmiManagementObject = "TotalPhysicalMemory"
                        },
                        new
                        {
                            Id = 4,
                            AgentDataTypeId = 1,
                            AgentTagSetId = 1,
                            IsHistorized = true,
                            PerformanceCounter = "PhysicalDisk;% Idle Time;_Total",
                            ScaleFactor = 0.0,
                            ScaleOffset = 0.0,
                            Tagname = "PhysicalDiskIdleTime",
                            ValueDataTypeId = 3
                        },
                        new
                        {
                            Id = 5,
                            AgentDataTypeId = 1,
                            AgentTagSetId = 1,
                            IsHistorized = true,
                            PerformanceCounter = "PhysicalDisk;% Disk Time;_Total",
                            ScaleFactor = 0.0,
                            ScaleOffset = 0.0,
                            Tagname = "PhysicalDiskWorkTime",
                            ValueDataTypeId = 3
                        },
                        new
                        {
                            Id = 6,
                            AgentDataTypeId = 1,
                            AgentTagSetId = 1,
                            IsHistorized = true,
                            PerformanceCounter = "LogicalDisk;% Free Space;_Total",
                            ScaleFactor = 0.0,
                            ScaleOffset = 0.0,
                            Tagname = "LogicalDiskFreeSpace",
                            ValueDataTypeId = 3
                        });
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Default"
                        });
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.Asset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AgentTagSetId")
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

                    b.HasIndex("HttpNodeRedTagSetId");

                    b.HasIndex("SnmpTagSetId");

                    b.ToTable("Asset");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AgentTagSetId = 1,
                            IpAddress = "127.0.0.1",
                            Name = "AssetMonitorNET Server",
                            SnmpTagSetId = 1
                        });
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.AssetProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ValueDataTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ValueDataTypeId");

                    b.ToTable("AssetProperty");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Agent TCP Port",
                            Name = "AgentTcpPort",
                            ValueDataTypeId = 2
                        },
                        new
                        {
                            Id = 2,
                            Description = "SNMP UDP Port",
                            Name = "SnmpUdpPort",
                            ValueDataTypeId = 2
                        },
                        new
                        {
                            Id = 3,
                            Description = "SNMP timeout",
                            Name = "SnmpTimeout",
                            ValueDataTypeId = 2
                        },
                        new
                        {
                            Id = 4,
                            Description = "SNMP number of retries",
                            Name = "SnmpRetries",
                            ValueDataTypeId = 2
                        },
                        new
                        {
                            Id = 5,
                            Description = "SNMP Community String",
                            Name = "SnmpCommunity",
                            ValueDataTypeId = 1
                        });
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.AssetPropertyDataType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("AssetPropertyDataType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DataType = "String"
                        },
                        new
                        {
                            Id = 2,
                            DataType = "Integer"
                        },
                        new
                        {
                            Id = 3,
                            DataType = "Double"
                        });
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.AssetPropertyValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<int>("AssetPropertyId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("AssetPropertyId");

                    b.ToTable("AssetPropertyValue");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            AssetId = 1,
                            AssetPropertyId = 2,
                            Value = "161"
                        },
                        new
                        {
                            Id = -2,
                            AssetId = 1,
                            AssetPropertyId = 3,
                            Value = "3000"
                        },
                        new
                        {
                            Id = -3,
                            AssetId = 1,
                            AssetPropertyId = 4,
                            Value = "1"
                        },
                        new
                        {
                            Id = -4,
                            AssetId = 1,
                            AssetPropertyId = 5,
                            Value = "public"
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

                    b.Property<double>("ScaleFactor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(1.0);

                    b.Property<double>("ScaleOffset")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(0.0);

                    b.Property<string>("Tagname")
                        .IsRequired()
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.Property<int>("ValueDataTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HttpNodeRedTagSetId");

                    b.HasIndex("ValueDataTypeId");

                    b.HasIndex("Id", "Tagname")
                        .IsUnique();

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
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

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

                    b.Property<bool>("IsHistorized")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("OID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OperationId")
                        .HasColumnType("int");

                    b.Property<double>("ScaleFactor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(1.0);

                    b.Property<double>("ScaleOffset")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(0.0);

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

                    b.HasIndex("Id", "Tagname")
                        .IsUnique();

                    b.ToTable("SnmpTag");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsHistorized = false,
                            OID = "1.3.6.1.2.1.1.5.0",
                            OperationId = 1,
                            ScaleFactor = 0.0,
                            ScaleOffset = 0.0,
                            SnmpTagSetId = 1,
                            Tagname = "sysName",
                            ValueDataTypeId = 5
                        },
                        new
                        {
                            Id = 2,
                            IsHistorized = false,
                            OID = "1.3.6.1.2.1.1.1.0",
                            OperationId = 1,
                            ScaleFactor = 0.0,
                            ScaleOffset = 0.0,
                            SnmpTagSetId = 1,
                            Tagname = "sysDescr",
                            ValueDataTypeId = 5
                        },
                        new
                        {
                            Id = 3,
                            IsHistorized = false,
                            OID = "1.3.6.1.2.1.1.2.0",
                            OperationId = 1,
                            ScaleFactor = 0.0,
                            ScaleOffset = 0.0,
                            SnmpTagSetId = 1,
                            Tagname = "sysObjectID",
                            ValueDataTypeId = 5
                        },
                        new
                        {
                            Id = 4,
                            IsHistorized = false,
                            OID = "1.3.6.1.2.1.1.3.0",
                            OperationId = 1,
                            ScaleFactor = 0.0,
                            ScaleOffset = 0.0,
                            SnmpTagSetId = 1,
                            Tagname = "sysUpTime",
                            ValueDataTypeId = 5
                        },
                        new
                        {
                            Id = 5,
                            IsHistorized = false,
                            OID = "1.3.6.1.2.1.1.4.0",
                            OperationId = 1,
                            ScaleFactor = 0.0,
                            ScaleOffset = 0.0,
                            SnmpTagSetId = 1,
                            Tagname = "sysContact",
                            ValueDataTypeId = 5
                        });
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

                    b.Property<int>("VersionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VersionId");

                    b.ToTable("SnmpTagSet");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Default",
                            VersionId = 2
                        });
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.SnmpVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("SnmpVersion");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Version = "V1"
                        },
                        new
                        {
                            Id = 2,
                            Version = "V2c"
                        });
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.TagDataType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("TagDataType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DataType = "Boolean"
                        },
                        new
                        {
                            Id = 2,
                            DataType = "Integer"
                        },
                        new
                        {
                            Id = 3,
                            DataType = "Float"
                        },
                        new
                        {
                            Id = 4,
                            DataType = "Double"
                        },
                        new
                        {
                            Id = 5,
                            DataType = "String"
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
                        .WithMany("AgentTag")
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

                    b.HasOne("AssetMonitorDataAccess.Models.HttpNodeRedTagSet", "HttpNodeRedTagSet")
                        .WithMany()
                        .HasForeignKey("HttpNodeRedTagSetId");

                    b.HasOne("AssetMonitorDataAccess.Models.SnmpTagSet", "SnmpTagSet")
                        .WithMany()
                        .HasForeignKey("SnmpTagSetId");
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.AssetProperty", b =>
                {
                    b.HasOne("AssetMonitorDataAccess.Models.AssetPropertyDataType", "ValueDataType")
                        .WithMany()
                        .HasForeignKey("ValueDataTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.AssetPropertyValue", b =>
                {
                    b.HasOne("AssetMonitorDataAccess.Models.Asset", "Asset")
                        .WithMany("AssetPropertyValues")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssetMonitorDataAccess.Models.AssetProperty", "AssetProperty")
                        .WithMany()
                        .HasForeignKey("AssetPropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.HttpNodeRedTag", b =>
                {
                    b.HasOne("AssetMonitorDataAccess.Models.HttpNodeRedTagSet", "HttpNodeRedTagSet")
                        .WithMany("HttpNodeRedTag")
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
                        .WithMany("SnmpTag")
                        .HasForeignKey("SnmpTagSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssetMonitorDataAccess.Models.TagDataType", "ValueDataType")
                        .WithMany()
                        .HasForeignKey("ValueDataTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.SnmpTagSet", b =>
                {
                    b.HasOne("AssetMonitorDataAccess.Models.SnmpVersion", "Version")
                        .WithMany()
                        .HasForeignKey("VersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
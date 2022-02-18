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
    [Migration("20220207215438_TagType_Long")]
    partial class TagType_Long
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

                    b.HasIndex("ValueDataTypeId");

                    b.HasIndex("AgentTagSetId", "Tagname")
                        .IsUnique();

                    b.ToTable("AgentTag");

                    b.HasCheckConstraint("CK_AgentTag_NotNullTagInfo", "[PerformanceCounter] IS NOT NULL OR [WmiManagementObject] IS NOT NULL OR [ServiceName] IS NOT NULL");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AgentDataTypeId = 1,
                            AgentTagSetId = 1,
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

            modelBuilder.Entity("AssetMonitorDataAccess.Models.HistoricalTagConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AgentTagId")
                        .HasColumnType("int");

                    b.Property<int>("HistorizationTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("SnmpTagId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HistorizationTypeId");

                    b.HasIndex("SnmpTagId");

                    b.HasIndex("AgentTagId", "SnmpTagId", "HistorizationTypeId")
                        .IsUnique()
                        .HasFilter("[AgentTagId] IS NOT NULL AND [SnmpTagId] IS NOT NULL");

                    b.ToTable("HistoricalTagConfig");

                    b.HasCheckConstraint("CK_HistorizationTagConfig_AgentOrSnmp", "([AgentTagId] IS NOT NULL AND [SnmpTagId] IS NULL) OR ([AgentTagId] IS NULL AND [SnmpTagId] IS NOT NULL)");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AgentTagId = 1,
                            HistorizationTypeId = 3
                        },
                        new
                        {
                            Id = 2,
                            AgentTagId = 1,
                            HistorizationTypeId = 2
                        },
                        new
                        {
                            Id = 3,
                            AgentTagId = 2,
                            HistorizationTypeId = 3
                        },
                        new
                        {
                            Id = 4,
                            AgentTagId = 3,
                            HistorizationTypeId = 3
                        },
                        new
                        {
                            Id = 5,
                            AgentTagId = 4,
                            HistorizationTypeId = 3
                        },
                        new
                        {
                            Id = 6,
                            AgentTagId = 5,
                            HistorizationTypeId = 3
                        },
                        new
                        {
                            Id = 7,
                            AgentTagId = 6,
                            HistorizationTypeId = 3
                        });
                });

            modelBuilder.Entity("AssetMonitorDataAccess.Models.HistoricalType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HistoricalType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "Last"
                        },
                        new
                        {
                            Id = 2,
                            Type = "Maximum"
                        },
                        new
                        {
                            Id = 3,
                            Type = "Average"
                        },
                        new
                        {
                            Id = 4,
                            Type = "Minimum"
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

            modelBuilder.Entity("AssetMonitorDataAccess.Models.SnmpAssetValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<int>("SnmpTagId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("SnmpTagId");

                    b.ToTable("SnmpAssetValue");
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

                    b.HasIndex("ValueDataTypeId");

                    b.HasIndex("SnmpTagSetId", "Tagname")
                        .IsUnique();

                    b.ToTable("SnmpTag");

                    b.HasData(
                        new
                        {
                            Id = 1,
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
                        },
                        new
                        {
                            Id = 6,
                            DataType = "Long"
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

            modelBuilder.Entity("AssetMonitorDataAccess.Models.HistoricalTagConfig", b =>
                {
                    b.HasOne("AssetMonitorDataAccess.Models.AgentTag", "AgentTag")
                        .WithMany("HistorizationTagConfigs")
                        .HasForeignKey("AgentTagId");

                    b.HasOne("AssetMonitorDataAccess.Models.HistoricalType", "HistorizationType")
                        .WithMany()
                        .HasForeignKey("HistorizationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssetMonitorDataAccess.Models.SnmpTag", "SnmpTag")
                        .WithMany("HistorizationTagConfigs")
                        .HasForeignKey("SnmpTagId");
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

            modelBuilder.Entity("AssetMonitorDataAccess.Models.SnmpAssetValue", b =>
                {
                    b.HasOne("AssetMonitorDataAccess.Models.Asset", "Asset")
                        .WithMany()
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssetMonitorDataAccess.Models.SnmpTag", "SnmpTag")
                        .WithMany()
                        .HasForeignKey("SnmpTagId")
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
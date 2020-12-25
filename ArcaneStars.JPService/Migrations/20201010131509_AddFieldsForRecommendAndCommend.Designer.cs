﻿// <auto-generated />
using System;
using ArcaneStars.Service.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ArcaneStars.JPService.Migrations
{
    [DbContext(typeof(ServiceDbContext))]
    [Migration("20201010131509_AddFieldsForRecommendAndCommend")]
    partial class AddFieldsForRecommendAndCommend
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ArcaneStars.JPService.Domains.Aggregates.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnName("created_by")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("created_on")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Experience")
                        .HasColumnName("experience")
                        .HasColumnType("varchar(2000) CHARACTER SET utf8mb4")
                        .HasMaxLength(2000);

                    b.Property<long>("RecommendId")
                        .HasColumnName("recommend_id")
                        .HasColumnType("bigint");

                    b.Property<string>("Remark")
                        .HasColumnName("remark")
                        .HasColumnType("varchar(1000) CHARACTER SET utf8mb4")
                        .HasMaxLength(1000);

                    b.Property<int>("Suggestion")
                        .HasColumnName("suggestion")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                        .HasMaxLength(200);

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("updated_by")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnName("updated_on")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("comments");
                });

            modelBuilder.Entity("ArcaneStars.JPService.Domains.Aggregates.Question", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnName("created_by")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("created_on")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Remark")
                        .HasColumnName("remark")
                        .HasColumnType("varchar(1000) CHARACTER SET utf8mb4")
                        .HasMaxLength(1000);

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnName("subject")
                        .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                        .HasMaxLength(200);

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("updated_by")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnName("updated_on")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("questions");
                });

            modelBuilder.Entity("ArcaneStars.JPService.Domains.Aggregates.QuestionTag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.Property<long>("QuestionId")
                        .HasColumnName("question_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("question_tags");
                });

            modelBuilder.Entity("ArcaneStars.JPService.Domains.Aggregates.Recommend", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnName("created_by")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("created_on")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("varchar(2000) CHARACTER SET utf8mb4")
                        .HasMaxLength(2000);

                    b.Property<string>("GetUrl")
                        .HasColumnName("get_url")
                        .HasColumnType("varchar(1000) CHARACTER SET utf8mb4")
                        .HasMaxLength(1000);

                    b.Property<decimal?>("Price")
                        .HasColumnName("price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<long>("QuestionId")
                        .HasColumnName("question_id")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                        .HasMaxLength(200);

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("updated_by")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnName("updated_on")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("recommends");
                });

            modelBuilder.Entity("ArcaneStars.JPService.Domains.Aggregates.RecommendMedia", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnName("created_by")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("created_on")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MediaType")
                        .HasColumnName("media_type")
                        .HasColumnType("int");

                    b.Property<long>("RecommendId")
                        .HasColumnName("recommend_id")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnName("title")
                        .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                        .HasMaxLength(200);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnName("url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("recommend_medias");
                });

            modelBuilder.Entity("ArcaneStars.JPService.Domains.Aggregates.RecommendSpec", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnName("created_by")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("created_on")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.Property<long>("RecommendId")
                        .HasColumnName("recommend_id")
                        .HasColumnType("bigint");

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("updated_by")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnName("updated_on")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnName("value")
                        .HasColumnType("varchar(1000) CHARACTER SET utf8mb4")
                        .HasMaxLength(1000);

                    b.HasKey("Id");

                    b.ToTable("recommend_specs");
                });
#pragma warning restore 612, 618
        }
    }
}
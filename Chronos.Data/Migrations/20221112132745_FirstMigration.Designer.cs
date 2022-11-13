﻿// <auto-generated />
using System;
using Chronos.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Chronos.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221112132745_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Chronos.Domain.Entities.Projeto", b =>
                {
                    b.Property<int>("ProjetoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjetoId"), 1L, 1);

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjetoId");

                    b.ToTable("Projetos");
                });

            modelBuilder.Entity("Chronos.Domain.Entities.Tarefa", b =>
                {
                    b.Property<int>("TarefaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TarefaId"), 1L, 1);

                    b.Property<DateTime?>("DataFinal")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicial")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("TotalHoras")
                        .HasColumnType("time");

                    b.Property<int>("Usuario_ProjetoId")
                        .HasColumnType("int");

                    b.HasKey("TarefaId");

                    b.HasIndex("Usuario_ProjetoId");

                    b.ToTable("Tarefas");
                });

            modelBuilder.Entity("Chronos.Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioId"), 1L, 1);

                    b.Property<string>("ConfirmacaoToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Confirmado")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Permissao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResetSenhaToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetSenhaVencimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Chronos.Domain.Entities.Usuario_Projeto", b =>
                {
                    b.Property<int>("Usuario_ProjetoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Usuario_ProjetoId"), 1L, 1);

                    b.Property<int>("ProjetoId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Usuario_ProjetoId");

                    b.HasIndex("ProjetoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Usuarios_Projetos");
                });

            modelBuilder.Entity("Chronos.Domain.Entities.Tarefa", b =>
                {
                    b.HasOne("Chronos.Domain.Entities.Usuario_Projeto", "Usuario_Projeto")
                        .WithMany()
                        .HasForeignKey("Usuario_ProjetoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario_Projeto");
                });

            modelBuilder.Entity("Chronos.Domain.Entities.Usuario_Projeto", b =>
                {
                    b.HasOne("Chronos.Domain.Entities.Projeto", "Projeto")
                        .WithMany("Usuarios")
                        .HasForeignKey("ProjetoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chronos.Domain.Entities.Usuario", "Usuario")
                        .WithMany("Projetos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Projeto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Chronos.Domain.Entities.Projeto", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Chronos.Domain.Entities.Usuario", b =>
                {
                    b.Navigation("Projetos");
                });
#pragma warning restore 612, 618
        }
    }
}
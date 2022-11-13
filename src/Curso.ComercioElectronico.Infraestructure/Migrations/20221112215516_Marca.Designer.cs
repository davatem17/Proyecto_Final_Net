﻿// <auto-generated />
using Curso.ComercioElectronico.Infraestructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Curso.ComercioElectronico.Infraestructure.Migrations
{
    [DbContext(typeof(ComercioElectronicoDbContext))]
    [Migration("20221112215516_Marca")]
    partial class Marca
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("Curso.ComercioElectronico.Domain.Marca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("TEXT");

                    b.Property<string>("PaisOrigen")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("TEXT");

                    b.Property<bool>("PresenciaInternacional")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Marcas");
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using MovieAngularJsApp.Models;

namespace MovieAngularJsApp.Migrations
{
    [DbContext(typeof(MoviesAppContext))]
    [Migration("20160112112900_addedMovieProperties")]
    partial class addedMovieProperties
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MovieAngularJsApp.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Director")
                        .IsRequired();

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<decimal>("TicketPrice");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");
                });
        }
    }
}

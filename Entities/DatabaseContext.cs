using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolokwium2.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace kolokwium2.Entities
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<MusicianTrack> MusicianTracks { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<MusicLabel> MusicLabels { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Musician>(e =>{
                e.ToTable("Musician");
                e.HasKey(e => e.IdMusician);
                e.Property(e => e.FirstName).HasMaxLength(30).IsRequired();
                e.Property(e => e.LastName).HasMaxLength(50).IsRequired();
                e.Property(e => e.Nickname).HasMaxLength(20);

                e.HasData(
                    new Musician{
                        IdMusician = 1,
                        FirstName = "Mikolaj",
                        LastName = "Glowala"
                    }
                );
            });

            modelBuilder.Entity<MusicLabel>(e =>{
                e.ToTable("MusicLabel");
                e.HasKey(e => e.IdMusicLabel);
                e.Property(e => e.Name).HasMaxLength(50).IsRequired();

                e.HasData(
                    new MusicLabel{
                        IdMusicLabel = 1,
                        Name = "Fajnanazwa"
                    }
                );
            });

           modelBuilder.Entity<Album>(e =>{
                e.ToTable("Album");
                e.HasKey(e => e.IdAlbum);
                e.Property(e => e.AlbumName).HasMaxLength(30).IsRequired();
                e.Property(e => e.PublishDate).IsRequired();
                e.HasOne(e => e.MusicLabel).WithMany(e => e.Albums).HasForeignKey(e => e.IdMusicLabel).OnDelete(DeleteBehavior.ClientSetNull);

                e.HasData(
                    new Album{
                        IdAlbum = 1,
                        AlbumName = "nazwa",
                        PublishDate = new System.DateTime(2022, 5, 4),
                        IdMusicLabel = 1
                    }
                );
            });

           modelBuilder.Entity<Track>(e =>{
                e.ToTable("Track");
                e.HasKey(e => e.IdTrack);
                e.Property(e => e.TrackName).HasMaxLength(20).IsRequired();
                e.Property(e => e.Duration).IsRequired();
                e.HasOne(e => e.Album).WithMany(e => e.Tracks).HasForeignKey(e => e.IdMusicAlbum).OnDelete(DeleteBehavior.ClientSetNull);

                e.HasData(
                    new Track{
                        IdTrack = 1,
                        TrackName = "Fajnytrack",
                        Duration = 20,
                        IdMusicAlbum = 1
                    }
                );
            });                           
            
            modelBuilder.Entity<MusicianTrack>(e =>
            {
                e.ToTable("Musician_Track");
                e.HasKey(e => new {e.IdTrack, e.IdMusician});
                e.HasOne(e => e.Track).WithMany(e => e.MusicianTracks).HasForeignKey(e => e.IdTrack).OnDelete(DeleteBehavior.ClientSetNull);
                e.HasOne(e => e.Musician).WithMany(e => e.MusicianTracks).HasForeignKey(e => e.IdMusician).OnDelete(DeleteBehavior.ClientSetNull);

                e.HasData(
                    new MusicianTrack
                    {
                        IdTrack = 1,
                        IdMusician = 1
                    }
                );

            });            

        }
        
    }
}
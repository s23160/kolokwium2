using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolokwium2.Entities;
using kolokwium2.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace kolokwium2.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly DatabaseContext _context;
        public DatabaseService(DatabaseContext context)
        {
            _context=context;
        }

        public async Task DeleteAsync(int idMusician)
        {
            _context.Musicians.Remove(await _context.Musicians.Where(e => e.IdMusician == idMusician).FirstOrDefaultAsync());
        }

        public IQueryable<Album> GetAlbumById(int idAlbum)
        {
            return _context.Albums
                .Where(e => e.IdAlbum == idAlbum)
                .Include(e => e.Tracks.OrderBy(e => e.Duration));
        }

        public IQueryable<Musician> GetMusicianById(int idMusician)
        {
            return _context.Musicians.Where(e => e.IdMusician == idMusician);
        }

        public async Task<bool> TrackExistInAlbum(int idMusician)
        {
            var exists = await _context.Tracks
                    .Where(e => e.IdMusicAlbum == null)
                    .Include(e => e.MusicianTracks.Where(e=> e.IdMusician == idMusician))
                    .FirstOrDefaultAsync();
            if(exists == null){
                return false;
            }
            else{
                return true;
            }

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolokwium2.Entities.Models;

namespace kolokwium2.Services
{
    public interface IDatabaseService
    {
        public Task DeleteAsync(int idMusician);
        public Task SaveChangesAsync();
        public IQueryable<Album> GetAlbumById(int idAlbum);
        public IQueryable<Musician> GetMusicianById(int idMusician);
        public Task<bool> TrackExistInAlbum (int idMusician);
    }
}
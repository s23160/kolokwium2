using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolokwium2.DTOs;
using kolokwium2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kolokwium2.Controllers
{
    [Route("api/album")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IDatabaseService _service;

        public AlbumController(IDatabaseService service)
        {
            _service=service;
        }

        [HttpGet("{idAlbum}")]
        public async Task<IActionResult> GetAlbum(int idAlbum)
        {
            return Ok(
                await _service.GetAlbumById(idAlbum)
                .Select(e =>
                    new AlbumGet{
                        IdAlbum = e.IdAlbum,
                        AlbumName = e.AlbumName,
                        PublishDate = e.PublishDate,
                        Tracks = e.Tracks.Select(e => new TrackGet{
                            IdTrack = e.IdTrack,
                            TrackName = e.TrackName,
                            Duration = e.Duration
                        }).ToList()

                    }).ToListAsync()
                );
        }

    }
}
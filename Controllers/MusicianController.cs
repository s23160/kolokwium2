using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolokwium2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kolokwium2.Controllers
{
    [Route("api/musician")]
    [ApiController]
    public class MusicianController : ControllerBase
    {
        private readonly IDatabaseService _service;

        public MusicianController(IDatabaseService service)
        {
            _service=service;
        }

        [HttpDelete("{idMusician}")]
        public async Task<IActionResult> DeleteMusician(int idMusician){
            if(await _service.GetMusicianById(idMusician).FirstOrDefaultAsync() is null){
                return NotFound("Nie znalezono muzyka o podanym id");
            }
            return Ok("Poprawnie usunieto muzyka");

            if(await _service.TrackExistInAlbum(idMusician)){
                return BadRequest("Muzyk ma utwory w albumie");
            }
            else{
                await _service.DeleteAsync(idMusician);
                await _service.SaveChangesAsync();
                return Ok("Poprawnie usunieto");
            }

        }
    }
}
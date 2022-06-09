using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolokwium2.DTOs
{
    public class AlbumGet
    {
        public int IdAlbum { get; set; }
        public string AlbumName { get; set; }
        public DateTime PublishDate { get; set; }
        public List<TrackGet> Tracks { get; set; }
    }

    public class TrackGet{
        public int IdTrack { get; set; }
        public string TrackName { get; set; }
        public int Duration { get; set; }
    }
}
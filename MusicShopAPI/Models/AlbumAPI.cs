using MusicShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicShopAPI.Models
{
    public class AlbumAPI : Album
    {
        public new string Genre { get; set; }
        public new string ListArtist { get; set; }
        public new string ListMusic { get; set; }

        
        public AlbumAPI()
        {

        }

        public AlbumAPI(Album album)
        {
            AlbumId = album.AlbumId;
            Title = album.Title;
            Description = album.Description;
            Date = album.Date;
            Genre = album.GetGenre();
            ListArtist = album.listeArtistes();
            ListMusic = album.listeMusiques();
            Cover = album.Cover;
            Price = album.Price;
        }
    }
}
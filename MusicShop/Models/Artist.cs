using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicShop.Models
{
    public class Artist //: IEnumerable
    {
        private int artistId;
        private string artistName;
        
        private string description;

        private List<Music> listMusic;
        private List<Album> listAlbum;
        
        [Key]
        public int ArtistId { get; set; }
        [Required]
        [Display(Name = "Nom d'artiste")]
        public string ArtistName { get; set; }

        public string Description { get; set; }
        [Display(Name = "Liste de musiques")]
        public virtual List<Music> ListMusic { get ; set ; }
        [Display(Name = "Liste d'albums")]
        public virtual List<Album> ListAlbum { get ; set ; }

        public Artist()
        {
            ListMusic = new List<Music>();
            ListAlbum = new List<Album>();
        }

        //public IEnumerator GetEnumerator()
        //{
        //    return GetEnumerator();
        //}
    }
}
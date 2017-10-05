using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicShop.Models
{
    public class Music //: IEnumerable
    {
        private int musicId;
        private string title;
        private int duration;
        private List<Artist> listArtist;
        private List<Album> listAlbum;

        [Key]
        public int MusicId { get; set; }
        [Required]
        [Display(Name = "Titre")]
        public string Title { get; set; }
        [Display(Name = "Durée (en secondes)")]
        public int Duration { get; set; }
        [Display(Name = "Liste d'artistes")]
        public virtual List<Artist> ListArtist { get; set; }
        [Display(Name = "Liste d'albums")]
        public virtual List<Album> ListAlbum { get; set; }

        public Music()
        {
            ListArtist = new List<Artist>();
            ListAlbum = new List<Album>();
        }

        //public IEnumerator GetEnumerator()
        //{
        //    return GetEnumerator();
        //}
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicShop.Models
{
    public class Album
    {
        private int albumId;
        private string title;
        private string description;
        private DateTime date;
        private Genre genre;
        private List<Artist> listArtist;
        private List<Music> listMusic;
        private string cover;
        private decimal price;

        [Key]
        public int AlbumId { get ; set ; }
        [Required]
        [Display(Name = "Titre de l'album")]
        public string Title { get; set; }
        
        public string Description { get; set; }
        [Display(Name = "Date de sortie")]
        public DateTime Date { get; set; }
        public Genre Genre { get; set; }
        [Display(Name = "Liste d'artistes")]
        public virtual List<Artist> ListArtist { get; set; }
        [Display(Name = "Liste de musiques")]
        public virtual List<Music> ListMusic { get; set; }
        [Display(Name = "Jaquette")]
        public string Cover { get; set; }
        [Display(Name = "Prix")]
        public decimal Price { get; set; }

        public Album()
        {
            ListArtist = new List<Artist>();
            ListMusic = new List<Music>();
        }

        public string listeArtistes()
        {
            string result = "";
            foreach(Artist artist in ListArtist)
            {
                result += artist.ArtistName + " ";
            }
            return result;
        }

        public string listeMusiques()
        {
            string result = "";
            foreach (Music music in ListMusic)
            {
                result += music.Title + " ";
            }
            return result;
        }

        public string GetGenre()
        {
            try
            {
                return Genre.Name;
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
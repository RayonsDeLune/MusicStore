using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicShop.Models
{
    public class Genre //: IEnumerable
    {
        private int genreId;
        private string name;
        private Genre parent;

        [Key]
        public int GenreId { get; set; }
        [Required]
        [Display(Name = "Nom")]
        public string Name { get; set; }
        public Genre Parent { get; set; }

        public Genre()
        { }

        //public IEnumerator GetEnumerator()
        //{
        //    return GetEnumerator();
        //}
    }
}
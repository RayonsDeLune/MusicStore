using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicShop.Models
{
    public class ItemCart : IEnumerable
    {
        private int itemCartId;
        private Album album;
        private int quantite;
        private decimal totalPrice;
        private decimal unitPrice;

        [Key]
        public int ItemCartId { get; set; }
        [Required]
        public Album Album { get; set; }
        [Required]
        [Display(Name = "Quantité")]
        public int Quantite { get; set; }
        [Display(Name = "Prix total")]
        public decimal TotalPrice { get; set; }
        [Display(Name ="Prix unitaire")]
        public decimal UnitPrice { get; set; }
        [Display(Name = "Panier")]
        public ShoppingCart ShoppingCart { get; set; }
        [Display(Name = "Commande")]
        public Order Order { get; set; }

        public ItemCart()
        {

        }

        public IEnumerator GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
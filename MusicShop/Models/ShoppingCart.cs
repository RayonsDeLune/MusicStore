using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicShop.Models
{
    public class ShoppingCart
    {
        private int shoppingCartId;
        private User user;
        private DateTime date;
        private List<ItemCart> listItemCart;

        [Key]
        public int ShoppingCartId { get; set; }
        [Display(Name = "Client")]
        public User User { get; set; }
        [Display(Name = "Date de création du panier")]
        public DateTime Date { get; set; }
        [Display(Name = "Liste d'articles")]
        public virtual List<ItemCart> ListItemCart { get; set; }

        public ShoppingCart()
        {
            ListItemCart = new List<ItemCart>();
        }

        public decimal getMontantTotal()
        {
            decimal total = 0;
            foreach(ItemCart item in ListItemCart)
            {
                total += item.TotalPrice;
            }
            return total;
        }

        public int getNombreArticles()
        {
            int nb = 0;
            foreach (ItemCart item in ListItemCart)
            {
                nb += item.Quantite;
            }
            return nb;
        }
    }
}
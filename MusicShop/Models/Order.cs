using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicShop.Models
{
    public class Order
    {
        private int orderId;
        private User user;
        private DateTime date;
        private List<ItemCart> listItemCart;
        private PaymentType payment;

        [Key]
        public int OrderId { get; set; }
        [Display(Name = "Client")]
        public User User { get; set; }
        [Display(Name = "Date de commande")]
        public DateTime Date { get; set; }
        [Display(Name = "Liste de produits")]
        public virtual List<ItemCart> ListItemCart { get; set; }
        [Display(Name = "Paiement")]
        public PaymentType Payment { get; set; }

        public Order()
        {
            ListItemCart = new List<ItemCart>();
        }

        public enum PaymentType
        {
            cheque,
            paypal,
            virement
        }
    }
}
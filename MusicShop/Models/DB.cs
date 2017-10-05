using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using System.Web;

namespace MusicShop.Models
{
    public class DB : DbContext
    {

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ItemCart> ItemCarts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Music> Musics { get; set; }

        public DB() : base("shop")
        { }
    }
}
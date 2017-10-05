using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicShop.Models
{
    public class User
    {
        private int userId;
        private string firstName;
        private string lastName;
        private DateTime birthDate;
        private string email;
        private string password;
        private string address;
        private string telephon;
        private RoleUser role;

        [Key]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Prénom")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Nom")]
        public string LastName { get; set; }
        [Display(Name = "Date de naissance")]
        public DateTime BirthDate { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adresse email")]
        public string Email { get; set; }
        [Required]
        [MinLength(5)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }
        [Display(Name = "Adresse postale")]
        public string Address { get; set; }
        [Display(Name = "Téléphone")]
        public string Telephon { get; set; }
        [Required]
        [Display(Name = "Rôle")]
        public RoleUser Role { get; set; }

        public User()
        { }

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public enum RoleUser
        {
            admin,
            client
        }
    }
}
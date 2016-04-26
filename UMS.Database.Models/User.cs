using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Database.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(254)]
        [Index(IsUnique = true)]
        [Required]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Salt { get; set; }

        public User() { }
        public User(string email, string firstName, string lastName, string password, string salt)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Salt = salt;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; } 
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } =  Array.Empty<byte>();
        public List<Character>? Characters { get; set; }
    }
}
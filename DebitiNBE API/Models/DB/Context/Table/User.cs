using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebitiNBE_API.Models.DB
{
    [Table("user")]
    public partial class User
    {
        public User()
        {
            FriendlistUser = new HashSet<Friendlist>();
            FriendlistUserId1Navigation = new HashSet<Friendlist>();
            RequestIdMandanteNavigation = new HashSet<Request>();
            RequestIdRiceventeNavigation = new HashSet<Request>();
        }

        [Key]
        [Column("ID", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [StringLength(45)]
        public string Email { get; set; }
        [Required]
        [StringLength(45)]
        public string Username { get; set; }
        [Required]
        [StringLength(45)]
        public string Password { get; set; }
        [StringLength(45)]
        public string Name { get; set; }
        [StringLength(45)]
        public string Lastname { get; set; }

        [InverseProperty(nameof(Friendlist.User))]
        public virtual ICollection<Friendlist> FriendlistUser { get; set; }
        [InverseProperty(nameof(Friendlist.UserId1Navigation))]
        public virtual ICollection<Friendlist> FriendlistUserId1Navigation { get; set; }
        [InverseProperty(nameof(Request.IdMandanteNavigation))]
        public virtual ICollection<Request> RequestIdMandanteNavigation { get; set; }
        [InverseProperty(nameof(Request.IdRiceventeNavigation))]
        public virtual ICollection<Request> RequestIdRiceventeNavigation { get; set; }
    }
}

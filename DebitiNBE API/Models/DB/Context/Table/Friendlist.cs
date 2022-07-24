using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebitiNBE_API.Models.DB
{
    [Table("friendlist")]
    public partial class Friendlist
    {
        [Key]
        [Column("user_ID", TypeName = "int(11)")]
        public int UserId { get; set; }
        [Key]
        [Column("user_ID1", TypeName = "int(11)")]
        public int UserId1 { get; set; }
        [Required]
        [Column(TypeName = "enum('active','accepted','ignored','blocked')")]
        public string Stato { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("FriendlistUser")]
        public virtual User User { get; set; }
        [ForeignKey(nameof(UserId1))]
        [InverseProperty("FriendlistUserId1Navigation")]
        public virtual User UserId1Navigation { get; set; }
    }
}

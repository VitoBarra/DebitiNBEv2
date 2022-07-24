using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebitiNBE_API.Models.DB
{
    [Table("paymentrequest")]
    public partial class Paymentrequest
    {
        [Key]
        [Column("ID", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("Request_ID", TypeName = "int(11)")]
        public int RequestId { get; set; }
        public float Credito { get; set; }
        [Required]
        [Column(TypeName = "enum('active','accepted','dennied')")]
        public string Stato { get; set; }

        [ForeignKey(nameof(RequestId))]
        [InverseProperty("Paymentrequest")]
        public virtual Request Request { get; set; }
    }
}

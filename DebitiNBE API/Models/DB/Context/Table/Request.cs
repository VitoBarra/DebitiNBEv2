using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebitiNBE_API.Models.DB
{
    [Table("request")]
    public partial class Request
    {
        public Request()
        {
            Paymentrequest = new HashSet<Paymentrequest>();
        }

        [Key]
        [Column("ID", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("ID_mandante", TypeName = "int(11)")]
        public int IdMandante { get; set; }
        [Column("ID_ricevente", TypeName = "int(11)")]
        public int IdRicevente { get; set; }
        public float Credito { get; set; }
        [Required]
        [Column(TypeName = "enum('waiting','accepted','dennied','completed')")]
        public string Stato { get; set; }

        [ForeignKey(nameof(IdMandante))]
        [InverseProperty(nameof(User.RequestIdMandanteNavigation))]
        public virtual User IdMandanteNavigation { get; set; }
        [ForeignKey(nameof(IdRicevente))]
        [InverseProperty(nameof(User.RequestIdRiceventeNavigation))]
        public virtual User IdRiceventeNavigation { get; set; }
        [InverseProperty("Request")]
        public virtual ICollection<Paymentrequest> Paymentrequest { get; set; }
    }
}

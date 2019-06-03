using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class TableUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? UserId { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Price { get; set; }

        public DateTimeOffset DateOfCreation { get; set; }
        [ForeignKey("TableAdId")]
        public virtual TableAd TableAd { get; set; }
        public int? TableAdId { get; set; }
    }
}

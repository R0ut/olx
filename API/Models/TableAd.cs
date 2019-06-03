using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class TableAd
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? AdId { get; set; }

        public string Location { get; set; }

        public string Model { get; set; }

        public string Brand { get; set; }

        public int ProductionYear { get; set; }

        public string Fuel { get; set; }

        public int HorsePower { get; set; }

        public int Mileage { get; set; }

        public bool IsDamaged { get; set; }
      
        [ForeignKey("TableUserId")]
        public virtual TableUser TableUser { get; set; }
        public int? TableUserId { get; set; }
    }
}

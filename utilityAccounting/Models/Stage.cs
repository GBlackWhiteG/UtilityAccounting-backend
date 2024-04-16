using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace utilityAccounting.Models
{
    public class Stage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BuildingId { get; set; }
        public int[] Tariffs { get; set; }
        public int[] Payments { get; set; }
    }
}

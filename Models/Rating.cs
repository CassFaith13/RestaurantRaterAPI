using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantRaterAPI.Models
{
    public class Rating
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [ForeignKey("Restaurant")]
        public int RestaurantID { get; set; }
        [Required]
        public double Score { get; set; }
    }
}
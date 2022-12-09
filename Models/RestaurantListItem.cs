namespace RestaurantRaterAPI.Models
{
    public class RestaurantListItem
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public double AverageScore { get; set; }
    }
}
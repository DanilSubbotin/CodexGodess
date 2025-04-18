namespace CodexGoddess.Models
{
    public class OrderItem
    {
        public int Id { get; set; } 

        public Order Order { get; set; }

        public Cars Cars { get; set; }
    }
}

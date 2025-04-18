using Microsoft.AspNetCore.Identity;
namespace CodexGoddess.Models
{
    public class Order
    {
        public int Id { get; set; }

        public IdentityUser User { get; set; }

        public string Address { get; set; }

        public string Status { get; set; }    

        public int TotalPrice { get; set; }
    }
}

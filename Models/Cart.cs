using Microsoft.AspNetCore.Identity;

namespace CodexGoddess.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public Cars Cars { get; set; }

        public IdentityUser User { get; set; }

        public int Quantity { get; set; }
    }
}

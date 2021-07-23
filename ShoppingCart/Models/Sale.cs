namespace ShoppingCart.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }



    }
}
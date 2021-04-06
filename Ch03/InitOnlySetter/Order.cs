namespace InitOnlySetter
{
    public class Order
    {
        public int OrderId { get; init; }
        public decimal TotalPrice { get; set; }
    }
}
namespace PAL
{
    using System;

    public class CartPAL
    {
        public long ID { get; set; }
        public long UserID { get; set; }
        public long BusinessID { get; set; }
        public int Qty { get; set; }
        public decimal OriginalPrice { get; set; }
        public int DiscountID { get; set; }
        public DateTime PickupFromTime { get; set; }
        public DateTime PickupToTime { get; set; }
        public decimal Donation { get; set; }
        public DateTime CreatedOn { get; set; }
        public string TransactionId { get; set; }
        public string Currency { get; set; }
        public string CustomerId { get; set; }
        public string CardType { get; set; }
        public string CardLastDigits { get; set; }
        public string TransactionResponse { get; set; }
    }
}


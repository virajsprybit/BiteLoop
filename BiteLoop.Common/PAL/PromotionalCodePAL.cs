namespace BiteLoop.Common
{
    using System;
    using System.Runtime.CompilerServices;
    public class PromotionalCodePAL
    {
        public int ID { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public int DiscountType { get; set; }
        public DateTime CouponStartTime { get; set; }
        public DateTime CouponEndTime { get; set; }
        public int State { get; set; }
        public bool SingleUse { get; set; }
        public int ReturnVal { get; set; }
        public decimal MinOrderAmount { get; set; }
        
    }
}

using System;
using Models.Identity;

namespace Models
{
    public class Availability
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsFree { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerCountry { get; set; }
        public string CustomerRegion { get; set; }
        public string CustomerCity { get; set; }
        public string Observations { get; set; }
        public string Notes { get; set; }
        public DateTime CompletedDate { get; set; }
        public bool IsCompleted { get; set; }

        public int? VoucherId { get; set; }
        public Voucher Voucher { get; set; }

        public byte[] RowVersion { get; set; }
    }
}

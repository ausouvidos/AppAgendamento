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

        public byte[] RowVersion { get; set; }
    }
}

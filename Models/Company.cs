using System;
using System.Collections.Generic;

namespace Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public DateTime Created { get; set; }
        public Status Status { get; set; }


        public byte[] RowVersion { get; set; }

        public ICollection<Voucher> Vouchers { get; set; }
    }
}

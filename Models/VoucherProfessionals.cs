using System;
using System.Collections.Generic;

namespace Models
{
    public class VoucherProfessionals
    {

        public int Id { get; set; }

        public int VoucherId { get; set; }
        public Voucher Voucher { get; set; }

        public Guid UserId { get; set; }
        public Identity.User User { get; set; }

    }
}

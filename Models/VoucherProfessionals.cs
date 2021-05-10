using System;
using System.Collections.Generic;

namespace Models
{
    public class VoucherProfessionals
    {

        public int Id { get; set; }

        public int VuocherId { get; set; }
        public Voucher Voucher { get; set; }

        public int ProfessionalId { get; set; }

    }
}

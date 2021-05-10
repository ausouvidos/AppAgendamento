using System;
using System.Collections.Generic;

namespace Models
{
    public class Voucher
    {
        public int Id { get; set; }
        public string Token { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public DateTime Created { get; set; }
        public int RemainingRedeemCount { get; set; }
        public byte[] RowVersion { get; set; }

        public ICollection<Availability> Availabilities { get; set; }
        public ICollection<VoucherProfessionals> VoucherProfessionals { get; set; }
    }
}

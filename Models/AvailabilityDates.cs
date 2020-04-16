using System;
using System.Collections.Generic;

namespace Models
{
    public class AvailabilityDates
    {
        public override bool Equals(object obj)
        {
            var item = obj as AvailabilityDates;

            if (item == null)
            {
                return false;
            }

            return item.Start == Start && item.End == End;
        }

        public override int GetHashCode() =>
            Start.GetHashCode() & End.GetHashCode();

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}

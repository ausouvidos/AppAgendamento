using System;

namespace Models.Identity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}

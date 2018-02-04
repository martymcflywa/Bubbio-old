using System;

namespace Bubbio.Domain.Types
{
    public class Baby
    {
        public Guid BabyId { get; set; }
        public Name Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
    }
}
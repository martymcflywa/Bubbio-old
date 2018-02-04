using System;

namespace Bubbio.Core.Types
{
    public class Baby
    {
        public Guid BabyId { get; set; }
        public Name Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
    }
}
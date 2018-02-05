using System;
using Bubbio.Core.Types;

namespace Bubbio.Domain.Tests.Scenarios
{
    internal sealed class BabyBuilder
    {
        private readonly Baby _baby;

        public BabyBuilder()
        {
            _baby = new Baby
            {
                BabyId = Guid.NewGuid(),
                DateOfBirth = new DateTime(2017, 10, 17),
                Name = new Name
                {
                    First = "Damon",
                    Last = "Ponce"
                },
                Gender = Gender.Boy
            };
        }

        public BabyBuilder WithFirstname(string firstname)
        {
            _baby.Name.First = firstname;
            return this;
        }

        public BabyBuilder WithMiddlename(string middlename)
        {
            _baby.Name.Middle = middlename;
            return this;
        }

        public BabyBuilder WithLastname(string lastname)
        {
            _baby.Name.Last = lastname;
            return this;
        }

        public BabyBuilder WithDateOfBirth(DateTime dateOfBirth)
        {
            _baby.DateOfBirth = dateOfBirth;
            return this;
        }

        public Baby Build()
        {
            return _baby;
        }
    }
}
using System;
using Bubbio.Core.Events;
using Bubbio.Core.Events.Enums;

namespace Bubbio.Domain.Tests.Scenarios
{
    internal sealed class BabyBuilder
    {
        private readonly CreateBaby _baby;

        public BabyBuilder()
        {
            _baby = new CreateBaby
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

        public CreateBaby Build()
        {
            return _baby;
        }
    }
}
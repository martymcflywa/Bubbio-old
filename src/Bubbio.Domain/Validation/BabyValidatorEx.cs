using System;
using Bubbio.Core.Events;

namespace Bubbio.Domain.Validation
{
    public static class BabyValidatorEx
    {
        public static CreateBaby Validate(this CreateBaby baby)
        {
            return baby
                .ValidateNames();
        }

        private static CreateBaby ValidateNames(this CreateBaby baby)
        {
            var first = baby.Name.First;
            var middle = baby.Name.Middle;
            var last = baby.Name.Last;

            baby.Name.First = first.Validate();
            baby.Name.Middle = middle.Validate();
            baby.Name.Last = last.Validate();

            if (!baby.Name.First.IsEmpty() && !baby.Name.Last.IsEmpty())
                return baby;

            throw new ArgumentException($"Invalid first name: {first} or last name: {last}");
        }
    }
}
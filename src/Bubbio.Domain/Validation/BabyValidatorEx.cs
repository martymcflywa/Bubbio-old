using System;
using Bubbio.Core.Validation;
using Bubbio.Domain.Types;

namespace Bubbio.Domain.Validation
{
    public static class BabyValidatorEx
    {
        public static Baby Validate(this Baby baby)
        {
            return baby
                .ValidateNames();
        }

        private static Baby ValidateNames(this Baby baby)
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
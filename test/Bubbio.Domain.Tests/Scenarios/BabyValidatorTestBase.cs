using System;
using Bubbio.Core.Events;
using Bubbio.Domain.Validation;
using Xunit;

namespace Bubbio.Domain.Tests.Scenarios
{
    public class BabyValidatorTestBase
    {
        private CreateBaby _babyPreValidation;
        private CreateBaby _babyPostValidation;

        protected string NamePreValidation { get; set; }
        protected string NamePostValidation { get; set; }

        protected void BabyIsValidated()
        {
            try
            {
                _babyPostValidation = _babyPreValidation.Validate();
            }
            catch (ArgumentException){}
        }

        protected void BabyWithFirstname(string name) =>
            _babyPreValidation = new BabyBuilder()
                .WithFirstname(name)
                .Build();

        protected void FirstnameIsFormatted(string expected) =>
            Assert.Equal(expected, _babyPostValidation.Name.First);

        protected void BabyWithMiddlename(string name) =>
            _babyPreValidation = new BabyBuilder()
                .WithMiddlename(name)
                .Build();

        protected void MiddlenameIsFormatted(string expected) =>
            Assert.Equal(expected, _babyPostValidation.Name.Middle);

        protected void BabyWithLastname(string name) =>
            _babyPreValidation = new BabyBuilder()
                .WithLastname(name)
                .Build();

        protected void LastnameIsFormatted(string expected) =>
            Assert.Equal(expected, _babyPostValidation.Name.Last);

        protected void BabyIsNotAccepted() =>
            Assert.Null(_babyPostValidation);
    }
}
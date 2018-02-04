using System;
using Bubbio.Core.Types;
using Bubbio.Domain.Tests.Examples;
using Bubbio.Domain.Tests.Scenarios;
using Bubbio.Domain.Validation;
using TestStack.BDDfy;
using Xunit;

namespace Bubbio.Domain.Tests
{
    public class BabyValidatorTests
    {
        private Baby _babyPreValidation;
        private Baby _babyPostValidation;

        private string NamePreValidation { get; set; }
        private string NamePostValidation { get; set; }

        [Fact]
        public void FormatFirstname()
        {
            this.Given(_ => BabyWithFirstname(NamePreValidation))
                .When(_ => BabyIsValidated())
                .Then(_ => FirstnameIsFormatted(NamePostValidation))
                .WithExamples(Names.RequireFormatting)
                .BDDfy();
        }

        [Fact]
        public void FormatMiddlename()
        {
            this.Given(_ => BabyWithMiddlename(NamePreValidation))
                .When(_ => BabyIsValidated())
                .Then(_ => MiddlenameIsFormatted(NamePostValidation))
                .WithExamples(Names.RequireFormatting)
                .BDDfy();
        }

        [Fact]
        public void FormatLastname()
        {
            this.Given(_ => BabyWithLastname(NamePreValidation))
                .When(_ => BabyIsValidated())
                .Then(_ => LastnameIsFormatted(NamePostValidation))
                .WithExamples(Names.RequireFormatting)
                .BDDfy();
        }

        [Fact]
        public void InvalidFirstname()
        {
            this.Given(_ => BabyWithFirstname(NamePreValidation))
                .When(_ => BabyIsValidated())
                .Then(_ => BabyIsNotAccepted())
                .WithExamples(Names.Invalid)
                .BDDfy();
        }

        [Fact]
        public void InvalidLastname()
        {
            this.Given(_ => BabyWithLastname(NamePreValidation))
                .When(_ => BabyIsValidated())
                .Then(_ => BabyIsNotAccepted())
                .WithExamples(Names.Invalid)
                .BDDfy();
        }

        private void BabyIsValidated()
        {
            try
            {
                _babyPostValidation = _babyPreValidation.Validate();
            }
            catch (ArgumentException){}
        }

        private void BabyWithFirstname(string name) =>
            _babyPreValidation = new BabyBuilder()
                .WithFirstname(name)
                .Build();

        private void FirstnameIsFormatted(string expected) =>
            Assert.Equal(expected, _babyPostValidation.Name.First);

        private void BabyWithMiddlename(string name) =>
            _babyPreValidation = new BabyBuilder()
                .WithMiddlename(name)
                .Build();

        private void MiddlenameIsFormatted(string expected) =>
            Assert.Equal(expected, _babyPostValidation.Name.Middle);

        private void BabyWithLastname(string name) =>
            _babyPreValidation = new BabyBuilder()
                .WithLastname(name)
                .Build();

        private void LastnameIsFormatted(string expected) =>
            Assert.Equal(expected, _babyPostValidation.Name.Last);

        private void BabyIsNotAccepted() =>
            Assert.Null(_babyPostValidation);
    }
}
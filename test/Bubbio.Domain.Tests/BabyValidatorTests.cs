using Bubbio.Domain.Tests.Examples;
using Bubbio.Domain.Tests.Scenarios;
using TestStack.BDDfy;
using Xunit;

namespace Bubbio.Domain.Tests
{
    public class BabyValidatorTests : BabyValidatorTestBase
    {
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
    }
}
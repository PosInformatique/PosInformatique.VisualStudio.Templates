//-----------------------------------------------------------------------
// <copyright file="$safeitemname$.cs" company="$company$">
//     Copyright (c) $company$. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace $rootnamespace$
{
    public class $safeitemname$
    {
        [Fact]
        public void Constructor()
        {
            var exception = new $classnameundertest$();

            exception.Message.Should().Be("Exception of type '$rootnamespace$.$classnameundertest$' was thrown.");
            exception.InnerException.Should().BeNull();
        }

        [Fact]
        public void Constructor_WithMessage()
        {
            var exception = new $classnameundertest$("The message");

            exception.Message.Should().Be("The message");
            exception.InnerException.Should().BeNull();
        }

        [Fact]
        public void Constructor_WithMessageAndInnerException()
        {
            var innerException = new FormatException("The inner exception");
            var exception = new $classnameundertest$("The message", innerException);

            exception.Message.Should().Be("The message");
            exception.InnerException.Should().BeSameAs(innerException);
        }
    }
}
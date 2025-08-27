using System;
using Xunit;
using FluentAssertions;

namespace StringCalculator.Test;

public class InvalidHeaderTests
{
    [Fact]
    public void When_HeaderHasNoNewline_Then_ShouldThrow()
    {
        var calc = new StringCalculator();
        var act = () => calc.Sum("//[***]1***2");
        act.Should().Throw<FormatException>();
    }

    [Fact]
    public void When_HeaderHasUnclosedBracket_Then_ShouldThrow()
    {
        var calc = new StringCalculator();
        var act = () => calc.Sum("//[***\n1***2");
        act.Should().Throw<FormatException>();
    }

    [Fact]
    public void When_HeaderHasEmptyDelimiter_Then_ShouldThrow()
    {
        var calc = new StringCalculator();
        var act = () => calc.Sum("//[]\n1");
        act.Should().Throw<FormatException>();
    }

    [Fact]
    public void When_HeaderHasSingleWithoutBracketsButMoreThanOneChar_Then_ShouldThrow()
    {
        var calc = new StringCalculator();
        var act = () => calc.Sum("//ab\n1ab2");
        act.Should().Throw<FormatException>();
    }

    [Fact]
    public void When_HeaderHasEmptyAndNonEmptyDelimiters_Then_ShouldThrow()
    {
        var calc = new StringCalculator();
        var act = () => calc.Sum("//[][;]\n1;2");
        act.Should().Throw<FormatException>();
    }
}
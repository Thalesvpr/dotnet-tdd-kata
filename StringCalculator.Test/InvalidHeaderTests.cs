using System;
using Xunit;

namespace StringCalculator.Test;

public class InvalidHeaderTests
{
    [Fact]
    public void When_HeaderHasNoNewline_Then_ShouldThrow()
    {
        var calc = new StringCalculator();
        Assert.Throws<FormatException>(() => calc.Sum("//[***]1***2"));
    }

    [Fact]
    public void When_HeaderHasUnclosedBracket_Then_ShouldThrow()
    {
        var calc = new StringCalculator();
        Assert.Throws<FormatException>(() => calc.Sum("//[***\n1***2"));
    }

    [Fact]
    public void When_HeaderHasEmptyDelimiter_Then_ShouldThrow()
    {
        var calc = new StringCalculator();
        Assert.Throws<FormatException>(() => calc.Sum("//[]\n1"));
    }

    [Fact]
    public void When_HeaderHasSingleWithoutBracketsButMoreThanOneChar_Then_ShouldThrow()
    {
        var calc = new StringCalculator();
        Assert.Throws<FormatException>(() => calc.Sum("//ab\n1ab2"));
    }

    [Fact]
    public void When_HeaderHasEmptyAndNonEmptyDelimiters_Then_ShouldThrow()
    {
        var calc = new StringCalculator();
        Assert.Throws<FormatException>(() => calc.Sum("//[][;]\n1;2"));
    }
}
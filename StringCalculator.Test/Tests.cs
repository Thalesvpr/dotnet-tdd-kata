using FluentAssertions;

namespace StringCalculator.Test;

public class Tests
{
    [Fact]
    public void Given_EmptyString_When_Sum_Then_ReturnsZero()
    {
        var calculator = new StringCalculator();
        var result = calculator.Sum("");
        result.Should().Be(0);
    }

    [Fact]
    public void Given_SingleNumberString_When_Sum_Then_ReturnsThatNumber()
    {
        var calculator = new StringCalculator();
        var result = calculator.Sum("1");
        result.Should().Be(1);
    }
    
    [Fact]
    public void Given_TwoNumbersSeparatedByComma_When_Sum_Then_ReturnsSum()
    {
        var calc = new StringCalculator();
        var result = calc.Sum("3,5");
        Assert.Equal(8, result);
    }

}
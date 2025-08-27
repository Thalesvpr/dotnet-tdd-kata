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
    
    [Fact]
    public void Given_MultipleNumbersSeparatedByComma_When_Sum_Then_ReturnsTheirSum()
    {
        var calc = new StringCalculator();

        var result = calc.Sum("1,2,3,4");

        Assert.Equal(10, result);
    }

    [Fact]
    public void Sum_HandlesCommaAndNewlineAsDelimiters()
    {
        var calc = new StringCalculator();

        var result = calc.Sum("1\n2,3");

        Assert.Equal(6, result);
    }

    [Fact]
    public void Given_Negatives_When_Sum_Then_ThrowsWithAllNegatives()
    {
        var calc = new StringCalculator();

        var ex = Assert.Throws<InvalidOperationException>(() => calc.Sum("1,-2,3,-5"));
        Assert.Contains("-2,-5", ex.Message);
    }


}
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

    [Fact]
    public void Given_NumbersGreaterThan1000_When_Sum_Then_IgnoreThem()
    {
        var calc = new StringCalculator();

        var result = calc.Sum("2,1001");

        Assert.Equal(2, result);
    }

    [Fact]
    public void Given_1000CountsBut1001Ignored_When_Sum_Then_Return1001()
    {
        var calc = new StringCalculator();

        var result = calc.Sum("1000,1,1001");

        Assert.Equal(1001, result);
    }


    [Fact]
    public void Given_CustomSingleCharDelimiter_When_Sum_Then_UsesIt()
    {
        var calc = new StringCalculator();

        var result = calc.Sum("//;\n1;2");

        Assert.Equal(3, result);
    }

    [Fact]
    public void Given_CustomSingleCharDelimiter_When_CommaIsUsed_Then_Fails()
    {
        var calc = new StringCalculator();
        
        Assert.Throws<FormatException>(() => calc.Sum("//;\n1,2"));
    }
    
}
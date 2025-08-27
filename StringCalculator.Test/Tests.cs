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
        result.Should().Be(8);
    }
    
    [Fact]
    public void Given_MultipleNumbersSeparatedByComma_When_Sum_Then_ReturnsTheirSum()
    {
        var calc = new StringCalculator();

        var result = calc.Sum("1,2,3,4");

        result.Should().Be(10);
    }

    [Fact]
    public void Sum_HandlesCommaAndNewlineAsDelimiters()
    {
        var calc = new StringCalculator();

        var result = calc.Sum("1\n2,3");

        result.Should().Be(6);
    }

    [Fact]
    public void Given_Negatives_When_Sum_Then_ThrowsWithAllNegatives()
    {
        var calc = new StringCalculator();

        var act = () => calc.Sum("1,-2,3,-5");
        act.Should().Throw<InvalidOperationException>().WithMessage("*-2,-5*");
    }

    [Fact]
    public void Given_NumbersGreaterThan1000_When_Sum_Then_IgnoreThem()
    {
        var calc = new StringCalculator();

        var result = calc.Sum("2,1001");

        result.Should().Be(2);
    }

    [Fact]
    public void Given_1000CountsBut1001Ignored_When_Sum_Then_Return1001()
    {
        var calc = new StringCalculator();

        var result = calc.Sum("1000,1,1001");

        result.Should().Be(1001);
    }


    [Fact]
    public void Given_CustomSingleCharDelimiter_When_Sum_Then_UsesIt()
    {
        var calc = new StringCalculator();

        var result = calc.Sum("//;\n1;2");

        result.Should().Be(3);
    }

    [Fact]
    public void Given_CustomSingleCharDelimiter_When_CommaIsUsed_Then_Fails()
    {
        var calc = new StringCalculator();
        
        var act = () => calc.Sum("//;\n1,2");
        act.Should().Throw<FormatException>();
    }
    
    [Fact]
    public void Given_VariableLengthDelimiter_When_Sum_Then_UsesIt()
    {
        var calc = new StringCalculator();

        var result = calc.Sum("//[***]\n1***2***3");

        result.Should().Be(6);
    }

    
    [Fact]
    public void Given_MultipleDelimiters_When_Sum_Then_UsesAll()
    {
        var calc = new StringCalculator();

        var result = calc.Sum("//[*][%]\n1*2%3");

        result.Should().Be(6);
    }

}
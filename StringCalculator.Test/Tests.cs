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
}
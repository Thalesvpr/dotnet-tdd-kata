namespace StringCalculator;

public class StringCalculator
{
    public int Sum(string input)
    {
        if (string.IsNullOrEmpty(input)) return 0;

        if (IsSingleNumber(input))
            return ParseSingle(input);

        if (input.Contains(','))
            return SumParts(input);

        throw new NotImplementedException();
    }


    private static bool IsSingleNumber(string input) =>
        !input.Contains(',') || !input.Contains('\n');

    private static int ParseSingle(string input) =>
        int.Parse(input);

    private static int SumParts(string input)
    {
        var replace = input.Replace('\n', ',');
        var parts = replace.Split(',', StringSplitOptions.RemoveEmptyEntries);
        var sum = 0;
        foreach (var part in parts)
        {
            sum += int.Parse(part);
        }
            
        return sum;
    }
}
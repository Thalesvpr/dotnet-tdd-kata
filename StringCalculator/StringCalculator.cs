namespace StringCalculator;

public class StringCalculator
{
    public int Sum(string input)
    {
        if (string.IsNullOrEmpty(input)) return 0;

        if (HasDelimiters(input))
            return SumParts(input);
        
        return int.Parse(input);
    }

    
    private static bool HasDelimiters(string input) =>
        input.Contains(',') || input.Contains('\n');

    private static string[] NormalizeToParts(string input)
    {
        var replace = input.Replace('\n', ',');
        return replace.Split(',', StringSplitOptions.RemoveEmptyEntries);
    }
    
    private static int SumParts(string input)
    {

        var parts = NormalizeToParts(input);
        var sum = 0;
        foreach (var part in parts)
        {
            sum += int.Parse(part);
        }
            
        return sum;
    }
}
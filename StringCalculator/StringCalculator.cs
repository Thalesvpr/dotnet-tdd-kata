namespace StringCalculator;

public class StringCalculator
{
    public int Sum(string input)
    {
        if (string.IsNullOrEmpty(input)) return 0;

        if (HasDelimiters(input))
            return SumParts(input);
        
        var n = int.Parse(input);
        if (n < 0)
            throw new InvalidOperationException($"Negatives not allowed: {n}");
        
        return n;
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
        
        var negatives = new List<string>(parts.Length);

        foreach (var part in parts)
        {
            if (part.Contains("-")) negatives.Add(part);
        }
        if (negatives.Count != 0) throw new InvalidOperationException(
            $"Negatives not allowed: {string.Join(",", negatives)}");
        
        var sum = 0;
        foreach (var part in parts)
        {
            sum += int.Parse(part);
        }
            
        return sum;
    }
}
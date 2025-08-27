namespace StringCalculator;

public class StringCalculator
{
    public int Sum(string input)
    {
        if (string.IsNullOrEmpty(input)) 
            return 0;
        
        if (StartsWithHeader(input))
            return SumWithCustomDelimiter(input);
        
        var parts = SplitDefault(input);
        
        var numbers = ToInts(parts);
        
        ThrowIfNegatives(numbers);
        
        return SumUpTo1000(numbers);
    }
    
    
    private static bool StartsWithHeader(string s) =>
        s.StartsWith("//");
    
    
    private static int SumWithCustomDelimiter(string input)
    {
        var (delimiter, body) = ParseHeader(input);
        
        var parts = body.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
        
        var numbers = ToInts(parts);
        
        ThrowIfNegatives(numbers);
        
        return SumUpTo1000(numbers);
    }
    
    
    private static (char delimiter, string body) ParseHeader(string input)
    {
        var nl = input.IndexOf('\n');
        
        if (nl < 0) 
            throw new FormatException("Invalid header");
        
        var header = input.Substring(2, nl - 2);
        
        if (header.Length != 1) 
            throw new FormatException("Only single-char delimiter");
        
        var body = input.Substring(nl + 1);
        
        return (header[0], body);
    }
    
    
    private static string[] SplitDefault(string input)
    {
        var normalized = input.Replace('\n', ',');
        
        return normalized.Split(',', StringSplitOptions.RemoveEmptyEntries);
    }
    
    
    private static int[] ToInts(string[] parts)
    {
        var arr = new int[parts.Length];
        
        for (var i = 0; i < parts.Length; i++)
            arr[i] = int.Parse(parts[i]);
        
        return arr;
    }
    
    
    private static void ThrowIfNegatives(int[] numbers)
    {
        var negatives = new List<int>();
        
        foreach (var n in numbers)
            if (n < 0) negatives.Add(n);
        
        if (negatives.Count > 0)
            throw new InvalidOperationException($"Negatives not allowed: {string.Join(",", negatives)}");
    }
    
    
    private static int SumUpTo1000(int[] numbers)
    {
        var sum = 0;
        
        foreach (var n in numbers)
            if (n <= 1000) sum += n;
        
        return sum;
    }
}
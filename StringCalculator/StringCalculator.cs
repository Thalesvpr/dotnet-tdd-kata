namespace StringCalculator;

public class StringCalculator
{
    public int Sum(string input)
    {
        if (string.IsNullOrEmpty(input)) 
            return 0;
        
        if (StartsWithHeader(input))
            return SumWithCustomDelimiters(input);
        
        var parts = SplitDefault(input);
        
        var numbers = ToInts(parts);
        
        ThrowIfNegatives(numbers);
        
        return SumUpTo1000(numbers);
    }
    
    
    private static bool StartsWithHeader(string s) =>
        s.StartsWith("//");
    
    
    private static int SumWithCustomDelimiters(string input)
    {
        var (delimiters, body) = ParseHeader(input);
        
        var parts = body.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        
        var numbers = ToInts(parts);
        
        ThrowIfNegatives(numbers);
        
        return SumUpTo1000(numbers);
    }
    
    
    private static (string[] delimiters, string body) ParseHeader(string input)
    {
        var nl = input.IndexOf('\n');
        if (nl < 0) throw new FormatException("Invalid header");
        
        var header = input.Substring(2, nl - 2);
        var body = input.Substring(nl + 1);
        
        if (header.StartsWith("[") && header.EndsWith("]"))
        {
            var dels = ExtractBracketedDelimiters(header);
            if (dels.Length == 0) throw new FormatException("Empty delimiter set");
            return (dels, body);
        }
        
        if (header.Length != 1) throw new FormatException("Only single-char delimiter");
        return (new[] { header }, body);
    }
    
    
    private static string[] ExtractBracketedDelimiters(string header)
    {
        var list = new List<string>();
        
        var start = 0;
        while (start < header.Length)
        {
            var open = header.IndexOf('[', start);
            if (open < 0) break;
            
            var close = header.IndexOf(']', open + 1);
            if (close < 0) throw new FormatException("Unclosed delimiter bracket");
            
            var d = header.Substring(open + 1, close - open - 1);
            if (d.Length == 0) throw new FormatException("Empty delimiter");
            list.Add(d);
            
            start = close + 1;
        }
        
        if (list.Count == 0) throw new FormatException("No delimiters found");
        
        return list.ToArray();
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

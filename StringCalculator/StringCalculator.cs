namespace StringCalculator;

public class StringCalculator
{
    public int Sum(string input)
    {
        if (string.IsNullOrEmpty(input)) 
            return 0;
        
        var parts = Parts(input);
        var numbers = Parse(parts);
        
        ThrowIfNegatives(numbers);
        
        return SumNumbers(numbers);
    }
    
    
    private static string[] Parts(string input)
    {
        var normalized = input.Replace('\n', ',');
        
        return normalized.Split(',', StringSplitOptions.RemoveEmptyEntries);
    }
    
    
    private static int[] Parse(string[] parts)
    {
        var nums = new int[parts.Length];
        
        for (int i = 0; i < parts.Length; i++)
            nums[i] = int.Parse(parts[i]);
        return nums;
    }
    
    
    private static void ThrowIfNegatives(int[] numbers)
    {
        var negatives = new List<int>();
        
        foreach (var n in numbers)
            if (n < 0) negatives.Add(n);
        if (negatives.Count > 0)
            throw new InvalidOperationException($"Negatives not allowed: {string.Join(",", negatives)}");
    }
    
    
    private static int SumNumbers(int[] numbers)
    {
        var sum = 0;
        
        foreach (var n in numbers) sum += n;
        return sum;
    }
}
namespace StringCalculator;

public class StringCalculator
{
    public int Sum(string input)
    {
        if (string.IsNullOrEmpty(input)) return 0;
        
        if (!input.Contains(','))
            return int.Parse(input);
        
        var parts = input.Split(',');
        var left = int.Parse(parts[0]);
        var right = int.Parse(parts[1]);
        return left + right;
        
        throw new NotImplementedException();
    }
}
namespace StringCalculator;

public class StringCalculator
{
    public int Sum(string input)
    {
        if (string.IsNullOrEmpty(input)) return 0;
        
        return int.Parse(input);
        
        throw new NotImplementedException();
    }
}
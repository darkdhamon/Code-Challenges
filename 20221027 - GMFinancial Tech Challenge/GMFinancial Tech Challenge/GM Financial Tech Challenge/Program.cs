namespace GM_Financial_Tech_Challenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var reader = new StreamReader(Console.OpenStandardInput());
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var lineParts = line!.Split(',');
                    if (lineParts.Length == 2)
                    {
                        var originalText = lineParts[0];
                        var charactersToRemove = lineParts[1].Trim().ToCharArray();
                        var finalText = originalText;
                        foreach (var c in charactersToRemove) finalText = finalText.Replace($"{c}", string.Empty);
                        Console.WriteLine(finalText);
                    }
                    else
                    {
                        // Number of Parameter conditions not specified in requirements
                    }
                }
                else
                {
                    // Empty Line Conditions not specified in requirements
                }
            }
        }
    }
}
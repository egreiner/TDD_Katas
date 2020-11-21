namespace Kata.Services.HappyNumbers
{
    using System.Linq;

    public class HappyNumberService
    {
        public bool IsHappyNumber(int value, int maxIterations = 1000)
        {
            // make this better
            // sometimes it needs some extra steps, just follow the flow...
            for (int i = 0; i < maxIterations && value != 1; i++) 
                value = GetSquareOfDigits(value);

            return value == 1;
        }

        private static int GetSquareOfDigits(int value)
        {
            // functional approach
            // this is slower due to LINQ...
            // It's up to you, what you need
            // i prefer this one
            var values= value.ToString().ToCharArray()
                        .Select(c => int.Parse(c.ToString()))
                        .Select(n => n * n);
            return values.Sum();
        }
    }
}
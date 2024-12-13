namespace Task3
{
    public class EratosthenesSieve
    {
        public static List<int> GetBasicNumbers(List<int> numbers)
        {
            int n = (int)Math.Sqrt(numbers.Count);
            for (int i = 2; i < n; i++)
            {
                if (numbers[i] != 0)
                {
                    for (int j = i * i; j < n; j += i)
                    {
                        numbers[j] = 0;
                    }
                }
            }

            List<int> basicNumbers = new();
            for (int i = 2; i < n; i++)
            {
                if (numbers[i] != 0)
                {
                    basicNumbers.Add(i);
                }
            }

            return basicNumbers;
        }
    }
}
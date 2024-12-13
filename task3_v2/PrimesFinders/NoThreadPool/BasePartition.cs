namespace Task3
{
    public class BasePartition : IPrimeProcessor
    {
        private int n;
        private int threadsAmount;
        private List<int> basicNumbers;
        private double duration;

        public BasePartition(int n, int threadsAmount, List<int> basicNumbers)
        {
            this.n = n;
            this.threadsAmount = threadsAmount;
            this.basicNumbers = basicNumbers;
        }
        public List<int> FindPrimes()
        {
            var startTimer = DateTime.Now;

            int rangeSize = n - (int)Math.Sqrt(n) + 1;
            bool[] isPrimeFlags = Enumerable.Repeat(true, rangeSize).ToArray();

            List<Thread> threads = new List<Thread>();
            int basicRangeSize = basicNumbers.Count / threadsAmount;
            int basicStart = 0;

            for (int i = 0; i < threadsAmount; ++i)
            {
                int basicEnd = (i == threadsAmount - 1) ? basicNumbers.Count : (basicStart + basicRangeSize);

                int currentBasicStart = basicStart; // Зафиксировать текущий диапазон
                int currentBasicEnd = basicEnd;
                threads.Add(new Thread(() => Process(basicNumbers, currentBasicStart, currentBasicEnd, isPrimeFlags)));

                basicStart = basicEnd;
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            // Извлечение простых чисел из флагов
            List<int> primes = new List<int>();
            int sqrtN = (int)Math.Sqrt(n);
            for (int i = 0; i < rangeSize; ++i)
            {
                if (isPrimeFlags[i])
                {
                    primes.Add(sqrtN + i);
                }
            }

            var endTimer = DateTime.Now;
            duration = (endTimer - startTimer).TotalSeconds;

            return primes;
        }
    

        private void Process(List<int> localBasicNumbers, int rangeStart, int rangeEnd, bool[] isPrimeFlags)
        {
            int sqrtN = (int)Math.Sqrt(n);
            for (int num = sqrtN; num <= n; ++num)
            {
                if (isPrimeFlags[num - sqrtN] && !CheckNumber(num, localBasicNumbers, rangeStart, rangeEnd))
                {
                    isPrimeFlags[num - sqrtN] = false;
                }
            }
        }

        private bool CheckNumber(int num, List<int> localBasicNumbers, int rangeStart, int rangeEnd)
        {
            for (int i = rangeStart; i < rangeEnd; ++i)
            {
                int baseNumber = localBasicNumbers[i];
                if (baseNumber * baseNumber > num) break; // Проверка делителей до sqrt(num)

                if (num % baseNumber == 0) return false; // Число не простое
            }
            return true; // Число простое
        }

        public double GetDuration()
        {
            return duration;
        }

        // Пример функции до рефакторинга
        private void ProcessOld(List<int> localBasicNumbers, int rangeStart, int rangeEnd, bool[] isPrimeFlags)
        {
            int sqrtN = (int)Math.Sqrt(n);
            for (int num = sqrtN; num <= n; ++num)
            {
                if (isPrimeFlags[num - sqrtN])
                {
                    for (int i = rangeStart; i < rangeEnd; ++i)
                    {
                        int baseNumber = localBasicNumbers[i];
                        if (baseNumber * baseNumber > num) break; // Проверка делителей до sqrt(num)

                        if (num % baseNumber == 0)
                        {
                            // Число не простое
                            isPrimeFlags[num - sqrtN] = false;
                            break;
                        }
                    }
                }
            }
        }


    }
}

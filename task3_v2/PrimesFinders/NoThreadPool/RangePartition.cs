namespace Task3
{
    public class RangePartition : IPrimeProcessor
    {
        private int n;
        private int threadsAmount;
        private List<int> basicNumbers;
        private double duration;

        public RangePartition(int n, int threadsAmount, List<int> basicNumbers)
        {
            this.n = n;
            this.threadsAmount = threadsAmount;
            this.basicNumbers = basicNumbers;
        }

      
        public List<int> FindPrimes()
        {
            var startTimer = DateTime.Now;

            List<Thread> threads = new List<Thread>();
            List<List<int>> threadResults = new List<List<int>>(threadsAmount);
            for (int i = 0; i < threadsAmount; ++i)
            {
                threadResults.Add(new List<int>());
            }

            int rangeSize = (n - (int)Math.Sqrt(n)) / threadsAmount;
            int start = (int)Math.Sqrt(n);

            for (int i = 0; i < threadsAmount; ++i)
            {
                int end = (i == threadsAmount - 1) ? n : (start + rangeSize - 1);
                int currentStart = start; // Для передачи в поток
                int index = i;            // Для передачи в поток
                threads.Add(new Thread(() => Process(currentStart, end, threadResults[index])));
                start += rangeSize;
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            List<int> primes = new();
            foreach (var localPrimes in threadResults)
            {
                primes.AddRange(localPrimes);
            }
            primes.Sort();

            var endTimer = DateTime.Now;
            duration = (endTimer - startTimer).TotalSeconds;

            return primes;
        }

        private void Process(int start, int end, List<int> localPrimes)
        {
            for (int num = start; num <= end; ++num)
            {
                if (IsPrime(num))
                {
                    localPrimes.Add(num);
                }
            }
        }

        private bool IsPrime(int number)
        {
            foreach (var basePrime in basicNumbers)
            {
                if (basePrime * basePrime > number) break; // Проверка делителей до sqrt(num)
                if (number % basePrime == 0) return false; // Число не простое
            }
            return true;
        }

        public double GetDuration()
        {
            return duration;
        }
    }
}

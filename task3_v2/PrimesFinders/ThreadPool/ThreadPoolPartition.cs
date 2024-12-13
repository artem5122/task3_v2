namespace Task3
{

    public class ThreadPoolPartition : IPrimeProcessor
    {
        private int n;
        private List<int> basicNumbers;
        private double duration;

        public ThreadPoolPartition(int n, List<int> basicNumbers)
        {
            this.n = n;
            this.basicNumbers = basicNumbers;
        }

        public List<int> FindPrimes()
        {
            var startTimer = DateTime.Now;

            int rangeSize = n - (int)Math.Sqrt(n) + 1;
            bool[] isPrimeFlags = Enumerable.Repeat(true, rangeSize).ToArray();

            List<Task> tasks = new();

            foreach (int baseNumber in basicNumbers)
            {
                tasks.Add(Task.Run(() => Process(baseNumber, isPrimeFlags)));
            }


            Task.WaitAll(tasks.ToArray());

            // Извлечение простых чисел из флагов
            List<int> primes = new();
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

        private void Process(int baseNumber, bool[] isPrimeFlags)
        {
            int sqrtN = (int)Math.Sqrt(n);
            for (int num = Math.Max(baseNumber * baseNumber, sqrtN); num <= n; ++num)
            {
                CheckNumber(num, baseNumber, isPrimeFlags, sqrtN);
            }
        }

        private void CheckNumber(int num, int baseNumber, bool[] isPrimeFlags, int sqrtN)
        {
            if (num % baseNumber == 0)
            {
                lock (isPrimeFlags)
                {
                    isPrimeFlags[num - sqrtN] = false;
                }
            }
        }
        public double GetDuration()
        {
            return duration;
        }
    }
}

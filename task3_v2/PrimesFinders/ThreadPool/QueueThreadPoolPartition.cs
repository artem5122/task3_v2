namespace Task3
{
    public class QueueThreadPoolPartition : IPrimeProcessor
    {
        private int n;
        private List<int> basicNumbers;
        private double duration;
        private int currentIndex;
        private object syncObj = new object();
        private int threadsAmount;


        public QueueThreadPoolPartition(int n, int threadsAmount, List<int> basicNumbers)
        {
            this.n = n;
            this.basicNumbers = basicNumbers;
            this.currentIndex = 0;
            this.threadsAmount = threadsAmount;
        }

        public List<int> FindPrimes()
        {
            var startTimer = DateTime.Now;

            int rangeSize = n - (int)Math.Sqrt(n) + 1;
            bool[] isPrimeFlags = Enumerable.Repeat(true, rangeSize).ToArray();

            List<Task> tasks = new();

            for (int i = 0; i < threadsAmount; i++)
            {
                tasks.Add(Task.Run(() => Process(isPrimeFlags)));
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

        private void Process(bool[] isPrimeFlags)
        {
            int sqrtN = (int)Math.Sqrt(n);

            while (true)
            {
                int baseNumber;
                lock (syncObj)
                {
                    if (currentIndex >= basicNumbers.Count)
                    {
                        break;
                    }

                    baseNumber = basicNumbers[currentIndex];
                    currentIndex++;
                }

                for (int num = Math.Max(baseNumber * baseNumber, sqrtN); num <= n; ++num)
                {
                    if (num % baseNumber == 0)
                    {
                        lock (isPrimeFlags)
                        {
                            isPrimeFlags[num - sqrtN] = false;
                        }
                    }
                }
            }
        }
        public double GetDuration()
        {
            return duration;
        }
    }

}

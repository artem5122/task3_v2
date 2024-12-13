namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            int n, threadsAmount;
            Console.Write("Введите n: ");
            n = int.Parse(Console.ReadLine());
            Console.Write("Введите (количество потоков): ");
            threadsAmount = int.Parse(Console.ReadLine());


            List<int> numbers = NumbersGenerator.GenerateNumbers(n);
            List<int> basicNumbers = EratosthenesSieve.GetBasicNumbers(numbers);
            List<int> primes;


            Executor.Execute(new RangePartition(n, threadsAmount, basicNumbers));
            Executor.Execute(new BasePartition(n, threadsAmount, basicNumbers));
            Executor.Execute(new ThreadPoolPartition(n, basicNumbers));
            primes = Executor.Execute(new QueueThreadPoolPartition(n, threadsAmount, basicNumbers));


            FoundPrimes foundPrimes = new(basicNumbers, primes);
        }

    }

}
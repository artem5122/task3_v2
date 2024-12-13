namespace Task3
{
    // Универсальный метод для запуска вычислений и вывода времени выполнения
    // where T : IPrimeProcessor определяет какого типа должен быть параметр передаваемый в функцию
    public static class Executor
    {
        public static List<int> Execute<T>(T partition) where T : IPrimeProcessor
        {
            var primes = partition.FindPrimes();
            Console.WriteLine($"Время выполнения: {partition.GetDuration()} секунд");
            return primes;
        }
    }
}

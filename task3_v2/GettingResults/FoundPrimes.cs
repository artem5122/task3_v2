namespace Task3
{
    public class FoundPrimes
    {
        private List<int> finalPrimeNumbers;

        public FoundPrimes(List<int> basicNumbers, List<int> primeNumbers)
        {
            // Объединяем базовые и найденные простые числа
            finalPrimeNumbers = new List<int>(basicNumbers);
            finalPrimeNumbers.AddRange(primeNumbers);

            AskOutputPreferences();
        }

        private void AskOutputPreferences()
        {
            string? printToConsole;
            string? saveToFile;

            while (true)
            {
                Console.Write("Вывести полученные числа в консоль? (y/n): ");
                printToConsole = Console.ReadLine()?.ToLower();
                if (printToConsole == "y" || printToConsole == "n")
                    break;
            }
            if (printToConsole == "y")
                PrintToConsole();

            while (true)
            {
                Console.Write("Хотите сохранить полученные числа в файл? (y/n): ");
                saveToFile = Console.ReadLine()?.ToLower();
                if (saveToFile == "y" || saveToFile == "n")
                    break;
            }

            if (saveToFile == "y")
                PrintToFile("C:\\Users\\Артем\\source\\repos\\Parallel\\task3_v2\\task3_v2\\Output\\prime numbers.txt");
        }

        private void PrintToConsole()
        {
            foreach (var prime in finalPrimeNumbers)
            {
                Console.WriteLine(prime);
            }
        }

        private void PrintToFile(string filename)
        {
            try
            {
                using (var writer = new StreamWriter(filename))
                {
                    foreach (var prime in finalPrimeNumbers)
                    {
                        writer.WriteLine(prime);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Ошибка записи в файл: {ex.Message}");
            }
        }
        public List<int> GetAllPrimeNumbers()
        {
            return finalPrimeNumbers;
        }
    }

}

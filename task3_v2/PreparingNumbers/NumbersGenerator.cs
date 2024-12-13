namespace Task3
{
    public static class NumbersGenerator
    {
        public static List<int> GenerateNumbers(int n)
        {
            List<int> array = new();
            for (int i = 0; i <= n; i++)
            {
                array.Add(i);
            }
            return array;
        }
    }
}

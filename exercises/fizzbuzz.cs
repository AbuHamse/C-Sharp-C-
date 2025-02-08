using System;

class Program
{
    static void Main(string[] args)
    {
        for (int i = 1; i <= 100; i++)
        {
            string result = i switch
            {
                int n when n % 3 == 0 && n % 5 == 0 => "FizzBuzz",
                int n when n % 3 == 0 => "Fizz",
                int n when n % 5 == 0 => "Buzz",
                _ => i.ToString()
            };

            Console.WriteLine(result);
        }
    }
}

using System;

namespace RandomNumberGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize the game
            Console.WriteLine("Welcome to the Random Number Guessing Game!");
            Console.WriteLine("I have selected a number between 1 and 100. Can you guess it?");
            
            // Generate a random number between 1 and 100
            Random random = new Random();
            int targetNumber = random.Next(1, 101);

            int attempts = 0;
            bool isGuessed = false;

            while (!isGuessed)
            {
                Console.Write("Enter your guess: ");
                string input = Console.ReadLine();
                int guess;

                // Validate input
                if (!int.TryParse(input, out guess))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                attempts++;

                // Check the guess
                if (guess < targetNumber)
                {
                    Console.WriteLine("Too low! Try again.");
                }
                else if (guess > targetNumber)
                {
                    Console.WriteLine("Too high! Try again.");
                }
                else
                {
                    Console.WriteLine($"Congratulations! You guessed the number in {attempts} attempts.");
                    isGuessed = true;
                }
            }

            Console.WriteLine("Thanks for playing!");
        }
    }
}

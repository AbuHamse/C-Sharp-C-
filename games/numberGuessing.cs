using System;
using System.Threading;

class GuessTheNumberGame
{
    static int numberToGuess; // The random number the player must guess
    static int attempts; // Number of attempts taken by the player
    static bool gameRunning = true; // Flag to control game loop

    static void Main()
    {
        Thread gameThread = new Thread(RunGame); // Create a separate thread for the game
        gameThread.Start(); // Start the game on a new thread

        // Keeping the main thread alive until the game ends
        while (gameThread.IsAlive) 
        {
            Thread.Sleep(100); // Prevents high CPU usage
        }
    }

    static void RunGame()
    {
        try
        {
            Random random = new Random();
            numberToGuess = random.Next(1, 100); // Generate a random number between 1-100
            attempts = 0;

            Console.WriteLine("🎮 Welcome to 'Guess the Number'!");
            Console.WriteLine("🔢 I have chosen a number between 1 and 100. Try to guess it!");

            while (gameRunning)
            {
                Console.Write("Enter your guess: ");
                string input = Console.ReadLine();

                try
                {
                    int userGuess = int.Parse(input); // Convert input to integer
                    attempts++;

                    // Use switch statement for cleaner control flow
                    switch (userGuess.CompareTo(numberToGuess))
                    {
                        case -1: // userGuess < numberToGuess
                            Console.WriteLine("📉 Too low! Try again.");
                            break;

                        case 1: // userGuess > numberToGuess
                            Console.WriteLine("📈 Too high! Try again.");
                            break;

                        case 0: // userGuess == numberToGuess
                            Console.WriteLine($"🎉 Congratulations! You guessed the number in {attempts} attempts.");
                            gameRunning = false; // End the game
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("❌ Invalid input! Please enter a valid number.");
                }
                catch (Exception ex) // Catch any unexpected errors
                {
                    Console.WriteLine($"⚠️ Error: {ex.Message}");
                }
            }

            Console.WriteLine("✅ Game Over. Thanks for playing!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"🚨 Unexpected error: {ex.Message}");
        }
    }
}

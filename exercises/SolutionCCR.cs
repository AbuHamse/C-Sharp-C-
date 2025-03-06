using System.Collections.Generic;

namespace Solution;

class Kata
{
    public static Dictionary<string, int> get_animals_count(int legsNumber, int headsNumber, int hornsNumber)
    {
        int cows = hornsNumber / 2;
        int rabbits = legsNumber / 2 - cows - headsNumber;
        int chickens = headsNumber - cows - rabbits; 
        
        return new Dictionary<string, int>()
        {
            { nameof(cows), cows },
            { nameof(rabbits), rabbits },
            { nameof(chickens), chickens }
        };
    }
}
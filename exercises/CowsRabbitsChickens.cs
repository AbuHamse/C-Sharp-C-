using System;
using System.Collections.Generic;

namespace Solution {
    class Kata {
        public static Dictionary<string, int> get_animals_count(int legs_number, int heads_number, int horns_number) {
            Dictionary<string, int> result = new Dictionary<string, int>();

            // Step 1: Validate input (horns should be even)
            if (horns_number % 2 != 0) {
                throw new ArgumentException("Invalid number of horns. It must be an even number.");
            }

            // Step 2: Calculate number of cows
            int cows = horns_number / 2;

            // Step 3: Check if cows exceed heads
            if (cows > heads_number) {
                throw new ArgumentException("Number of cows cannot exceed the number of heads.");
            }

            // Step 4: Solve for rabbits
            int rabbits = ((legs_number - 4 * cows) / 2) - (heads_number - cows);

            // Step 5: Solve for chickens
            int chickens = (heads_number - cows) - rabbits;

            // Step 6: Validate the result (no negative counts)
            if (chickens < 0 || cows < 0 || rabbits < 0) {
                throw new ArgumentException("No valid solution. Please check the input.");
            }

            // Step 7: Store results in dictionary
            result["chickens"] = chickens;
            result["rabbits"] = rabbits;
            result["cows"] = cows;

            return result;
        }
    }
}

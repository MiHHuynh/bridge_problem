##Problem Statement

A number of people are trying to cross a bridge at night with a single flashlight. In order to cross the bridge, the person or people must have the flashlight. A maximum of two people can cross at a time. Each person has a different walking speed, and when a pair of people cross, the time of the crossing will be the slower person’s time. Calculate the fastest time it will take a group of people to cross given any number of people with any set of speeds.

###Example 1:

The fastest amount of time four people with speeds 1 min, 2, 5, and 10 can get across the bridge is 17 minutes.

| Step   |      Left of Bridge      |  On Bridge | Right of Bridge | Time Cost |
|----------|:-------------:|:------:| :------:| :------:|
| 1 |  {1, 2, 5, 10} | → (1, 2) → | {1,2}| 2|
| 2 |    {2, 5, 10}   |   ← (2) ← | {1}| 2|
| 3 | {2} |   → (5, 10) →  | {1, 5, 10}| 10|
| 4 | {1, 2} |    ← (1) ← | {5, 10}| 1|
| 5 | { } |    → (1, 2) → | {1, 2, 5, 10}| 2|

###Example 2:
The fastest amount of time five people with speeds 1 min, 2, 5, 10, 15 can get across the bridge is 28 minutes.

| Step   |      Left of Bridge      |  On Bridge | Right of Bridge | Time Cost |
|----------|:-------------:|:------:| :------:| :------:|
| 1 |  {1, 2, 5, 10, 15} | → (1, 2) → | {1,2}| 2|
| 2 |    {2, 5, 10, 15}   |  ← (2) ←  |{1} |2 |
| 3 |  {2, 5}|  → (10, 15) →   | {1, 10, 15} | 15 |
| 4 | {1, 2, 5} |   ← (1) ← | {10, 15} | 1 |
| 5 | {5} | → (1, 2) →   | {1, 2, 10, 15} | 2 |
| 6 |  {1, 5}|   ← (1) ←  | {2, 10, 15}| 1|
| 7 | { } |   → (1, 5) →  | {1, 2, 5, 10, 15}| 5|

##Solution

###Design
Once we start dealing with larger groups of people (>4), a pattern starts to emerge. Because there is the need for someone to bring back the flashlight after every crossing, in order to not add unnecessary time cost, it is ideal for the fastest people to be the flashlight carriers. To further minimize time cost, we do not want the slowest people to each cross individually, so the best way to have them all cross is to let them go together in pairs (this essentially “erases” the time cost of every other slow person; e.g. by having 10 and 5 cross together, you only add a time cost of 10 to the total, as opposed to having each of them cross separately while paired with 1 or 2, causing the time cost to increase by 10 and 5).

Thus, every set of crossings that are set up to get rid of the two slowest people (in the example above, steps 1-4) will then create a subproblem with two fewer people. In other words, a group of people of size n will become a subgroup of n-2 after each batch of slow people has successfully crossed.

If we continue with this method repeatedly, we will end up with a base case of n = 2 (if the original number of people was even) or n = 3 (if the original number of people was odd).

For n = 2, if there is a person A and person B and A < B, the total time cost will be B as they cross together.

For n = 3, if there is a person A, person B, and person C and A < B < C, the total time cost will be A+B+C as shown:

| Step   |      Left of Bridge      |  On Bridge | Right of Bridge | Time Cost |
|----------|:-------------:|:------:| :------:| :------:|
| 1 |  {A, B, C} | → (A, B) → | {A, B}| B|
| 2 | {A, C} 	|	← (A) ← 	|	{B}		|	A
| 3 |  {} 	|		→ (A, C) → 	|	{A, B, C} | C

Given that we have an optimal solution for subproblem with n - 2 people, and for the base cases of n = 2 and n = 3, we can say we have an optimal solution for n by means of using recursion.

###Optimality
In order to get the smallest time cost, there are a set of steps what we must always do in order to reduce size n to size n - 2 (in regular English, this means we need to do a set of steps in order to successfully cross the two slowest people in the current set). Those steps are:

Note: Given that A & B are the fastest and second fastest person respectively, and C and D are the second slowest and slowest respectively…

A & B cross					Time cost: B
A returns with the flashlight		Time cost: A

C & D cross					Time cost: D
B returns with the flashlight		Time cost: B

After step #4, n will become n - 2. We must repeat steps 1 through 4 repeatedly until we hit a base case of n = 2 or n = 3, and then finish the problem to make n = 0 (meaning all people have successfully crossed).

There are a few reasons why this method is optimal. Having the slowest people cross first will cause one of them to have to come back, effectively adding both C’s and D’s time cost to the total. Having either C or D cross with A will, again, cause both C’s and D’s time cost to be added to the total, because they will be crossing separately. There is just no other way to keep the time cost as low if we separate C and D.

###Implementation
A list will be used as opposed to an array for this problem because we do not know the size of n from the start.

Ask user for number of people and their speeds.
Add values into list.

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MichelleHuynh_BridgeProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            //initialize a list
            List<int> speeds_of_walkers = new List<int>();
            //Gets user input on # of walkers + speeds of each
            Console.WriteLine("How many people?");
            int number_of_people = Convert.ToInt32(Console.ReadLine());
            //Check for zero
            while (number_of_people == 0) {
                Console.WriteLine("Obviously calculating the speed for zero people would be zero. \nEnter a proper number of people: ");
                number_of_people = Convert.ToInt32(Console.ReadLine());
            }  
            //Check for negative
            while (number_of_people < 0) {
                Console.WriteLine("You can't have negative people. Enter a proper number of people: ");
                number_of_people = Convert.ToInt32(Console.ReadLine());
            }
            //Add values to list + sort
            SetUpList(speeds_of_walkers, number_of_people);       
            int last_index = speeds_of_walkers.Count() - 1;
            Console.WriteLine("The lowest total time to get everyone across the bridge with people of speeds");
            for (int i = 0; i < number_of_people; i++) {
                Console.WriteLine(speeds_of_walkers[i]);
            }
            Console.WriteLine("is " + CalcTotalTime(speeds_of_walkers, last_index) + " minutes.");
        } // end main
        //ASSUME: List is sorted from least to most
        static int CalcTotalTime(List<int> speeds, int lastIndex)
        {
            int numberOfPeople = lastIndex + 1;
            int subset_time_to_cross;
            switch (numberOfPeople)
            {
                case 0:
                    subset_time_to_cross = 0;
                    return subset_time_to_cross;
                case 1:
                    subset_time_to_cross = speeds[0];
                    return subset_time_to_cross;
                case 2:
                    subset_time_to_cross = speeds[1];
                    return subset_time_to_cross;
                case 3:
                    subset_time_to_cross = speeds[0] + speeds[1] + speeds[2];
                    return subset_time_to_cross;
                default:
                    subset_time_to_cross = speeds[1] + speeds[0] + speeds[lastIndex] + speeds[1];
                    return subset_time_to_cross + CalcTotalTime(speeds, lastIndex - 2);
            }
        } //calctotaltime
        static void SetUpList(List<int> speeds, int numberOfPeople) 
        {
            if (numberOfPeople == 0) {
                Console.WriteLine("Zero people? Get out of here.");
                return;
            }
            //int index_counter = 0;
            int people_counter = 1;
            while (people_counter <= numberOfPeople) {
                Console.WriteLine("Enter speed of person " + people_counter + ":");
                speeds.Add(Convert.ToInt32(Console.ReadLine()));
                people_counter++;
            }
            speeds.Sort();
        } //end setuplist
    }
}
```
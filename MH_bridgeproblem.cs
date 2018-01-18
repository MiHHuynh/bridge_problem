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
            List<int> speeds_of_walkers = new List<int>();

            //Gets user input on # of walkers + speeds of each
            Console.WriteLine("How many people?");
            int number_of_people;
            bool result = Int32.TryParse(Console.ReadLine(), out number_of_people);
            while (!result || number_of_people < 0) {
                Console.WriteLine("Enter a proper number of people: ");
                result = Int32.TryParse(Console.ReadLine(), out number_of_people);
            }

            //Add values to list + sort
            SetUpList(speeds_of_walkers, number_of_people);       

            // Solve for time and write to console
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
                case 0: // obviously 0 people takes 0 time
                    subset_time_to_cross = 0;
                    return subset_time_to_cross;
                case 1: // when 1 person crosses, time cost is speed of that person
                    subset_time_to_cross = speeds[0];
                    return subset_time_to_cross;
                case 2: // base case n = 2; when 2 people cross, time cost is the greater of two speeds
                    subset_time_to_cross = speeds[1];
                    return subset_time_to_cross;
                case 3: // base case n = 3; when 3 people cross, time cost is sum of all three speeds
                    subset_time_to_cross = speeds[0] + speeds[1] + speeds[2];
                    return subset_time_to_cross;
                default:
                    subset_time_to_cross = speeds[1] + speeds[0] + speeds[lastIndex] + speeds[1];
                    return subset_time_to_cross + CalcTotalTime(speeds, lastIndex - 2);
            }
        } //calctotaltime

        //Gets input from user to put into list and sorts
        static void SetUpList(List<int> speeds, int numberOfPeople) 
        {
            int people_counter = 1;
            while (people_counter <= numberOfPeople) {
                Console.WriteLine("Enter speed of person " + people_counter + ":");
                int speed_input;
                bool result = Int32.TryParse(Console.ReadLine(), out speed_input);
                while (!result || speed_input < 0) {
                    Console.WriteLine("Enter a proper speed for person " + people_counter + ": ");
                    result = Int32.TryParse(Console.ReadLine(), out speed_input);
                }
                speeds.Add(speed_input);
                people_counter++;
            }
            speeds.Sort();
        } //end setuplist
    }
}




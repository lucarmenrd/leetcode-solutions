/* leetcode 347: Top K Frequent Elements
 * 
 * Given an integer array nums and an integer k,
 * return the k most frequent elements.
 * You may return the answer in any order. */

using System;
using System.Collections;
using System.Collections.Generic;

namespace leetcode347_1
{
    public class Solution
    {
        public static void Main(string[] args)
        {
            /* given variables */

            /* example 1: */
            //int[] nums = { 1, 1, 1, 2, 2, 3 };
            //int k = 2;

            /* example 2: */
            //int[] nums = { 1 };
            //int k = 1;

            /* further testing */
            //int[] nums = { 1, 3, 1, 2, 1, 2 };
            //int[] nums = { 1, 3, 2, 2, 3, 2 };
            int[] nums = { 3, 7, 3, 1, 1, 9, 2, 3, 8, 8, 2, 6, 2, 9, 4, 2 };
            int k = 2;

            int[] results = new int[k];
            int solutionCase = 3;

            /* display task info in console */
            Console.WriteLine("Given an int array nums and an int k, return the k most frequent elements.");
            Console.WriteLine();

            Console.Write("Input: nums = [");
            for (int index = 0; index < nums.Length - 1; index++)
            {
                Console.Write($"{nums[index]}, ");
            }
            Console.WriteLine($"{nums[nums.Length - 1]}], k = {k}");
            Console.WriteLine();

            /* compute solution */
            switch (solutionCase)
            {
                case 1:
                    Console.WriteLine("solution 01:");
                    results = TopKFrequent01(nums, k);
                    break;
                case 2:
                    Console.WriteLine("solution 02:");
                    results = TopKFrequent02(nums, k);
                    break;
                case 3:
                    Console.WriteLine("solution 03:");
                    results = TopKFrequent03(nums, k);
                    break;
            }

            /* output solution to console */
            Console.Write("Output: [");
            for (int index = 0; index < k - 1; index++)
            {
                Console.Write($"{results[index]}, ");
            }
            Console.WriteLine($"{results[k - 1]}]");
        }

        /* solution 03: using dictionary */
        public static int[] TopKFrequent03(int[] nums, int k)
        {
            /* 1. create dictionary to store each unique element and its occurence into */
            Dictionary<int, int> elemOccurence = new Dictionary<int, int>();

            foreach (int element in nums)
            {
                int counter = 1;

                // check if the dictionary contains this element and increase its counter;
                // otherwise add it to the dictionary and count it once
                if (elemOccurence.ContainsKey(element))
                {
                    elemOccurence[element]++;
                }
                else
                {
                    elemOccurence.Add(element, counter);
                }
            }

            /* TESTING: output dictionary to console */
            /*Console.WriteLine("counting elements:");
            foreach (KeyValuePair<int, int> item in elemOccurence)
            {
                Console.WriteLine("{0}: {1}", item.Key, item.Value);
            }*/

            /* 2. get the most frequent k elements */
            int[] results = new int[k];

            // find the max occurence in dictionary, store it into an array then delete it from dictionary;
            // repeat k times
            for (int i = 0; i < k; i++ )
            {
                int maxVal = 0;
                int maxValElem = 0;

                foreach (KeyValuePair<int, int> item in elemOccurence)
                {
                    if (item.Value > maxVal)
                    {
                        maxVal = item.Value;
                        maxValElem = item.Key;
                    }
                }

                elemOccurence.Remove(maxValElem);
                results[i] = maxValElem;
            }

            return results;
        }

        /* solution 02: using hastable */
        public static int[] TopKFrequent02(int[] nums, int k)
        {
            /* 1. create hashtable to store each unique element and its occurence into */
            Hashtable elemOccurence = new Hashtable();

            foreach (int element in nums)
            {
                int counter = 1;

                // check if the hashtable contains this element and increase its counter;
                // otherwise add it to the hashtable and count it once
                if (elemOccurence.ContainsKey(element))
                {
                    elemOccurence[element] = (int)elemOccurence[element] + 1;
                }
                else
                {
                    elemOccurence.Add(element, counter);
                }
            }

            /* TESTING: output hashtable to console */
            /*Console.WriteLine("counting elements:");
            foreach (DictionaryEntry item in elemOccurence)
            {
                Console.WriteLine("{0}: {1}", item.Key, item.Value);
            }*/

            /* 2. get the most frequent k elements */
            int[] results = new int[k];

            // find the max occurence in hashtable, store it into an array then delete it from hashtable;
            // repeat k times
            for (int i = 0; i < k; i++)
            {
                int maxVal = 0;
                int maxValElem = 0;

                foreach (DictionaryEntry item in elemOccurence)
                {
                    if ((int)item.Value > maxVal)
                    {
                        maxVal = (int)item.Value;
                        maxValElem = (int)item.Key;
                    }
                }

                elemOccurence.Remove(maxValElem);
                results[i] = maxValElem;
            }

            return results;
        }

        /* solution 01: sorting arrays */
        public static int[] TopKFrequent01(int[] nums, int k)
        {
            /* 1. sort nums lower -> higher */
            for (int i = 0; i < nums.Length - 1; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] > nums[j])
                    {
                        int copy = nums[i];
                        nums[i] = nums[j];
                        nums[j] = copy;
                    }
                }
            }

            /* TESTING: output sorted nums to console */
            /*Console.Write("sorted nums = [");
            for (int index = 0; index < nums.Length - 1; index++)
            {
                Console.Write($"{nums[index]}, ");
            }
            Console.WriteLine($"{nums[nums.Length - 1]}]");*/

            /* 2. count element occurence and store into a matrix */
            int[,] resultsMatrix = new int[nums.Length, 2];
            int indexNums = 0;
            int indexResults = 0;

            while (indexNums < nums.Length)
            {
                // reset counter; count this element once
                int counter = 1;
                int thisElement = nums[indexNums];

                // boundary enforcing
                if (indexNums < (nums.Length - 1))
                {
                    // keep counting duplicates till reaching different element
                    while (thisElement == nums[indexNums + 1])
                    {
                        counter++;
                        indexNums++;

                        // boundary enforcing
                        if (indexNums >= (nums.Length - 1))
                        {
                            break;
                        }
                    }
                }

                // store the unique element and its occurence in a matrix
                resultsMatrix[indexResults, 0] = thisElement;
                resultsMatrix[indexResults, 1] = counter;
                indexNums++;
                indexResults++;
            }

            /* TESTING: output results matrix */
            /*Console.WriteLine("counting results matrix:");
            for (int i = 0; i < resultsMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < resultsMatrix.GetLength(1); j++)
                {
                    Console.Write($"{resultsMatrix[i, j]} ");
                }
                Console.WriteLine();
            }*/

            /* 3. sort the results matrix array higher -> lower */
            int[,] copyMatrix = new int[1, 2];
            for (int i = 0; i < resultsMatrix.GetLength(0) - 1; i++)
            {
                for (int j = i + 1; j < resultsMatrix.GetLength(0); j++)
                {
                    if (resultsMatrix[i, 1] < resultsMatrix[j, 1])
                    {
                        copyMatrix[0, 0] = resultsMatrix[i, 0];
                        copyMatrix[0, 1] = resultsMatrix[i, 1];

                        resultsMatrix[i, 0] = resultsMatrix[j, 0];
                        resultsMatrix[i, 1] = resultsMatrix[j, 1];

                        resultsMatrix[j, 0] = copyMatrix[0, 0];
                        resultsMatrix[j, 1] = copyMatrix[0, 1];
                    }
                }
            }

            /* TESTING: output sorted results matrix */
            /*Console.WriteLine("sorted counting results matrix = ");
            for (int i = 0; i < resultsMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < resultsMatrix.GetLength(1); j++)
                {
                    Console.Write($"{resultsMatrix[i, j]} ");
                }
                Console.WriteLine();
            }*/

            /* 4. get the most frequent k elements */
            int[] results = new int[k];
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = resultsMatrix[i, 0];
            }

            return results;
        }
    }
}

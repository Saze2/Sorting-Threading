using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExtensionMethods;

namespace PRPSorting
{
    internal class Quicksort
    {
        static void Main(string[] args)
        {
            List<int> randomList;
            var stopwatch = new System.Diagnostics.Stopwatch();

            for (int i = 0; i < 100; i++)
            {
                randomList = GenerateIntegerList(10000);
                stopwatch.Start();
                randomList.Quicksort();
                stopwatch.Stop();
                Console.WriteLine(stopwatch.Elapsed.TotalMilliseconds);
                stopwatch.Reset();
            }

            //randomList = GenerateIntegerList(1000000);
            //stopwatch.Start();
            //randomList.Quicksort();
            //PrintListToConsole(randomList.Quicksort());
            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.Elapsed.TotalMilliseconds);
            //stopwatch.Reset();


            //randomList = GenerateIntegerList(1000000);
            //stopwatch.Start();
            //randomList.QuicksortSequential();
            //PrintListToConsole(randomList.Quicksort());
            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.Elapsed.TotalMilliseconds);
            //stopwatch.Reset();
        }

        #region ListHelpers
        private static List<int> GenerateIntegerList(int elementCount = 20)
        {
            List<int> list = new List<int>();
            Random random = new Random();
            for (int i = 0; i < elementCount; i++)
            {
                list.Add(random.Next(1, 10000));
            }
            return list;
        }

        private static void PrintListToConsole(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (i < list.Count - 1)
                {
                    Console.Write($"{list[i]}, ");
                }
                else
                {
                    Console.Write($"{list[i]} ");
                }

            }
            Console.WriteLine();
        }
        #endregion
    }
}

namespace ExtensionMethods
{

    public static class ListExtensions
    {

        public static List<int> Quicksort(this List<int> list)
        {
            int low = 0;
            int high = list.Count - 1;
            int[] arr = list.ToArray();
            QuicksortListHelper(arr, low, high);
            //QuicksortListHelperSequential(arr, low, high);
            return arr.OfType<int>().ToList();
        }

        public static List<int> QuicksortSequential(this List<int> list)
        {
            int low = 0;
            int high = list.Count - 1;
            int[] arr = list.ToArray();
            QuicksortListHelperSequential(arr, low, high);
            return arr.OfType<int>().ToList();
        }

        private static void QuicksortListHelper(int[] arr, int low, int high)
        {
            const int THRESHOLD = 10000;
            int index;
            if (low < high)
            {
                if (high - low < THRESHOLD)
                {
                    index = Partition(arr, low, high);
                    QuicksortListHelperSequential(arr, low, index - 1);
                    QuicksortListHelperSequential(arr, index + 1, high);
                }
                else
                {
                    index = Partition(arr, low, high);

                    Parallel.Invoke(
                    () => QuicksortListHelper(arr, low, index - 1),
                    () => QuicksortListHelper(arr, index + 1, high)
                    );
                }
            }

        }

        private static void QuicksortListHelperSequential(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int index = Partition(arr, low, high);
                QuicksortListHelperSequential(arr, low, index - 1);
                QuicksortListHelperSequential(arr, index + 1, high);
            }
        }

        static private int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = (low - 1);

            for (int j = low; j <= high - 1; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                }
            }
            (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
            return (i + 1);
        }
    }
}


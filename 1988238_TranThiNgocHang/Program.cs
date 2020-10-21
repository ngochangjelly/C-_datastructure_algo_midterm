using System;
using System.IO;

namespace _1988238_TranThiNgocHang
{
    class Program
    {
        // helper functions
        public static void display_array_elements(int[] data)
        {
            foreach (var element in data)
            {
                Console.Write(element + " ");
            }
            Console.Write("\n\n");
        }

        public static int[] readHexNumbers(string filename)
        {
            string fileContent;
            try
            {
                fileContent = File.ReadAllText(filename);
                string[] words = fileContent.Split(' ');
                int length = words.Length;
                int i = 0;
                int[] intNumbers = new int[length];
                foreach (var word in words)
                {
                    int value = Int32.Parse(word, System.Globalization.NumberStyles.HexNumber);
                    intNumbers[i] = value;
                    i++;
                }
                return intNumbers;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.ToString());
                return new int[] { };
            }

        }
        public static void WriteStringToFile(string file_name, string content)
        {
            System.IO.File.WriteAllText(file_name, content);
        }
        public static string ConvertDecArrayToHexString(int[] int_array)
        {
            string sorted_array_string = " ";
            foreach (var element in int_array)
            {
                sorted_array_string = string.Concat(sorted_array_string, " ", string.Format("{0:x}", element).ToString());
            }
            return sorted_array_string;
        }
        // end helper functions

        // 2. selection search 
        public static int[] Selection_Sort(int[] data)
        {
            int smallest;
            for (int i = 0; i < data.Length - 1; i++)
            {
                smallest = i;

                for (int index = i + 1; index < data.Length; index++)
                {
                    if (data[index] < data[smallest])
                    {
                        smallest = index;
                    }
                }
                Swap(data, i, smallest);
            }
            return data;

        }

        public static void Swap(int[] data, int first, int second)
        {
            int temporary = data[first];
            data[first] = data[second];
            data[second] = temporary;
        }
        // 2. end selection sort

        // 4. binary search
        public static int BinarySearch(int[] arr, int key)
        {
            int min = 0;
            int max = arr.Length - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (key == arr[mid])
                {
                    return ++mid;
                }
                else if (key < arr[mid])
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            return -1;
        }
        // 4. end binary search



        // main function
        public static void Main(string[] args)
        {
            // 1
            string hex_number_file_name = "hex_number.txt";
            int[] res = readHexNumbers(hex_number_file_name);

            // 2, 3
            int[] sorted_int_array = Selection_Sort(res);
            string sorted_hex_array_string = ConvertDecArrayToHexString(sorted_int_array);
            string sorted_numbers_file_name = "sorted_numbers.txt";
            WriteStringToFile(sorted_numbers_file_name, sorted_hex_array_string);

            // 4
            int number_to_find = 36;
            int position = BinarySearch(sorted_int_array, number_to_find);
            Console.WriteLine(string.Concat("position of number ", number_to_find, " in this array is: ", position));

        }
    }
}

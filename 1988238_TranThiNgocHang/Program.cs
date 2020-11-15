using System;
using System.IO;
using System.Collections;


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
                // get file in current directory instead of bin/Debug/netcoreapp3.1
                fileContent = File.ReadAllText(@"../../../" + filename);
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
            System.IO.File.WriteAllText(@"../../../" + file_name, content);
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


        // 5. queue
        internal class Queue
        {
            internal int front;
            internal int rear;
            internal int size;

            internal int capacity;

            internal int[] array;

            public Queue(int capacity)
            {
                this.capacity = capacity;
                front = this.size = 0;
                rear = capacity - 1;
                array = new int[capacity];
            }
        }

        internal class QueueHelper
        {
            // Queue is full when size becomes equal to 
            // the capacity 
            internal bool IsFull(Queue queue)
            {
                return (queue.size == queue.capacity);
            }

            // Queue is empty when size is 0
            internal bool IsEmpty(Queue queue)
            {
                return (queue.size == 0);
            }

            // Method to add an item to the queue. We will add items from rear of Queue.
            // It changes rear and size
            internal void Push(Queue queue, int item)
            {
                if (IsFull(queue))
                {
                    return;
                }
                queue.rear = (queue.rear + 1) % queue.capacity;
                queue.array[queue.rear] = item;
                queue.size = queue.size + 1;
            }

            // Method to remove an item from queue. We will remove items from front of Queue
            // It changes front and size
            internal void Pop(Queue queue)
            {
                if (IsEmpty(queue))
                {
                    Console.WriteLine("The Queue is empty");
                    return;
                }

                int item = queue.array[queue.front];
                queue.front = (queue.front + 1) % queue.capacity;
                queue.size = queue.size - 1;
                Console.WriteLine("Item deleted is {0}", item);
            }

            // Method to get front element of queue
            internal void GetFrontElement(Queue queue)
            {
                if (IsEmpty(queue))
                {
                    Console.WriteLine("The Queue is empty");
                    return;
                }
                Console.WriteLine("Front : {0}", queue.array[queue.front]);
            }

            // Method to get rear element of queue
            internal void GetRearElement(Queue queue)
            {
                if (IsEmpty(queue))
                {
                    Console.WriteLine("The Queue is empty");
                    return;
                }
                Console.WriteLine("Rear : {0}", queue.array[queue.rear]);
            }

            internal void PrintQueue(Queue queue)
            {
                for (int i = queue.front; i <= queue.rear; i++)
                {
                    Console.Write(queue.array[i] + " ");
                }
                Console.WriteLine();
            }
        }

        // end queue implementation

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
            Console.WriteLine("Enter the number you want to find: ");
            int number_to_find = int.Parse(Console.ReadLine());
            int position = BinarySearch(sorted_int_array, number_to_find);
            
            if (position == -1)
            {
                Console.WriteLine("Not found!");
            } else
                Console.WriteLine(string.Concat("position of ", number_to_find, " in this array is: ", position));
            {

            }

            // 5
            //create a queue with int array length
            Queue even_queue = new Queue(res.Length);
            Queue odd_queue = new Queue(res.Length);
            QueueHelper my_queue_helper = new QueueHelper();
            foreach (var element in res)
            {
                if (element % 2 == 0)
                {
                    my_queue_helper.Push(even_queue, element);
                }
                else
                {
                    my_queue_helper.Push(odd_queue, element);
                }
            }
            Console.WriteLine("Odd queue:");
            my_queue_helper.PrintQueue(odd_queue);
            Console.WriteLine("Even queue:");
            my_queue_helper.PrintQueue(even_queue);
            string odd_number_file_content = "";
            string even_number_file_content = "";
            string odd_queue_file_name = "odd_queue.txt";
            string even_queue_file_name = "even_queue.txt";
            for (int i = 0; i < odd_queue.size - 1; i++)
            {
                odd_number_file_content  += " " +odd_queue.array[i];
            }
            for (int i = 0; i < even_queue.size - 1; i++)
            {
                even_number_file_content += " " + even_queue.array[i];
            }
            WriteStringToFile(odd_queue_file_name, odd_number_file_content);
            Console.WriteLine("Write odd numbers queue into " + odd_queue_file_name + '!');
            WriteStringToFile(even_queue_file_name, even_number_file_content);
            Console.WriteLine("Write even numbers queue into " + even_queue_file_name + '!');
        }
    }
}

using System;
//Sean Botha - 577288
namespace Assignment1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Declare and initialize array
            string[] subjects = { "MAT181", "PRG181", "STA181", "LPR181", "COA181", "PRG182" };
            //Get the length of the array
            int size = subjects.Length;
            //Get the user input for what to search for
            Console.WriteLine("Enter the subject code you want to search for");
            string subject = Console.ReadLine();
            //Get the user input for what sort method to use
            Console.WriteLine("Enter the type of sort method you'd like to use\n1:Bubble\n2:Quick\n3:Merge");
            int sort = Convert.ToInt32(Console.ReadLine());
            //Get user input for what search method to use
            Console.WriteLine("Enter the type of search method you'd like to use\n1:Linear\n2:Binary");
            int search = Convert.ToInt32(Console.ReadLine());

            //Print the sorted array
            Console.WriteLine(optionSort(sort, subjects, size));
            //Print what index the subject was found at
            Console.WriteLine(optionSearch(search, subjects, subject, size));
        }

        //Search methods
        //Linear
        static string linearSearch(string[] sa, string s, int l)
        {
            //Loop through the array
            for (int i = 0; i < l; i++)
            {
                //If the searched for item matches the array at that element, return that element
                if (sa[i].Equals(s))
                {
                    return sa[i] + " was found at index: " + (i + 1);
                }
            }
            return "Subject not found";
        }

        //Binary
        static string binarySearch(string[] sa, string s, int lower, int upper)
        {
            //Get the middle element
            int mid = lower + (upper - lower) / 2;

            //Test to see if the searched for string equals the middle element
            //Note - eventually this will be true and the element will be returned
            if (sa[mid].Equals(s))
            {
                return sa[mid] + " was found at index: " + (mid + 1);
            }

            //Test to see if the searched for element is to the left or right of the array
            //If it is to the left this else if statement will run
            else if (sa[mid].CompareTo(s) > 0)
            {
                //Get a new upper limit
                upper = mid - 1;
                //Returning the method essentially creates a loop that will loop until the searched for element equals the middle element
                return binarySearch(sa, s, lower, upper);
            }

            //If it is to the right this else statement will run
            else
            {
                //Get a new lower limit
                lower = mid + 1;
                return binarySearch(sa, s, lower, upper);
            }
            return "Subject not found";
        }

        //Sorting
        //Bubble
        static void bubbleSort(string[] sa, int l)
        {
            //Loop to compare the first element
            for (int i = 0; i < l - 1; i++)
            {
                //Loop to compare the following element
                for (int j = i + 1; j < l; j++)
                {
                    //Compare the two elements
                    if (sa[j].CompareTo(sa[i]) < 0)
                    {
                        //Sort the elements
                        swap(sa, i, j);
                    }
                }
            }
        }

        //Quick
        //Helper method used in partition and bubble sort methods
        static void swap(string[] sa, int i, int j)
        {
            string temp = sa[i];
            sa[i] = sa[j];
            sa[j] = temp;
        }
        //Helper method - Partition algorithm (last element)
        static int partition(string[] sa, int lower, int upper)
        {
            //Get the pivot element
            string pivot = sa[upper];

            //Index of smaller element
            int i = (lower - 1);

            for (int j = lower; j < upper; j++)
            {
                //Test to see if current element is smaller than the pivot
                if (sa[j].CompareTo(pivot) < 0)
                {
                    //Increase the smaller element and swap
                    i++;
                    swap(sa, i, j);
                }
            }
            swap(sa, i + 1, upper);
            return (i + 1);
        }
        static void quickSort(string[] sa, int lower, int upper)
        {
            if (lower < upper)
            {
                int pi = partition(sa, lower, upper);

                //Separately sort elements before partition and after partition
                quickSort(sa, lower, pi - 1);
                quickSort(sa, pi + 1, upper);
            }
        }

        static void merge(string[] sa, int lower, int mid, int upper)
        {
            //Find sizes of two subarrays to be merged
            int n1 = mid - lower + 1;
            int n2 = upper - mid;

            //Create temp arrays
            string[] one = new string[n1];
            string[] two = new string[n2];
            int i, j;

            //Copy data to temp arrays
            for (i = 0; i < n1; ++i)
            {
                one[i] = sa[lower + i];
            }
            for (j = 0; j < n2; ++j)
            {
                two[j] = sa[mid + 1 + j];
            }
            i = 0;
            j = 0;

            int k = lower;
            //Merge the arrays again
            while (i < n1 && j < n2)
            {
                if (one[i].CompareTo(two[j]) < 0)
                {
                    sa[k] = one[i];
                    i++;
                }
                else
                {
                    sa[k] = two[j];
                    j++;
                }
                k++;
            }

            //Copy remaining elements
            while (i < n1)
            {
                sa[k] = one[i];
                i++;
                k++;
            }
            while (j < n2)
            {
                sa[k] = two[j];
                j++;
                k++;
            }
        }

        static void mergesort(string[] sa, int lower, int upper)
        {
            if (lower < upper)
            {
                //Find the midpoint
                int mid = lower + (upper - lower) / 2;

                //Sort the halves
                mergesort(sa, lower, mid);
                mergesort(sa, mid + 1, upper);

                //Merge the sorted halves
                merge(sa, lower, mid, upper);
            }
        }

        //toString method to display the array's contents
        public static string displaySubjects(string[] sa, int s)
        {
            string sorted = "";
            for (int i = 0; i < s; i++)
            {
                sorted += sa[i] + " ";
            }
            return sorted;
        }

        //Test which sort method was chosen by user
        public static string optionSort(int sort, string[] sa, int size)
        {
            switch (sort)
            {
                case 1:
                    bubbleSort(sa, size);
                    return displaySubjects(sa, size);
                    break;


                case 2:
                    quickSort(sa, 0, size);
                    return displaySubjects(sa, size);
                    break;

                case 3:
                    mergesort(sa, 0, size);
                    return displaySubjects(sa, size);
                    break;

                default:
                    return "Invalid sort option has been chosen";
                    break;
            }
        }

        //Test which search option was chosen
        public static string optionSearch(int search, string[] sa, string s, int size)
        {
            switch (search)
            {
                case 1:
                    return linearSearch(sa, s, size);
                    break;

                case 2:
                    return binarySearch(sa, s, 0, size);
                    break;

                default:
                    return "Invalid search option has been chosen";
                    break;
            }
        }
    }
}
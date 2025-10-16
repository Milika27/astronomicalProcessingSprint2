//Milika Geldenhuys, team name, sprint 1 
//date: 18/09/2025
//version: 1.0
//AstronomicalProcessing
//the program records the number of neutrino interactions per hour for 24 hours.
//these hourly values are displayed in a list box.
//The program can sort these values, search within the array and the data can be edited.


using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AstronomicalProcessing1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //array of 24 elements
        static int max = 24;
        int[] neutInter = new int[max];
        bool empty = true;

        //initialise random
        Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();

        }

        private void DisplayError(string msg, string caption)
        {
            MessageBox.Show($"{msg}", $"{caption}", MessageBoxButton.OK, MessageBoxImage.Error);
        }//end DisplayError

        private void DisplayMessage(string msg, string caption)
        {
            MessageBox.Show($"{msg}", $"{caption}", MessageBoxButton.OK, MessageBoxImage.Information);
        }// end DisplayMessage

        private void DisplayStatus(string statusMsg)
        {
            txtStatus.Text = statusMsg;
        }//end DisplayStatus

        //display array method
        private void DisplayArray()
        {
            if (!empty) //if the listbox is not empty
            {
                lbxInput.Items.Clear();
                for (int i = 0; i < max; i++)
                {
                    lbxInput.Items.Add($"{neutInter[i]}");//display items from the neutInter array
                }
            }
            else//if we have no array to call from an error occurs 
            {
                DisplayStatus("Nothing to display!");
                DisplayError("Nothing to display", "Invalid Operation");
            }
        }//end DisplayArray

        private int BinarySearch(int[] neutInter, int input)
        {
            int low = 0;
            int high = neutInter.Length - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;

                if (input == neutInter[mid])
                    return mid; //found at index mid
                else if (neutInter[mid] < input)
                    low = mid + 1; //search right half
                else
                    high = mid - 1; //search left half
            }
            return -1; //not found
        }//end BinarySearch

        //fill array with integers
        private void btnDisplayData_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < max; i++)
            {
                neutInter[i] = random.Next(100 + 1);//displays the array with 24 random numbers ranging between 0-100
            }
            empty = false;
            DisplayArray();
            DisplayStatus("Data Loaded");
            DisplayMessage("Data loaded into array.", "Success");

        }//end btnDisplayData_Click

        private void BubbleSort()
        {
            if (empty)//if no data is loaded in yet an error occurs
            {
                DisplayError("No data to sort. Please load data first.", "Sort Error");
                return;
            }

            //sort the array using a bubble sort
            int n = neutInter.Length;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (neutInter[j] > neutInter[j + 1])//determines if two adjacent elements are out of order and need to be swapped
                    {
                        //swap elements until sorted
                        int temp = neutInter[j];
                        neutInter[j] = neutInter[j + 1];
                        neutInter[j + 1] = temp;
                    }
                }

            }
            DisplayArray();
            DisplayStatus("Data Sorted");
            DisplayMessage("Data has been sorted.", "Sort Successful");

        }//end btnSort_Click

        //search using the BinarySearch method
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            BubbleSort();//sorts data when you press the search button

            if (int.TryParse(txtSearch.Text, out int input))
            {
                int index = BinarySearch(neutInter, input);

                if (index != -1)//checks whether the input exists in the listbox array
                {
                    DisplayStatus($"Search result = {input}");
                    DisplayMessage($"Found {input} at index {index}.", "Search Result");
                    lbxInput.SelectedIndex = index;
                    lbxInput.ScrollIntoView(lbxInput.Items[index]);
                }
                else //if the int that the user searches for is not in the listbox an error occurs
                {
                    DisplayMessage($"{input} not found in the data.", "Search Result");
                }

                txtSearch.Clear();//clear textbox after operation
            }
            else//error displayed when an invalid input is taken in
            {
                DisplayStatus("input error");
                DisplayError("Please enter a valid number to search for.", "Input Error");
            }

        }//btnSearch_Click

        //edit a selected array item with a new value provided by the user 
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            if (!int.TryParse(txtEdit.Text, out int newValue)) //if the user inputs a string an error occurs
            {
                DisplayStatus("Invalid selection");
                DisplayError("Please enter a valid number in the edit text box.", "Input Error");
                return;
            }

            if (lbxInput.SelectedIndex == -1) //if no item is selected in the listbox an error occurs 
            {
                DisplayStatus("Invalid selection");
                DisplayError("Please select an item to edit from the list.", "Selection Required");
                return;
            }

            //if the user selects a valid int 
            int selectedIndex = lbxInput.SelectedIndex;
            neutInter[selectedIndex] = newValue;

            DisplayArray();
            DisplayStatus($"Updated index {selectedIndex} to value {newValue}");
            DisplayMessage($"Item at index {selectedIndex} updated.", "Edit Successful");

            txtEdit.Clear();//clear textbox after operation
        }

    }//end class
}//end namespace
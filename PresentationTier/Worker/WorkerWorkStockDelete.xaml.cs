using ApplicationTier;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace PresentationTier.Worker
{
    /// <summary>
    ///     This class contains UI contents (within the .xaml class) as well as a Button Click Method which houses the methods which
    ///     control what happens upon activating the event handler. 
    ///     
    ///     In this case it deletes a Stock from the database.
    ///     
    ///     The class also creates new objects of the relevant type, depending on which table of the database it is dealing with. 
    ///     It calls methods from the Business Logic Layer to fill those objects with data, mainly using the data inputted by the
    ///     user in the varying text boxes/date fields found in the UI as the method parameters. Occasionally it will also use 
    ///     other object data that has already been returned as the method parameters.
    ///     
    ///     The class also contains several navigation Controls, which control how the user moves around the application.
    /// 
    ///     The layout of my three-layer architecture was based heavily on The Code Project's 'Three Layer Architecture in C# .NET'
    ///     by Parikshit Patel found here: https://www.codeproject.com/Articles/36847/Three-Layer-Architecture-in-C-NET-2. As such 
    ///     the DbConnection.cs, DataAccessLayer.cs, BusinessLogicLayer, 'X'Object.cs and some of the .xaml.cs classes found 
    ///     throughout the Presentation Tier use either whole or small bits of code from this walk-through. However, many aspects 
    ///     of the code found in the walk-through have either been built upon or modified heavily and will bare very little 
    ///     resemblance, if any, to the original; to suit the nature of my project, and are my own ideas. This paragraph will be 
    ///     found in the DbConnection.cs, DataAccessLayer.cs, BusinessLogicLayer.cs, XObject.cs classes as well as in all .xaml.cs 
    ///     classes found in the Presentation Tier for referencing purposes.
    /// </summary>
    public partial class WorkerWorkStockDelete : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI,
        ///     Focuses on the first UI input box,
        ///     And instantiates the BusinessLogicLayer class, creating an object of that type.
        /// </summary>
        public WorkerWorkStockDelete()
        {
            InitializeComponent();
            stockIdInput.Focus();
            businessLogicLayer = new BusinessLogicLayer();
        }

        /// <summary>
        ///     Deletes a Stock from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (stockIdInput.Text.Equals("") || locationIdInput.Text.Equals(""))
                {
                    MessageBox.Show("Please input the text boxes.");
                    stockIdInput.Focus();
                    return;
                }
                else if ((!Regex.IsMatch(stockIdInput.Text, "^[0-9]*$")) || (!Regex.IsMatch(locationIdInput.Text, "^[0-9]*$")))
                {
                    MessageBox.Show("Please input only numerical characters into the text boxes.");
                    stockIdInput.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred, please contact your administrator." + "\n\n" + "The error message is: " + "\n\n" + ex.ToString());
            }

            StockObject stock;
            StockObject stockId = businessLogicLayer.CheckStockByID(int.Parse(stockIdInput.Text));
            StockObject stockIdAndLocationId = businessLogicLayer.CheckStockByIDAndLocation(int.Parse(stockIdInput.Text), int.Parse(locationIdInput.Text));
            StockObject stockAllocated = businessLogicLayer.CheckStockIsAllocated(int.Parse(stockIdInput.Text));
            int stockAllocatedQuantity = stockAllocated.allocated_quantity;
            LocationObject locationId = businessLogicLayer.CheckLocationsByID(int.Parse(locationIdInput.Text));
            LocationObject location;

            try
            {
                if (!int.Parse(stockIdInput.Text).Equals(stockId.stock_id))
                {
                    MessageBox.Show("The Stock ID provided does not exist.");
                    stockIdInput.Focus();
                    return;
                }
                else if (!int.Parse(locationIdInput.Text).Equals(locationId.location_id))
                {
                    MessageBox.Show("The Location ID provided does not exist.");
                    stockIdInput.Focus();
                    return;
                }
                else if (!int.Parse(stockIdInput.Text).Equals(stockIdAndLocationId.stock_id) && !int.Parse(locationIdInput.Text).Equals(stockIdAndLocationId.location_id))
                {
                    MessageBox.Show("The Stock ID and Location ID provided do not match.");
                    stockIdInput.Focus();
                    return;

                }
                else if (stockAllocatedQuantity > 0)
                {
                    MessageBox.Show("The Stock provided is currently allocated towards an order and so cannot be deleted. \n\nPlease contact an administrator if the stock still needs to be deleted.");
                    stockIdInput.Focus();
                    return;
                }
                else
                {
                    stock = businessLogicLayer.DeleteCurrentStock(int.Parse(stockIdInput.Text));
                    location = businessLogicLayer.MarkLocationUnallocated(int.Parse(locationIdInput.Text));
                    MessageBox.Show("The provided stock has been deleted from the system. \n\nThe location it occupied has now been unallocated.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred, please contact your administrator." + "\n\n" + "The error message is: " + "\n\n" + ex.ToString());
            }
        }

        /// <summary>
        ///     Navigation methods that instantiate the class it wants to move to and then uses the built in WPF 
        ///     'NavigationService' to do move to that object type.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            WorkerViewStock workerViewStock = new WorkerViewStock();
            this.NavigationService.Navigate(workerViewStock);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            WorkerWorkStockAdd workerWorkStockAdd = new WorkerWorkStockAdd();
            this.NavigationService.Navigate(workerWorkStockAdd);
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            WorkerWorkStockDelete workerWorkStockDelete = new WorkerWorkStockDelete();
            this.NavigationService.Navigate(workerWorkStockDelete);
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            WorkerViewLocations workerViewLocations = new WorkerViewLocations();
            this.NavigationService.Navigate(workerViewLocations);
        }

        private void Button_Click5(object sender, RoutedEventArgs e)
        {
            WorkerViewWarehouses workerViewWarehouses = new WorkerViewWarehouses();
            this.NavigationService.Navigate(workerViewWarehouses);
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            WorkerHomepage workerHomepage = new WorkerHomepage();
            this.NavigationService.Navigate(workerHomepage);
        }
    }
}
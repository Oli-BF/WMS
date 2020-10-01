using ApplicationTier;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace PresentationTier.Manager
{
    /// <summary>
    ///     This class contains UI contents (within the .xaml class) as well as a Button Click Method which houses the methods which
    ///     control what happens upon activating the event handler. 
    ///     
    ///     In this case it edits a stock in the database.
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
    public partial class ManagerManageStockEdit : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI,
        ///     Focuses on the first UI input box,
        ///     And instantiates the BusinessLogicLayer class, creating an object of that type.
        /// </summary>
        public ManagerManageStockEdit()
        {
            InitializeComponent();
            currentStockIdInput.Focus();
            businessLogicLayer = new BusinessLogicLayer();
        }

        /// <summary>
        ///     Edits a stock in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentStockIdInput.Text.Equals("") || currentLocationIdInput.Text.Equals("") || 
                    newAccountIdInput.Text.Equals("") || newProductIdInput.Text.Equals("") || newWarehouseIdInput.Text.Equals("") || newLocationIdInput.Equals("") ||
                    newQuantityInput.Text.Equals("") || newAllocatedQuantityInput.Text.Equals("") || newAvailabilityStatusInput.Text.Equals(""))
                {
                    MessageBox.Show("Please input all text boxes.");
                    currentStockIdInput.Focus();
                    return;
                }
                else if ((!Regex.IsMatch(currentStockIdInput.Text, "^[0-9]*$")) || (!Regex.IsMatch(currentLocationIdInput.Text, "^[0-9]*$")) ||
                         (!Regex.IsMatch(newAccountIdInput.Text, "^[0-9]*$")) || (!Regex.IsMatch(newProductIdInput.Text, "^[0-9]*$")) ||
                         (!Regex.IsMatch(newWarehouseIdInput.Text, "^[0-9]*$")) || (!Regex.IsMatch(newLocationIdInput.Text, "^[0-9]*$")) ||
                         (!Regex.IsMatch(newQuantityInput.Text, "^[0-9]*$")) || (!Regex.IsMatch(newAllocatedQuantityInput.Text, "^[0-9]*$")))
                {
                    MessageBox.Show("Please input only numerical characters into all text boxes apart from the current and new 'Availability Status Input'.");
                    currentStockIdInput.Focus();
                    return;
                }
                else if (!(newAvailabilityStatusInput.Text.ToLower().Equals("true") || newAvailabilityStatusInput.Text.ToLower().Equals("false")))
                {
                    MessageBox.Show("Please input only 'True' or 'False' into the current and new Availability Status Input text boxes.");
                    newAvailabilityStatusInput.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred, please contact your administrator." + "\n\n" + "The error message is: " + "\n\n" + ex.ToString());
            }

            StockObject stock;
            StockObject stockCurrentId = businessLogicLayer.CheckStockByID(int.Parse(currentStockIdInput.Text));
            StockObject stockCurrentIdAndWarehouseAndLocation = businessLogicLayer.CheckStockByIDAndWarehouseAndLocation(int.Parse(currentStockIdInput.Text),
                                                                                                                         int.Parse(currentWarehouseIdInput.Text),
                                                                                                                         int.Parse(currentLocationIdInput.Text));
            StockObject stockCurrentWarehouseAndLocation = businessLogicLayer.CheckStockByWarehouseAndLocation(int.Parse(currentWarehouseIdInput.Text), 
                                                                                                               int.Parse(currentLocationIdInput.Text));
            StockObject stockNewWarehouseAndLocation = businessLogicLayer.CheckStockByWarehouseAndLocation(int.Parse(newWarehouseIdInput.Text), 
                                                                                                           int.Parse(newLocationIdInput.Text));
            LocationObject locationNewLocationByWarehouse = businessLogicLayer.CheckLocationByIDAndWarehouse(int.Parse(newLocationIdInput.Text),
                                                                                                             int.Parse(newWarehouseIdInput.Text));
            AccountObject accountId = businessLogicLayer.CheckAccountsByID(int.Parse(newAccountIdInput.Text));
            ProductObject productId = businessLogicLayer.CheckProductsByID(int.Parse(newProductIdInput.Text));
            WarehouseObject warehouseId = businessLogicLayer.CheckWarehousesByID(int.Parse(newWarehouseIdInput.Text));
            LocationObject locationId = businessLogicLayer.CheckLocationsByID(int.Parse(newLocationIdInput.Text));
            LocationObject locationUnallocated;
            LocationObject locationAllocated;

            try
            {
                if (!int.Parse(currentStockIdInput.Text).Equals(stockCurrentId.stock_id))
                {
                    MessageBox.Show("The current Stock ID provided does not exist.");
                    currentStockIdInput.Focus();
                    return;
                }
                else if (!int.Parse(currentStockIdInput.Text).Equals(stockCurrentIdAndWarehouseAndLocation.stock_id) &&
                         !int.Parse(currentWarehouseIdInput.Text).Equals(stockCurrentIdAndWarehouseAndLocation.warehouse_id) &&
                         !int.Parse(currentLocationIdInput.Text).Equals(stockCurrentIdAndWarehouseAndLocation.location_id))
                {
                    MessageBox.Show("The current Stock provided does not exist in that Warehouse & Location.");
                    currentStockIdInput.Focus();
                    return;
                }
                else if (!int.Parse(newAccountIdInput.Text).Equals(accountId.account_id))
                {
                    MessageBox.Show("The new Account ID provided does not exist.");
                    newAccountIdInput.Focus();
                    return;
                }
                else if (!int.Parse(newProductIdInput.Text).Equals(productId.product_id))
                {
                    MessageBox.Show("The new product ID provided does not exist.");
                    newProductIdInput.Focus();
                    return;
                }
                else if (!int.Parse(newWarehouseIdInput.Text).Equals(warehouseId.warehouse_id))
                {
                    MessageBox.Show("The new Warehouse ID provided does not exist.");
                    newWarehouseIdInput.Focus();
                    return;
                }
                else if (!int.Parse(newLocationIdInput.Text).Equals(locationId.location_id))
                {
                    MessageBox.Show("The new Location ID provided does not exist.");
                    newLocationIdInput.Focus();
                    return;
                }
                else if (int.Parse(newQuantityInput.Text) < int.Parse(newAllocatedQuantityInput.Text))
                {
                    MessageBox.Show("The new Allocated Quantity cannot be greater than the new Quantity.");
                    newQuantityInput.Focus();
                    return;
                }
                else if (!int.Parse(newLocationIdInput.Text).Equals(locationNewLocationByWarehouse.location_id))
                {
                    MessageBox.Show("The new Location ID provided does not exist in that warehouse.");
                    currentStockIdInput.Focus();
                    return;
                }
                else if (int.Parse(newWarehouseIdInput.Text).Equals(stockNewWarehouseAndLocation.warehouse_id) && 
                         !int.Parse(newWarehouseIdInput.Text).Equals(stockCurrentWarehouseAndLocation.warehouse_id) ||
                         int.Parse(newLocationIdInput.Text).Equals(stockNewWarehouseAndLocation.location_id) && 
                         !int.Parse(newLocationIdInput.Text).Equals(stockCurrentWarehouseAndLocation.location_id))
                {
                    MessageBox.Show("Stock already exists in that Warehouse and Location.");
                    newWarehouseIdInput.Focus();
                    return;
                }
                else
                {
                    stock = businessLogicLayer.EditCurrentStock(int.Parse(newAccountIdInput.Text), int.Parse(newProductIdInput.Text), int.Parse(newWarehouseIdInput.Text),
                                                                int.Parse(newLocationIdInput.Text), int.Parse(newQuantityInput.Text), int.Parse(newAllocatedQuantityInput.Text),
                                                                bool.Parse(newAvailabilityStatusInput.Text.ToLower()),
                                                                int.Parse(currentStockIdInput.Text));
                    locationUnallocated = businessLogicLayer.MarkLocationUnallocated(int.Parse(currentLocationIdInput.Text));
                    locationAllocated = businessLogicLayer.MarkLocationAllocated(int.Parse(newLocationIdInput.Text));
                    MessageBox.Show("The Stock provided has been updated. \n\nThe current Location has been marked unallocated. \n\nThe new Location has been marked allocated");
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
            ManagerViewStockAll managerViewStockAll = new ManagerViewStockAll();
            this.NavigationService.Navigate(managerViewStockAll);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            ManagerManageStockAdd managerManageStockAdd = new ManagerManageStockAdd();
            this.NavigationService.Navigate(managerManageStockAdd);
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            ManagerManageStockDelete managerManageStockDelete = new ManagerManageStockDelete();
            this.NavigationService.Navigate(managerManageStockDelete);
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            ManagerManageStockEdit managerManageStockEdit = new ManagerManageStockEdit();
            this.NavigationService.Navigate(managerManageStockEdit);
        }

        private void Button_Click5(object sender, RoutedEventArgs e)
        {
            ManagerViewLocationsAll ManagerViewLocationsAll = new ManagerViewLocationsAll();
            this.NavigationService.Navigate(ManagerViewLocationsAll);
        }

        private void Button_Click6(object sender, RoutedEventArgs e)
        {
            ManagerViewWarehousesAll managerViewWarehousesAll = new ManagerViewWarehousesAll();
            this.NavigationService.Navigate(managerViewWarehousesAll);
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            ManagerHomepage managerHomepage = new ManagerHomepage();
            this.NavigationService.Navigate(managerHomepage);
        }
    }
}
﻿using ApplicationTier;
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
    ///     In this case it adds a new Stock to the database.
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
    public partial class WorkerWorkStockAdd : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI,
        ///     Focuses on the first UI input box,
        ///     And instantiates the BusinessLogicLayer class, creating an object of that type.
        /// </summary>
        public WorkerWorkStockAdd()
        {
            InitializeComponent();
            accountIdInput.Focus();
            businessLogicLayer = new BusinessLogicLayer();
        }

        /// <summary>
        ///     Add new Stock to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (accountIdInput.Text.Equals("") || productIdInput.Text.Equals("") || warehouseIdInput.Text.Equals("") || quantityInput.Text.Equals(""))
                {
                    MessageBox.Show("Please input all text boxes.");
                    accountIdInput.Focus();
                    return;
                }
                else if ((!Regex.IsMatch(accountIdInput.Text, "^[0-9]*$")) || (!Regex.IsMatch(productIdInput.Text, "^[0-9]*$")) ||
                         (!Regex.IsMatch(warehouseIdInput.Text, "^[0-9]*$")) || (!Regex.IsMatch(quantityInput.Text, "^[0-9]*$")))
                {
                    MessageBox.Show("Please input only numerical characters into all text boxes.");
                    accountIdInput.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred, please contact your administrator." + "\n\n" + "The error message is: " + "\n\n" + ex.ToString());
            }

            StockObject stock;
            AccountObject account = businessLogicLayer.CheckAccountsByID(int.Parse(accountIdInput.Text));
            ProductObject product = businessLogicLayer.CheckProductsByID(int.Parse(productIdInput.Text));
            WarehouseObject warehouse = businessLogicLayer.CheckWarehousesByID(int.Parse(warehouseIdInput.Text));
            LocationObject location = businessLogicLayer.FindFreeLocation(int.Parse(warehouseIdInput.Text));
            int stockLocation = location.location_id;

            try
            {
                if (!int.Parse(accountIdInput.Text).Equals(account.account_id))
                {
                    MessageBox.Show("The Account ID provided does not exist.");
                    accountIdInput.Focus();
                    return;
                }
                else if (!int.Parse(productIdInput.Text).Equals(product.product_id))
                {
                    MessageBox.Show("The product ID provided does not exist.");
                    productIdInput.Focus();
                    return;
                }
                else if (!int.Parse(warehouseIdInput.Text).Equals(warehouse.warehouse_id))
                {
                    MessageBox.Show("The Warehouse ID provided does not exist.");
                    warehouseIdInput.Focus();
                    return;
                }
                else if (stockLocation.Equals(0))
                {
                    MessageBox.Show("There are no more locations free in warehouse: " + warehouseIdInput.Text);
                    warehouseIdInput.Focus();
                    return;
                }
                else
                {
                    stock = businessLogicLayer.InsertNewStock(int.Parse(accountIdInput.Text), int.Parse(productIdInput.Text), int.Parse(warehouseIdInput.Text),
                                                              stockLocation, int.Parse(quantityInput.Text));
                    location = businessLogicLayer.MarkLocationAllocated(stockLocation);
                    MessageBox.Show("The Stock provided has been added to the system. \n\nA location has been found for the Stock and is now set as allocated. \n\nThe Stock (Pallet) is now ready to be put away, please see the location code as to where.");
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
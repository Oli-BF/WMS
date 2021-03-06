﻿using ApplicationTier;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PresentationTier.Admin
{
    /// <summary>
    ///     This class contains UI contents (within the .xaml class) as well as a Button Click Method which houses the methods which
    ///     control what happens upon activating the event handler. 
    ///     
    ///     In this case it edits a warehouse in the database.
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
    public partial class AdminManageWarehousesEdit : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI,
        ///     Focuses on the first UI input box,
        ///     And instantiates the BusinessLogicLayer class, creating an object of that type.
        /// </summary>
        public AdminManageWarehousesEdit()
        {
            InitializeComponent();
            currentWarehouseNameInput.Focus();
            businessLogicLayer = new BusinessLogicLayer();
        }

        /// <summary>
        ///     Edits a warehouse in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            WarehouseObject warehouse;
            WarehouseObject warehouseCurrent;
            WarehouseObject warehouseNew;
            warehouseCurrent = businessLogicLayer.CheckWarehousesByName(currentWarehouseNameInput.Text.ToLower());
            warehouseNew = businessLogicLayer.CheckWarehousesByName(newWarehouseNameInput.Text.ToLower());
            try
            {
                if (currentWarehouseNameInput.Text.Equals("") || newWarehouseNameInput.Text.Equals(""))
                {
                    MessageBox.Show("Please input all the text boxes.");
                    currentWarehouseNameInput.Focus();
                    return;
                }
                else if (!currentWarehouseNameInput.Text.ToLower().Equals(warehouseCurrent.name))
                {
                    MessageBox.Show("The 'Current Account Name' does not exist.");
                    currentWarehouseNameInput.Focus();
                    return;
                }
                else if (newWarehouseNameInput.Text.ToLower().Equals(warehouseNew.name))
                {
                    MessageBox.Show("The 'New Account Name' already exists.");
                    newWarehouseNameInput.Focus();
                    return;
                }
                else
                {
                    warehouse = businessLogicLayer.EditCurrentWarehouse(newWarehouseNameInput.Text.ToLower(), currentWarehouseNameInput.Text.ToLower());
                    MessageBox.Show(currentWarehouseNameInput.Text + " has been updated.");
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
            AdminViewWarehousesAll adminViewWarehousesAll = new AdminViewWarehousesAll();
            this.NavigationService.Navigate(adminViewWarehousesAll);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            AdminManageWarehousesAdd adminManageWarehousesAdd = new AdminManageWarehousesAdd();
            this.NavigationService.Navigate(adminManageWarehousesAdd);
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            AdminManageWarehousesDelete adminManageWarehousesDelete = new AdminManageWarehousesDelete();
            this.NavigationService.Navigate(adminManageWarehousesDelete);
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            AdminManageWarehousesEdit adminManageWarehousesEdit = new AdminManageWarehousesEdit();
            this.NavigationService.Navigate(adminManageWarehousesEdit);
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            AdminHomepage adminHomepage = new AdminHomepage();
            this.NavigationService.Navigate(adminHomepage);
        }
    }
}
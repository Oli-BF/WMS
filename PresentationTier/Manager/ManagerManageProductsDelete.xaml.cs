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
    ///     In this case it deletes a product from the database.
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
    public partial class ManagerManageProductsDelete : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI,
        ///     Focuses on the first UI input box,
        ///     And instantiates the BusinessLogicLayer class, creating an object of that type.
        /// </summary>
        public ManagerManageProductsDelete()
        {
            InitializeComponent();
            accountIdInput.Focus();
            businessLogicLayer = new BusinessLogicLayer();
        }

        /// <summary>
        ///     Deletes a product from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (accountIdInput.Text.Equals("") || titleInput.Text.Equals("") || skuInput.Text.Equals(""))
                {
                    MessageBox.Show("Please input all text boxes.");
                    accountIdInput.Focus();
                    return;
                }
                else if (!Regex.IsMatch(accountIdInput.Text, "^[0-9]*$"))
                {
                    MessageBox.Show("Please input only numerical characters into the Account ID text box.");
                    accountIdInput.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred, please contact your administrator." + "\n\n" + "The error message is: " + "\n\n" + ex.ToString());
            }

            ProductObject product = businessLogicLayer.CheckProductsAll(int.Parse(accountIdInput.Text), titleInput.Text.ToLower(), skuInput.Text.ToLower());

            try
            {
                if (!(int.Parse(accountIdInput.Text).Equals(product.account_id) && titleInput.Text.ToLower().Equals(product.title) && skuInput.Text.ToLower().Equals(product.sku)))
                {
                    MessageBox.Show("The product provided does not exist.");
                    accountIdInput.Focus();
                    return;
                }
                else
                {
                    product = businessLogicLayer.DeleteCurrentProduct(int.Parse(accountIdInput.Text), titleInput.Text.ToLower(), skuInput.Text.ToLower());
                    MessageBox.Show(titleInput.Text + " has been deleted from the system.");
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
            ManagerViewProductsAll managerViewProductsAll = new ManagerViewProductsAll();
            this.NavigationService.Navigate(managerViewProductsAll);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            ManagerManageProductsAdd managerManageProductsAdd = new ManagerManageProductsAdd();
            this.NavigationService.Navigate(managerManageProductsAdd);
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            ManagerManageProductsDelete managerManageProductsDelete = new ManagerManageProductsDelete();
            this.NavigationService.Navigate(managerManageProductsDelete);
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            ManagerManageProductsEdit managerManageProductsEdit = new ManagerManageProductsEdit();
            this.NavigationService.Navigate(managerManageProductsEdit);
        }

        private void Button_Click5(object sender, RoutedEventArgs e)
        {
            ManagerViewAccountsAll managerViewAccountsAll = new ManagerViewAccountsAll();
            this.NavigationService.Navigate(managerViewAccountsAll);
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            ManagerHomepage managerHomepage = new ManagerHomepage();
            this.NavigationService.Navigate(managerHomepage);
        }
    }
}
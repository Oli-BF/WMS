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
    ///     In this case it edits a product in the database.
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
    public partial class ManagerManageProductsEdit : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI,
        ///     Focuses on the first UI input box,
        ///     And instantiates the BusinessLogicLayer class, creating an object of that type.
        /// </summary>
        public ManagerManageProductsEdit()
        {
            InitializeComponent();
            currentAccountIdInput.Focus();
            businessLogicLayer = new BusinessLogicLayer();
        }

        /// <summary>
        ///     Edits a product in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentAccountIdInput.Text.Equals("") || currentTitleInput.Text.Equals("") || currentSkuInput.Text.Equals("") ||
                    newAccountIdInput.Text.Equals("") || newTitleInput.Text.Equals("") || newSkuInput.Text.Equals(""))
                {
                    MessageBox.Show("Please input all text boxes.");
                    currentAccountIdInput.Focus();
                    return;
                }
                else if ((!Regex.IsMatch(currentAccountIdInput.Text, "^[0-9]*$")) || (!Regex.IsMatch(newAccountIdInput.Text, "^[0-9]*$")))
                {
                    MessageBox.Show("Please input only numerical characters into the Account ID text box.");
                    currentAccountIdInput.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred, please contact your administrator." + "\n\n" + "The error message is: " + "\n\n" + ex.ToString());
            }

            ProductObject product;
            ProductObject productAll = businessLogicLayer.CheckProductsAll(int.Parse(currentAccountIdInput.Text), currentTitleInput.Text.ToLower(), currentSkuInput.Text.ToLower());
            ProductObject productCurrent = businessLogicLayer.CheckProductsByIDAndSku(int.Parse(currentAccountIdInput.Text), currentSkuInput.Text.ToLower());
            ProductObject productNew = businessLogicLayer.CheckProductsByIDAndSku(int.Parse(newAccountIdInput.Text), newSkuInput.Text.ToLower());
            AccountObject account = businessLogicLayer.CheckAccountsByID(int.Parse(newAccountIdInput.Text));

            try
            {
                if (!(int.Parse(currentAccountIdInput.Text).Equals(productAll.account_id) && currentTitleInput.Text.ToLower().Equals(productAll.title) && 
                      currentSkuInput.Text.ToLower().Equals(productAll.sku)))
                {
                    MessageBox.Show("The 'current' product provided does not exist.");
                    currentAccountIdInput.Focus();
                    return;
                }
                else if (!int.Parse(newAccountIdInput.Text).Equals(account.account_id))
                {
                    MessageBox.Show("The 'New Account ID' provided does not exist.");
                    newAccountIdInput.Focus();
                    return;
                }
                else if (int.Parse(newAccountIdInput.Text).Equals(productNew.account_id) && !int.Parse(newAccountIdInput.Text).Equals(productCurrent.account_id) ||
                         newSkuInput.Text.ToLower().Equals(productNew.sku) && !newSkuInput.Text.ToLower().Equals(productCurrent.sku))
                {
                    MessageBox.Show("An Account with that SKU already exists.");
                    newAccountIdInput.Focus();
                    return;
                }
                else
                {
                    product = businessLogicLayer.EditCurrentProduct(int.Parse(newAccountIdInput.Text), newTitleInput.Text.ToLower(), newSkuInput.Text.ToLower(),
                                                                    int.Parse(currentAccountIdInput.Text), currentTitleInput.Text.ToLower(), currentSkuInput.Text.ToLower());
                    MessageBox.Show(currentTitleInput.Text + " has been updated.");
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
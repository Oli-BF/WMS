using ApplicationTier;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PresentationTier.Admin
{
    /// <summary>
    ///     This class contains UI contents (within the .xaml class) as well as a Button Click Method which houses the methods which
    ///     control what happens upon activating the event handler. 
    ///     
    ///     In this case it deletes an account from the database.
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
    public partial class AdminManageAccountsDelete : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI,
        ///     Focuses on the first UI input box,
        ///     And instantiates the BusinessLogicLayer class, creating an object of that type.
        /// </summary>
        public AdminManageAccountsDelete()
        {
            InitializeComponent();
            accountNameInput.Focus();
            businessLogicLayer = new BusinessLogicLayer();
        }

        /// <summary>
        ///     Deletes an account from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            AccountObject account = new AccountObject();
            account = businessLogicLayer.CheckAccountsByName(accountNameInput.Text.ToLower());
            try
            {
                if (accountNameInput.Text.Equals(""))
                {
                    MessageBox.Show("Please input the text box.");
                    accountNameInput.Focus();
                    return;
                }
                else if (!accountNameInput.Text.ToLower().Equals(account.name))
                {
                    MessageBox.Show("An account with that name does not exist.");
                    accountNameInput.Focus();
                    return;
                }
                else
                {
                    account = businessLogicLayer.DeleteCurrentAccount(accountNameInput.Text.ToLower());
                    MessageBox.Show(accountNameInput.Text + " has been deleted from the system.");
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
            AdminViewAccountsAll adminViewAccountsAll = new AdminViewAccountsAll();
            this.NavigationService.Navigate(adminViewAccountsAll);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            AdminManageAccountsAdd adminManageAccountsAdd = new AdminManageAccountsAdd();
            this.NavigationService.Navigate(adminManageAccountsAdd);
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            AdminManageAccountsDelete adminManageAccountsDelete = new AdminManageAccountsDelete();
            this.NavigationService.Navigate(adminManageAccountsDelete);
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            AdminManageAccountsEdit adminManageAccountsEdit = new AdminManageAccountsEdit();
            this.NavigationService.Navigate(adminManageAccountsEdit);
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            AdminHomepage adminHomepage = new AdminHomepage();
            this.NavigationService.Navigate(adminHomepage);
        }
    }
}
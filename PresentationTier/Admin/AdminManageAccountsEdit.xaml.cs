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
    ///     In this case it edits an account in the database.
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
    public partial class AdminManageAccountsEdit : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI,
        ///     Focuses on the first UI input box,
        ///     And instantiates the BusinessLogicLayer class, creating an object of that type.
        /// </summary>
        public AdminManageAccountsEdit()
        {
            InitializeComponent();
            currentAccountNameInput.Focus();
            businessLogicLayer = new BusinessLogicLayer();
        }

        /// <summary>
        ///     Edits an account in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            AccountObject account;
            AccountObject accountCurrent;
            AccountObject accountNew;
            accountCurrent = businessLogicLayer.CheckAccountsByName(currentAccountNameInput.Text.ToLower());
            accountNew = businessLogicLayer.CheckAccountsByName(newAccountNameInput.Text.ToLower());
            try
            {
                if (currentAccountNameInput.Text.Equals("") || newAccountNameInput.Text.Equals(""))
                {
                    MessageBox.Show("Please input all the text boxes.");
                    currentAccountNameInput.Focus();
                    return;
                }
                else if (!currentAccountNameInput.Text.ToLower().Equals(accountCurrent.name))
                {
                    MessageBox.Show("The 'Current Account Name' does not exist.");
                    currentAccountNameInput.Focus();
                    return;
                }
                else if (newAccountNameInput.Text.ToLower().Equals(accountNew.name))
                {
                    MessageBox.Show("The 'New Account Name' already exists.");
                    newAccountNameInput.Focus();
                    return;
                }
                else
                {
                    account = businessLogicLayer.EditCurrentAccount(newAccountNameInput.Text.ToLower(), currentAccountNameInput.Text.ToLower());
                    MessageBox.Show(currentAccountNameInput.Text + " has been updated.");
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
using ApplicationTier;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace PresentationTier.Admin
{
    /// <summary>
    ///     This class contains UI contents (within the .xaml class) as well as a Button Click Method which houses the methods which
    ///     control what happens upon activating the event handler. 
    ///     
    ///     In this case it adds a new User to the database.
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
    public partial class AdminManageUsersAdd : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI,
        ///     Focuses on the first UI input box,
        ///     And instantiates the BusinessLogicLayer class, creating an object of that type.
        /// </summary>
        public AdminManageUsersAdd()
        {
            InitializeComponent();
            firstNameInput.Focus();
            businessLogicLayer = new BusinessLogicLayer();
        }

        /// <summary>
        ///     Adds a new User to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            UserObject user = new UserObject();
            user = businessLogicLayer.CheckUserInsert(emailInput.Text.ToLower());
            try
            {
                if ((firstNameInput.Text.Equals("")) || (lastNameInput.Text.Equals("")) || (telephoneInput.Text.Equals("")) || (emailInput.Text.Equals("")) || (roleInput.Text.Equals("")) ||
                    (passwordInput.Password.Equals("")) || (passwordConfirmInput.Password.Equals("")))
                {
                    MessageBox.Show("Please input all text boxes.");
                    firstNameInput.Focus();
                    return;
                }
                else if ((!Regex.IsMatch(firstNameInput.Text, @"^[a-zA-Z]+$")) || (!Regex.IsMatch(lastNameInput.Text, @"^[a-zA-Z]+$")))
                {
                    MessageBox.Show("Please input only alphabetical characters for the first and last name text boxes");
                    firstNameInput.Focus();
                    return;
                }
                else if (!Regex.IsMatch(telephoneInput.Text, "^[0-9]{11}$"))
                {
                    MessageBox.Show("Please input only 11 numerical characters in the telephone text box.");
                    telephoneInput.Focus();
                    return;
                }
                // Used Official Microsoft email regex:
                // https://github.com/Microsoft/referencesource/blob/master/System.ComponentModel.DataAnnotations/DataAnnotations/EmailAddressAttribute.cs
                else if (!Regex.IsMatch(emailInput.Text, @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$"))
                {
                    MessageBox.Show("Please input a valid email address.");
                    emailInput.Focus();
                    return;
                }
                else if (!(roleInput.Text.ToLower().Equals("worker") || roleInput.Text.ToLower().Equals("manager") || roleInput.Text.ToLower().Equals("admin")))
                {
                    MessageBox.Show("Please input only 'worker', 'manager' or 'admin' in the role text box.");
                    roleInput.Focus();
                    return;
                }
                else if ((!passwordInput.Password.Equals(passwordConfirmInput.Password)))
                {
                    MessageBox.Show("Please make sure the inputted passwords match.");
                    passwordInput.Focus();
                    return;
                }
                else if ((!Regex.IsMatch(passwordInput.Password, @"^[a-zA-Z0-9]{6,14}")) || (!Regex.IsMatch(passwordConfirmInput.Password, @"^[a-zA-Z0-9]{6,14}")))
                {
                    MessageBox.Show("Please make sure the password is only alphanumerical and between 6-14 characters long.");
                    passwordInput.Focus();
                    return;
                }
                else if (emailInput.Text.ToLower().Equals(user.email))
                {
                    MessageBox.Show("A user with that email already exists.");
                    emailInput.Focus();
                    return;
                }
                else
                {
                    user = businessLogicLayer.InsertNewUser(firstNameInput.Text.ToLower(), lastNameInput.Text.ToLower(), long.Parse(telephoneInput.Text),
                           emailInput.Text.ToLower(), roleInput.Text.ToLower(), passwordInput.Password);
                    MessageBox.Show(firstNameInput.Text + " " + lastNameInput.Text + " has been added to the system.");
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
            AdminViewUsersAll adminViewUsers = new AdminViewUsersAll();
            this.NavigationService.Navigate(adminViewUsers);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            AdminViewUsersWorkers adminViewUsersWorkers = new AdminViewUsersWorkers();
            this.NavigationService.Navigate(adminViewUsersWorkers);
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            AdminViewUsersManagers adminViewUsersManagers = new AdminViewUsersManagers();
            this.NavigationService.Navigate(adminViewUsersManagers);
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            AdminViewUsersAdmins adminViewUsersAdmins = new AdminViewUsersAdmins();
            this.NavigationService.Navigate(adminViewUsersAdmins);
        }

        private void Button_Click5(object sender, RoutedEventArgs e)
        {
            AdminManageUsersAdd adminManageUsersAdd = new AdminManageUsersAdd();
            this.NavigationService.Navigate(adminManageUsersAdd);
        }

        private void Button_Click6(object sender, RoutedEventArgs e)
        {
            AdminManageUsersDelete adminManageUsersDelete = new AdminManageUsersDelete();
            this.NavigationService.Navigate(adminManageUsersDelete);
        }

        private void Button_Click7(object sender, RoutedEventArgs e)
        {
            AdminManageUsersEdit adminManageUsersEdit = new AdminManageUsersEdit();
            this.NavigationService.Navigate(adminManageUsersEdit);
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            AdminHomepage adminHomepage = new AdminHomepage();
            this.NavigationService.Navigate(adminHomepage);
        }
    }
}
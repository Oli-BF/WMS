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
    ///     In this case it edits a user in the database.
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
    public partial class AdminManageUsersEdit : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI,
        ///     Focuses on the first UI input box,
        ///     And instantiates the BusinessLogicLayer class, creating an object of that type.
        /// </summary>
        public AdminManageUsersEdit()
        {
            InitializeComponent();
            currentFirstNameInput.Focus();
            businessLogicLayer = new BusinessLogicLayer();
        }

        /// <summary>
        ///     Edits a user in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            UserObject user = new UserObject();
            UserObject userCurrent = new UserObject();
            UserObject userNew = new UserObject();
            userCurrent = businessLogicLayer.CheckUsersAllEdit(currentFirstNameInput.Text.ToLower(), currentLastNameInput.Text.ToLower(), 
                                                               long.Parse(currentTelephoneInput.Text.ToLower()), currentEmailInput.Text.ToLower(),
                                                               currentRoleInput.Text.ToLower(), currentPasswordInput.Password);
            userNew = businessLogicLayer.CheckUsersEmailEdit(newEmailInput.Text.ToLower());
            try
            {
                if (currentFirstNameInput.Text.Equals("") || currentLastNameInput.Text.Equals("") || currentTelephoneInput.Text.Equals("") ||
                    currentEmailInput.Text.Equals("") || currentRoleInput.Text.Equals("") || currentPasswordInput.Password.Equals(""))
                {
                    MessageBox.Show("Please input all 'current' text boxes.");
                    currentFirstNameInput.Focus();
                    return;
                }
                if ((newFirstNameInput.Text.Equals("")) || (newLastNameInput.Text.Equals("")) || (newTelephoneInput.Text.Equals("")) || (newEmailInput.Text.Equals("")) || 
                    (newRoleInput.Text.Equals("")) || (newPasswordInput.Password.Equals("")))
                {
                    MessageBox.Show("Please input all 'new' text boxes. They can be the same as the 'current' text boxes.");
                    newFirstNameInput.Focus();
                    return;
                }
                else if ((!Regex.IsMatch(currentFirstNameInput.Text, @"^[a-zA-Z]+$")) || (!Regex.IsMatch(currentLastNameInput.Text, @"^[a-zA-Z]+$")) ||
                         (!Regex.IsMatch(newFirstNameInput.Text, @"^[a-zA-Z]+$")) || (!Regex.IsMatch(newLastNameInput.Text, @"^[a-zA-Z]+$")))
                {
                    MessageBox.Show("Please input only alphabetical characters for the first and last name text boxes");
                    currentFirstNameInput.Focus();
                    return;
                }
                else if ((!Regex.IsMatch(currentTelephoneInput.Text, "^[0-9]{11}$")) || (!Regex.IsMatch(newTelephoneInput.Text, "^[0-9]{11}$")))
                {
                    MessageBox.Show("Please input only 11 numerical characters in the telephone text boxes.");
                    currentTelephoneInput.Focus();
                    return;
                }
                // Official Microsoft email regex: https://github.com/Microsoft/referencesource/blob/master/System.ComponentModel.DataAnnotations/DataAnnotations/EmailAddressAttribute.cs
                else if (!Regex.IsMatch(currentEmailInput.Text, @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$"))
                {
                    MessageBox.Show("Please input a valid 'current' email address.");
                    currentEmailInput.Focus();
                    return;
                }
                // Official Microsoft email regex: https://github.com/Microsoft/referencesource/blob/master/System.ComponentModel.DataAnnotations/DataAnnotations/EmailAddressAttribute.cs
                else if (!Regex.IsMatch(newEmailInput.Text, @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$"))
                {
                    MessageBox.Show("Please input a valid 'new' email address.");
                    newEmailInput.Focus();
                    return;
                }
                else if (!(currentRoleInput.Text.ToLower().Equals("worker") || currentRoleInput.Text.ToLower().Equals("manager") || 
                           currentRoleInput.Text.ToLower().Equals("admin") || newRoleInput.Text.ToLower().Equals("worker") || 
                           newRoleInput.Text.ToLower().Equals("manager") || newRoleInput.Text.ToLower().Equals("admin")))
                {
                    MessageBox.Show("Please input only 'worker', 'manager' or 'admin' in the role text boxes.");
                    currentRoleInput.Focus();
                    return;
                }
                else if (!(currentFirstNameInput.Text.ToLower().Equals(userCurrent.first_name) || currentLastNameInput.Text.ToLower().Equals(userCurrent.last_name) || 
                           currentTelephoneInput.Text.Equals(userCurrent.telephone) || currentEmailInput.Text.ToLower().Equals(userCurrent.email) || 
                           currentRoleInput.Text.ToLower().Equals(userCurrent.role) || currentPasswordInput.Password.Equals(userCurrent.password)))
                {
                    MessageBox.Show("The 'Current' user details do not exist.");
                    currentEmailInput.Focus();
                    return;
                }
                else if (newEmailInput.Text.ToLower().Equals(userNew.email) && !newEmailInput.Text.ToLower().Equals(userCurrent.email))
                {
                    MessageBox.Show("The 'New' user with that email account already exists.");
                    currentEmailInput.Focus();
                    return;
                }
                else
                {
                    user = businessLogicLayer.EditCurrentUser(newFirstNameInput.Text.ToLower(), newLastNameInput.Text.ToLower(), long.Parse(newTelephoneInput.Text), newEmailInput.Text.ToLower(), 
                                                              newRoleInput.Text.ToLower(), newPasswordInput.Password, currentFirstNameInput.Text.ToLower(), currentLastNameInput.Text.ToLower(),
                                                              long.Parse(currentTelephoneInput.Text), currentEmailInput.Text.ToLower(), currentRoleInput.Text.ToLower(), currentPasswordInput.Password);
                    MessageBox.Show(currentFirstNameInput.Text + " " + currentLastNameInput.Text + " has been updated.");
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
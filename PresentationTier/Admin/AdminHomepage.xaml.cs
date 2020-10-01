using System.Windows;
using System.Windows.Controls;

namespace PresentationTier.Admin
{
    /// <summary>
    ///     This class is the Admin Homepage and contains primarily UI contents (within the .xaml class). It also contains Navigation
    ///     Controls, which control how the user moves around the application.
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
    public partial class AdminHomepage : Page
    {
        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI
        /// </summary>
        public AdminHomepage()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Navigation methods that instantiate the class it wants to move to and then uses the built in WPF 
        ///     'NavigationService' to do move to that object type.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            AdminViewUsersAll adminViewUsersAll = new AdminViewUsersAll();
            this.NavigationService.Navigate(adminViewUsersAll);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            AdminViewAccountsAll adminViewAccountsAll = new AdminViewAccountsAll();
            this.NavigationService.Navigate(adminViewAccountsAll);
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            AdminViewWarehousesAll adminViewWarehousesAll = new AdminViewWarehousesAll();
            this.NavigationService.Navigate(adminViewWarehousesAll);
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            AdminViewLocationsAll adminViewLocationsAll = new AdminViewLocationsAll();
            this.NavigationService.Navigate(adminViewLocationsAll);
        }

        private void Button_Click5(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.NavigationService.Navigate(login);
        }
    }
}
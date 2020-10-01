using System.Windows;
using System.Windows.Controls;
using ApplicationTier;
using System.Data;

namespace PresentationTier.Admin
{
    /// <summary>
    ///     This class is responsible for displaying a DataGrid in the UI containing the data from the database. It is displayed by
    ///     the FillDataGrid() method. This method calls a method from the Business Logic Layer which in turns calls a method in 
    ///     the Data Access Layer (this contains the customized SQL query) which in turn calls a method in the DbConnection class,
    ///     which then returns a DataTable containing all the data requested by the SQL query through the same way it was 
    ///     requested. 
    /// 
    ///     The Data Grid being shown in this case is All User Mangers.
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
    public partial class AdminViewUsersManagers : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI,
        ///     instantiates the BusinessLogicLayer class, creating an object of that type,
        ///     and calls the FillDataGird method.
        /// </summary>
        public AdminViewUsersManagers()
        {
            InitializeComponent();
            businessLogicLayer = new BusinessLogicLayer();
            FillDataGrid();
        }

        /// <summary>
        ///     Fills dataTable with data and then sets dataGrid1 (found in the .xaml) as its source.
        /// </summary>
        private void FillDataGrid()
        {
            DataTable dataTable = businessLogicLayer.SelectAllUsersManagers();
            UsersDataGrid.ItemsSource = dataTable.DefaultView;
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
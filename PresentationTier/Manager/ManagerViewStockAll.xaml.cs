using System.Data;
using System.Windows;
using System.Windows.Controls;
using ApplicationTier;

namespace PresentationTier.Manager
{
    /// <summary>
    ///     This class is responsible for displaying a DataGrid in the UI containing the data from the database. It is displayed by
    ///     the FillDataGrid() method. This method calls a method from the Business Logic Layer which in turns calls a method in 
    ///     the Data Access Layer (this contains the customized SQL query) which in turn calls a method in the DbConnection class,
    ///     which then returns a DataTable containing all the data requested by the SQL query through the same way it was 
    ///     requested. 
    /// 
    ///     The Data Grid being shown in this case is All Stock.
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
    ///     
    ///     For referencing purposes - I posted on Stack Overflow early on when I was attempting to get the data to show up in a
    ///     DataGrid, this is an old way that I am no longer using and little, if any, code that was posted is still in use. 
    ///     Find link below:
    ///     https://stackoverflow.com/questions/62885694/displaying-database-data-in-a-datagrid-using-wpf-and-wcf-in-an-n-tier-applicatio/62892729#62892729
    /// </summary>
    public partial class ManagerViewStockAll : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI,
        ///     instantiates the BusinessLogicLayer class, creating an object of that type,
        ///     and calls the FillDataGird method.
        /// </summary>
        public ManagerViewStockAll()
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
            DataTable dataTable = businessLogicLayer.SelectAllStock();
            dataGrid1.ItemsSource = dataTable.DefaultView;
        }

        /// <summary>
        ///     The following method is used to print out the data of the DataGrid that is shown on screen, I used the
        ///     following tutorial to learn how to do it and the code is taken from there. Link Below:
        ///     http://www.nullskull.com/a/1378/wpf-printing-and-print-preview.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(dataGrid1, "Print");
                MessageBox.Show("The on screen Stocks have been sent to the printer.");
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
            ManagerViewStockAll managerViewStockAll = new ManagerViewStockAll();
            this.NavigationService.Navigate(managerViewStockAll);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            ManagerManageStockAdd managerManageStockAdd = new ManagerManageStockAdd();
            this.NavigationService.Navigate(managerManageStockAdd);
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            ManagerManageStockDelete managerManageStockDelete = new ManagerManageStockDelete();
            this.NavigationService.Navigate(managerManageStockDelete);
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            ManagerManageStockEdit managerManageStockEdit = new ManagerManageStockEdit();
            this.NavigationService.Navigate(managerManageStockEdit);
        }

        private void Button_Click5(object sender, RoutedEventArgs e)
        {
            ManagerViewLocationsAll ManagerViewLocationsAll = new ManagerViewLocationsAll();
            this.NavigationService.Navigate(ManagerViewLocationsAll);
        }

        private void Button_Click6(object sender, RoutedEventArgs e)
        {
            ManagerViewWarehousesAll managerViewWarehousesAll = new ManagerViewWarehousesAll();
            this.NavigationService.Navigate(managerViewWarehousesAll);
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            ManagerHomepage managerHomepage = new ManagerHomepage();
            this.NavigationService.Navigate(managerHomepage);
        }
    }
}
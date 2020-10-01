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
    ///     The Data Grid being shown in this case is All Receipt Lines.
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
    public partial class ManagerViewReceiptLinesAll : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI,
        ///     instantiates the BusinessLogicLayer class, creating an object of that type,
        ///     and calls the FillDataGird method.
        /// </summary>
        public ManagerViewReceiptLinesAll()
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
            DataTable dataTable = businessLogicLayer.SelectAllReceiptLines();
            dataGrid1.ItemsSource = dataTable.DefaultView;
        }

        /// <summary>
        ///     Navigation methods that instantiate the class it wants to move to and then uses the built in WPF 
        ///     'NavigationService' to do move to that object type.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            ManagerViewReceiptsAll managerViewReceiptsAll = new ManagerViewReceiptsAll();
            this.NavigationService.Navigate(managerViewReceiptsAll);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            ManagerManageReceiptsAdd managerManageReceiptsAdd = new ManagerManageReceiptsAdd();
            this.NavigationService.Navigate(managerManageReceiptsAdd);
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            ManagerManageReceiptsDelete managerManageReceiptsDelete = new ManagerManageReceiptsDelete();
            this.NavigationService.Navigate(managerManageReceiptsDelete);
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            ManagerManageReceiptsEdit managerManageReceiptsEdit = new ManagerManageReceiptsEdit();
            this.NavigationService.Navigate(managerManageReceiptsEdit);
        }

        private void Button_Click5(object sender, RoutedEventArgs e)
        {
            ManagerViewReceiptLinesAll managerViewReceiptLinesAll = new ManagerViewReceiptLinesAll();
            this.NavigationService.Navigate(managerViewReceiptLinesAll);
        }

        private void Button_Click6(object sender, RoutedEventArgs e)
        {
            ManagerManageReceiptLinesAdd managerManageReceiptLinesAdd = new ManagerManageReceiptLinesAdd();
            this.NavigationService.Navigate(managerManageReceiptLinesAdd);
        }

        private void Button_Click7(object sender, RoutedEventArgs e)
        {
            ManagerManageReceiptLinesDelete managerManageReceiptLinesDelete = new ManagerManageReceiptLinesDelete();
            this.NavigationService.Navigate(managerManageReceiptLinesDelete);
        }

        private void Button_Click8(object sender, RoutedEventArgs e)
        {
            ManagerManageReceiptLinesEdit managerManageReceiptLinesEdit = new ManagerManageReceiptLinesEdit();
            this.NavigationService.Navigate(managerManageReceiptLinesEdit);
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            ManagerHomepage managerHomepage = new ManagerHomepage();
            this.NavigationService.Navigate(managerHomepage);
        }
    }
}
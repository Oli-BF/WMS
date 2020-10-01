using System.Windows;
using System.Windows.Controls;

namespace PresentationTier.Worker
{
    /// <summary>
    ///     This class is the Worker Homepage and contains primarily UI contents (within the .xaml class). It also contains Navigation
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
    public partial class WorkerHomepage : Page
    {
        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI
        /// </summary>
        public WorkerHomepage()
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
            WorkerViewProducts workerViewProducts = new WorkerViewProducts();
            this.NavigationService.Navigate(workerViewProducts);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            WorkerViewReceipts workerViewReceipts = new WorkerViewReceipts();
            this.NavigationService.Navigate(workerViewReceipts);
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            WorkerViewStock workerViewStock = new WorkerViewStock();
            this.NavigationService.Navigate(workerViewStock);
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            WorkerViewOrders workerViewOrders = new WorkerViewOrders();
            this.NavigationService.Navigate(workerViewOrders);
        }

        private void Button_Click5(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.NavigationService.Navigate(login);
        }
    }
}
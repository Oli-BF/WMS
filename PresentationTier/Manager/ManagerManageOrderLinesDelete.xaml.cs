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
    ///     In this case it deletes an Order Line from the database.
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
    public partial class ManagerManageOrderLinesDelete : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI,
        ///     Focuses on the first UI input box,
        ///     And instantiates the BusinessLogicLayer class, creating an object of that type.
        /// </summary>
        public ManagerManageOrderLinesDelete()
        {
            InitializeComponent();
            orderLineIdInput.Focus();
            businessLogicLayer = new BusinessLogicLayer();
        }

        /// <summary>
        ///     Deletes an order line from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (orderLineIdInput.Text.Equals(""))
                {
                    MessageBox.Show("Please input the text box.");
                    orderLineIdInput.Focus();
                    return;
                }
                else if (!Regex.IsMatch(orderLineIdInput.Text, "^[0-9]*$"))
                {
                    MessageBox.Show("Please input only numerical characters into the text box.");
                    orderLineIdInput.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred, please contact your administrator." + "\n\n" + "The error message is: " + "\n\n" + ex.ToString());
            }

            OrderLineObject orderLine = businessLogicLayer.CheckOrderLinesByID(int.Parse(orderLineIdInput.Text));

            try
            {
                // Order Line Does not Exist
                if (!int.Parse(orderLineIdInput.Text).Equals(orderLine.order_line_id))
                {
                    MessageBox.Show("The Order Line ID provided does not exist.");
                    orderLineIdInput.Focus();
                    return;
                }
                // Delete Order Line
                else
                {
                    orderLine = businessLogicLayer.DeleteCurrentOrderLine(int.Parse(orderLineIdInput.Text));
                    MessageBox.Show("The provided Order Line has been deleted from the system.");
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
            ManagerViewOrdersAll managerViewOrdersAll = new ManagerViewOrdersAll();
            this.NavigationService.Navigate(managerViewOrdersAll);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            ManagerManageOrdersAdd managerManageOrdersAdd = new ManagerManageOrdersAdd();
            this.NavigationService.Navigate(managerManageOrdersAdd);
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            ManagerManageOrdersDelete managerManageOrdersDelete = new ManagerManageOrdersDelete();
            this.NavigationService.Navigate(managerManageOrdersDelete);
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            ManagerManageOrdersEdit managerManageOrdersEdit = new ManagerManageOrdersEdit();
            this.NavigationService.Navigate(managerManageOrdersEdit);
        }

        private void Button_Click5(object sender, RoutedEventArgs e)
        {
            ManagerViewOrderLinesAll managerViewOrderLinesAll = new ManagerViewOrderLinesAll();
            this.NavigationService.Navigate(managerViewOrderLinesAll);
        }

        private void Button_Click6(object sender, RoutedEventArgs e)
        {
            ManagerManageOrderLinesAdd managerManageOrderLinesAdd = new ManagerManageOrderLinesAdd();
            this.NavigationService.Navigate(managerManageOrderLinesAdd);
        }

        private void Button_Click7(object sender, RoutedEventArgs e)
        {
            ManagerManageOrderLinesAllocate managerManageOrderLinesAllocate = new ManagerManageOrderLinesAllocate();
            this.NavigationService.Navigate(managerManageOrderLinesAllocate);
        }

        private void Button_Click8(object sender, RoutedEventArgs e)
        {
            ManagerManageOrderLinesDelete managerManageOrderLinesDelete = new ManagerManageOrderLinesDelete();
            this.NavigationService.Navigate(managerManageOrderLinesDelete);
        }

        private void Button_Click9(object sender, RoutedEventArgs e)
        {
            ManagerManageOrderLinesEdit managerManageOrderLinesEdit = new ManagerManageOrderLinesEdit();
            this.NavigationService.Navigate(managerManageOrderLinesEdit);
        }

        private void Button_Click10(object sender, RoutedEventArgs e)
        {
            ManagerViewPicksAll managerViewPicksAll = new ManagerViewPicksAll();
            this.NavigationService.Navigate(managerViewPicksAll);
        }

        private void Button_Click11(object sender, RoutedEventArgs e)
        {
            ManagerManagePicksDelete managerManagePicksDelete = new ManagerManagePicksDelete();
            this.NavigationService.Navigate(managerManagePicksDelete);
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            ManagerHomepage managerHomepage = new ManagerHomepage();
            this.NavigationService.Navigate(managerHomepage);
        }
    }
}
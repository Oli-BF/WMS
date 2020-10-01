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
    ///     In this case it deletes a pick from the database.
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
    public partial class ManagerManagePicksDelete : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI,
        ///     Focuses on the first UI input box,
        ///     And instantiates the BusinessLogicLayer class, creating an object of that type.
        /// </summary>
        public ManagerManagePicksDelete()
        {
            InitializeComponent();
            pickIdInput.Focus();
            businessLogicLayer = new BusinessLogicLayer();
        }

        /// <summary>
        ///     Deletes a pick from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pickIdInput.Text.Equals(""))
                {
                    MessageBox.Show("Please input the text box.");
                    pickIdInput.Focus();
                    return;
                }
                else if (!Regex.IsMatch(pickIdInput.Text, "^[0-9]*$"))
                {
                    MessageBox.Show("Please input only numerical characters into the text box.");
                    pickIdInput.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred, please contact your administrator." + "\n\n" + "The error message is: " + "\n\n" + ex.ToString());
            }

            // Check Picks by Pick ID
            PickObject pick1 = businessLogicLayer.CheckPicksByPickID(int.Parse(pickIdInput.Text));

            // Select Picks Order Line ID and Location ID And Quantity by Pick ID
            PickObject pick2 = businessLogicLayer.SelectPicksOrderLineLocationIDAndQuantityByPickID(int.Parse(pickIdInput.Text));
            int pickOrderLineID = pick2.order_line_id;
            int pickLocationID = pick2.location_id;
            int pickQuantity = pick2.quantity;

            // Get Order Line Order ID
            OrderLineObject orderLine1 = businessLogicLayer.GetOrderLineOrderID(pickOrderLineID);
            int orderLineOrderID = orderLine1.order_id;

            // Select Stock ID by Picks Location ID
            StockObject stock1 = businessLogicLayer.SelectStockIDByPicksLocationID(pickLocationID);
            int stockID = stock1.stock_id;

            // Select Stock Location by Stock ID
            StockObject stock6 = businessLogicLayer.SelectStockLocationIDByID(stockID);
            int stockLocation = stock6.location_id;

            try
            {
                if (!int.Parse(pickIdInput.Text).Equals(pick1.pick_id))
                {
                    MessageBox.Show("The Pick ID provided does not exist.");
                    pickIdInput.Focus();
                    return;
                }
                else
                {
                    // Undo Stock Quantity
                    StockObject stock2 = businessLogicLayer.UndoStockQuantity(pickQuantity, stockLocation);


                    // Undo Stock Availability Status
                    StockObject stock4 = businessLogicLayer.SelectStockQuantityByLocationID(stockLocation);
                    int stockQuantity = stock4.quantity;

                    if (stockQuantity > 0)
                    {
                        StockObject stockObject3 = businessLogicLayer.UndoStockAvailabilityStatus(stockLocation);
                    }


                    // Undo Location Allocation
                    StockObject stock5 = businessLogicLayer.SelectStockAvailabilityStatusByStockLocation(stockLocation);
                    bool stockAvailabilityStatus = stock5.availability_status;
                    if (stockAvailabilityStatus.Equals(true))
                    {
                        LocationObject location1 = businessLogicLayer.SetLocationAllocatedTrue(stockLocation);
                    }


                    // Undo Stock Allocated Quantity
                    StockObject stock3 = businessLogicLayer.UndoStockAllocatedQuantity(pickQuantity, stockLocation);


                    // Delete Current Pick
                    PickObject pick = businessLogicLayer.DeleteCurrentPick(int.Parse(pickIdInput.Text));


                    // Undo Order Status
                    OrderLineObject orderLine2 = businessLogicLayer.GetOrderLineQuantityByOLIDAndOID(orderLineOrderID, orderLineOrderID);
                    int orderLineQuantity = orderLine2.quantity;

                    PickObject pick3 = businessLogicLayer.GetPickQuantityByOrderID(orderLineOrderID);
                    int pickQuantity2 = pick3.quantity;

                    if (!pickQuantity2.Equals(orderLineQuantity))
                    {
                        OrderObject order = businessLogicLayer.SetOrderStatusCreated(orderLineOrderID);
                    }
                        

                    // Message Box
                    MessageBox.Show("Pick: " + pickIdInput.Text + " has been deleted from the system. \n\nThe Pick Quantity has been unallocated in Stock. \n\nThe Stock Availability Status has been set back to True where relevant. \n\nThe Order Status has been set back to Created where relevant. \n\nThe Location Allocation has been set back to Allocated where relevant.");
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
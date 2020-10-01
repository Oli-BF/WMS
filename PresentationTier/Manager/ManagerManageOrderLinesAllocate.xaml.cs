using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using ApplicationTier;
using MessageBox = System.Windows.MessageBox;

namespace PresentationTier.Manager
{
    /// <summary>
    ///     This class contains UI contents (within the .xaml class) as well as a Button Click Method which houses the methods which
    ///     control what happens upon activating the event handler. 
    ///     
    ///     In this case it allocates an new order line.
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
    public partial class ManagerManageOrderLinesAllocate : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI,
        ///     Focuses on the first UI input box,
        ///     And instantiates the BusinessLogicLayer class, creating an object of that type.
        /// </summary>
        public ManagerManageOrderLinesAllocate()
        {
            InitializeComponent();
            orderLineIdInput.Focus();
            businessLogicLayer = new BusinessLogicLayer();
        }

        /// <summary>
        ///     Allocates an order line.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllocateButton_Click(object sender, RoutedEventArgs e)
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


            // Get Order Line Order ID
            OrderLineObject orderLine1 = businessLogicLayer.GetOrderLineOrderID(int.Parse(orderLineIdInput.Text));
            int orderLineOrderID = orderLine1.order_id;


            // Get Order Line Product ID - Order Line Object
            OrderLineObject orderline2 = businessLogicLayer.GetOrderLineProductIDAndTitleOLObj(int.Parse(orderLineIdInput.Text));
            int orderLineProductID = orderline2.product_id;

            // Get Order Line Product ID - Product Object
            ProductObject product1 = businessLogicLayer.GetOrderLineProductIDAndTitlePrObj(int.Parse(orderLineIdInput.Text));
            string orderLineProductTitle = product1.title;


            // Get Product ID from Stock where Product ID = Order Line Product ID
            StockObject stockObject = businessLogicLayer.GetStockProductID(orderLineProductID);
            int stockProductID = stockObject.product_id;


            // Get Order Account ID
            OrderObject order1 = businessLogicLayer.GetOrderAccountID(orderLineOrderID);
            int orderAccountID = order1.account_id;


            // Get Order Warehouse ID
            OrderObject order2 = businessLogicLayer.GetOrderWarehouseID(orderLineOrderID);
            int orderWarehouseID = order2.warehouse_id;


            // Get Account, Product And Warehouse IDs from Stock By Order AID Order Line PID And Order WID
            StockObject stockObject987 = businessLogicLayer.GetStockAccountProductAndWarehouseID(orderAccountID, orderLineProductID,
                                                                                                 orderWarehouseID);
            int stockAccountID123 = stockObject987.account_id;
            int stockProductID123 = stockObject987.product_id;
            int stockWarehouseID123 = stockObject987.warehouse_id;


            // Get Nearest Available Stock - Stock Object
            StockObject stock1 = businessLogicLayer.GetAvailableStockStObj(orderLineProductID, orderAccountID, orderWarehouseID);
            int stockID = stock1.stock_id;
            int stockLocationID = stock1.location_id;

            // Get Nearest Available Stock - Location Object
            LocationObject location1 = businessLogicLayer.GetAvailableStockLoObj(orderLineProductID, orderAccountID, orderWarehouseID);
            string stockLocationCode = location1.location_code;


            // Get Order Line Quantity
            OrderLineObject orderLine3 = businessLogicLayer.GetOrderLineQuantity(int.Parse(orderLineIdInput.Text));
            int orderLineQuantity = orderLine3.quantity;


            // Get Stock Quantity by Stock ID
            StockObject stock4 = businessLogicLayer.GetStockQuantityByID(stockID);
            int stockQuantity = stock4.quantity;


            // Get Stock Account ID, Product ID And Warehouse ID By Stock ID
            StockObject stockWhatever = businessLogicLayer.GetStockAccountProductAndWarehouseIDByStockID(stockID);
            int stockAccountID = stockWhatever.account_id;
            int stockProductID2 = stockWhatever.product_id;
            int stockWarehouseID = stockWhatever.warehouse_id;


            // Get Stock Sum By Product ID
            int stockSum = 0;
            if (!(stockAccountID.Equals(0) && stockProductID2.Equals(0) && stockWarehouseID.Equals(0)))
            {
                StockObject stock5 = businessLogicLayer.GetStockSumByProductID(stockAccountID, stockProductID2, stockWarehouseID);
                stockSum = stock5.quantity;
            }

            try
            {
                // Order Line Does not Exist
                if (!int.Parse(orderLineIdInput.Text).Equals(orderLine.order_line_id))
                {
                    MessageBox.Show("The Order Line ID provided does not exist.");
                    orderLineIdInput.Focus();
                    return;
                }
                // Product Not in Stock
                else if (!orderLineProductID.Equals(stockProductID))
                {
                    MessageBox.Show("The Product pertaining to that Order Line ID is not in Stock.");
                    orderLineIdInput.Focus();
                    return;
                }
                // Account, Product and Warehouse do not match in Stock
                else if (!(orderAccountID.Equals(stockAccountID123) && orderLineProductID.Equals(stockProductID123) && orderWarehouseID.Equals(stockWarehouseID123)))
                {
                    MessageBox.Show("The Order Line Provided does not have a matching Account, Product and Warehouse in Stock.");
                    orderLineIdInput.Focus();
                    return;
                }
                // Not Enough Stock
                else if (stockSum < orderLineQuantity)
                {
                    MessageBox.Show("There is not enough Stock of the Product pertaining to that Order Line ID to fully allocate the Order Line.");
                    orderLineIdInput.Focus();
                    return;
                }
                else
                {
                    // Implement if time permits 
                        // If stock matches exact then go to that one (i.e. 50 to 50 or 25 to 25) instead of just nearest - Change query used and more if statements
                    if (orderLineQuantity > stockQuantity)
                    {
                        int orderLineQuantityRemaining = 0;
                        while (orderLineQuantityRemaining < orderLineQuantity)
                        {
                            // Get Nearest Available Stock - Stock ID & Location ID
                            StockObject stock123 = businessLogicLayer.GetAvailableStockStObj(orderLineProductID, orderAccountID, orderWarehouseID);
                            int stockID2 = stock123.stock_id;
                            int stockLocationID123 = stock123.location_id;


                            // Get Nearest Available Stock - Location Code
                            LocationObject location123 = businessLogicLayer.GetAvailableStockLoObj(orderLineProductID, orderAccountID, orderWarehouseID);
                            string stockLocationCode2 = location123.location_code;


                            // Get Stock Quantity by Stock ID
                            StockObject stock12345 = businessLogicLayer.GetStockQuantityByID(stockID2);
                            int stockQuantityTest = stock12345.quantity;


                            // Nearest Available Stock - Set Availability Status to False - Locking it in for this order
                            StockObject stock1234 = businessLogicLayer.SetAvailabilityFalse(stockID2);


                            if (stockQuantityTest < orderLineQuantity - orderLineQuantityRemaining)
                            {
                                // Generate Pick
                                PickObject pickTest = businessLogicLayer.GeneratePick(orderLineOrderID, int.Parse(orderLineIdInput.Text), stockLocationID123, stockLocationCode2,
                                                                                      orderLineProductID, orderLineProductTitle, stockQuantityTest);


                                // Update Stock Quantity - stockQuantity
                                StockObject stockTest = businessLogicLayer.UpdateStockQuantity(stockQuantityTest, stockID2);
                            }
                            else
                            {
                                // Generate Pick
                                PickObject pickTest123 = businessLogicLayer.GeneratePick(orderLineOrderID, int.Parse(orderLineIdInput.Text), stockLocationID123, stockLocationCode2,
                                                                                         orderLineProductID, orderLineProductTitle, orderLineQuantity - orderLineQuantityRemaining);


                                // Update Stock Quantity - orderLineQuantity - orderLineQuantityRemaining
                                StockObject stockTest2 = businessLogicLayer.UpdateStockQuantity(orderLineQuantity - orderLineQuantityRemaining, stockID2);
                            }


                            // Nearest Available Stock - Set Availability Status to True - Opening it up for future orders
                            StockObject stock12345678 = businessLogicLayer.GetStockQuantityByID(stockID2);
                            int stockQuantityTest2 = stock12345678.quantity;
                            if (stockQuantityTest2 > 0)
                            {
                                StockObject stock6 = businessLogicLayer.SetAvailabilityTrue(stockID2);
                            }


                            // Update Location 'allocated' if Stock Availability Status equals 0 (By Stock ID) 
                            StockObject stockWhat = businessLogicLayer.GetStockAvailabilityStatus(stockID2);
                            bool stockAvailabilityStatus = stockWhat.availability_status;
                            if (stockAvailabilityStatus.Equals(false))
                            {
                                LocationObject location2 = businessLogicLayer.SetLocationAllocatedFalse(stockLocationID123); 
                                MessageBox.Show("All Stock from Location ID: " + stockLocationID123 + " has been taken and as such, it has been set to Available again.");
                            }


                            // Get Pick Quantity by Order Line ID + Set orderLineQuantityRemaining = pickQuantity
                            PickObject pickTest2 = businessLogicLayer.GetPickQuantityByOrderLineID(int.Parse(orderLineIdInput.Text));
                            int pickQuantity = pickTest2.quantity;
                            orderLineQuantityRemaining = pickQuantity;
                        }

                        // Update Orders Status to Allocated (if Order Line Quantity matches that of Pick Quantity where Order Line Matches)
                        OrderLineObject orderLine123 = businessLogicLayer.GetOrderLineQuantityByOLIDAndOID(orderLineOrderID, orderLineOrderID);
                        int orderLineQuantity2 = orderLine123.quantity;

                        PickObject pickTest3 = businessLogicLayer.GetPickQuantityByOrderID(orderLineOrderID);
                        int pickQuantity2 = pickTest3.quantity;

                        if (pickQuantity2.Equals(orderLineQuantity2))
                        {
                            OrderObject order3 = businessLogicLayer.SetOrderStatusAllocated(orderLineOrderID);
                            MessageBox.Show("All Order Lines belonging to Order: " + orderLineOrderID + " have now been Allocated.");
                        }


                        // Message Box
                        MessageBox.Show("Order Line: " + orderLineIdInput.Text + " has been allocated. \n\nA pick has been created, please see the Picks table for more information.");
                        return;
                    }
                    else
                    {
                        // Set Nearest Available Stock - Availability Status to false - Locking it in for this order
                        StockObject stock2 = businessLogicLayer.SetAvailabilityFalse(stockID);


                        // Generate Pick
                        PickObject pick1 = businessLogicLayer.GeneratePick(orderLineOrderID, int.Parse(orderLineIdInput.Text), stockLocationID, stockLocationCode,
                                                                           orderLineProductID, orderLineProductTitle, orderLineQuantity);


                        // Update Stock Quantity - orderLineQuantity
                        StockObject stock3 = businessLogicLayer.UpdateStockQuantity(orderLineQuantity, stockID);


                        // Nearest Available Stock - Set Availability Status to True - Opening it up for future orders
                        StockObject stock12345 = businessLogicLayer.GetStockQuantityByID(stockID);
                        int stockQuantity2 = stock12345.quantity;
                        if (stockQuantity2 != 0)
                        {
                            StockObject stock6 = businessLogicLayer.SetAvailabilityTrue(stockID);
                        }


                        // Update Location 'allocated' to false if Stock Availability Status equals 0 (empty) 
                        StockObject stockWhat = businessLogicLayer.GetStockAvailabilityStatus(stockID);
                        bool stockAvailabilityStatus = stockWhat.availability_status;
                        if (stockAvailabilityStatus.Equals(false))
                        {
                            LocationObject location2 = businessLogicLayer.SetLocationAllocatedFalse(stockLocationID);
                            MessageBox.Show("All Stock from Location ID: " + stockLocationID + " has been taken and as such, it has been set to Available again.");
                        }


                        // Update Orders Status to Allocated (if Order Line Quantity matches that of Pick Quantity where Order Line Matches)
                        OrderLineObject orderLine123 = businessLogicLayer.GetOrderLineQuantityByOLIDAndOID(orderLineOrderID, orderLineOrderID);
                        int orderLineQuantity2 = orderLine123.quantity;

                        PickObject pickTest3 = businessLogicLayer.GetPickQuantityByOrderID(orderLineOrderID); 
                        int pickQuantity2 = pickTest3.quantity;

                        if (pickQuantity2.Equals(orderLineQuantity2))
                        {
                            OrderObject order3 = businessLogicLayer.SetOrderStatusAllocated(orderLineOrderID);
                            MessageBox.Show("All Order Lines belonging to Order: " + orderLineOrderID + " have now been Allocated.");
                        }


                        // Message Box
                        MessageBox.Show("Order Line: " + orderLineIdInput.Text + " has been allocated. \n\nA pick has been created, please see the Picks table for more information.");
                        return;
                    }  
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
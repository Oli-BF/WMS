using ApplicationTier;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Globalization;

namespace PresentationTier.Manager
{
    /// <summary>
    ///     This class contains UI contents (within the .xaml class) as well as a Button Click Method which houses the methods which
    ///     control what happens upon activating the event handler. 
    ///     
    ///     In this case it adds a new order to the database.
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
    public partial class ManagerManageOrdersAdd : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI,
        ///     Focuses on the first UI input box,
        ///     And instantiates the BusinessLogicLayer class, creating an object of that type.
        /// </summary>
        public ManagerManageOrdersAdd()
        {
            InitializeComponent();
            orderReferenceInput.Focus();
            businessLogicLayer = new BusinessLogicLayer();
        }

        /// <summary>
        ///     Adds a new Order to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string dateTimeFormat = "dd-MM-yyyy HH:mm";
            DateTime CurrentTime = DateTime.Now;
            try
            {
                if (orderReferenceInput.Text.Equals("") || accountIdInput.Text.Equals("") || warehouseIdInput.Text.Equals("") || firstNameInput.Text.Equals("") ||
                    lastNameInput.Text.Equals("") || addressLine1Input.Text.Equals("") || addressLine2Input.Text.Equals("") || cityInput.Equals("") || 
                    postcodeInput.Text.Equals(""))
                {
                    MessageBox.Show("Please input all text boxes.");
                    orderReferenceInput.Focus();
                    return;
                }
                else if ((!Regex.IsMatch(accountIdInput.Text, "^[0-9]*$")) || (!Regex.IsMatch(warehouseIdInput.Text, "^[0-9]*$")))
                {
                    MessageBox.Show("Please input only numerical characters into the Account ID and Warehouse ID text boxes.");
                    accountIdInput.Focus();
                    return;
                }
                // https://stackoverflow.com/questions/31817105/regex-for-date-pattern/31817136
                else if (!DateTime.TryParseExact(dispatchDateInput.Text, dateTimeFormat, null, DateTimeStyles.None, out CurrentTime))
                {
                    MessageBox.Show("Please input a valid date and time using the datetime picker.");
                    dispatchDateInput.Focus();
                    return;
                }
                // https://en.wikipedia.org/wiki/Postcodes_in_the_United_Kingdom#Validation
                else if (!Regex.IsMatch(postcodeInput.Text, "^(([A-Z]{1,2}[0-9][A-Z0-9]?|ASCN|STHL|TDCU|BBND|[BFS]IQQ|PCRN|TKCA) ?[0-9][A-Z]{2}|BFPO ?[0-9]{1,4}|(KY[0-9]|MSR|VG|AI)[ -]?[0-9]{4}|[A-Z]{2} ?[0-9]{2}|GE ?CX|GIR ?0A{2}|SAN ?TA1)$"))
                {
                    MessageBox.Show("Please input a valid UK Postcode (In Capitals).");
                    postcodeInput.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred, please contact your administrator." + "\n\n" + "The error message is: " + "\n\n" + ex.ToString());
            }

            OrderObject order = businessLogicLayer.CheckOrdersByRefAndAccountID(orderReferenceInput.Text.ToLower(), int.Parse(accountIdInput.Text));
            AccountObject account = businessLogicLayer.CheckAccountsByID(int.Parse(accountIdInput.Text));
            WarehouseObject warehouse = businessLogicLayer.CheckWarehousesByID(int.Parse(warehouseIdInput.Text));

            try
            {
                if (!int.Parse(accountIdInput.Text).Equals(account.account_id))
                {
                    MessageBox.Show("The Account ID provided does not exist.");
                    accountIdInput.Focus();
                    return;
                }
                else if (!int.Parse(warehouseIdInput.Text).Equals(warehouse.warehouse_id))
                {
                    MessageBox.Show("The Warehouse ID provided does not exist.");
                    warehouseIdInput.Focus();
                    return;
                }
                else if (orderReferenceInput.Text.ToLower().Equals(order.order_reference) && int.Parse(accountIdInput.Text).Equals(order.account_id))
                {
                    MessageBox.Show("The Order Reference: " + orderReferenceInput.Text + " already exists for Account: " + accountIdInput.Text);
                    orderReferenceInput.Focus();
                    return;
                }
                else
                {
                    order = businessLogicLayer.InsertNewOrder(orderReferenceInput.Text.ToLower(), int.Parse(accountIdInput.Text), int.Parse(warehouseIdInput.Text),
                                                              DateTime.Parse(dispatchDateInput.Text), firstNameInput.Text.ToLower(), lastNameInput.Text.ToLower(),
                                                              addressLine1Input.Text.ToLower(), addressLine2Input.Text.ToLower(), cityInput.Text.ToLower(),
                                                              postcodeInput.Text.ToLower());
                    MessageBox.Show(orderReferenceInput.Text + " has been added to the system.");
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
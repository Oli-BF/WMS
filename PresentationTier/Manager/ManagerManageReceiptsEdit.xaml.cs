using ApplicationTier;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Globalization;
using MessageBox = System.Windows.MessageBox;

namespace PresentationTier.Manager
{
    /// <summary>
    ///     This class contains UI contents (within the .xaml class) as well as a Button Click Method which houses the methods which
    ///     control what happens upon activating the event handler. 
    ///     
    ///     In this case it edits a receipt in the database.
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
    public partial class ManagerManageReceiptsEdit : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI,
        ///     Focuses on the first UI input box,
        ///     And instantiates the BusinessLogicLayer class, creating an object of that type.
        /// </summary>
        public ManagerManageReceiptsEdit()
        {
            InitializeComponent();
            currentReceiptReferenceInput.Focus();
            businessLogicLayer = new BusinessLogicLayer();
        }

        /// <summary>
        ///     Edits a receipt in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string dateTimeFormat = "dd-MM-yyyy HH:mm";
            DateTime CurrentTime = DateTime.Now;
            try
            {
                if (currentReceiptReferenceInput.Text.Equals("") || currentAccountIdInput.Text.Equals("") || currentWarehouseIdInput.Text.Equals("") ||
                    newReceiptReferenceInput.Text.Equals("") || newAccountIdInput.Text.Equals("") || newWarehouseIdInput.Text.Equals(""))

                {
                    MessageBox.Show("Please input all text boxes.");
                    currentReceiptReferenceInput.Focus();
                    return;
                }
                else if ((!Regex.IsMatch(currentAccountIdInput.Text, "^[0-9]*$")) || (!Regex.IsMatch(currentWarehouseIdInput.Text, "^[0-9]*$")) ||
                         (!Regex.IsMatch(newAccountIdInput.Text, "^[0-9]*$")) || (!Regex.IsMatch(newWarehouseIdInput.Text, "^[0-9]*$")))
                {
                    MessageBox.Show("Please input only numerical characters into the Account ID and Warehouse ID text boxes.");
                    currentAccountIdInput.Focus();
                    return;
                }
                // https://stackoverflow.com/questions/31817105/regex-for-date-pattern/31817136
                else if ((!DateTime.TryParseExact(currentExpectedDateInput.Text, dateTimeFormat, null, DateTimeStyles.None, out CurrentTime)) ||
                         (!DateTime.TryParseExact(newExpectedDateInput.Text, dateTimeFormat, null, DateTimeStyles.None, out CurrentTime)))
                {
                    MessageBox.Show("Please input valid dates and times using the datetime pickers.");
                    currentExpectedDateInput.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred, please contact your administrator." + "\n\n" + "The error message is: " + "\n\n" + ex.ToString());
            }

            ReceiptObject receipt;
            ReceiptObject receiptAllCurrent = businessLogicLayer.CheckReceiptsAll(currentReceiptReferenceInput.Text.ToLower(), int.Parse(currentAccountIdInput.Text), 
                                                                                  int.Parse(currentWarehouseIdInput.Text), DateTime.Parse(currentExpectedDateInput.Text));
            ReceiptObject receiptCurrent = businessLogicLayer.CheckReceiptsByRefAndAccountID(currentReceiptReferenceInput.Text.ToLower(), int.Parse(currentAccountIdInput.Text));
            ReceiptObject receiptNew = businessLogicLayer.CheckReceiptsByRefAndAccountID(newReceiptReferenceInput.Text.ToLower(), int.Parse(newAccountIdInput.Text));
            AccountObject account = businessLogicLayer.CheckAccountsByID(int.Parse(newAccountIdInput.Text));
            WarehouseObject warehouse = businessLogicLayer.CheckWarehousesByID(int.Parse(newWarehouseIdInput.Text));

            try
            {
                if (!(currentReceiptReferenceInput.Text.ToLower().Equals(receiptAllCurrent.receipt_reference) &&
                      int.Parse(currentAccountIdInput.Text).Equals(receiptAllCurrent.account_id) &&
                      int.Parse(currentWarehouseIdInput.Text).Equals(receiptAllCurrent.warehouse_id) &&
                      DateTime.Parse(currentExpectedDateInput.Text).Equals(receiptAllCurrent.expected_date)))
                {
                    MessageBox.Show("The Current Receipt provided does not exist.");
                    currentReceiptReferenceInput.Focus();
                    return;
                }
                else if (!int.Parse(newAccountIdInput.Text).Equals(account.account_id))
                {
                    MessageBox.Show("The New Account ID provided does not exist.");
                    newAccountIdInput.Focus();
                    return;
                }
                else if (!int.Parse(newWarehouseIdInput.Text).Equals(warehouse.warehouse_id))
                {
                    MessageBox.Show("The New Warehouse ID provided does not exist.");
                    newWarehouseIdInput.Focus();
                    return;
                }
                else if (newReceiptReferenceInput.Text.ToLower().Equals(receiptNew.receipt_reference) &&
                         !newReceiptReferenceInput.Text.ToLower().Equals(receiptCurrent.receipt_reference) || 
                         int.Parse(newAccountIdInput.Text).Equals(receiptNew.account_id) &&
                         !int.Parse(newAccountIdInput.Text).Equals(receiptCurrent.account_id))
                {
                    MessageBox.Show("The Receipt Reference: " + newReceiptReferenceInput.Text + " already exists for Account: " + newAccountIdInput.Text);
                    newReceiptReferenceInput.Focus();
                    return;
                }
                else if (receiptedDateInput.Value.HasValue.Equals(false))
                {
                    receipt = businessLogicLayer.EditCurrentReceiptNoRecDate(newReceiptReferenceInput.Text.ToLower(), int.Parse(newAccountIdInput.Text),
                                                                             int.Parse(newWarehouseIdInput.Text), DateTime.Parse(newExpectedDateInput.Text),
                                                                             currentReceiptReferenceInput.Text.ToLower(), int.Parse(currentAccountIdInput.Text),
                                                                             int.Parse(currentWarehouseIdInput.Text), DateTime.Parse(currentExpectedDateInput.Text));
                    MessageBox.Show(currentReceiptReferenceInput.Text + " has been updated without a Receipted Date.");
                    return;
                }
                else
                {
                    receipt = businessLogicLayer.EditCurrentReceipt(newReceiptReferenceInput.Text.ToLower(), int.Parse(newAccountIdInput.Text), int.Parse(newWarehouseIdInput.Text),
                                                                    DateTime.Parse(newExpectedDateInput.Text), DateTime.Parse(receiptedDateInput.Text),
                                                                    currentReceiptReferenceInput.Text.ToLower(), int.Parse(currentAccountIdInput.Text),
                                                                    int.Parse(currentWarehouseIdInput.Text), DateTime.Parse(currentExpectedDateInput.Text));
                    MessageBox.Show(currentReceiptReferenceInput.Text + " has been updated with a Receipt Date.");
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
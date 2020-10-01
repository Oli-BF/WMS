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
    ///     In this case it deletes a receipt line from the database.
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
    public partial class ManagerManageReceiptLinesDelete : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        /// <summary>
        ///     Constructor:
        ///     This Initializes the UI,
        ///     Focuses on the first UI input box,
        ///     And instantiates the BusinessLogicLayer class, creating an object of that type.
        /// </summary>
        public ManagerManageReceiptLinesDelete()
        {
            InitializeComponent();
            receiptLineIdInput.Focus();
            businessLogicLayer = new BusinessLogicLayer();
        }

        /// <summary>
        ///     Deletes a receipt line from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (receiptLineIdInput.Text.Equals(""))
                {
                    MessageBox.Show("Please input the text box.");
                    receiptLineIdInput.Focus();
                    return;
                }
                else if (!Regex.IsMatch(receiptLineIdInput.Text, "^[0-9]*$"))
                {
                    MessageBox.Show("Please input only numerical characters into the text box.");
                    receiptLineIdInput.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred, please contact your administrator." + "\n\n" + "The error message is: " + "\n\n" + ex.ToString());
            }

            ReceiptLineObject receiptLine = businessLogicLayer.CheckReceiptLinesByID(int.Parse(receiptLineIdInput.Text));

            try
            {
                if (!int.Parse(receiptLineIdInput.Text).Equals(receiptLine.receipt_line_id))
                {
                    MessageBox.Show("The Receipt Line provided does not Exist.");
                    receiptLineIdInput.Focus();
                    return;
                }
                else
                {
                    receiptLine = businessLogicLayer.DeleteCurrentReceiptLine(int.Parse(receiptLineIdInput.Text));
                    MessageBox.Show("The provided Receipt Line has been deleted from the system.");
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
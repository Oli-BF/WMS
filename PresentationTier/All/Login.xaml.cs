using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ApplicationTier;
using PresentationTier.Admin;
using PresentationTier.Manager;
using PresentationTier.Worker;

namespace PresentationTier
{
    public partial class Login : Page
    {
        private BusinessLogicLayer businessLogicLayer;

        public Login()
        {
            InitializeComponent();
            emailInput.Focus();
            businessLogicLayer = new BusinessLogicLayer();
        }

        private void RadioButton_Checked1(object sender, RoutedEventArgs e)
        {
            radioButton1.IsChecked.Equals(true);
        }

        private void RadioButton_Checked2(object sender, RoutedEventArgs e)
        {
            radioButton2.IsChecked.Equals(true);
        }

        private void RadioButton_Checked3(object sender, RoutedEventArgs e)
        {
            radioButton3.IsChecked.Equals(true);
        }

        // https://www.codeproject.com/Articles/36847/Three-Layer-Architecture-in-C-NET-2
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserObject user = new UserObject();

            if (radioButton1.IsChecked.Equals(true))
            {
                user = businessLogicLayer.Login(emailInput.Text, radioButton1.Content.ToString().ToLower(), passwordInput.Password);
            }
            else if (radioButton2.IsChecked.Equals(true))
            {
                user = businessLogicLayer.Login(emailInput.Text, radioButton2.Content.ToString().ToLower(), passwordInput.Password);
            }
            else 
            {
                user = businessLogicLayer.Login(emailInput.Text, radioButton3.Content.ToString().ToLower(), passwordInput.Password);
            }
                
            try
            {
                if ((radioButton1.IsChecked == false) & (radioButton2.IsChecked == false) & (radioButton3.IsChecked == false))
                {
                    MessageBox.Show("Please select a user type using the buttons provided.");
                    emailInput.Focus();
                    return;
                }
                else if ((emailInput.Text.Equals("")) || (passwordInput.Password.Equals("")))
                {
                    MessageBox.Show("Please input both a username and a password.");
                    emailInput.Focus();
                    return;
                }
                else if ((emailInput.Text != user.email) && (passwordInput.Password != user.password))
                {
                    MessageBox.Show("You're details were not recognised, please try again.");
                    emailInput.Focus();
                    return;
                }
                else if ((radioButton1.IsChecked == true) && (emailInput.Text.ToLower().Trim().Equals(user.email)) && (radioButton1.Content.ToString().ToLower().Equals(user.role)) &&
                         (passwordInput.Password.Trim().Equals(user.password)))
                {
                    WorkerHomepage workerHomepage = new WorkerHomepage();
                    this.NavigationService.Navigate(workerHomepage);
                }
                else if ((radioButton2.IsChecked == true) && (emailInput.Text.ToLower().Trim().Equals(user.email)) && (radioButton2.Content.ToString().ToLower().Equals(user.role)) && 
                         (passwordInput.Password.Trim().Equals(user.password)))
                {
                    ManagerHomepage managerHomepage = new ManagerHomepage();
                    this.NavigationService.Navigate(managerHomepage);
                }
                else if ((radioButton3.IsChecked == true) && (emailInput.Text.ToLower().Trim().Equals(user.email)) && (radioButton3.Content.ToString().ToLower().Equals(user.role)) &&  
                         (passwordInput.Password.Trim().Equals(user.password)))
                {
                    AdminHomepage adminHomepage = new AdminHomepage();
                    this.NavigationService.Navigate(adminHomepage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred, please contact your administrator." + "\n\n" + "The error message is: " + "\n\n" + ex.ToString());
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationTier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            // https://social.msdn.microsoft.com/Forums/en-US/1d3a82f0-8144-4fcd-b22d-8a65c8c5b0d7/disable-backspace-keyboard-event-in-navigation-application?forum=wpf
            this.NavigationService.Navigated += new NavigatedEventHandler(NavigationService_Navigated);
        }

        private void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            this.NavigationService.RemoveBackEntry();
        }
    }
}
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
using System.Windows.Threading;
using Vimachem.Hackathon.ViewModel;
using Vimachem.Hackathon.Views;

namespace Vimachem.Hackathon

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MyMainWindow : Window
    {
        private Machine machine = new Machine();
        private Brush OriginalBrush;
        public MyMainWindow()
        {
            this.DataContext = new BaseViewModel();
            InitializeComponent();
            Nav.Tabs = Tabs;


            Tabs.SelectionChanged += Tabs_SelectionChanged;
            Loaded += MainWindow_Loaded;
            RecipeDetails.Content = String.Format("{0} > {1} > {2}", RecipeManager.GetCustomerName(), RecipeManager.GetRecipeName(), RecipeManager.GetRecipeBatch());
            Machine.Current.Error += Current_Error;
            Machine.Current.Message += Current_Message;
            Production.Current.NewTube += Current_NewTube;
            OriginalBrush = Messages.Background;
        }

        private void Current_NewTube(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                RecipeDetails.Content = String.Format("{0} > {1} > {2} | {3}/{4}", RecipeManager.GetCustomerName(), RecipeManager.GetRecipeName(), RecipeManager.GetRecipeBatch(), Production.Current.Count, Production.Current.Total);

            }));
          
        }

        private void Current_Message(object sender, string e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                Messages.Content = e;
                Messages.Background = OriginalBrush;
                Messages.Foreground = Brushes.White;
            }));
          
        }

        private void Current_Error(object sender, string e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                Messages.Content = e;
                Messages.Background = Brushes.Red;
            }));
     
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Customer.Content = RecipeManager.GetCustomerName();
        }

        private void Tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            //{
            //    SelectedTab.Content = ((TabItem)Tabs.SelectedItem).Header.ToString();
            //}));
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            Tabs.SelectedIndex = 0; 
        }

        private void MaintenanceButton_Click(object sender, RoutedEventArgs e)
        {
            Tabs.SelectedIndex = 2; 
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.ShowDialog();
        }
    }
}

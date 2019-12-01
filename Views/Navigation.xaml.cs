using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Vimachem.Hackathon.Views
{
    /// <summary>
    /// Interaction logic for Navigation.xaml
    /// </summary>
    public partial class Navigation : UserControl
    {
        public Navigation()
        {
            InitializeComponent();
        }
        public event EventHandler JobLoaded;
        public TabControl Tabs;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Machine.Current.StartBearings();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Machine.Current.StopBearings();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Machine.Current.ResetBearings();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Machine.Current.StopMachine();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Machine.Current.StartMachine();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Machine.Current.ResetMachine();
        }

        private void Main_Click(object sender, RoutedEventArgs e)
        {
            Tabs.SelectedIndex = 0;
        }

        private void Recipe_Click(object sender, RoutedEventArgs e)
        {
            Tabs.SelectedIndex = 1;

        }

        private void Maintenace_Click(object sender, RoutedEventArgs e)
        {
            Tabs.SelectedIndex = 2;

        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            Tabs.SelectedIndex = 3;

        }

        private void Schematics_Click(object sender, RoutedEventArgs e)
        {
            Tabs.SelectedIndex = 4;

        }

        private void Shift_Click(object sender, RoutedEventArgs e)
        {
            Tabs.SelectedIndex = 5;

        }

        private void LoadJob_Click(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem((o) => Production.Current.RunJob(400));
        }

    }
}

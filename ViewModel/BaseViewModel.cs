using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Vimachem.Hackathon.Views;
using Vimachem.Hackathon.Views.Tabs;

namespace Vimachem.Hackathon.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {        
        private TabItem currentTab;
        public TabItem CurrentTab
        {
            get
            {
                return currentTab;
            }
            set
            {
                currentTab = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("CurrentTab"));
                TabName = currentTab.Header.ToString();
            }
        }

        private ObservableCollection<TabItem> tabItems;
        public ObservableCollection<TabItem> TabItems
        {
            get
            {
                return tabItems;
            }
            set
            {
                tabItems = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TabItems"));
            }
        }

        private String tabName;

        public event PropertyChangedEventHandler PropertyChanged;

        public String TabName
        {
            get {
                return currentTab?.Header.ToString();
            }
            set
            {
                tabName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TabName"));
            }
        }

        public ICommand MainCommand;

        public BaseViewModel()
        {

        }

        private void Main()
        {
           // CurrentTab =  tabItems.Select(x => x.Header.ToString() == "Main");
        }
    }
}
